// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
#if NET7_0_OR_GREATER
using System.Runtime.InteropServices.GeneratedMarshalling;
#endif

namespace System.Drawing
{
    internal static partial class SafeNativeMethods
    {
        internal static unsafe partial class Gdip
        {
            [GeneratedDllImport(LibraryName, EntryPoint = nameof(GdipGetPathWorldBounds))]
            private static partial int GdipGetPathWorldBounds_impl(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef path, out RectangleF gprectf,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef matrix, IntPtr pen);

            // Passing null SafeHandles in P/Invokes is not supported so we will do it the manual way.
            // TODO: Use a marshaller once it can be used on all frameworks.
            internal static int GdipGetPathWorldBounds(HandleRef path, out RectangleF gprectf, HandleRef matrix, SafePenHandle? penOptional)
            {
                bool releasePen = false;
                try
                {
                    IntPtr nativePen = IntPtr.Zero;
                    if (penOptional != null)
                    {
                        penOptional.DangerousAddRef(ref releasePen);
                        nativePen = penOptional.DangerousGetHandle();
                    }

                    return GdipGetPathWorldBounds_impl(path, out gprectf, matrix, nativePen);
                }
                finally
                {
                    if (releasePen)
                    {
                        penOptional!.DangerousRelease();
                    }
                }
            }
        }
    }
}
