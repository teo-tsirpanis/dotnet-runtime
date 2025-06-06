// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

/*****************************************************************************
 *
 * GC Information Encoding API
 *
 */

#include <stdint.h>

#include "gcinfoencoder.h"

using namespace GcInfoEncoderExt;

#include "targetosarch.h"

#ifdef _DEBUG
    #ifndef LOGGING
        #define LOGGING
    #endif
#endif

#ifndef STANDALONE_BUILD
#include "log.h"
#include "simplerhash.h"
#include "bitposition.h"
#endif
#include "../jit/jitstd/algorithm.h"

#ifdef MEASURE_GCINFO
#define GCINFO_WRITE(writer, val, numBits, counter) \
    {                                               \
        writer.Write(val, numBits);                 \
        m_CurrentMethodSize.counter += numBits;     \
        m_CurrentMethodSize.TotalSize += numBits;   \
    }
#define GCINFO_WRITE_VARL_U(writer, val, base, counter) \
    {                                               \
        size_t __temp =                             \
         writer.EncodeVarLengthUnsigned(val, base); \
        m_CurrentMethodSize.counter += __temp;      \
        m_CurrentMethodSize.TotalSize += __temp;    \
    }
#define GCINFO_WRITE_VARL_S(writer, val, base, counter) \
    {                                               \
        size_t __temp =                             \
         writer.EncodeVarLengthSigned(val, base);   \
        m_CurrentMethodSize.counter += __temp;      \
        m_CurrentMethodSize.TotalSize += __temp;    \
    }
#define GCINFO_WRITE_VECTOR(writer, vector, counter)   \
    {                                               \
        WriteSlotStateVector(writer, vector);       \
        for(UINT32 i = 0; i < m_NumSlots; i++)      \
        {                                           \
            if(!m_SlotTable[i].IsDeleted() &&       \
               !m_SlotTable[i].IsUntracked())       \
            {                                       \
                m_CurrentMethodSize.counter++;      \
                m_CurrentMethodSize.TotalSize++;    \
            }                                       \
        }                                           \
    }
#define GCINFO_WRITE_VAR_VECTOR(writer, vector, baseSkip, baseRun, counter)   \
    {                                                                         \
        size_t __temp =                                                       \
           WriteSlotStateVarLengthVector(writer, vector, baseSkip, baseRun);  \
        m_CurrentMethodSize.counter += __temp;                                \
        m_CurrentMethodSize.TotalSize += __temp;                              \
    }
#else
#define GCINFO_WRITE(writer, val, numBits, counter) \
        writer.Write(val, numBits);

#define GCINFO_WRITE_VARL_U(writer, val, base, counter) \
        writer.EncodeVarLengthUnsigned(val, base);

#define GCINFO_WRITE_VARL_S(writer, val, base, counter) \
        writer.EncodeVarLengthSigned(val, base);

#define GCINFO_WRITE_VECTOR(writer, vector, counter)   \
        WriteSlotStateVector(writer, vector);

#define GCINFO_WRITE_VAR_VECTOR(writer, vector, baseSkip, baseRun, counter)   \
        WriteSlotStateVarLengthVector(writer, vector, baseSkip, baseRun);
#endif

#define LOG_GCSLOTDESC_FMT "%s%c%d%s%s%s"
#define LOG_GCSLOTDESC_ARGS(pDesc) (pDesc)->IsRegister() ? "register"                                          \
                                        : GcStackSlotBaseNames[(pDesc)->Slot.Stack.Base],                      \
                                   (pDesc)->IsRegister() ? ' ' : (pDesc)->Slot.Stack.SpOffset < 0 ? '-' : '+', \
                                   (pDesc)->IsRegister() ? (pDesc)->Slot.RegisterNumber                        \
                                        : ((pDesc)->Slot.Stack.SpOffset),                                      \
                                   (pDesc)->IsPinned() ? " pinned" : "",                                       \
                                   (pDesc)->IsInterior() ? " interior" : "",                                   \
                                   (pDesc)->IsUntracked() ? " untracked" : ""

#define LOG_REGTRANSITION_FMT "register %u%s%s"
#define LOG_REGTRANSITION_ARGS(RegisterNumber, Flags)           \
                RegisterNumber,                                 \
                (Flags & GC_SLOT_PINNED)   ? " pinned"   : "",  \
                (Flags & GC_SLOT_INTERIOR) ? " interior" : ""

#define LOG_STACKTRANSITION_FMT "%s%c%d%s%s%s"
#define LOG_STACKTRANSITION_ARGS(BaseRegister, StackOffset, Flags)  \
        GcStackSlotBaseNames[BaseRegister],                         \
        ((StackOffset) < 0) ? '-' : '+',                            \
        ((StackOffset) >= 0) ? (StackOffset)                        \
                             : -(StackOffset),                      \
                (Flags & GC_SLOT_PINNED)   ? " pinned"   : "",      \
                (Flags & GC_SLOT_INTERIOR) ? " interior" : "",      \
                (Flags & GC_SLOT_UNTRACKED) ? " untracked" : ""

class BitArray
{
    friend class BitArrayIterator;
    typedef uint32_t ChunkType;
    static constexpr size_t NumBitsPerChunk = sizeof(ChunkType) * CHAR_BIT;
public:
    BitArray(IAllocator* pJitAllocator, size_t numBits)
    {
        const size_t numChunks = (numBits + NumBitsPerChunk - 1) / NumBitsPerChunk;
        m_pData = (ChunkType*)pJitAllocator->Alloc(sizeof(ChunkType) * numChunks);
        m_pEndData = m_pData + numChunks;
#ifdef MUST_CALL_IALLOCATOR_FREE
        m_pJitAllocator = pJitAllocator;
#endif
    }

    inline void SetBit( size_t pos )
    {
        size_t element = pos / NumBitsPerChunk;
        int bpos = (int)(pos % NumBitsPerChunk);
        m_pData[element] |= ((ChunkType)1 << bpos);
    }

    inline void ClearBit( size_t pos )
    {
        size_t element = pos / NumBitsPerChunk;
        int bpos = (int)(pos % NumBitsPerChunk);
        m_pData[element] &= ~((ChunkType)1 << bpos);
    }

    inline void SetAll()
    {
        ChunkType* ptr = m_pData;
        while(ptr < m_pEndData)
            *(ptr++) = ~(ChunkType)0;
    }

    inline void ClearAll()
    {
        ChunkType* ptr = m_pData;
        while(ptr < m_pEndData)
            *(ptr++) = (ChunkType)0;
    }

    inline void WriteBit( size_t pos, BOOL val)
    {
        if(val)
            SetBit(pos);
        else
            ClearBit(pos);
    }

    inline uint32_t ReadBit( size_t pos ) const
    {
        size_t element = pos / NumBitsPerChunk;
        int bpos = (int)(pos % NumBitsPerChunk);
        return (m_pData[element] & ((ChunkType)1 << bpos));
    }

    inline bool operator==(const BitArray &other) const
    {
        _ASSERTE(other.m_pEndData - other.m_pData == m_pEndData - m_pData);
        ChunkType* dest = m_pData;
        ChunkType* src = other.m_pData;
        return 0 == memcmp(dest, src, (m_pEndData - m_pData) * sizeof(ChunkType));
    }

    inline int GetHashCode() const
    {
        const int* src = (const int*)m_pData;
        int result = *src++;
        while (src < (const int*)m_pEndData)
            result = _rotr(result, 5) ^ *src++;
        return result;
    }

    inline BitArray& operator=(const BitArray &other)
    {
        _ASSERTE(other.m_pEndData - other.m_pData == m_pEndData - m_pData);
        ChunkType* dest = m_pData;
        ChunkType* src = other.m_pData;
        while(dest < m_pEndData)
            *(dest++) = *(src++);

        return *this;
    }

    inline BitArray& operator|=(const BitArray &other)
    {
        _ASSERTE(other.m_pEndData - other.m_pData == m_pEndData - m_pData);
        ChunkType* dest = m_pData;
        ChunkType* src = other.m_pData;
        while(dest < m_pEndData)
            *(dest++) |= *(src++);

        return *this;
    }

#ifdef MUST_CALL_IALLOCATOR_FREE
    ~BitArray()
    {
        m_pAllocator->Free( m_pData );
    }
#endif

    static void* operator new(size_t size, IAllocator* allocator)
    {
        return allocator->Alloc(size);
    }

private:
    ChunkType * m_pData;
    ChunkType * m_pEndData;
#ifdef MUST_CALL_IALLOCATOR_FREE
    IAllocator* m_pJitAllocator;
#endif
};


class BitArrayIterator
{
public:
    BitArrayIterator(BitArray* bitArray)
    {
        m_pCurData = (unsigned *)bitArray->m_pData;
        m_pEndData = (unsigned *)bitArray->m_pEndData;
        m_curBits = *m_pCurData;
        m_curBit = 0;
        m_curBase = 0;
        GetNext();
    }
    void operator++(int dummy) //int dummy is c++ for "this is postfix ++"
    {
        GetNext();
    }

    void operator++() // prefix ++
    {
        GetNext();
    }
    void GetNext()
    {
        m_curBits -= m_curBit;
        while (m_curBits == 0)
        {
            m_pCurData++;
            m_curBase += 32;
            if (m_pCurData == m_pEndData)
                break;
            m_curBits = *m_pCurData;
        }
        m_curBit = (unsigned)((int)m_curBits & -(int)m_curBits);
    }
    unsigned operator*()
    {
        assert(!end() && (m_curBit != 0));
        unsigned bitPosition = BitPosition(m_curBit);
        return bitPosition + m_curBase;
    }
    bool end()
    {
        return (m_pCurData == m_pEndData);
    }
private:
    unsigned*   m_pCurData;
    unsigned*   m_pEndData;
    unsigned    m_curBits;
    unsigned    m_curBit;
    unsigned    m_curBase;
};

class LiveStateFuncs
{
public:
    static int GetHashCode(const BitArray * key)
    {
        return key->GetHashCode();
    }

    static bool Equals(const BitArray * k1, const BitArray * k2)
    {
        return *k1 == *k2;
    }
};

class GcInfoNoMemoryException
{
};

class GcInfoHashBehavior
{
public:
    static const unsigned s_growth_factor_numerator = 3;
    static const unsigned s_growth_factor_denominator = 2;

    static const unsigned s_density_factor_numerator = 3;
    static const unsigned s_density_factor_denominator = 4;

    static const unsigned s_minimum_allocation = 7;

    inline static void DECLSPEC_NORETURN NoMemory()
    {
        throw GcInfoNoMemoryException();
    }
};

typedef SimplerHashTable<const BitArray *, LiveStateFuncs, UINT32, GcInfoHashBehavior> LiveStateHashTable;

#ifdef MEASURE_GCINFO
// Fi = fully-interruptible; we count any method that has one or more interruptible ranges
// Pi = partially-interruptible; methods with zero fully-interruptible ranges
GcInfoSize g_FiGcInfoSize;
GcInfoSize g_PiGcInfoSize;
// Number of methods with GcInfo that have SlimHeader
size_t g_NumSlimHeaders = 0;
// Number of methods with GcInfo that have FatHeader
size_t g_NumFatHeaders = 0;

GcInfoSize::GcInfoSize()
{
    memset(this, 0, sizeof(*this));
}

GcInfoSize& GcInfoSize::operator+=(const GcInfoSize& other)
{
    TotalSize += other.TotalSize;

    NumMethods += other.NumMethods;
    NumCallSites += other.NumCallSites;
    NumRanges += other.NumRanges;
    NumRegs += other.NumRegs;
    NumStack += other.NumStack;
    NumUntracked += other.NumUntracked;
    NumTransitions += other.NumTransitions;
    SizeOfCode += other.SizeOfCode;
    EncInfoSize += other.EncInfoSize;

    UntrackedSlotSize += other.UntrackedSlotSize;
    NumUntrackedSize += other.NumUntrackedSize;
    FlagsSize += other.FlagsSize;
    CodeLengthSize += other.CodeLengthSize;
    ProEpilogSize += other.ProEpilogSize;
    SecObjSize += other.SecObjSize;
    GsCookieSize += other.GsCookieSize;
    GenericsCtxSize += other.GenericsCtxSize;
    StackBaseSize += other.StackBaseSize;
    ReversePInvokeFrameSize += other.ReversePInvokeFrameSize;
    FixedAreaSize += other.FixedAreaSize;
    NumCallSitesSize += other.NumCallSitesSize;
    NumRangesSize += other.NumRangesSize;
    CallSitePosSize += other.CallSitePosSize;
    RangeSize += other.RangeSize;
    NumRegsSize += other.NumRegsSize;
    NumStackSize += other.NumStackSize;
    RegSlotSize += other.RegSlotSize;
    StackSlotSize += other.StackSlotSize;
    CallSiteStateSize += other.CallSiteStateSize;
    EhPosSize += other.EhPosSize;
    EhStateSize += other.EhStateSize;
    ChunkPtrSize += other.ChunkPtrSize;
    ChunkMaskSize += other.ChunkMaskSize;
    ChunkFinalStateSize += other.ChunkFinalStateSize;
    ChunkTransitionSize += other.ChunkTransitionSize;

    return *this;
}

void GcInfoSize::Log(DWORD level, const char * header)
{
    if(LoggingOn(LF_GCINFO, level))
    {
        LogSpew(LF_GCINFO, level, header);

        LogSpew(LF_GCINFO, level, "---COUNTS---\n");
        LogSpew(LF_GCINFO, level, "NumMethods: %zu\n", NumMethods);
        LogSpew(LF_GCINFO, level, "NumCallSites: %zu\n", NumCallSites);
        LogSpew(LF_GCINFO, level, "NumRanges: %zu\n", NumRanges);
        LogSpew(LF_GCINFO, level, "NumRegs: %zu\n", NumRegs);
        LogSpew(LF_GCINFO, level, "NumStack: %zu\n", NumStack);
        LogSpew(LF_GCINFO, level, "NumUntracked: %zu\n", NumUntracked);
        LogSpew(LF_GCINFO, level, "NumTransitions: %zu\n", NumTransitions);
        LogSpew(LF_GCINFO, level, "SizeOfCode: %zu\n", SizeOfCode);
        LogSpew(LF_GCINFO, level, "EncInfoSize: %zu\n", EncInfoSize);

        LogSpew(LF_GCINFO, level, "---SIZES(bits)---\n");
        LogSpew(LF_GCINFO, level, "Total: %zu\n", TotalSize);
        LogSpew(LF_GCINFO, level, "UntrackedSlot: %zu\n", UntrackedSlotSize);
        LogSpew(LF_GCINFO, level, "NumUntracked: %zu\n", NumUntrackedSize);
        LogSpew(LF_GCINFO, level, "Flags: %zu\n", FlagsSize);
        LogSpew(LF_GCINFO, level, "CodeLength: %zu\n", CodeLengthSize);
        LogSpew(LF_GCINFO, level, "Prolog/Epilog: %zu\n", ProEpilogSize);
        LogSpew(LF_GCINFO, level, "SecObj: %zu\n", SecObjSize);
        LogSpew(LF_GCINFO, level, "GsCookie: %zu\n", GsCookieSize);
        LogSpew(LF_GCINFO, level, "GenericsCtx: %zu\n", GenericsCtxSize);
        LogSpew(LF_GCINFO, level, "StackBase: %zu\n", StackBaseSize);
        LogSpew(LF_GCINFO, level, "FixedArea: %zu\n", FixedAreaSize);
        LogSpew(LF_GCINFO, level, "ReversePInvokeFrame: %zu\n", ReversePInvokeFrameSize);
        LogSpew(LF_GCINFO, level, "NumCallSites: %zu\n", NumCallSitesSize);
        LogSpew(LF_GCINFO, level, "NumRanges: %zu\n", NumRangesSize);
        LogSpew(LF_GCINFO, level, "CallSiteOffsets: %zu\n", CallSitePosSize);
        LogSpew(LF_GCINFO, level, "Ranges: %zu\n", RangeSize);
        LogSpew(LF_GCINFO, level, "NumRegs: %zu\n", NumRegsSize);
        LogSpew(LF_GCINFO, level, "NumStack: %zu\n", NumStackSize);
        LogSpew(LF_GCINFO, level, "RegSlots: %zu\n", RegSlotSize);
        LogSpew(LF_GCINFO, level, "StackSlots: %zu\n", StackSlotSize);
        LogSpew(LF_GCINFO, level, "CallSiteStates: %zu\n", CallSiteStateSize);
        LogSpew(LF_GCINFO, level, "EhOffsets: %zu\n", EhPosSize);
        LogSpew(LF_GCINFO, level, "EhStates: %zu\n", EhStateSize);
        LogSpew(LF_GCINFO, level, "ChunkPointers: %zu\n", ChunkPtrSize);
        LogSpew(LF_GCINFO, level, "ChunkMasks: %zu\n", ChunkMaskSize);
        LogSpew(LF_GCINFO, level, "ChunkFinalStates: %zu\n", ChunkFinalStateSize);
        LogSpew(LF_GCINFO, level, "Transitions: %zu\n", ChunkTransitionSize);
    }
}

#endif

template <typename GcInfoEncoding> TGcInfoEncoder<GcInfoEncoding>::TGcInfoEncoder(
            ICorJitInfo*                pCorJitInfo,
            CORINFO_METHOD_INFO*        pMethodInfo,
            IAllocator*                 pJitAllocator,
            NoMemoryFunction            pNoMem
            )
    :   m_Info1( pJitAllocator ),
        m_Info2( pJitAllocator ),
        m_InterruptibleRanges( pJitAllocator ),
        m_LifetimeTransitions( pJitAllocator )
{
#ifdef MEASURE_GCINFO
    // This causes multiple complus.log files in JIT64.  TODO: consider using ICorJitInfo::logMsg instead.
    InitializeLogging();
#endif

    _ASSERTE( pCorJitInfo != NULL );
    _ASSERTE( pMethodInfo != NULL );
    _ASSERTE( pJitAllocator != NULL );
    _ASSERTE( pNoMem != NULL );

    m_pCorJitInfo = pCorJitInfo;
    m_pMethodInfo = pMethodInfo;
    m_pAllocator = pJitAllocator;
    m_pNoMem = pNoMem;

    m_SlotTableSize = m_SlotTableInitialSize;
    m_SlotTable = (GcSlotDesc*) m_pAllocator->Alloc( m_SlotTableSize*sizeof(GcSlotDesc) );
    m_NumSlots = 0;
#ifdef PARTIALLY_INTERRUPTIBLE_GC_SUPPORTED
    m_pCallSites = NULL;
    m_pCallSiteSizes = NULL;
    m_NumCallSites = 0;
#endif
    m_BlockSize = 0;

    m_GSCookieStackSlot = NO_GS_COOKIE;
    m_GSCookieValidRangeStart = 0;
    _ASSERTE(sizeof(m_GSCookieValidRangeEnd) == sizeof(UINT32));
    m_GSCookieValidRangeEnd = (UINT32) (-1); // == UINT32.MaxValue
    m_GenericsInstContextStackSlot = NO_GENERICS_INST_CONTEXT;
    m_contextParamType = GENERIC_CONTEXTPARAM_NONE;

    m_StackBaseRegister = NO_STACK_BASE_REGISTER;
    m_SizeOfEditAndContinuePreservedArea = NO_SIZE_OF_EDIT_AND_CONTINUE_PRESERVED_AREA;
#ifdef TARGET_ARM64
    m_SizeOfEditAndContinueFixedStackFrame = 0;
#endif
    m_ReversePInvokeFrameSlot = NO_REVERSE_PINVOKE_FRAME;
#ifdef TARGET_AMD64
    m_WantsReportOnlyLeaf = false;
#elif defined(TARGET_ARM) || defined(TARGET_ARM64) || defined(TARGET_LOONGARCH64) || defined(TARGET_RISCV64)
    m_HasTailCalls = false;
#endif // TARGET_AMD64
    m_IsVarArg = false;
    m_pLastInterruptibleRange = NULL;

#ifdef _DEBUG
    m_IsSlotTableFrozen = FALSE;
#endif //_DEBUG

    m_CodeLength = 0;
#ifdef FIXED_STACK_PARAMETER_SCRATCH_AREA
    m_SizeOfStackOutgoingAndScratchArea = -1;
#endif // FIXED_STACK_PARAMETER_SCRATCH_AREA

}

#ifdef PARTIALLY_INTERRUPTIBLE_GC_SUPPORTED
template <typename GcInfoEncoding> void TGcInfoEncoder<GcInfoEncoding>::DefineCallSites(UINT32* pCallSites, BYTE* pCallSiteSizes, UINT32 numCallSites)
{
    m_pCallSites = pCallSites;
    m_pCallSiteSizes = pCallSiteSizes;
    m_NumCallSites = numCallSites;
#ifdef _DEBUG
    for(UINT32 i=0; i<numCallSites; i++)
    {
        _ASSERTE(pCallSiteSizes[i] > 0);
        _ASSERTE(GcInfoEncoding::DENORMALIZE_CODE_OFFSET(GcInfoEncoding::NORMALIZE_CODE_OFFSET(pCallSites[i])) == pCallSites[i]);
        if(i > 0)
        {
            UINT32 prevEnd = pCallSites[i-1] + pCallSiteSizes[i-1];
            UINT32 curStart = pCallSites[i];
            _ASSERTE(curStart >= prevEnd);
        }
    }
#endif
}
#endif

template <typename GcInfoEncoding> GcSlotId TGcInfoEncoder<GcInfoEncoding>::GetRegisterSlotId( UINT32 regNum, GcSlotFlags flags )
{
    // We could lookup an existing identical slot in the slot table (via some hashtable mechanism).
    // We just create duplicates for now.

#ifdef _DEBUG
    _ASSERTE( !m_IsSlotTableFrozen );
#endif

    if( m_NumSlots == m_SlotTableSize )
    {
        GrowSlotTable();
    }
    _ASSERTE( m_NumSlots < m_SlotTableSize );

    _ASSERTE( (flags & (GC_SLOT_IS_REGISTER | GC_SLOT_IS_DELETED | GC_SLOT_UNTRACKED)) == 0 );
    m_SlotTable[ m_NumSlots ].Slot.RegisterNumber = regNum;
    m_SlotTable[ m_NumSlots ].Flags = (GcSlotFlags) (flags | GC_SLOT_IS_REGISTER);

    GcSlotId newSlotId;
    newSlotId = m_NumSlots++;

    return newSlotId;
}

template <typename GcInfoEncoding> GcSlotId TGcInfoEncoder<GcInfoEncoding>::GetStackSlotId( INT32 spOffset, GcSlotFlags flags, GcStackSlotBase spBase )
{
    // We could lookup an existing identical slot in the slot table (via some hashtable mechanism).
    // We just create duplicates for now.

#ifdef _DEBUG
    _ASSERTE( !m_IsSlotTableFrozen );
#endif

    if( m_NumSlots == m_SlotTableSize )
    {
        GrowSlotTable();
    }
    _ASSERTE( m_NumSlots < m_SlotTableSize );

    // Not valid to reference anything below the current stack pointer
    _ASSERTE(GC_SP_REL != spBase || spOffset >= 0);

    _ASSERTE( (flags & (GC_SLOT_IS_REGISTER | GC_SLOT_IS_DELETED)) == 0 );

    if (!(TargetOS::IsApplePlatform && TargetArchitecture::IsArm64))
    {
        // the spOffset for the stack slot is required to be pointer size aligned
        _ASSERTE((spOffset % TARGET_POINTER_SIZE) == 0);
    }

    m_SlotTable[ m_NumSlots ].Slot.Stack.SpOffset = spOffset;
    m_SlotTable[ m_NumSlots ].Slot.Stack.Base = spBase;
    m_SlotTable[ m_NumSlots ].Flags = flags;

    GcSlotId newSlotId;
    newSlotId = m_NumSlots++;

    return newSlotId;
}

template <typename GcInfoEncoding> void TGcInfoEncoder<GcInfoEncoding>::GrowSlotTable()
{
    m_SlotTableSize *= 2;
    GcSlotDesc* newSlotTable = (GcSlotDesc*) m_pAllocator->Alloc( m_SlotTableSize * sizeof(GcSlotDesc) );
    memcpy( newSlotTable, m_SlotTable, m_NumSlots * sizeof(GcSlotDesc) );

#ifdef MUST_CALL_JITALLOCATOR_FREE
    m_pAllocator->Free( m_SlotTable );
#endif

    m_SlotTable = newSlotTable;
}

template <typename GcInfoEncoding> void TGcInfoEncoder<GcInfoEncoding>::WriteSlotStateVector(BitStreamWriter &writer, const BitArray& vector)
{
    for(UINT32 i = 0; i < m_NumSlots && !m_SlotTable[i].IsUntracked(); i++)
    {
        if(!m_SlotTable[i].IsDeleted())
            writer.Write(vector.ReadBit(i) ? 1 : 0, 1);
        else
            _ASSERTE(vector.ReadBit(i) == 0);
    }
}

template <typename GcInfoEncoding> void TGcInfoEncoder<GcInfoEncoding>::DefineInterruptibleRange( UINT32 startInstructionOffset, UINT32 length )
{
    UINT32 stopInstructionOffset = startInstructionOffset + length;

    UINT32 normStartOffset = GcInfoEncoding::NORMALIZE_CODE_OFFSET(startInstructionOffset);
    UINT32 normStopOffset = GcInfoEncoding::NORMALIZE_CODE_OFFSET(stopInstructionOffset);

    // Ranges must not overlap and must be passed sorted by increasing offset
    _ASSERTE(
        m_pLastInterruptibleRange == NULL ||
        normStartOffset >= m_pLastInterruptibleRange->NormStopOffset
        );

    // Ignore empty ranges
    if(normStopOffset > normStartOffset)
    {
        if(m_pLastInterruptibleRange
            && normStartOffset == m_pLastInterruptibleRange->NormStopOffset)
        {
            // Merge adjacent ranges
            m_pLastInterruptibleRange->NormStopOffset = normStopOffset;
        }
        else
        {
            InterruptibleRange range;
            range.NormStartOffset = normStartOffset;
            range.NormStopOffset = normStopOffset;
            m_pLastInterruptibleRange = m_InterruptibleRanges.Append();
            *m_pLastInterruptibleRange = range;
        }
    }

    LOG((LF_GCINFO, LL_INFO1000000, "interruptible at %x length %x\n", startInstructionOffset, length));
}



//
// For inputs, pass zero as offset
//
template <typename GcInfoEncoding> void TGcInfoEncoder<GcInfoEncoding>::SetSlotState(
                            UINT32      instructionOffset,
                            GcSlotId    slotId,
                            GcSlotState slotState
                            )
{
    _ASSERTE( (m_SlotTable[ slotId ].Flags & GC_SLOT_UNTRACKED) == 0 );

    LifetimeTransition transition;

    transition.SlotId = slotId;
    transition.CodeOffset = instructionOffset;
    transition.BecomesLive = ( slotState == GC_SLOT_LIVE );
    transition.IsDeleted = FALSE;

    *( m_LifetimeTransitions.Append() ) = transition;

    LOG((LF_GCINFO, LL_INFO1000000, LOG_GCSLOTDESC_FMT " %s at %x\n", LOG_GCSLOTDESC_ARGS(&m_SlotTable[slotId]), slotState == GC_SLOT_LIVE ? "live" : "dead", instructionOffset));
}


template <typename GcInfoEncoding> void TGcInfoEncoder<GcInfoEncoding>::SetIsVarArg()
{
    m_IsVarArg = true;
}

template <typename GcInfoEncoding> void TGcInfoEncoder<GcInfoEncoding>::SetCodeLength( UINT32 length )
{
    _ASSERTE( length > 0 );
    _ASSERTE( m_CodeLength == 0 || m_CodeLength == length );
    m_CodeLength = length;
}

template <typename GcInfoEncoding> void TGcInfoEncoder<GcInfoEncoding>::SetPrologSize( UINT32 prologSize )
{
    _ASSERTE(prologSize != 0);
    _ASSERTE(m_GSCookieValidRangeStart == 0 || m_GSCookieValidRangeStart == prologSize);
    _ASSERTE(m_GSCookieValidRangeEnd == (UINT32)(-1) || m_GSCookieValidRangeEnd == prologSize+1);

    m_GSCookieValidRangeStart = prologSize;
    // satisfy asserts that assume m_GSCookieValidRangeStart != 0 ==> m_GSCookieValidRangeStart < m_GSCookieValidRangeEnd
    m_GSCookieValidRangeEnd   = prologSize+1;
}

template <typename GcInfoEncoding> void TGcInfoEncoder<GcInfoEncoding>::SetGSCookieStackSlot( INT32 spOffsetGSCookie, UINT32 validRangeStart, UINT32 validRangeEnd )
{
    _ASSERTE( spOffsetGSCookie != NO_GS_COOKIE );
    _ASSERTE( m_GSCookieStackSlot == NO_GS_COOKIE || m_GSCookieStackSlot == spOffsetGSCookie );
    _ASSERTE( validRangeStart < validRangeEnd );

    m_GSCookieStackSlot       = spOffsetGSCookie;
    m_GSCookieValidRangeStart = validRangeStart;
    m_GSCookieValidRangeEnd   = validRangeEnd;
}

template <typename GcInfoEncoding> void TGcInfoEncoder<GcInfoEncoding>::SetGenericsInstContextStackSlot( INT32 spOffsetGenericsContext, GENERIC_CONTEXTPARAM_TYPE type)
{
    _ASSERTE( spOffsetGenericsContext != NO_GENERICS_INST_CONTEXT);
    _ASSERTE( m_GenericsInstContextStackSlot == NO_GENERICS_INST_CONTEXT || m_GenericsInstContextStackSlot == spOffsetGenericsContext );

    m_GenericsInstContextStackSlot = spOffsetGenericsContext;
    m_contextParamType = type;
}

template <typename GcInfoEncoding> void TGcInfoEncoder<GcInfoEncoding>::SetStackBaseRegister( UINT32 regNum )
{
    _ASSERTE( regNum != NO_STACK_BASE_REGISTER );
    _ASSERTE(GcInfoEncoding::DENORMALIZE_STACK_BASE_REGISTER(GcInfoEncoding::NORMALIZE_STACK_BASE_REGISTER(regNum)) == regNum);
    _ASSERTE( m_StackBaseRegister == NO_STACK_BASE_REGISTER || m_StackBaseRegister == regNum );
#if defined(TARGET_LOONGARCH64)
    assert(regNum == 3 || 22 == regNum);
#elif defined(TARGET_RISCV64)
    assert(regNum == 2 || 8 == regNum);
#endif
    m_StackBaseRegister = regNum;
}

template <typename GcInfoEncoding> void TGcInfoEncoder<GcInfoEncoding>::SetSizeOfEditAndContinuePreservedArea( UINT32 slots )
{
    _ASSERTE( slots != NO_SIZE_OF_EDIT_AND_CONTINUE_PRESERVED_AREA );
    _ASSERTE( m_SizeOfEditAndContinuePreservedArea == NO_SIZE_OF_EDIT_AND_CONTINUE_PRESERVED_AREA );
    m_SizeOfEditAndContinuePreservedArea = slots;
}

#ifdef TARGET_ARM64
template <typename GcInfoEncoding> void TGcInfoEncoder<GcInfoEncoding>::SetSizeOfEditAndContinueFixedStackFrame( UINT32 size )
{
    m_SizeOfEditAndContinueFixedStackFrame = size;
}
#endif

#ifdef TARGET_AMD64
template <typename GcInfoEncoding> void TGcInfoEncoder<GcInfoEncoding>::SetWantsReportOnlyLeaf()
{
    m_WantsReportOnlyLeaf = true;
}
#elif defined(TARGET_ARM) || defined(TARGET_ARM64) || defined(TARGET_LOONGARCH64) || defined(TARGET_RISCV64)
template <typename GcInfoEncoding> void TGcInfoEncoder<GcInfoEncoding>::SetHasTailCalls()
{
    m_HasTailCalls = true;
}
#endif // TARGET_AMD64

#ifdef FIXED_STACK_PARAMETER_SCRATCH_AREA
template <typename GcInfoEncoding> void TGcInfoEncoder<GcInfoEncoding>::SetSizeOfStackOutgoingAndScratchArea( UINT32 size )
{
    _ASSERTE( size != (UINT32)-1 );
    _ASSERTE( m_SizeOfStackOutgoingAndScratchArea == (UINT32)-1 || m_SizeOfStackOutgoingAndScratchArea == size );
    m_SizeOfStackOutgoingAndScratchArea = size;
}
#endif // FIXED_STACK_PARAMETER_SCRATCH_AREA

template <typename GcInfoEncoding> void TGcInfoEncoder<GcInfoEncoding>::SetReversePInvokeFrameSlot(INT32 spOffset)
{
    m_ReversePInvokeFrameSlot = spOffset;
}

struct GcSlotDescAndId
{
    GcSlotDesc m_SlotDesc;
    UINT32 m_SlotId;
};

struct CompareSlotDescAndIdBySlotDesc
{
    bool operator()(const GcSlotDescAndId& first, const GcSlotDescAndId& second)
    {
        const GcSlotDesc* pFirst = &first.m_SlotDesc;
        const GcSlotDesc* pSecond = &second.m_SlotDesc;

        int firstFlags = pFirst->Flags ^ GC_SLOT_UNTRACKED;
        int secondFlags = pSecond->Flags ^ GC_SLOT_UNTRACKED;

        // All registers come before all stack slots
        // All untracked come last
        // Then sort them by flags, ensuring that the least-frequent interior/pinned flag combinations are first
        // This is accomplished in the comparison of flags, since we encode IsRegister in the highest flag bit
        // And we XOR the UNTRACKED flag to place them last in the second highest flag bit
        if( firstFlags != secondFlags )
            return firstFlags > secondFlags;

        // Then sort them by slot
        if( pFirst->IsRegister() )
        {
            _ASSERTE( pSecond->IsRegister() );
            if( pFirst->Slot.RegisterNumber != pSecond->Slot.RegisterNumber )
                return pFirst->Slot.RegisterNumber < pSecond->Slot.RegisterNumber;
        }
        else
        {
            _ASSERTE( !pSecond->IsRegister() );
            if( pFirst->Slot.Stack.SpOffset != pSecond->Slot.Stack.SpOffset )
                return pFirst->Slot.Stack.SpOffset < pSecond->Slot.Stack.SpOffset;

            // This is arbitrary, but we want to make sure they are considered separate slots
            if( pFirst->Slot.Stack.Base != pSecond->Slot.Stack.Base )
                return pFirst->Slot.Stack.Base < pSecond->Slot.Stack.Base;
        }

        // If we get here, the slots are identical
        _ASSERTE(!"Duplicate slots definitions found in GC information!");
        return false;
    }
};

struct CompareLifetimeTransitionsByOffsetThenSlot
{
    bool operator()(const GcInfoEncoderExt::LifetimeTransition& first, const GcInfoEncoderExt::LifetimeTransition& second)
    {
        UINT32 firstOffset  = first.CodeOffset;
        UINT32 secondOffset = second.CodeOffset;

        if (firstOffset == secondOffset)
        {
            return first.SlotId < second.SlotId;
        }
        else
        {
            return firstOffset < secondOffset;
        }
    }
};

struct CompareLifetimeTransitionsBySlot
{
    bool operator()(const GcInfoEncoderExt::LifetimeTransition& first, const GcInfoEncoderExt::LifetimeTransition& second)
    {
        UINT32 firstOffset  = first.CodeOffset;
        UINT32 secondOffset = second.CodeOffset;

        // FIXME: GcInfoEncoding::
        // _ASSERTE(GetNormCodeOffsetChunk(firstOffset) == GetNormCodeOffsetChunk(secondOffset));

        // Sort them by slot
        if( first.SlotId != second.SlotId)
        {
            return first.SlotId < second.SlotId;
        }

        // Then sort them by code offset
        if( firstOffset < secondOffset )
        {
            return true;
        }
        else
        {
            _ASSERTE(( firstOffset > secondOffset ) && "Redundant transitions found in GC info!");
            return false;
        }
    }
};

BitStreamWriter::MemoryBlockList::MemoryBlockList()
    : m_head(nullptr),
      m_tail(nullptr)
{
}

BitStreamWriter::MemoryBlock* BitStreamWriter::MemoryBlockList::AppendNew(IAllocator* allocator, size_t bytes)
{
    auto* memBlock = reinterpret_cast<MemoryBlock*>(allocator->Alloc(sizeof(MemoryBlock) + bytes));
    memBlock->m_next = nullptr;

    if (m_tail != nullptr)
    {
        _ASSERTE(m_head != nullptr);
        m_tail->m_next = memBlock;
    }
    else
    {
        _ASSERTE(m_head == nullptr);
        m_head = memBlock;
    }

    m_tail = memBlock;
    return memBlock;
}

void BitStreamWriter::MemoryBlockList::Dispose(IAllocator* allocator)
{
#ifdef MUST_CALL_JITALLOCATOR_FREE
    for (MemoryBlock* block = m_head, *next; block != nullptr; block = next)
    {
        next = block->m_next;
        allocator->Free(block);
    }
    m_head = nullptr;
    m_tail = nullptr;
#endif
}

template <typename GcInfoEncoding> void TGcInfoEncoder<GcInfoEncoding>::FinalizeSlotIds()
{
#ifdef _DEBUG
    m_IsSlotTableFrozen = TRUE;
#endif
}

template <typename GcInfoEncoding> void TGcInfoEncoder<GcInfoEncoding>::Build()
{
#ifdef _DEBUG
    _ASSERTE(m_IsSlotTableFrozen || m_NumSlots == 0);

    _ASSERTE((1 << GcInfoEncoding::NUM_NORM_CODE_OFFSETS_PER_CHUNK_LOG2) == GcInfoEncoding::NUM_NORM_CODE_OFFSETS_PER_CHUNK);

    char methodName[256];
    m_pCorJitInfo->printMethodName(m_pMethodInfo->ftn, methodName, sizeof(methodName));

    char className[256];
    m_pCorJitInfo->printClassName(m_pCorJitInfo->getMethodClass(m_pMethodInfo->ftn), className, sizeof(className));

    LOG((LF_GCINFO, LL_INFO100,
         "Entering GcInfoEncoder::Build() for method %s:%s\n",
         className, methodName));
#endif


    ///////////////////////////////////////////////////////////////////////
    // Method header
    ///////////////////////////////////////////////////////////////////////


    UINT32 hasGSCookie = (m_GSCookieStackSlot != NO_GS_COOKIE);
    UINT32 hasContextParamType = (m_GenericsInstContextStackSlot != NO_GENERICS_INST_CONTEXT);
    UINT32 hasReversePInvokeFrame = (m_ReversePInvokeFrameSlot != NO_REVERSE_PINVOKE_FRAME);

    BOOL slimHeader = (!m_IsVarArg && !hasGSCookie &&
        !hasContextParamType && (m_InterruptibleRanges.Count() == 0) && !hasReversePInvokeFrame &&
        ((m_StackBaseRegister == NO_STACK_BASE_REGISTER) || (GcInfoEncoding::NORMALIZE_STACK_BASE_REGISTER(m_StackBaseRegister) == 0))) &&
#ifdef TARGET_AMD64
        !m_WantsReportOnlyLeaf &&
#elif defined(TARGET_ARM) || defined(TARGET_ARM64) || defined(TARGET_LOONGARCH64) || defined(TARGET_RISCV64)
        !m_HasTailCalls &&
#endif // TARGET_AMD64
        (m_SizeOfEditAndContinuePreservedArea == NO_SIZE_OF_EDIT_AND_CONTINUE_PRESERVED_AREA);

    // All new code is generated for the latest GCINFO_VERSION.
    // So, always encode ReversePInvokeFrameSlot where applicable.
    if (slimHeader)
    {
        // Slim encoding means nothing special, partially interruptible, maybe a default frame register
        GCINFO_WRITE(m_Info1, 0, 1, FlagsSize); // Slim encoding
#if defined(TARGET_LOONGARCH64)
        assert(m_StackBaseRegister == 22 || 3 == m_StackBaseRegister);
#elif defined(TARGET_RISCV64)
        assert(m_StackBaseRegister == 8 || 2 == m_StackBaseRegister);
#endif
        GCINFO_WRITE(m_Info1, (m_StackBaseRegister == NO_STACK_BASE_REGISTER) ? 0 : 1, 1, FlagsSize);
    }
    else
    {
        GCINFO_WRITE(m_Info1, 1, 1, FlagsSize); // Fat encoding
        GCINFO_WRITE(m_Info1, (m_IsVarArg ? 1 : 0), 1, FlagsSize);
        GCINFO_WRITE(m_Info1, 0 /* unused - was hasSecurityObject */, 1, FlagsSize);
        GCINFO_WRITE(m_Info1, (hasGSCookie ? 1 : 0), 1, FlagsSize);
        GCINFO_WRITE(m_Info1, 0 /* unused - was hasPSPSymStackSlot */, 1, FlagsSize);
        GCINFO_WRITE(m_Info1, m_contextParamType, 2, FlagsSize);
#if defined(TARGET_LOONGARCH64)
        assert(m_StackBaseRegister == 22 || 3 == m_StackBaseRegister);
#elif defined(TARGET_RISCV64)
        assert(m_StackBaseRegister == 8 || 2 == m_StackBaseRegister);
#endif
        GCINFO_WRITE(m_Info1, ((m_StackBaseRegister != NO_STACK_BASE_REGISTER) ? 1 : 0), 1, FlagsSize);
#ifdef TARGET_AMD64
        GCINFO_WRITE(m_Info1, (m_WantsReportOnlyLeaf ? 1 : 0), 1, FlagsSize);
#elif defined(TARGET_ARM) || defined(TARGET_ARM64) || defined(TARGET_LOONGARCH64) || defined(TARGET_RISCV64)
        GCINFO_WRITE(m_Info1, (m_HasTailCalls ? 1 : 0), 1, FlagsSize);
#endif // TARGET_AMD64
        GCINFO_WRITE(m_Info1, ((m_SizeOfEditAndContinuePreservedArea != NO_SIZE_OF_EDIT_AND_CONTINUE_PRESERVED_AREA) ? 1 : 0), 1, FlagsSize);
        GCINFO_WRITE(m_Info1, (hasReversePInvokeFrame ? 1 : 0), 1, FlagsSize);
    }

    _ASSERTE( m_CodeLength > 0 );
    _ASSERTE(GcInfoEncoding::DENORMALIZE_CODE_LENGTH(GcInfoEncoding::NORMALIZE_CODE_LENGTH(m_CodeLength)) == m_CodeLength);
    GCINFO_WRITE_VARL_U(m_Info1, GcInfoEncoding::NORMALIZE_CODE_LENGTH(m_CodeLength), GcInfoEncoding::CODE_LENGTH_ENCBASE, CodeLengthSize);

    if(hasGSCookie)
    {
        _ASSERTE(!slimHeader);
        // Save the valid code range, to be used for determining when GS cookie validation
        // should be performed
        // Encode an intersection of valid offsets
        UINT32 intersectionStart = m_GSCookieValidRangeStart;
        UINT32 intersectionEnd = m_GSCookieValidRangeEnd;

        _ASSERTE(intersectionStart > 0 && intersectionStart < m_CodeLength);
        _ASSERTE(intersectionEnd > 0 && intersectionEnd <= m_CodeLength);
        _ASSERTE(intersectionStart <= intersectionEnd);
        UINT32 normPrologSize = GcInfoEncoding::NORMALIZE_CODE_OFFSET(intersectionStart);
        UINT32 normEpilogSize = GcInfoEncoding::NORMALIZE_CODE_OFFSET(m_CodeLength) - GcInfoEncoding::NORMALIZE_CODE_OFFSET(intersectionEnd);
        _ASSERTE(normPrologSize > 0 && normPrologSize < m_CodeLength);
        _ASSERTE(normEpilogSize < m_CodeLength);

        GCINFO_WRITE_VARL_U(m_Info1, normPrologSize-1, GcInfoEncoding::NORM_PROLOG_SIZE_ENCBASE, ProEpilogSize);
        GCINFO_WRITE_VARL_U(m_Info1, normEpilogSize, GcInfoEncoding::NORM_EPILOG_SIZE_ENCBASE, ProEpilogSize);
    }
    else if (hasContextParamType)
    {
        _ASSERTE(!slimHeader);
        // Save the prolog size, to be used for determining when it is not safe
        // to report generics param context and the security object
        _ASSERTE(m_GSCookieValidRangeStart > 0 && m_GSCookieValidRangeStart < m_CodeLength);
        UINT32 normPrologSize = GcInfoEncoding::NORMALIZE_CODE_OFFSET(m_GSCookieValidRangeStart);
        _ASSERTE(normPrologSize > 0 && normPrologSize < m_CodeLength);

        GCINFO_WRITE_VARL_U(m_Info1, normPrologSize-1, GcInfoEncoding::NORM_PROLOG_SIZE_ENCBASE, ProEpilogSize);
    }

    // Encode the offset to the GS cookie.
    if(hasGSCookie)
    {
        _ASSERTE(!slimHeader);
#ifdef _DEBUG
        LOG((LF_GCINFO, LL_INFO1000, "GS cookie at " FMT_STK "\n",
             DBG_STK(m_GSCookieStackSlot)
             ));
#endif

        GCINFO_WRITE_VARL_S(m_Info1, GcInfoEncoding::NORMALIZE_STACK_SLOT(m_GSCookieStackSlot), GcInfoEncoding::GS_COOKIE_STACK_SLOT_ENCBASE, GsCookieSize);

    }

    // Encode the offset to the generics type context.
    if(m_GenericsInstContextStackSlot != NO_GENERICS_INST_CONTEXT)
    {
        _ASSERTE(!slimHeader);
#ifdef _DEBUG
        LOG((LF_GCINFO, LL_INFO1000, "Generics instantiation context at " FMT_STK "\n",
             DBG_STK(m_GenericsInstContextStackSlot)
             ));
#endif
        GCINFO_WRITE_VARL_S(m_Info1, GcInfoEncoding::NORMALIZE_STACK_SLOT(m_GenericsInstContextStackSlot), GcInfoEncoding::GENERICS_INST_CONTEXT_STACK_SLOT_ENCBASE, GenericsCtxSize);
    }

    if(!slimHeader && (m_StackBaseRegister != NO_STACK_BASE_REGISTER))
    {
#if defined(TARGET_LOONGARCH64)
        assert(m_StackBaseRegister == 22 || 3 == m_StackBaseRegister);
#elif defined(TARGET_RISCV64)
        assert(m_StackBaseRegister == 8 || 2 == m_StackBaseRegister);
#endif
        GCINFO_WRITE_VARL_U(m_Info1, GcInfoEncoding::NORMALIZE_STACK_BASE_REGISTER(m_StackBaseRegister), GcInfoEncoding::STACK_BASE_REGISTER_ENCBASE, StackBaseSize);
    }

    if (m_SizeOfEditAndContinuePreservedArea != NO_SIZE_OF_EDIT_AND_CONTINUE_PRESERVED_AREA)
    {
        GCINFO_WRITE_VARL_U(m_Info1, m_SizeOfEditAndContinuePreservedArea, GcInfoEncoding::SIZE_OF_EDIT_AND_CONTINUE_PRESERVED_AREA_ENCBASE, EncInfoSize);
#ifdef TARGET_ARM64
        GCINFO_WRITE_VARL_U(m_Info1, m_SizeOfEditAndContinueFixedStackFrame, GcInfoEncoding::SIZE_OF_EDIT_AND_CONTINUE_FIXED_STACK_FRAME_ENCBASE, EncInfoSize);
#endif
    }

    if (hasReversePInvokeFrame)
    {
        _ASSERTE(!slimHeader);
        GCINFO_WRITE_VARL_S(m_Info1, GcInfoEncoding::NORMALIZE_STACK_SLOT(m_ReversePInvokeFrameSlot), GcInfoEncoding::REVERSE_PINVOKE_FRAME_ENCBASE, ReversePInvokeFrameSize);
    }

#ifdef FIXED_STACK_PARAMETER_SCRATCH_AREA
    if (!slimHeader)
    {
        _ASSERTE( m_SizeOfStackOutgoingAndScratchArea != (UINT32)-1 );
        GCINFO_WRITE_VARL_U(m_Info1, GcInfoEncoding::NORMALIZE_SIZE_OF_STACK_AREA(m_SizeOfStackOutgoingAndScratchArea), GcInfoEncoding::SIZE_OF_STACK_AREA_ENCBASE, FixedAreaSize);
    }
#endif // FIXED_STACK_PARAMETER_SCRATCH_AREA

    UINT32 numInterruptibleRanges = (UINT32) m_InterruptibleRanges.Count();

    InterruptibleRange *pRanges = NULL;
    if(numInterruptibleRanges)
    {
        pRanges = (InterruptibleRange*) m_pAllocator->Alloc(numInterruptibleRanges * sizeof(InterruptibleRange));
        m_InterruptibleRanges.CopyTo(pRanges);
    }

    BitArray liveState(m_pAllocator, m_NumSlots);
    BitArray couldBeLive(m_pAllocator, m_NumSlots);
    liveState.ClearAll();
    couldBeLive.ClearAll();

#ifdef PARTIALLY_INTERRUPTIBLE_GC_SUPPORTED
    _ASSERTE(m_NumCallSites == 0 || m_pCallSites != NULL);

    ///////////////////////////////////////////////////////////////////////
    // Normalize call sites
    ///////////////////////////////////////////////////////////////////////

    _ASSERTE(m_NumCallSites == 0 || numInterruptibleRanges == 0);

    UINT32 numCallSites = 0;
    for(UINT32 callSiteIndex = 0; callSiteIndex < m_NumCallSites; callSiteIndex++)
    {
        UINT32 callSite = m_pCallSites[callSiteIndex];
        callSite += m_pCallSiteSizes[callSiteIndex];

        _ASSERTE(GcInfoEncoding::DENORMALIZE_CODE_OFFSET(GcInfoEncoding::NORMALIZE_CODE_OFFSET(callSite)) == callSite);
        UINT32 normOffset = GcInfoEncoding::NORMALIZE_CODE_OFFSET(callSite);
        m_pCallSites[numCallSites++] = normOffset;
    }

    GCINFO_WRITE_VARL_U(m_Info1, numCallSites, GcInfoEncoding::NUM_SAFE_POINTS_ENCBASE, NumCallSitesSize);
    m_NumCallSites = numCallSites;
#endif // PARTIALLY_INTERRUPTIBLE_GC_SUPPORTED

    if (slimHeader)
    {
        _ASSERTE(numInterruptibleRanges == 0);
    }
    else
    {
        GCINFO_WRITE_VARL_U(m_Info1, numInterruptibleRanges, GcInfoEncoding::NUM_INTERRUPTIBLE_RANGES_ENCBASE, NumRangesSize);
    }



#ifdef PARTIALLY_INTERRUPTIBLE_GC_SUPPORTED
    ///////////////////////////////////////////////////////////////////////
    // Encode call site offsets
    ///////////////////////////////////////////////////////////////////////

    UINT32 numBitsPerOffset = CeilOfLog2(GcInfoEncoding::NORMALIZE_CODE_OFFSET(m_CodeLength));

    for(UINT32 callSiteIndex = 0; callSiteIndex < m_NumCallSites; callSiteIndex++)
    {
        UINT32 normOffset = m_pCallSites[callSiteIndex];

        _ASSERTE(normOffset < (UINT32)1 << (numBitsPerOffset+1));
        GCINFO_WRITE(m_Info1, normOffset, numBitsPerOffset, CallSitePosSize);
    }
#endif // PARTIALLY_INTERRUPTIBLE_GC_SUPPORTED


    ///////////////////////////////////////////////////////////////////////
    // Encode fully-interruptible ranges
    ///////////////////////////////////////////////////////////////////////

    if(numInterruptibleRanges)
    {
        UINT32 lastStopOffset = 0;

        for(UINT32 i = 0; i < numInterruptibleRanges; i++)
        {
            UINT32 normStartOffset = pRanges[i].NormStartOffset;
            UINT32 normStopOffset = pRanges[i].NormStopOffset;

            size_t normStartDelta = normStartOffset - lastStopOffset;
            size_t normStopDelta = normStopOffset - normStartOffset;
            _ASSERTE(normStopDelta > 0);

            lastStopOffset = normStopOffset;

            GCINFO_WRITE_VARL_U(m_Info1, normStartDelta, GcInfoEncoding::INTERRUPTIBLE_RANGE_DELTA1_ENCBASE, RangeSize);

            GCINFO_WRITE_VARL_U(m_Info1, normStopDelta-1, GcInfoEncoding::INTERRUPTIBLE_RANGE_DELTA2_ENCBASE, RangeSize);
        }
    }


    ///////////////////////////////////////////////////////////////////////
    // Pre-process transitions
    ///////////////////////////////////////////////////////////////////////


    size_t numTransitions = m_LifetimeTransitions.Count();
    LifetimeTransition *pTransitions = (LifetimeTransition*)m_pAllocator->Alloc(numTransitions * sizeof(LifetimeTransition));
    m_LifetimeTransitions.CopyTo(pTransitions);

    LifetimeTransition* pEndTransitions = pTransitions + numTransitions;
    LifetimeTransition* pCurrent;

    //-----------------------------------------------------------------
    // Sort the lifetime transitions by offset (then by slot id).
    //-----------------------------------------------------------------

    // Don't use the CQuickSort algorithm, it's prone to stack overflows
    jitstd::sort(
        pTransitions,
        pTransitions + numTransitions,
        CompareLifetimeTransitionsByOffsetThenSlot()
        );

    // Eliminate transitions outside the method
    while(pEndTransitions > pTransitions)
    {
        LifetimeTransition *pPrev = pEndTransitions - 1;
        if(pPrev->CodeOffset < m_CodeLength)
            break;

        _ASSERTE(pPrev->CodeOffset == m_CodeLength && !pPrev->BecomesLive);
        pEndTransitions = pPrev;
    }

    // Now eliminate any pairs of dead/live transitions for the same slot at the same offset.
    EliminateRedundantLiveDeadPairs(&pTransitions, &numTransitions, &pEndTransitions);

#ifdef _DEBUG
    numTransitions = -1;
#endif
    ///////////////////////////////////////////////////////////////////////
    // Sort the slot table
    ///////////////////////////////////////////////////////////////////////

    {
        GcSlotDescAndId* sortedSlots = (GcSlotDescAndId*) m_pAllocator->Alloc(m_NumSlots * sizeof(GcSlotDescAndId));
        UINT32* sortOrder = (UINT32*) m_pAllocator->Alloc(m_NumSlots * sizeof(UINT32));

        for(UINT32 i = 0; i < m_NumSlots; i++)
        {
            sortedSlots[i].m_SlotDesc = m_SlotTable[i];
            sortedSlots[i].m_SlotId = i;
        }

        jitstd::sort(sortedSlots, sortedSlots + m_NumSlots, CompareSlotDescAndIdBySlotDesc());

        for(UINT32 i = 0; i < m_NumSlots; i++)
        {
            sortOrder[sortedSlots[i].m_SlotId] = i;
        }

        // Re-order the slot table
        for(UINT32 i = 0; i < m_NumSlots; i++)
        {
            m_SlotTable[i] = sortedSlots[i].m_SlotDesc;
        }

        // Update transitions to assign new slot ids
        for(pCurrent = pTransitions; pCurrent < pEndTransitions; pCurrent++)
        {
            UINT32 newSlotId = sortOrder[pCurrent->SlotId];
            pCurrent->SlotId = newSlotId;
        }

#ifdef MUST_CALL_JITALLOCATOR_FREE
        m_pAllocator->Free( sortedSlots );
        m_pAllocator->Free( sortOrder );
#endif
    }

    if (GcInfoEncoding::CODE_OFFSETS_NEED_NORMALIZATION) {
        // Do a pass to normalize transition offsets
        for(pCurrent = pTransitions; pCurrent < pEndTransitions; pCurrent++)
        {
            _ASSERTE(pCurrent->CodeOffset <= m_CodeLength);
            pCurrent->CodeOffset = GcInfoEncoding::NORMALIZE_CODE_OFFSET(pCurrent->CodeOffset);
        }
    }

    ///////////////////////////////////////////////////////////////////
    // Find out which slots are really used
    ///////////////////////////////////////////////////////////////////

    couldBeLive.ClearAll();

#ifdef PARTIALLY_INTERRUPTIBLE_GC_SUPPORTED
    if(m_NumCallSites)
    {
        _ASSERTE(m_pCallSites != NULL);
        liveState.ClearAll();

        UINT32 callSiteIndex = 0;
        UINT32 callSite = m_pCallSites[0];

        for(pCurrent = pTransitions; pCurrent < pEndTransitions; )
        {
            if(pCurrent->CodeOffset >= callSite)
            {
                couldBeLive |= liveState;

                if(++callSiteIndex == m_NumCallSites)
                    break;

                callSite = m_pCallSites[callSiteIndex];
            }
            else
            {
                UINT32 slotIndex = pCurrent->SlotId;
                BYTE becomesLive = pCurrent->BecomesLive;
                _ASSERTE((liveState.ReadBit(slotIndex) && !becomesLive)
                        || (!liveState.ReadBit(slotIndex) && becomesLive));

                liveState.WriteBit(slotIndex, becomesLive);
                pCurrent++;
            }
        }
        // There could be call sites after the last transition
        if(callSiteIndex < m_NumCallSites)
        {
            couldBeLive |= liveState;
        }
    }

    if(numInterruptibleRanges)
    {
        liveState.ClearAll();

        InterruptibleRange *pCurrentRange = pRanges;
        InterruptibleRange *pEndRanges = pRanges + numInterruptibleRanges;

        for(pCurrent = pTransitions; pCurrent < pEndTransitions; )
        {
            // Find the first transition at offset > of the start of the current range
            LifetimeTransition *pFirstAfterStart = pCurrent;
            while(pFirstAfterStart->CodeOffset <= pCurrentRange->NormStartOffset)
            {
                UINT32 slotIndex = (UINT32) (pFirstAfterStart->SlotId);
                BYTE becomesLive = pFirstAfterStart->BecomesLive;
                _ASSERTE((liveState.ReadBit(slotIndex) && !becomesLive)
                        || (!liveState.ReadBit(slotIndex) && becomesLive));
                liveState.WriteBit(slotIndex, becomesLive);

                if(++pFirstAfterStart == pEndTransitions)
                    break;
            }

            couldBeLive |= liveState;

            // Now iterate through all the remaining transitions in the range,
            //   making the offset range-relative, and tracking live state
            UINT32 rangeStop = pCurrentRange->NormStopOffset;

            for(pCurrent = pFirstAfterStart; pCurrent < pEndTransitions && pCurrent->CodeOffset < rangeStop; pCurrent++)
            {
                UINT32 slotIndex = (UINT32) (pCurrent->SlotId);
                BYTE becomesLive = pCurrent->BecomesLive;
                _ASSERTE((liveState.ReadBit(slotIndex) && !becomesLive)
                        || (!liveState.ReadBit(slotIndex) && becomesLive));
                liveState.WriteBit(slotIndex, becomesLive);
                couldBeLive.SetBit(slotIndex);
            }

            // Move to the next range
            if(pCurrentRange < pEndRanges - 1)
            {
                pCurrentRange++;
            }
            else
            {
                break;
            }
        }
    }

    //-----------------------------------------------------------------
    // Mark unneeded slots as deleted
    //-----------------------------------------------------------------

    UINT32 numUsedSlots = 0;
    for(UINT32 i = 0; i < m_NumSlots; i++)
    {
        if(!(m_SlotTable[i].IsUntracked()) && (couldBeLive.ReadBit(i) == 0))
        {
            m_SlotTable[i].MarkDeleted();
        }
        else
            numUsedSlots++;
    }

    if(numUsedSlots < m_NumSlots)
    {
        // Delete transitions on unused slots
        LifetimeTransition *pNextFree = pTransitions;
        for(pCurrent = pTransitions; pCurrent < pEndTransitions; pCurrent++)
        {
            UINT32 slotId = pCurrent->SlotId;
            if(!m_SlotTable[slotId].IsDeleted())
            {
                if(pCurrent > pNextFree)
                {
                    *pNextFree = *pCurrent;
                }
                pNextFree++;
            }
        }
        pEndTransitions = pNextFree;
    }

#else  // PARTIALLY_INTERRUPTIBLE_GC_SUPPORTED

    UINT32 numUsedSlots = m_NumSlots;

#endif  // PARTIALLY_INTERRUPTIBLE_GC_SUPPORTED


    ///////////////////////////////////////////////////////////////////////
    // Encode slot table
    ///////////////////////////////////////////////////////////////////////

    //------------------------------------------------------------------
    // Count registers and stack slots
    //------------------------------------------------------------------

    UINT32 numRegisters;
    UINT32 numUntrackedSlots;
    UINT32 numStackSlots;

    {
        UINT32 numDeleted = 0;
        UINT32 i;
        for(i = 0; i < m_NumSlots && m_SlotTable[i].IsRegister(); i++)
        {
            if(m_SlotTable[i].IsDeleted())
            {
                numDeleted++;
            }
        }
        numRegisters = i - numDeleted;

        for(; i < m_NumSlots && !m_SlotTable[i].IsUntracked(); i++)
        {
            if(m_SlotTable[i].IsDeleted())
            {
                numDeleted++;
            }
        }
        numStackSlots = i - (numRegisters + numDeleted);
    }
    numUntrackedSlots = numUsedSlots - (numRegisters + numStackSlots);

    // Common case: nothing, or a few registers
    if (numRegisters)
    {
        GCINFO_WRITE(m_Info1, 1, 1, FlagsSize);
        GCINFO_WRITE_VARL_U(m_Info1, numRegisters, GcInfoEncoding::NUM_REGISTERS_ENCBASE, NumRegsSize);
    }
    else
    {
        GCINFO_WRITE(m_Info1, 0, 1, FlagsSize);
    }
    if (numStackSlots || numUntrackedSlots)
    {
        GCINFO_WRITE(m_Info1, 1, 1, FlagsSize);
        GCINFO_WRITE_VARL_U(m_Info1, numStackSlots, GcInfoEncoding::NUM_STACK_SLOTS_ENCBASE, NumStackSize);
        GCINFO_WRITE_VARL_U(m_Info1, numUntrackedSlots, GcInfoEncoding::NUM_UNTRACKED_SLOTS_ENCBASE, NumUntrackedSize);
    }
    else
    {
        GCINFO_WRITE(m_Info1, 0, 1, FlagsSize);
    }

    UINT32 currentSlot = 0;

    if(numUsedSlots == 0)
        goto lExitSuccess;

    if(numRegisters > 0)
    {
        GcSlotDesc *pSlotDesc;
        do
        {
            _ASSERTE(currentSlot < m_NumSlots);
            pSlotDesc = &m_SlotTable[currentSlot++];
        }
        while(pSlotDesc->IsDeleted());
        _ASSERTE(pSlotDesc->IsRegister());

        // Encode slot identification
        UINT32 currentNormRegNum = pSlotDesc->Slot.RegisterNumber;
        GCINFO_WRITE_VARL_U(m_Info1, currentNormRegNum, GcInfoEncoding::REGISTER_ENCBASE, RegSlotSize);
        GCINFO_WRITE(m_Info1, pSlotDesc->Flags, 2, RegSlotSize);

        for(UINT32 j = 1; j < numRegisters; j++)
        {
            UINT32 lastNormRegNum = currentNormRegNum;
            GcSlotFlags lastFlags = pSlotDesc->Flags;

            do
            {
                _ASSERTE(currentSlot < m_NumSlots);
                pSlotDesc = &m_SlotTable[currentSlot++];
            }
            while(pSlotDesc->IsDeleted());
            _ASSERTE(pSlotDesc->IsRegister());

            currentNormRegNum = pSlotDesc->Slot.RegisterNumber;

            if(lastFlags != GC_SLOT_IS_REGISTER)
            {
                GCINFO_WRITE_VARL_U(m_Info1, currentNormRegNum, GcInfoEncoding::REGISTER_ENCBASE, RegSlotSize);
                GCINFO_WRITE(m_Info1, pSlotDesc->Flags, 2, RegSlotSize);
            }
            else
            {
                _ASSERTE(pSlotDesc->Flags == GC_SLOT_IS_REGISTER);
                GCINFO_WRITE_VARL_U(m_Info1, currentNormRegNum - lastNormRegNum - 1, GcInfoEncoding::REGISTER_DELTA_ENCBASE, RegSlotSize);
            }
        }
    }

    if(numStackSlots > 0)
    {
        GcSlotDesc *pSlotDesc;
        do
        {
            _ASSERTE(currentSlot < m_NumSlots);
            pSlotDesc = &m_SlotTable[currentSlot++];
        }
        while(pSlotDesc->IsDeleted());
        _ASSERTE(!pSlotDesc->IsRegister());
        _ASSERTE(!pSlotDesc->IsUntracked());

        // Encode slot identification
        _ASSERTE((pSlotDesc->Slot.Stack.Base & ~3) == 0);
        GCINFO_WRITE(m_Info1, pSlotDesc->Slot.Stack.Base, 2, StackSlotSize);
        INT32 currentNormStackSlot = GcInfoEncoding::NORMALIZE_STACK_SLOT(pSlotDesc->Slot.Stack.SpOffset);
        GCINFO_WRITE_VARL_S(m_Info1, currentNormStackSlot, GcInfoEncoding::STACK_SLOT_ENCBASE, StackSlotSize);

        GCINFO_WRITE(m_Info1, pSlotDesc->Flags, 2, StackSlotSize);

        for(UINT32 j = 1; j < numStackSlots; j++)
        {
            INT32 lastNormStackSlot = currentNormStackSlot;
            GcSlotFlags lastFlags = pSlotDesc->Flags;

            do
            {
                _ASSERTE(currentSlot < m_NumSlots);
                pSlotDesc = &m_SlotTable[currentSlot++];
            }
            while(pSlotDesc->IsDeleted());
            _ASSERTE(!pSlotDesc->IsRegister());
            _ASSERTE(!pSlotDesc->IsUntracked());

            currentNormStackSlot = GcInfoEncoding::NORMALIZE_STACK_SLOT(pSlotDesc->Slot.Stack.SpOffset);

            _ASSERTE((pSlotDesc->Slot.Stack.Base & ~3) == 0);
            GCINFO_WRITE(m_Info1, pSlotDesc->Slot.Stack.Base, 2, StackSlotSize);

            if(lastFlags != GC_SLOT_BASE)
            {
                GCINFO_WRITE_VARL_S(m_Info1, currentNormStackSlot, GcInfoEncoding::STACK_SLOT_ENCBASE, StackSlotSize);
                GCINFO_WRITE(m_Info1, pSlotDesc->Flags, 2, StackSlotSize);
            }
            else
            {
                _ASSERTE(pSlotDesc->Flags == GC_SLOT_BASE);
                GCINFO_WRITE_VARL_U(m_Info1, currentNormStackSlot - lastNormStackSlot, GcInfoEncoding::STACK_SLOT_DELTA_ENCBASE, StackSlotSize);
            }
        }
    }

    if(numUntrackedSlots > 0)
    {
        GcSlotDesc *pSlotDesc;
        do
        {
            _ASSERTE(currentSlot < m_NumSlots);
            pSlotDesc = &m_SlotTable[currentSlot++];
        }
        while(pSlotDesc->IsDeleted());
        _ASSERTE(!pSlotDesc->IsRegister());
        _ASSERTE(pSlotDesc->IsUntracked());

        // Encode slot identification
        _ASSERTE((pSlotDesc->Slot.Stack.Base & ~3) == 0);
        GCINFO_WRITE(m_Info1, pSlotDesc->Slot.Stack.Base, 2, UntrackedSlotSize);
        INT32 currentNormStackSlot = GcInfoEncoding::NORMALIZE_STACK_SLOT(pSlotDesc->Slot.Stack.SpOffset);
        GCINFO_WRITE_VARL_S(m_Info1, currentNormStackSlot, GcInfoEncoding::STACK_SLOT_ENCBASE, UntrackedSlotSize);

        GCINFO_WRITE(m_Info1, pSlotDesc->Flags, 2, UntrackedSlotSize);

        for(UINT32 j = 1; j < numUntrackedSlots; j++)
        {
            INT32 lastNormStackSlot = currentNormStackSlot;
            GcSlotFlags lastFlags = pSlotDesc->Flags;

            do
            {
                _ASSERTE(currentSlot < m_NumSlots);
                pSlotDesc = &m_SlotTable[currentSlot++];
            }
            while(pSlotDesc->IsDeleted());
            _ASSERTE(!pSlotDesc->IsRegister());
            _ASSERTE(pSlotDesc->IsUntracked());

            currentNormStackSlot = GcInfoEncoding::NORMALIZE_STACK_SLOT(pSlotDesc->Slot.Stack.SpOffset);

            _ASSERTE((pSlotDesc->Slot.Stack.Base & ~3) == 0);
            GCINFO_WRITE(m_Info1, pSlotDesc->Slot.Stack.Base, 2, UntrackedSlotSize);

            if(lastFlags != GC_SLOT_UNTRACKED)
            {
                GCINFO_WRITE_VARL_S(m_Info1, currentNormStackSlot, GcInfoEncoding::STACK_SLOT_ENCBASE, UntrackedSlotSize);
                GCINFO_WRITE(m_Info1, pSlotDesc->Flags, 2, UntrackedSlotSize);
            }
            else
            {
                _ASSERTE(pSlotDesc->Flags == GC_SLOT_UNTRACKED);
                GCINFO_WRITE_VARL_U(m_Info1, currentNormStackSlot - lastNormStackSlot, GcInfoEncoding::STACK_SLOT_DELTA_ENCBASE, UntrackedSlotSize);
            }
        }
    }


#ifdef PARTIALLY_INTERRUPTIBLE_GC_SUPPORTED
    //-----------------------------------------------------------------
    // Encode GC info at call sites
    //-----------------------------------------------------------------

    if(m_NumCallSites)
    {

        _ASSERTE(m_pCallSites != NULL);

        liveState.ClearAll();

        UINT32 callSiteIndex = 0;
        UINT32 callSite = m_pCallSites[0];

        // Create a hash table for storing the locations of the live sets
        LiveStateHashTable hashMap(m_pAllocator);

        bool outOfMemory = false;
        try
        {
            for(pCurrent = pTransitions; pCurrent < pEndTransitions; )
            {
                if(pCurrent->CodeOffset >= callSite)
                {
                    // Time to record the call site

                    // Add it to the table if it doesn't exist
                    UINT32 liveStateOffset = 0;
                    if (!hashMap.Lookup(&liveState, &liveStateOffset))
                    {
                        BitArray * newLiveState = new (m_pAllocator) BitArray(m_pAllocator, m_NumSlots);
                        *newLiveState = liveState;
                        hashMap.Set(newLiveState, (UINT32)(-1));
                    }


                    if(++callSiteIndex == m_NumCallSites)
                        break;

                    callSite = m_pCallSites[callSiteIndex];
                }
                else
                {
                    UINT32 slotIndex = pCurrent->SlotId;
                    BYTE becomesLive = pCurrent->BecomesLive;
                    _ASSERTE((liveState.ReadBit(slotIndex) && !becomesLive)
                            || (!liveState.ReadBit(slotIndex) && becomesLive));
                    liveState.WriteBit(slotIndex, becomesLive);
                    pCurrent++;
                }
            }

            // Check for call sites at offsets past the last transition
            if (callSiteIndex < m_NumCallSites)
            {
                UINT32 liveStateOffset = 0;
                if (!hashMap.Lookup(&liveState, &liveStateOffset))
                {
                    BitArray * newLiveState = new (m_pAllocator) BitArray(m_pAllocator, m_NumSlots);
                    *newLiveState = liveState;
                    hashMap.Set(newLiveState, (UINT32)(-1));
                }
            }
        }
        catch (GcInfoNoMemoryException&)
        {
            outOfMemory = true;
        }

        if (outOfMemory)
        {
            m_pNoMem();
        }

        // Figure out the largest offset, and total size of the sets
        // Be sure to figure out the largest offset in the order that we will be emitting
        // them in and not the order of their appearances in the safe point array.
        // TODO: we should sort this to improve locality (the more frequent ones at the beginning)
        // and to improve the indirection size (if the largest one is last, we *might* be able
        // so use 1 less bit for each indirection for the offset encoding).
        UINT32 largestSetOffset = 0;
        UINT32 sizeofSets = 0;
        for (LiveStateHashTable::KeyIterator iter = hashMap.Begin(), end = hashMap.End(); !iter.Equal(end); iter.Next())
        {
            largestSetOffset = sizeofSets;
            sizeofSets += SizeofSlotStateVarLengthVector(*iter.Get(), GcInfoEncoding::LIVESTATE_RLE_SKIP_ENCBASE, GcInfoEncoding::LIVESTATE_RLE_RUN_ENCBASE);
        }

        // Now that we know the largest offset, we can figure out how much the indirection
        // will cost us and commit
        UINT32 numBitsPerPointer = ((largestSetOffset < 2) ? 1 : CeilOfLog2(largestSetOffset + 1));
        const size_t sizeofEncodedNumBitsPerPointer = BitStreamWriter::SizeofVarLengthUnsigned(numBitsPerPointer, GcInfoEncoding::POINTER_SIZE_ENCBASE);
        const size_t sizeofNoIndirection = m_NumCallSites * (numRegisters + numStackSlots);
        const size_t sizeofIndirection = sizeofEncodedNumBitsPerPointer  // Encode the pointer sizes
                                         + (m_NumCallSites * numBitsPerPointer) // Encode the pointers
                                         + 7 // Up to 7 bits of alignment padding
                                         + sizeofSets; // Encode the actual live sets

        liveState.ClearAll();

        callSiteIndex = 0;
        callSite = m_pCallSites[0];

        if (sizeofIndirection < sizeofNoIndirection)
        {
            // we are using an indirection
            GCINFO_WRITE(m_Info1, 1, 1, FlagsSize);
            GCINFO_WRITE_VARL_U(m_Info1, numBitsPerPointer - 1, GcInfoEncoding::POINTER_SIZE_ENCBASE, CallSiteStateSize);

            // Now encode the live sets and record the real offset
            for (LiveStateHashTable::KeyIterator iter = hashMap.Begin(), end = hashMap.End(); !iter.Equal(end); iter.Next())
            {
                _ASSERTE(FitsIn<UINT32>(m_Info2.GetBitCount()));
                iter.SetValue((UINT32)m_Info2.GetBitCount());
                GCINFO_WRITE_VAR_VECTOR(m_Info2, *iter.Get(), GcInfoEncoding::LIVESTATE_RLE_SKIP_ENCBASE, GcInfoEncoding::LIVESTATE_RLE_RUN_ENCBASE, CallSiteStateSize);
            }

            _ASSERTE(sizeofSets == m_Info2.GetBitCount());

            for(pCurrent = pTransitions; pCurrent < pEndTransitions; )
            {
                if(pCurrent->CodeOffset >= callSite)
                {
                    // Time to encode the call site

                    // Find the match and emit it
                    UINT32 liveStateOffset = 0;
                    bool found = hashMap.Lookup(&liveState, &liveStateOffset);
                    _ASSERTE(found);
                    (void)found;
                    GCINFO_WRITE(m_Info1, liveStateOffset, numBitsPerPointer, CallSiteStateSize);


                    if(++callSiteIndex == m_NumCallSites)
                        break;

                    callSite = m_pCallSites[callSiteIndex];
                }
                else
                {
                    UINT32 slotIndex = pCurrent->SlotId;
                    BYTE becomesLive = pCurrent->BecomesLive;
                    _ASSERTE((liveState.ReadBit(slotIndex) && !becomesLive)
                            || (!liveState.ReadBit(slotIndex) && becomesLive));
                    liveState.WriteBit(slotIndex, becomesLive);
                    pCurrent++;
                }
            }

            // Encode call sites at offsets past the last transition
            {
                UINT32 liveStateOffset = 0;
                bool found = hashMap.Lookup(&liveState, &liveStateOffset);
                _ASSERTE(found);
                (void)found;
                for( ; callSiteIndex < m_NumCallSites; callSiteIndex++)
                {
                    GCINFO_WRITE(m_Info1, liveStateOffset, numBitsPerPointer, CallSiteStateSize);
                }
            }
        }
        else
        {
            // we are not using an indirection
            GCINFO_WRITE(m_Info1, 0, 1, FlagsSize);

            for(pCurrent = pTransitions; pCurrent < pEndTransitions; )
            {
                if(pCurrent->CodeOffset >= callSite)
                {
                    // Time to encode the call site
                    GCINFO_WRITE_VECTOR(m_Info1, liveState, CallSiteStateSize);

                    if(++callSiteIndex == m_NumCallSites)
                        break;

                    callSite = m_pCallSites[callSiteIndex];
                }
                else
                {
                    UINT32 slotIndex = pCurrent->SlotId;
                    BYTE becomesLive = pCurrent->BecomesLive;
                    _ASSERTE((liveState.ReadBit(slotIndex) && !becomesLive)
                            || (!liveState.ReadBit(slotIndex) && becomesLive));
                    liveState.WriteBit(slotIndex, becomesLive);
                    pCurrent++;
                }
            }

            // Encode call sites at offsets past the last transition
            for( ; callSiteIndex < m_NumCallSites; callSiteIndex++)
            {
                GCINFO_WRITE_VECTOR(m_Info1, liveState, CallSiteStateSize);
            }
        }

#ifdef MUST_CALL_JITALLOCATOR_FREE
        // Cleanup
        for (LiveStateHashTable::KeyIterator iter = hashMap.Begin(), end = hashMap.End(); !iter.Equal(end); iter.Next())
        {
            m_pAllocator->Free((LPVOID)iter.Get());
        }
#endif // MUST_CALL_JITALLOCATOR_FREE

    }
#endif  // PARTIALLY_INTERRUPTIBLE_GC_SUPPORTED


    ///////////////////////////////////////////////////////////////////////
    // Fully-interruptible: Encode lifetime transitions
    ///////////////////////////////////////////////////////////////////////

    if(numInterruptibleRanges)
    {
#ifdef PARTIALLY_INTERRUPTIBLE_GC_SUPPORTED
        //-----------------------------------------------------
        // Under partially-interruptible, make the transition
        //  offsets relative to the interruptible regions
        //-----------------------------------------------------

        // Compute total length of interruptible ranges
        UINT32 totalInterruptibleLength = 0;
        for(UINT32 i = 0; i < numInterruptibleRanges; i++)
        {
            InterruptibleRange *pRange = &pRanges[i];
            totalInterruptibleLength += pRange->NormStopOffset - pRange->NormStartOffset;
        }
        _ASSERTE(totalInterruptibleLength <= GcInfoEncoding::NORMALIZE_CODE_OFFSET(m_CodeLength));

        liveState.ClearAll();
        // Re-use couldBeLive
        BitArray& liveStateAtPrevRange = couldBeLive;
        liveStateAtPrevRange.ClearAll();

        InterruptibleRange *pCurrentRange = pRanges;
        InterruptibleRange *pEndRanges = pRanges + numInterruptibleRanges;
        UINT32 cumInterruptibleLength = 0;

        for(pCurrent = pTransitions; pCurrent < pEndTransitions; )
        {
            _ASSERTE(!m_SlotTable[pCurrent->SlotId].IsDeleted());

            // Find the first transition at offset > of the start of the current range
            LifetimeTransition *pFirstAfterStart = pCurrent;
            while(pFirstAfterStart->CodeOffset <= pCurrentRange->NormStartOffset)
            {
                UINT32 slotIndex = (UINT32) (pFirstAfterStart->SlotId);
                BYTE becomesLive = pFirstAfterStart->BecomesLive;
                _ASSERTE((liveState.ReadBit(slotIndex) && !becomesLive)
                        || (!liveState.ReadBit(slotIndex) && becomesLive));
                liveState.WriteBit(slotIndex, becomesLive);

                if(++pFirstAfterStart == pEndTransitions)
                    break;
            }

            // Now compare the liveState with liveStateAtPrevRange
            LifetimeTransition *pFirstPreserved = pFirstAfterStart;
            for(UINT32 slotIndex = 0; slotIndex < m_NumSlots; slotIndex++)
            {
                uint32_t isLive = liveState.ReadBit(slotIndex);
                if(isLive != liveStateAtPrevRange.ReadBit(slotIndex))
                {
                    pFirstPreserved--;
                    _ASSERTE(pFirstPreserved >= pCurrent);
                    pFirstPreserved->CodeOffset = cumInterruptibleLength;
                    pFirstPreserved->SlotId = slotIndex;
                    pFirstPreserved->BecomesLive = (isLive) ? 1 : 0;
                    _ASSERTE(!pFirstPreserved->IsDeleted);
                }
            }

            // Mark all the other transitions since last range as deleted
            _ASSERTE(pCurrent <= pFirstPreserved);
            while(pCurrent < pFirstPreserved)
            {
                (pCurrent++)->IsDeleted = TRUE;
            }

            // Now iterate through all the remaining transitions in the range,
            //   making the offset range-relative, and tracking live state
            UINT32 rangeStop = pCurrentRange->NormStopOffset;

            for(pCurrent = pFirstAfterStart; pCurrent < pEndTransitions && pCurrent->CodeOffset < rangeStop; pCurrent++)
            {
                pCurrent->CodeOffset =
                                pCurrent->CodeOffset -
                                pCurrentRange->NormStartOffset +
                                cumInterruptibleLength;

                UINT32 slotIndex = (UINT32) (pCurrent->SlotId);
                BYTE becomesLive = pCurrent->BecomesLive;
                _ASSERTE((liveState.ReadBit(slotIndex) && !becomesLive)
                        || (!liveState.ReadBit(slotIndex) && becomesLive));
                liveState.WriteBit(slotIndex, becomesLive);
            }

            // Move to the next range
            if(pCurrentRange < pEndRanges - 1)
            {
                cumInterruptibleLength += pCurrentRange->NormStopOffset - pCurrentRange->NormStartOffset;
                pCurrentRange++;

                liveStateAtPrevRange = liveState;
            }
            else
            {
                pEndTransitions = pCurrent;
                break;
            }
        }

        // Make another pass, deleting everything that's marked as deleted
        LifetimeTransition *pNextFree = pTransitions;
        for(pCurrent = pTransitions; pCurrent < pEndTransitions; pCurrent++)
        {
            if(!pCurrent->IsDeleted)
            {
                if(pCurrent > pNextFree)
                {
                    *pNextFree = *pCurrent;
                }
                pNextFree++;
            }
        }
        pEndTransitions = pNextFree;

#else
        UINT32 totalInterruptibleLength = GcInfoEncoding::NORMALIZE_CODE_OFFSET(m_CodeLength);

#endif //PARTIALLY_INTERRUPTIBLE_GC_SUPPORTED

        //
        // Initialize chunk pointers
        //
        UINT32 numChunks = (totalInterruptibleLength + GcInfoEncoding::NUM_NORM_CODE_OFFSETS_PER_CHUNK - 1) / GcInfoEncoding::NUM_NORM_CODE_OFFSETS_PER_CHUNK;
        _ASSERTE(numChunks > 0);

        size_t* pChunkPointers = (size_t*) m_pAllocator->Alloc(numChunks*sizeof(size_t));
        ZeroMemory(pChunkPointers, numChunks*sizeof(size_t));

        //------------------------------------------------------------------
        // Encode transitions
        //------------------------------------------------------------------

        LOG((LF_GCINFO, LL_INFO1000, "Encoding %i lifetime transitions.\n", pEndTransitions - pTransitions));


        liveState.ClearAll();
        couldBeLive.ClearAll();

        for(pCurrent = pTransitions; pCurrent < pEndTransitions; )
        {
            _ASSERTE(pCurrent->CodeOffset < m_CodeLength);

            UINT32 currentChunk = GetNormCodeOffsetChunk(pCurrent->CodeOffset);
            _ASSERTE(currentChunk < numChunks);
            UINT32 numTransitionsInCurrentChunk = 1;

            for(;;)
            {
                UINT32 slotIndex = (UINT32) (pCurrent->SlotId);
                BYTE becomesLive = pCurrent->BecomesLive;
                _ASSERTE((liveState.ReadBit(slotIndex) && !becomesLive)
                        || (!liveState.ReadBit(slotIndex) && becomesLive));
                liveState.WriteBit(slotIndex, becomesLive);
                couldBeLive.SetBit(slotIndex);

                pCurrent++;
                if(pCurrent == pEndTransitions || GetNormCodeOffsetChunk(pCurrent->CodeOffset) != currentChunk)
                    break;

                numTransitionsInCurrentChunk++;
            }

            //-----------------------------------------------------
            // Time to encode the chunk
            //-----------------------------------------------------

            _ASSERTE(numTransitionsInCurrentChunk > 0);

            // Sort the transitions in this chunk by slot
            jitstd::sort(
                pCurrent - numTransitionsInCurrentChunk,
                pCurrent,
                CompareLifetimeTransitionsBySlot()
                );

            // Save chunk pointer
            pChunkPointers[currentChunk] = m_Info2.GetBitCount() + 1;

            // Write couldBeLive slot map
            GCINFO_WRITE_VAR_VECTOR(m_Info2, couldBeLive, GcInfoEncoding::LIVESTATE_RLE_SKIP_ENCBASE, GcInfoEncoding::LIVESTATE_RLE_RUN_ENCBASE, ChunkMaskSize);

            LOG((LF_GCINFO, LL_INFO100000,
                         "Chunk %d couldBeLive (%04x-%04x):\n", currentChunk,
                         currentChunk * GcInfoEncoding::NUM_NORM_CODE_OFFSETS_PER_CHUNK,
                         ((currentChunk + 1) * GcInfoEncoding::NUM_NORM_CODE_OFFSETS_PER_CHUNK) - 1
                         ));

            // Write final state
            // For all the bits set in couldBeLive.
            UINT32 i;
            for (BitArrayIterator iter(&couldBeLive); !iter.end(); iter++)
            {
                i = *iter;
                {
                    _ASSERTE(!m_SlotTable[i].IsDeleted());
                    _ASSERTE(!m_SlotTable[i].IsUntracked());
                    GCINFO_WRITE(   m_Info2,
                                    liveState.ReadBit(i) ? 1 : 0,
                                    1,
                                    ChunkFinalStateSize
                                    );

                    LOG((LF_GCINFO, LL_INFO100000,
                         "\t" LOG_GCSLOTDESC_FMT " %s at end of chunk.\n",
                         LOG_GCSLOTDESC_ARGS(&m_SlotTable[i]),
                         liveState.ReadBit(i) ? "live" : "dead"));
                }
            }

            // Write transitions offsets
            UINT32 normChunkBaseCodeOffset = currentChunk * GcInfoEncoding::NUM_NORM_CODE_OFFSETS_PER_CHUNK;

            LifetimeTransition* pT = pCurrent - numTransitionsInCurrentChunk;

            for (BitArrayIterator iter(&couldBeLive); !iter.end(); iter++)
            {
                i = *iter;

                while(pT < pCurrent)
                {
                    GcSlotId slotId = pT->SlotId;
                    if(slotId != i)
                        break;

                    _ASSERTE(couldBeLive.ReadBit(slotId));

                    LOG((LF_GCINFO, LL_INFO100000,
                         "\tTransition " LOG_GCSLOTDESC_FMT " going %s at offset %04x.\n",
                         LOG_GCSLOTDESC_ARGS(&m_SlotTable[pT->SlotId]),
                         pT->BecomesLive ? "live" : "dead",
                         (int) pT->CodeOffset ));

                    // Write code offset delta
                    UINT32 normCodeOffset = pT->CodeOffset;
                    UINT32 normCodeOffsetDelta = normCodeOffset - normChunkBaseCodeOffset;

                    // Don't encode transitions at offset 0 as they are useless
                    if(normCodeOffsetDelta)
                    {
                        _ASSERTE(normCodeOffsetDelta < GcInfoEncoding::NUM_NORM_CODE_OFFSETS_PER_CHUNK);

                        GCINFO_WRITE(m_Info2, 1, 1, ChunkTransitionSize);
                        GCINFO_WRITE(m_Info2, normCodeOffsetDelta, GcInfoEncoding::NUM_NORM_CODE_OFFSETS_PER_CHUNK_LOG2, ChunkTransitionSize);

#ifdef MEASURE_GCINFO
                        m_CurrentMethodSize.NumTransitions++;
#endif
                    }

                    pT++;
                }

                // Write terminator
                GCINFO_WRITE(m_Info2, 0, 1, ChunkTransitionSize);

            }
            _ASSERTE(pT == pCurrent);

            couldBeLive = liveState;
        }

        //---------------------------------------------------------------------
        // The size of chunk encodings is now known. Write the chunk pointers.
        //---------------------------------------------------------------------


        // Find the largest pointer
        size_t largestPointer = 0;
        for(int i = numChunks - 1; i >=0; i--)
        {
            largestPointer = pChunkPointers[i];
            if(largestPointer > 0)
                break;
        }

        UINT32 numBitsPerPointer = CeilOfLog2(largestPointer + 1);
        GCINFO_WRITE_VARL_U(m_Info1, numBitsPerPointer, GcInfoEncoding::POINTER_SIZE_ENCBASE, ChunkPtrSize);

        if(numBitsPerPointer)
        {
            for(UINT32 i = 0; i < numChunks; i++)
            {
                GCINFO_WRITE(m_Info1, pChunkPointers[i], numBitsPerPointer, ChunkPtrSize);
            }
        }

        //-------------------------------------------------------------------
        // Cleanup
        //-------------------------------------------------------------------

#ifdef MUST_CALL_JITALLOCATOR_FREE
        m_pAllocator->Free(pRanges);
        m_pAllocator->Free(pChunkPointers);
#endif
    }


#ifdef MUST_CALL_JITALLOCATOR_FREE
    m_pAllocator->Free(pTransitions);
#endif


lExitSuccess:;

    //-------------------------------------------------------------------
    // Update global stats
    //-------------------------------------------------------------------

#ifdef MEASURE_GCINFO
    if (slimHeader)
    {
        g_NumSlimHeaders++;
    }
    else
    {
        g_NumFatHeaders++;
    }

    m_CurrentMethodSize.NumMethods = 1;
#ifdef PARTIALLY_INTERRUPTIBLE_GC_SUPPORTED
    m_CurrentMethodSize.NumCallSites = m_NumCallSites;
#endif
    m_CurrentMethodSize.NumRanges = numInterruptibleRanges;
    m_CurrentMethodSize.NumRegs = numRegisters;
    m_CurrentMethodSize.NumStack = numStackSlots;
    m_CurrentMethodSize.NumUntracked = numUntrackedSlots;
    m_CurrentMethodSize.SizeOfCode = m_CodeLength;
    if(numInterruptibleRanges)
    {
        g_FiGcInfoSize += m_CurrentMethodSize;
        m_CurrentMethodSize.Log(LL_INFO100, "=== FullyInterruptible method breakdown ===\r\n");
        g_FiGcInfoSize.Log(LL_INFO10, "=== FullyInterruptible global breakdown ===\r\n");
    }
    else
    {
        g_PiGcInfoSize += m_CurrentMethodSize;
        m_CurrentMethodSize.Log(LL_INFO100, "=== PartiallyInterruptible method breakdown ===\r\n");
        g_PiGcInfoSize.Log(LL_INFO10, "=== PartiallyInterruptible global breakdown ===\r\n");
    }
    LogSpew(LF_GCINFO, LL_INFO10, "Total SlimHeaders: %zu\n", g_NumSlimHeaders);
    LogSpew(LF_GCINFO, LL_INFO10, "NumMethods: %zu\n", g_NumFatHeaders);
#endif
}

template <typename GcInfoEncoding> void TGcInfoEncoder<GcInfoEncoding>::SizeofSlotStateVarLengthVector(const BitArray &vector,
                                                   UINT32          baseSkip,
                                                   UINT32          baseRun,
                                                   UINT32         *pSizeofSimple,
                                                   UINT32         *pSizeofRLE,
                                                   UINT32         *pSizeofRLENeg)
{
    // Try 3 different encodings
    UINT32 sizeofSimple = 1;
    UINT32 sizeofRLE;
    UINT32 sizeofRLENeg;
    for(UINT32 i = 0; i < m_NumSlots && !m_SlotTable[i].IsUntracked(); i++)
    {
        if(!m_SlotTable[i].IsDeleted())
            sizeofSimple++;
    }

    if (sizeofSimple <= 2 + baseSkip + 1 + baseRun + 1)
    {
        // simple encoding is smaller than the smallest of the others
        // without even trying
        sizeofRLE = sizeofSimple + 1;
        sizeofRLENeg = sizeofSimple + 1;
    }
    else
    {
        sizeofRLE = 2; // For the header
        sizeofRLENeg = 2;

        UINT32 rleStart = 0;
        bool fPrev = false;
        UINT32 i;
        for(i = 0; i < m_NumSlots && !m_SlotTable[i].IsUntracked(); i++)
        {
            if(!m_SlotTable[i].IsDeleted())
            {
                if (vector.ReadBit(i))
                {
                    if (!fPrev)
                    {
                        // Skipping is done
                        sizeofRLE += BitStreamWriter::SizeofVarLengthUnsigned(i - rleStart, baseSkip);
                        sizeofRLENeg += BitStreamWriter::SizeofVarLengthUnsigned(i - rleStart, baseRun);
                        rleStart = i + 1;
                        fPrev = true;
                    }
                }
                else
                {
                    if (fPrev)
                    {
                        // Run is done
                        sizeofRLE += BitStreamWriter::SizeofVarLengthUnsigned(i - rleStart, baseRun);
                        sizeofRLENeg += BitStreamWriter::SizeofVarLengthUnsigned(i - rleStart, baseSkip);
                        rleStart = i + 1;
                        fPrev = false;
                    }
                }
            }
            else
            {
               rleStart++;
            }
        }

        _ASSERTE(i >= rleStart);
        sizeofRLE += BitStreamWriter::SizeofVarLengthUnsigned(i - rleStart, fPrev ? baseRun : baseSkip);
        sizeofRLENeg += BitStreamWriter::SizeofVarLengthUnsigned(i - rleStart, fPrev ? baseSkip : baseRun);
    }

    *pSizeofSimple = sizeofSimple;
    *pSizeofRLE = sizeofRLE;
    *pSizeofRLENeg = sizeofRLENeg;
}

template <typename GcInfoEncoding> UINT32 TGcInfoEncoder<GcInfoEncoding>::SizeofSlotStateVarLengthVector(const BitArray &vector,
                                                    UINT32          baseSkip,
                                                    UINT32          baseRun)
{
    // Try 3 different encodings
    UINT32 sizeofSimple;
    UINT32 sizeofRLE;
    UINT32 sizeofRLENeg;
    SizeofSlotStateVarLengthVector(vector, baseSkip, baseRun, &sizeofSimple, &sizeofRLE, &sizeofRLENeg);

    if (sizeofSimple <= sizeofRLE && sizeofSimple <= sizeofRLENeg)
        return sizeofSimple;
    if (sizeofRLE <= sizeofRLENeg)
        return sizeofRLE;
    return sizeofRLENeg;
}

template <typename GcInfoEncoding> UINT32 TGcInfoEncoder<GcInfoEncoding>::WriteSlotStateVarLengthVector(BitStreamWriter &writer,
                                                    const BitArray  &vector,
                                                    UINT32           baseSkip,
                                                    UINT32           baseRun)
{
    // Try 3 different encodings
    UINT32 sizeofSimple;
    UINT32 sizeofRLE;
    UINT32 sizeofRLENeg;
    SizeofSlotStateVarLengthVector(vector, baseSkip, baseRun, &sizeofSimple, &sizeofRLE, &sizeofRLENeg);
    UINT32 result;

#ifdef _DEBUG
    size_t initial = writer.GetBitCount();
#endif // _DEBUG

    if (sizeofSimple <= sizeofRLE && sizeofSimple <= sizeofRLENeg)
    {
        // Simple encoding is smallest
        writer.Write(0, 1);
        WriteSlotStateVector(writer, vector);
        result = sizeofSimple;
    }
    else
    {
        // One of the RLE encodings is the smallest
        writer.Write(1, 1);

        if (sizeofRLENeg < sizeofRLE)
        {
            writer.Write(1, 1);
            UINT32 swap = baseSkip;
            baseSkip = baseRun;
            baseRun = swap;
            result = sizeofRLENeg;
        }
        else
        {
            writer.Write(0, 1);
            result = sizeofRLE;
        }


        UINT32 rleStart = 0;
        UINT32 i;
        bool fPrev = false;
        for(i = 0; i < m_NumSlots && !m_SlotTable[i].IsUntracked(); i++)
        {
            if(!m_SlotTable[i].IsDeleted())
            {

                if (vector.ReadBit(i))
                {
                    if (!fPrev)
                    {
                        // Skipping is done
                        writer.EncodeVarLengthUnsigned(i - rleStart, baseSkip);
                        rleStart = i + 1;
                        fPrev = true;
                    }
                }
                else
                {
                    if (fPrev)
                    {
                        // Run is done
                        writer.EncodeVarLengthUnsigned(i - rleStart, baseRun);
                        rleStart = i + 1;
                        fPrev = false;
                    }
                }
            }
            else
            {
                rleStart++;
            }
        }

        _ASSERTE(i >= rleStart);
        writer.EncodeVarLengthUnsigned(i - rleStart, fPrev ? baseRun : baseSkip);
    }

#ifdef _DEBUG
    _ASSERTE(result + initial == writer.GetBitCount());
#endif // _DEBUG

    return result;
}


template <typename GcInfoEncoding> void TGcInfoEncoder<GcInfoEncoding>::EliminateRedundantLiveDeadPairs(LifetimeTransition** ppTransitions,
                                                    size_t* pNumTransitions,
                                                    LifetimeTransition** ppEndTransitions)
{
    LifetimeTransition* pTransitions = *ppTransitions;
    LifetimeTransition* pEndTransitions = *ppEndTransitions;

    LifetimeTransition* pNewTransitions = NULL;
    LifetimeTransition* pNewTransitionsCopyPtr = NULL;
    for (LifetimeTransition* pCurrent = pTransitions; pCurrent < pEndTransitions; pCurrent++)
    {
        // Is pCurrent the first of a dead/live pair?
        LifetimeTransition* pNext = pCurrent + 1;
        if (pNext < pEndTransitions &&
            pCurrent->CodeOffset == pNext->CodeOffset &&
            pCurrent->SlotId == pNext->SlotId &&
            pCurrent->IsDeleted == pNext->IsDeleted &&
            pCurrent->BecomesLive != pNext->BecomesLive)
        {
            // They are a pair we want to delete.  If this is the first pair we've encountered, allocate
            // the new array:
            if (pNewTransitions == NULL)
            {
                pNewTransitions = (LifetimeTransition*)m_pAllocator->Alloc((*pNumTransitions) * sizeof(LifetimeTransition));
                pNewTransitionsCopyPtr = pNewTransitions;
                // Copy from the start up to (but not including) pCurrent...
                for (LifetimeTransition* pCopyPtr = pTransitions; pCopyPtr < pCurrent; pCopyPtr++, pNewTransitionsCopyPtr++)
                    *pNewTransitionsCopyPtr = *pCopyPtr;
            }
            pCurrent++;
        }
        else
        {
            // pCurrent is not part of a pair.  If we're copying, copy.
            if (pNewTransitionsCopyPtr != NULL)
            {
                *pNewTransitionsCopyPtr++ = *pCurrent;
            }
        }
    }
    // If we deleted any pairs, substitute the new array for the old.
    if (pNewTransitions != NULL)
    {
        m_pAllocator->Free(pTransitions);
        *ppTransitions = pNewTransitions;
        assert(pNewTransitionsCopyPtr != NULL);
        *ppEndTransitions = pNewTransitionsCopyPtr;
        *pNumTransitions = (*ppEndTransitions) - (*ppTransitions);
    }
}

//
// Write encoded information to its final destination and frees temporary buffers.
// The encoder shouldn't be used anymore after calling this method.
//
template <typename GcInfoEncoding> BYTE* TGcInfoEncoder<GcInfoEncoding>::Emit()
{
    size_t cbGcInfoSize = m_Info1.GetByteCount() +
                          m_Info2.GetByteCount();

    LOG((LF_GCINFO, LL_INFO100, "GcInfoEncoder::Emit(): Size of GC info is %u bytes, code size %u bytes.\n", (unsigned)cbGcInfoSize, m_CodeLength ));

    BYTE* destBuffer = (BYTE *)eeAllocGCInfo(cbGcInfoSize);
    // Allocator will throw an exception on failure.
    // NOTE: the returned pointer may not be aligned during ngen.
    _ASSERTE( destBuffer );

    BYTE* ptr = destBuffer;

    m_Info1.CopyTo( ptr );
    ptr += m_Info1.GetByteCount();
    m_Info1.Dispose();

    m_Info2.CopyTo( ptr );
    ptr += m_Info2.GetByteCount();
    m_Info2.Dispose();

#ifdef MUST_CALL_JITALLOCATOR_FREE
    m_pAllocator->Free( m_SlotTable );
#endif

    return destBuffer;
}

template <typename GcInfoEncoding> void * TGcInfoEncoder<GcInfoEncoding>::eeAllocGCInfo (size_t        blockSize)
{
    m_BlockSize = blockSize;
    return m_pCorJitInfo->allocGCInfo(blockSize);
}

template <typename GcInfoEncoding> size_t TGcInfoEncoder<GcInfoEncoding>::GetEncodedGCInfoSize() const
{
    return m_BlockSize;
}

BitStreamWriter::BitStreamWriter( IAllocator* pAllocator )
{
    m_pAllocator = pAllocator;
    m_BitCount = 0;
#ifdef _DEBUG
    m_MemoryBlocksCount = 0;
#endif

    // Allocate memory blocks lazily
    m_OutOfBlockSlot = m_pCurrentSlot = (size_t*) NULL;
    m_FreeBitsInCurrentSlot = 0;
}

//
// bit 0 is the least significative bit
// The stream encodes the first come bit in the least significant bit of each byte
//
void BitStreamWriter::Write( size_t data, UINT32 count )
{
    _ASSERT(count <= BITS_PER_SIZE_T);

    if(count)
    {
        // Increment it now as we change count later on
        m_BitCount += count;

        if( count > m_FreeBitsInCurrentSlot )
        {
            if( m_FreeBitsInCurrentSlot > 0 )
            {
                _ASSERTE(m_FreeBitsInCurrentSlot < BITS_PER_SIZE_T);
                WriteInCurrentSlot( data, m_FreeBitsInCurrentSlot );
                count -= m_FreeBitsInCurrentSlot;
                data >>= m_FreeBitsInCurrentSlot;
            }

            _ASSERTE( count > 0 );

            // Initialize the next slot
            if( ++m_pCurrentSlot >= m_OutOfBlockSlot )
            {
                // Get a new memory block
                AllocMemoryBlock();
            }

            InitCurrentSlot();

            // Write the remainder
            WriteInCurrentSlot( data, count );
            m_FreeBitsInCurrentSlot -= count;
        }
        else
        {
            WriteInCurrentSlot( data, count );
            m_FreeBitsInCurrentSlot -= count;
            // if m_FreeBitsInCurrentSlot becomes 0 a nwe slot will initialized on the next request
        }
    }
}


void BitStreamWriter::CopyTo( BYTE* buffer )
{
    int i,c;
    BYTE* source = NULL;

    MemoryBlock* pMemBlock = m_MemoryBlocks.Head();
    if( pMemBlock == NULL )
        return;

    while (pMemBlock->Next() != NULL)
    {
        source = (BYTE*) pMemBlock->Contents;
        // @TODO: use memcpy instead
        for( i = 0; i < m_MemoryBlockSize; i++ )
        {
            *( buffer++ ) = *( source++ );
        }

        pMemBlock = pMemBlock->Next();
    }

    source = (BYTE*) pMemBlock->Contents;
    // The number of bytes to copy in the last block
    c = (int) ((BYTE*) ( m_pCurrentSlot + 1 ) - source - m_FreeBitsInCurrentSlot/8);
    _ASSERTE( c >= 0 );
    // @TODO: use memcpy instead
    for( i = 0; i < c; i++ )
    {
        *( buffer++ ) = *( source++ );
    }

}

void BitStreamWriter::Dispose()
{
    m_MemoryBlocks.Dispose(m_pAllocator);
}

int BitStreamWriter::SizeofVarLengthUnsigned( size_t n, UINT32 base)
{
    // If a value gets so big we are probably doing something wrong
    _ASSERTE(((INT32)(UINT32)n) >= 0);
    _ASSERTE((base > 0) && (base < BITS_PER_SIZE_T));
    size_t numEncodings = size_t{ 1 } << base;
    int bitsUsed;
    for(bitsUsed = base+1; ; bitsUsed += base+1)
    {
        if( n < numEncodings )
        {
            return bitsUsed;
        }
        else
        {
            n >>= base;
        }
    }
    return bitsUsed;
}

int BitStreamWriter::EncodeVarLengthUnsigned( size_t n, UINT32 base)
{
    // If a value gets so big we are probably doing something wrong
    _ASSERTE(((INT32)(UINT32)n) >= 0);
    _ASSERTE((base > 0) && (base < BITS_PER_SIZE_T));
    size_t numEncodings = size_t{ 1 } << base;
    int bitsUsed;
    for(bitsUsed = base+1; ; bitsUsed += base+1)
    {
        if( n < numEncodings )
        {
            Write( n, base+1 ); // This sets the extension bit to zero
            return bitsUsed;
        }
        else
        {
            size_t currentChunk = n & (numEncodings-1);
            Write( currentChunk | numEncodings, base+1 );
            n >>= base;
        }
    }
    return bitsUsed;
}

int BitStreamWriter::EncodeVarLengthSigned( SSIZE_T n, UINT32 base )
{
    _ASSERTE((base > 0) && (base < BITS_PER_SIZE_T));
    size_t numEncodings = size_t{ 1 } << base;
    for(int bitsUsed = base+1; ; bitsUsed += base+1)
    {
        size_t currentChunk = ((size_t) n) & (numEncodings-1);
        size_t topmostBit = currentChunk & (numEncodings >> 1);
        n >>= base; // signed arithmetic shift
        if((topmostBit && (n == (SSIZE_T)-1)) || (!topmostBit && (n == 0)))
        {
            // The topmost bit correctly represents the sign
            Write( currentChunk, base+1 ); // This sets the extension bit to zero
            return bitsUsed;
        }
        else
        {
            Write( currentChunk | numEncodings, base+1 );
        }
    }
}

// Instantiate the encoder so other files can use it
template class TGcInfoEncoder<TargetGcInfoEncoding>;
