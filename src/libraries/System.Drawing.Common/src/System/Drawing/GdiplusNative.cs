// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.Win32.SafeHandles;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace System.Drawing
{
    // Raw function imports for gdiplus
    // Functions are loaded manually in order to accomodate different shared library names on Unix.
    internal static partial class SafeNativeMethods
    {
        internal static unsafe partial class Gdip
        {
            // Shared function imports (all platforms)
            [DllImport(LibraryName)]
            internal static extern int GdipBeginContainer(SafeGraphicsHandle graphics, ref RectangleF dstRect, ref RectangleF srcRect, GraphicsUnit unit, out int state);

            [DllImport(LibraryName)]
            internal static extern int GdipBeginContainer2(SafeGraphicsHandle graphics, out int state);

            [DllImport(LibraryName)]
            internal static extern int GdipBeginContainerI(SafeGraphicsHandle graphics, ref Rectangle dstRect, ref Rectangle srcRect, GraphicsUnit unit, out int state);

            [DllImport(LibraryName)]
            internal static extern int GdipEndContainer(SafeGraphicsHandle graphics, int state);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipCreateAdjustableArrowCap(float height, float width, bool isFilled, out IntPtr adjustableArrowCap);

            [DllImport(LibraryName)]
            internal static extern int GdipGetAdjustableArrowCapHeight(HandleRef adjustableArrowCap, out float height);

            [DllImport(LibraryName)]
            internal static extern int GdipSetAdjustableArrowCapHeight(HandleRef adjustableArrowCap, float height);

            [DllImport(LibraryName)]
            internal static extern int GdipSetAdjustableArrowCapWidth(HandleRef adjustableArrowCap, float width);

            [DllImport(LibraryName)]
            internal static extern int GdipGetAdjustableArrowCapWidth(HandleRef adjustableArrowCap, out float width);

            [DllImport(LibraryName)]
            internal static extern int GdipSetAdjustableArrowCapMiddleInset(HandleRef adjustableArrowCap, float middleInset);

            [DllImport(LibraryName)]
            internal static extern int GdipGetAdjustableArrowCapMiddleInset(HandleRef adjustableArrowCap, out float middleInset);

            [DllImport(LibraryName)]
            internal static extern int GdipSetAdjustableArrowCapFillState(HandleRef adjustableArrowCap, bool fillState);

            [DllImport(LibraryName)]
            internal static extern int GdipGetAdjustableArrowCapFillState(HandleRef adjustableArrowCap, out bool fillState);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipGetCustomLineCapType(IntPtr customCap, out CustomLineCapType capType);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipDeleteCustomLineCap(IntPtr customCap);

            [DllImport(LibraryName)]
            internal static extern int GdipDeleteCustomLineCap(HandleRef customCap);

            [DllImport(LibraryName)]
            internal static extern int GdipCloneCustomLineCap(HandleRef customCap, out IntPtr clonedCap);

            [DllImport(LibraryName)]
            internal static extern int GdipSetCustomLineCapStrokeCaps(HandleRef customCap, LineCap startCap, LineCap endCap);

            [DllImport(LibraryName)]
            internal static extern int GdipGetCustomLineCapStrokeCaps(HandleRef customCap, out LineCap startCap, out LineCap endCap);

            [DllImport(LibraryName)]
            internal static extern int GdipSetCustomLineCapStrokeJoin(HandleRef customCap, LineJoin lineJoin);

            [DllImport(LibraryName)]
            internal static extern int GdipGetCustomLineCapStrokeJoin(HandleRef customCap, out LineJoin lineJoin);

            [DllImport(LibraryName)]
            internal static extern int GdipSetCustomLineCapBaseCap(HandleRef customCap, LineCap baseCap);

            [DllImport(LibraryName)]
            internal static extern int GdipGetCustomLineCapBaseCap(HandleRef customCap, out LineCap baseCap);

            [DllImport(LibraryName)]
            internal static extern int GdipSetCustomLineCapBaseInset(HandleRef customCap, float inset);

            [DllImport(LibraryName)]
            internal static extern int GdipGetCustomLineCapBaseInset(HandleRef customCap, out float inset);

            [DllImport(LibraryName)]
            internal static extern int GdipSetCustomLineCapWidthScale(HandleRef customCap, float widthScale);

            [DllImport(LibraryName)]
            internal static extern int GdipGetCustomLineCapWidthScale(HandleRef customCap, out float widthScale);

            [DllImport(LibraryName)]
            internal static extern int GdipDeletePathIter(HandleRef pathIter);

            [DllImport(LibraryName)]
            internal static extern int GdipPathIterNextSubpath(HandleRef pathIter, out int resultCount, out int startIndex, out int endIndex, out bool isClosed);

            [DllImport(LibraryName)]
            internal static extern int GdipPathIterNextPathType(HandleRef pathIter, out int resultCount, out byte pathType, out int startIndex, out int endIndex);

            [DllImport(LibraryName)]
            internal static extern int GdipPathIterNextMarker(HandleRef pathIter, out int resultCount, out int startIndex, out int endIndex);

            [DllImport(LibraryName)]
            internal static extern int GdipPathIterGetCount(HandleRef pathIter, out int count);

            [DllImport(LibraryName)]
            internal static extern int GdipPathIterGetSubpathCount(HandleRef pathIter, out int count);

            [DllImport(LibraryName)]
            internal static extern int GdipPathIterHasCurve(HandleRef pathIter, out bool hasCurve);

            [DllImport(LibraryName)]
            internal static extern int GdipPathIterRewind(HandleRef pathIter);

            [DllImport(LibraryName)]
            internal static extern int GdipPathIterEnumerate(HandleRef pathIter, out int resultCount, PointF* points, byte* types, int count);

            [DllImport(LibraryName)]
            internal static extern int GdipPathIterCopyData(HandleRef pathIter, out int resultCount, PointF* points, byte* types, int startIndex, int endIndex);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipCreateHatchBrush(int hatchstyle, int forecol, int backcol, out SafeBrushHandle brush);

            [DllImport(LibraryName)]
            internal static extern int GdipGetHatchStyle(SafeBrushHandle brush, out int hatchstyle);

            [DllImport(LibraryName)]
            internal static extern int GdipGetHatchForegroundColor(SafeBrushHandle brush, out int forecol);

            [DllImport(LibraryName)]
            internal static extern int GdipGetHatchBackgroundColor(SafeBrushHandle brush, out int backcol);

            [DllImport(LibraryName)]
            internal static extern int GdipCloneBrush(SafeBrushHandle brush, out SafeBrushHandle clonebrush);

            [DllImport(LibraryName)]
            internal static extern int GdipDeleteBrush(IntPtr brush);

#pragma warning disable DLLIMPORTGENANALYZER015 // Use 'GeneratedDllImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time
            // TODO: [DllImportGenerator] Switch to use GeneratedDllImport once we support blittable structs defined in other assemblies.
            [DllImport(LibraryName)]
            internal static extern int GdipCreateLineBrush(ref PointF point1, ref PointF point2, int color1, int color2, WrapMode wrapMode, out SafeBrushHandle lineGradient);

            [DllImport(LibraryName)]
            internal static extern int GdipCreateLineBrushI(ref Point point1, ref Point point2, int color1, int color2, WrapMode wrapMode, out SafeBrushHandle lineGradient);

            [DllImport(LibraryName)]
            internal static extern int GdipCreateLineBrushFromRect(ref RectangleF rect, int color1, int color2, LinearGradientMode lineGradientMode, WrapMode wrapMode, out SafeBrushHandle lineGradient);

            [DllImport(LibraryName)]
            internal static extern int GdipCreateLineBrushFromRectI(ref Rectangle rect, int color1, int color2, LinearGradientMode lineGradientMode, WrapMode wrapMode, out SafeBrushHandle lineGradient);

            [DllImport(LibraryName)]
            internal static extern int GdipCreateLineBrushFromRectWithAngle(ref RectangleF rect, int color1, int color2, float angle, bool isAngleScaleable, WrapMode wrapMode, out SafeBrushHandle lineGradient);

            [DllImport(LibraryName)]
            internal static extern int GdipCreateLineBrushFromRectWithAngleI(ref Rectangle rect, int color1, int color2, float angle, bool isAngleScaleable, WrapMode wrapMode, out SafeBrushHandle lineGradient);
#pragma warning restore DLLIMPORTGENANALYZER015 // Use 'GeneratedDllImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time

            [DllImport(LibraryName)]
            internal static extern int GdipSetLineColors(SafeBrushHandle brush, int color1, int color2);

            [DllImport(LibraryName)]
            internal static extern int GdipGetLineColors(SafeBrushHandle brush, int[] colors);

            [DllImport(LibraryName)]
            internal static extern int GdipGetLineRect(SafeBrushHandle brush, out RectangleF gprectf);

            [DllImport(LibraryName)]
            internal static extern int GdipGetLineGammaCorrection(SafeBrushHandle brush, out bool useGammaCorrection);

            [DllImport(LibraryName)]
            internal static extern int GdipSetLineGammaCorrection(SafeBrushHandle brush, bool useGammaCorrection);

            [DllImport(LibraryName)]
            internal static extern int GdipSetLineSigmaBlend(SafeBrushHandle brush, float focus, float scale);

            [DllImport(LibraryName)]
            internal static extern int GdipSetLineLinearBlend(SafeBrushHandle brush, float focus, float scale);

            [DllImport(LibraryName)]
            internal static extern int GdipGetLineBlendCount(SafeBrushHandle brush, out int count);

            [DllImport(LibraryName)]
            internal static extern int GdipGetLineBlend(SafeBrushHandle brush, IntPtr blend, IntPtr positions, int count);

            [DllImport(LibraryName)]
            internal static extern int GdipSetLineBlend(SafeBrushHandle brush, IntPtr blend, IntPtr positions, int count);

            [DllImport(LibraryName)]
            internal static extern int GdipGetLinePresetBlendCount(SafeBrushHandle brush, out int count);

            [DllImport(LibraryName)]
            internal static extern int GdipGetLinePresetBlend(SafeBrushHandle brush, IntPtr blend, IntPtr positions, int count);

            [DllImport(LibraryName)]
            internal static extern int GdipSetLinePresetBlend(SafeBrushHandle brush, IntPtr blend, IntPtr positions, int count);

            [DllImport(LibraryName)]
            internal static extern int GdipSetLineWrapMode(SafeBrushHandle brush, int wrapMode);

            [DllImport(LibraryName)]
            internal static extern int GdipGetLineWrapMode(SafeBrushHandle brush, out int wrapMode);

            [DllImport(LibraryName)]
            internal static extern int GdipResetLineTransform(SafeBrushHandle brush);

            [DllImport(LibraryName)]
            internal static extern int GdipMultiplyLineTransform(SafeBrushHandle brush, SafeMatrixHandle matrix, MatrixOrder order);

            [DllImport(LibraryName)]
            internal static extern int GdipGetLineTransform(SafeBrushHandle brush, SafeMatrixHandle matrix);

            [DllImport(LibraryName)]
            internal static extern int GdipSetLineTransform(SafeBrushHandle brush, SafeMatrixHandle matrix);

            [DllImport(LibraryName)]
            internal static extern int GdipTranslateLineTransform(SafeBrushHandle brush, float dx, float dy, MatrixOrder order);

            [DllImport(LibraryName)]
            internal static extern int GdipScaleLineTransform(SafeBrushHandle brush, float sx, float sy, MatrixOrder order);

            [DllImport(LibraryName)]
            internal static extern int GdipRotateLineTransform(SafeBrushHandle brush, float angle, MatrixOrder order);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipCreatePathGradient(PointF* points, int count, WrapMode wrapMode, out SafeBrushHandle brush);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipCreatePathGradientI(Point* points, int count, WrapMode wrapMode, out SafeBrushHandle brush);

            [DllImport(LibraryName)]
            internal static extern int GdipCreatePathGradientFromPath(SafeGraphicsPathHandle path, out SafeBrushHandle brush);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPathGradientCenterColor(SafeBrushHandle brush, out int color);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPathGradientCenterColor(SafeBrushHandle brush, int color);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPathGradientSurroundColorsWithCount(SafeBrushHandle brush, int[] color, ref int count);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPathGradientSurroundColorsWithCount(SafeBrushHandle brush, int[] argb, ref int count);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPathGradientCenterPoint(SafeBrushHandle brush, out PointF point);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPathGradientCenterPoint(SafeBrushHandle brush, ref PointF point);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPathGradientRect(SafeBrushHandle brush, out RectangleF gprectf);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPathGradientPointCount(SafeBrushHandle brush, out int count);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPathGradientSurroundColorCount(SafeBrushHandle brush, out int count);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPathGradientBlendCount(SafeBrushHandle brush, out int count);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPathGradientBlend(SafeBrushHandle brush, float[] blend, float[] positions, int count);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPathGradientBlend(SafeBrushHandle brush, IntPtr blend, IntPtr positions, int count);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPathGradientPresetBlendCount(SafeBrushHandle brush, out int count);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPathGradientPresetBlend(SafeBrushHandle brush, int[] blend, float[] positions, int count);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPathGradientPresetBlend(SafeBrushHandle brush, int[] blend, float[] positions, int count);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPathGradientSigmaBlend(SafeBrushHandle brush, float focus, float scale);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPathGradientLinearBlend(SafeBrushHandle brush, float focus, float scale);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPathGradientWrapMode(SafeBrushHandle brush, int wrapmode);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPathGradientWrapMode(SafeBrushHandle brush, out int wrapmode);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPathGradientTransform(SafeBrushHandle brush, SafeMatrixHandle matrix);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPathGradientTransform(SafeBrushHandle brush, SafeMatrixHandle matrix);

            [DllImport(LibraryName)]
            internal static extern int GdipResetPathGradientTransform(SafeBrushHandle brush);

            [DllImport(LibraryName)]
            internal static extern int GdipMultiplyPathGradientTransform(SafeBrushHandle brush, SafeMatrixHandle matrix, MatrixOrder order);

            [DllImport(LibraryName)]
            internal static extern int GdipTranslatePathGradientTransform(SafeBrushHandle brush, float dx, float dy, MatrixOrder order);

            [DllImport(LibraryName)]
            internal static extern int GdipScalePathGradientTransform(SafeBrushHandle brush, float sx, float sy, MatrixOrder order);

            [DllImport(LibraryName)]
            internal static extern int GdipRotatePathGradientTransform(SafeBrushHandle brush, float angle, MatrixOrder order);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPathGradientFocusScales(SafeBrushHandle brush, float[] xScale, float[] yScale);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPathGradientFocusScales(SafeBrushHandle brush, float xScale, float yScale);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipCreateImageAttributes(out IntPtr imageattr);

            [DllImport(LibraryName)]
            internal static extern int GdipCloneImageAttributes(HandleRef imageattr, out IntPtr cloneImageattr);

            [DllImport(LibraryName)]
            internal static extern int GdipDisposeImageAttributes(HandleRef imageattr);

            [DllImport(LibraryName)]
            internal static extern int GdipSetImageAttributesColorMatrix(HandleRef imageattr, ColorAdjustType type, bool enableFlag, ColorMatrix? colorMatrix, ColorMatrix? grayMatrix, ColorMatrixFlag flags);

            [DllImport(LibraryName)]
            internal static extern int GdipSetImageAttributesThreshold(HandleRef imageattr, ColorAdjustType type, bool enableFlag, float threshold);

            [DllImport(LibraryName)]
            internal static extern int GdipSetImageAttributesGamma(HandleRef imageattr, ColorAdjustType type, bool enableFlag, float gamma);

            [DllImport(LibraryName)]
            internal static extern int GdipSetImageAttributesNoOp(HandleRef imageattr, ColorAdjustType type, bool enableFlag);

            [DllImport(LibraryName)]
            internal static extern int GdipSetImageAttributesColorKeys(HandleRef imageattr, ColorAdjustType type, bool enableFlag, int colorLow, int colorHigh);

            [DllImport(LibraryName)]
            internal static extern int GdipSetImageAttributesOutputChannel(HandleRef imageattr, ColorAdjustType type, bool enableFlag, ColorChannelFlag flags);

            [DllImport(LibraryName, CharSet = CharSet.Unicode)]
            internal static extern int GdipSetImageAttributesOutputChannelColorProfile(HandleRef imageattr, ColorAdjustType type, bool enableFlag, string colorProfileFilename);

            [DllImport(LibraryName)]
            internal static extern int GdipSetImageAttributesRemapTable(HandleRef imageattr, ColorAdjustType type, bool enableFlag, int mapSize, IntPtr map);

            [DllImport(LibraryName)]
            internal static extern int GdipSetImageAttributesWrapMode(HandleRef imageattr, int wrapmode, int argb, bool clamp);

            [DllImport(LibraryName)]
            internal static extern int GdipGetImageAttributesAdjustedPalette(HandleRef imageattr, IntPtr palette, ColorAdjustType type);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipGetImageDecodersSize(out int numDecoders, out int size);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipGetImageDecoders(int numDecoders, int size, IntPtr decoders);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipGetImageEncodersSize(out int numEncoders, out int size);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipGetImageEncoders(int numEncoders, int size, IntPtr encoders);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipCreateSolidFill(int color, out SafeBrushHandle brush);

            [DllImport(LibraryName)]
            internal static extern int GdipSetSolidFillColor(SafeBrushHandle brush, int color);

            [DllImport(LibraryName)]
            internal static extern int GdipGetSolidFillColor(SafeBrushHandle brush, out int color);


            [DllImport(LibraryName)]
            internal static extern int GdipCreateTexture(HandleRef bitmap, int wrapmode, out SafeBrushHandle texture);

            [DllImport(LibraryName)]
            internal static extern int GdipCreateTexture2(HandleRef bitmap, int wrapmode, float x, float y, float width, float height, out SafeBrushHandle texture);

            [DllImport(LibraryName)]
            internal static extern int GdipCreateTextureIA(HandleRef bitmap, HandleRef imageAttrib, float x, float y, float width, float height, out SafeBrushHandle texture);

            [DllImport(LibraryName)]
            internal static extern int GdipCreateTexture2I(HandleRef bitmap, int wrapmode, int x, int y, int width, int height, out SafeBrushHandle texture);

            [DllImport(LibraryName)]
            internal static extern int GdipCreateTextureIAI(HandleRef bitmap, HandleRef imageAttrib, int x, int y, int width, int height, out SafeBrushHandle texture);

            [DllImport(LibraryName)]
            internal static extern int GdipSetTextureTransform(SafeBrushHandle brush, SafeMatrixHandle matrix);

            [DllImport(LibraryName)]
            internal static extern int GdipGetTextureTransform(SafeBrushHandle brush, SafeMatrixHandle matrix);

            [DllImport(LibraryName)]
            internal static extern int GdipResetTextureTransform(SafeBrushHandle brush);

            [DllImport(LibraryName)]
            internal static extern int GdipMultiplyTextureTransform(SafeBrushHandle brush, SafeMatrixHandle matrix, MatrixOrder order);

            [DllImport(LibraryName)]
            internal static extern int GdipTranslateTextureTransform(SafeBrushHandle brush, float dx, float dy, MatrixOrder order);

            [DllImport(LibraryName)]
            internal static extern int GdipScaleTextureTransform(SafeBrushHandle brush, float sx, float sy, MatrixOrder order);

            [DllImport(LibraryName)]
            internal static extern int GdipRotateTextureTransform(SafeBrushHandle brush, float angle, MatrixOrder order);

            [DllImport(LibraryName)]
            internal static extern int GdipSetTextureWrapMode(SafeBrushHandle brush, int wrapMode);

            [DllImport(LibraryName)]
            internal static extern int GdipGetTextureWrapMode(SafeBrushHandle brush, out int wrapMode);

            [DllImport(LibraryName)]
            internal static extern int GdipGetTextureImage(SafeBrushHandle brush, out IntPtr image);

            [DllImport(LibraryName)]
            internal static extern int GdipGetFontCollectionFamilyCount(HandleRef fontCollection, out int numFound);

            [DllImport(LibraryName)]
            internal static extern int GdipGetFontCollectionFamilyList(HandleRef fontCollection, int numSought, IntPtr[] gpfamilies, out int numFound);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipCloneFontFamily(IntPtr fontfamily, out IntPtr clonefontfamily);

            [DllImport(LibraryName, CharSet = CharSet.Unicode)]
            internal static extern int GdipCreateFontFamilyFromName(string name, HandleRef fontCollection, out IntPtr FontFamily);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipGetGenericFontFamilySansSerif(out IntPtr fontfamily);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipGetGenericFontFamilySerif(out IntPtr fontfamily);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipGetGenericFontFamilyMonospace(out IntPtr fontfamily);

            [DllImport(LibraryName)]
            internal static extern int GdipDeleteFontFamily(HandleRef fontFamily);

            [DllImport(LibraryName, CharSet = CharSet.Unicode)]
            internal static extern int GdipGetFamilyName(HandleRef family, char* name, int language);

            [DllImport(LibraryName)]
            internal static extern int GdipIsStyleAvailable(HandleRef family, FontStyle style, out int isStyleAvailable);

            [DllImport(LibraryName)]
            internal static extern int GdipGetEmHeight(HandleRef family, FontStyle style, out int EmHeight);

            [DllImport(LibraryName)]
            internal static extern int GdipGetCellAscent(HandleRef family, FontStyle style, out int CellAscent);

            [DllImport(LibraryName)]
            internal static extern int GdipGetCellDescent(HandleRef family, FontStyle style, out int CellDescent);

            [DllImport(LibraryName)]
            internal static extern int GdipGetLineSpacing(HandleRef family, FontStyle style, out int LineSpaceing);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipNewInstalledFontCollection(out IntPtr fontCollection);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipNewPrivateFontCollection(out IntPtr fontCollection);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipDeletePrivateFontCollection(ref IntPtr fontCollection);

            [DllImport(LibraryName, CharSet = CharSet.Unicode)]
            internal static extern int GdipPrivateAddFontFile(HandleRef fontCollection, string filename);

            [DllImport(LibraryName)]
            internal static extern int GdipPrivateAddMemoryFont(HandleRef fontCollection, IntPtr memory, int length);

            [DllImport(LibraryName)]
            internal static extern int GdipCreateFont(HandleRef fontFamily, float emSize, FontStyle style, GraphicsUnit unit, out IntPtr font);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipCreateFontFromDC(IntPtr hdc, ref IntPtr font);

            [DllImport(LibraryName)]
            internal static extern int GdipCloneFont(HandleRef font, out IntPtr cloneFont);

            [DllImport(LibraryName)]
            internal static extern int GdipDeleteFont(HandleRef font);

            [DllImport(LibraryName)]
            internal static extern int GdipGetFamily(HandleRef font, out IntPtr family);

            [DllImport(LibraryName)]
            internal static extern int GdipGetFontStyle(HandleRef font, out FontStyle style);

            [DllImport(LibraryName)]
            internal static extern int GdipGetFontSize(HandleRef font, out float size);

            [DllImport(LibraryName)]
            internal static extern int GdipGetFontHeight(HandleRef font, SafeGraphicsHandle graphics, out float size);

            [DllImport(LibraryName)]
            internal static extern int GdipGetFontHeightGivenDPI(HandleRef font, float dpi, out float size);

            [DllImport(LibraryName)]
            internal static extern int GdipGetFontUnit(HandleRef font, out GraphicsUnit unit);

            [DllImport(LibraryName)]
            internal static extern int GdipGetLogFontW(HandleRef font, SafeGraphicsHandle graphics, ref Interop.User32.LOGFONT lf);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipCreatePen1(int argb, float width, int unit, out SafePenHandle pen);

            [DllImport(LibraryName)]
            internal static extern int GdipCreatePen2(SafeBrushHandle brush, float width, int unit, out SafePenHandle pen);

            [DllImport(LibraryName)]
            internal static extern int GdipClonePen(SafePenHandle pen, out SafePenHandle clonedPen);

            [DllImport(LibraryName)]
            internal static extern int GdipDeletePen(IntPtr Pen);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPenMode(SafePenHandle pen, PenAlignment penAlign);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPenMode(SafePenHandle pen, out PenAlignment penAlign);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPenWidth(SafePenHandle pen, float width);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPenWidth(SafePenHandle pen, float[] width);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPenLineCap197819(SafePenHandle pen, int startCap, int endCap, int dashCap);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPenStartCap(SafePenHandle pen, int startCap);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPenEndCap(SafePenHandle pen, int endCap);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPenStartCap(SafePenHandle pen, out int startCap);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPenEndCap(SafePenHandle pen, out int endCap);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPenDashCap197819(SafePenHandle pen, out int dashCap);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPenDashCap197819(SafePenHandle pen, int dashCap);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPenLineJoin(SafePenHandle pen, int lineJoin);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPenLineJoin(SafePenHandle pen, out int lineJoin);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPenCustomStartCap(SafePenHandle pen, HandleRef customCap);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPenCustomStartCap(SafePenHandle pen, out IntPtr customCap);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPenCustomEndCap(SafePenHandle pen, HandleRef customCap);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPenCustomEndCap(SafePenHandle pen, out IntPtr customCap);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPenMiterLimit(SafePenHandle pen, float miterLimit);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPenMiterLimit(SafePenHandle pen, float[] miterLimit);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPenTransform(SafePenHandle pen, SafeMatrixHandle matrix);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPenTransform(SafePenHandle pen, SafeMatrixHandle matrix);

            [DllImport(LibraryName)]
            internal static extern int GdipResetPenTransform(SafePenHandle pen);

            [DllImport(LibraryName)]
            internal static extern int GdipMultiplyPenTransform(SafePenHandle pen, SafeMatrixHandle matrix, MatrixOrder order);

            [DllImport(LibraryName)]
            internal static extern int GdipTranslatePenTransform(SafePenHandle pen, float dx, float dy, MatrixOrder order);

            [DllImport(LibraryName)]
            internal static extern int GdipScalePenTransform(SafePenHandle pen, float sx, float sy, MatrixOrder order);

            [DllImport(LibraryName)]
            internal static extern int GdipRotatePenTransform(SafePenHandle pen, float angle, MatrixOrder order);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPenColor(SafePenHandle pen, int argb);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPenColor(SafePenHandle pen, out int argb);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPenBrushFill(SafePenHandle pen, SafeBrushHandle brush);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPenBrushFill(SafePenHandle pen, out SafeBrushHandle brush);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPenFillType(SafePenHandle pen, out int pentype);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPenDashStyle(SafePenHandle pen, out int dashstyle);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPenDashStyle(SafePenHandle pen, int dashstyle);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPenDashArray(SafePenHandle pen, HandleRef memorydash, int count);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPenDashOffset(SafePenHandle pen, float[] dashoffset);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPenDashOffset(SafePenHandle pen, float dashoffset);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPenDashCount(SafePenHandle pen, out int dashcount);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPenDashArray(SafePenHandle pen, float[] memorydash, int count);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPenCompoundCount(SafePenHandle pen, out int count);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPenCompoundArray(SafePenHandle pen, float[] array, int count);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPenCompoundArray(SafePenHandle pen, float[] array, int count);

            [DllImport(LibraryName)]
            internal static extern int GdipSetWorldTransform(SafeGraphicsHandle graphics, SafeMatrixHandle matrix);

            [DllImport(LibraryName)]
            internal static extern int GdipResetWorldTransform(SafeGraphicsHandle graphics);

            [DllImport(LibraryName)]
            internal static extern int GdipMultiplyWorldTransform(SafeGraphicsHandle graphics, SafeMatrixHandle matrix, MatrixOrder order);

            [DllImport(LibraryName)]
            internal static extern int GdipTranslateWorldTransform(SafeGraphicsHandle graphics, float dx, float dy, MatrixOrder order);

            [DllImport(LibraryName)]
            internal static extern int GdipScaleWorldTransform(SafeGraphicsHandle graphics, float sx, float sy, MatrixOrder order);

            [DllImport(LibraryName)]
            internal static extern int GdipRotateWorldTransform(SafeGraphicsHandle graphics, float angle, MatrixOrder order);

            [DllImport(LibraryName)]
            internal static extern int GdipGetWorldTransform(SafeGraphicsHandle graphics, SafeMatrixHandle matrix);

            [DllImport(LibraryName)]
            internal static extern int GdipSetCompositingMode(SafeGraphicsHandle graphics, CompositingMode compositingMode);

            [DllImport(LibraryName)]
            internal static extern int GdipSetTextRenderingHint(SafeGraphicsHandle graphics, TextRenderingHint textRenderingHint);

            [DllImport(LibraryName)]
            internal static extern int GdipSetTextContrast(SafeGraphicsHandle graphics, int textContrast);

            [DllImport(LibraryName)]
            internal static extern int GdipSetInterpolationMode(SafeGraphicsHandle graphics, InterpolationMode interpolationMode);

            [DllImport(LibraryName)]
            internal static extern int GdipGetCompositingMode(SafeGraphicsHandle graphics, out CompositingMode compositingMode);

            [DllImport(LibraryName)]
            internal static extern int GdipSetRenderingOrigin(SafeGraphicsHandle graphics, int x, int y);

            [DllImport(LibraryName)]
            internal static extern int GdipGetRenderingOrigin(SafeGraphicsHandle graphics, out int x, out int y);

            [DllImport(LibraryName)]
            internal static extern int GdipSetCompositingQuality(SafeGraphicsHandle graphics, CompositingQuality quality);

            [DllImport(LibraryName)]
            internal static extern int GdipGetCompositingQuality(SafeGraphicsHandle graphics, out CompositingQuality quality);

            [DllImport(LibraryName)]
            internal static extern int GdipSetSmoothingMode(SafeGraphicsHandle graphics, SmoothingMode smoothingMode);

            [DllImport(LibraryName)]
            internal static extern int GdipGetSmoothingMode(SafeGraphicsHandle graphics, out SmoothingMode smoothingMode);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPixelOffsetMode(SafeGraphicsHandle graphics, PixelOffsetMode pixelOffsetMode);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPixelOffsetMode(SafeGraphicsHandle graphics, out PixelOffsetMode pixelOffsetMode);

            [DllImport(LibraryName)]
            internal static extern int GdipGetTextRenderingHint(SafeGraphicsHandle graphics, out TextRenderingHint textRenderingHint);

            [DllImport(LibraryName)]
            internal static extern int GdipGetTextContrast(SafeGraphicsHandle graphics, out int textContrast);

            [DllImport(LibraryName)]
            internal static extern int GdipGetInterpolationMode(SafeGraphicsHandle graphics, out InterpolationMode interpolationMode);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPageUnit(SafeGraphicsHandle graphics, out GraphicsUnit unit);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPageScale(SafeGraphicsHandle graphics, out float scale);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPageUnit(SafeGraphicsHandle graphics, GraphicsUnit unit);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPageScale(SafeGraphicsHandle graphics, float scale);

            [DllImport(LibraryName)]
            internal static extern int GdipGetDpiX(SafeGraphicsHandle graphics, out float dpi);

            [DllImport(LibraryName)]
            internal static extern int GdipGetDpiY(SafeGraphicsHandle graphics, out float dpi);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipCreateMatrix(out SafeMatrixHandle matrix);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipCreateMatrix2(float m11, float m12, float m21, float m22, float dx, float dy, out SafeMatrixHandle matrix);

#pragma warning disable DLLIMPORTGENANALYZER015 // Use 'GeneratedDllImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time
            // TODO: [DllImportGenerator] Switch to use GeneratedDllImport once we support blittable structs defined in other assemblies.
            [DllImport(LibraryName)]
            internal static extern int GdipCreateMatrix3(ref RectangleF rect, PointF* dstplg, out SafeMatrixHandle matrix);

            [DllImport(LibraryName)]
            internal static extern int GdipCreateMatrix3I(ref Rectangle rect, Point* dstplg, out SafeMatrixHandle matrix);
#pragma warning restore DLLIMPORTGENANALYZER015 // Use 'GeneratedDllImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time

            [DllImport(LibraryName)]
            internal static extern int GdipCloneMatrix(SafeMatrixHandle matrix, out SafeMatrixHandle clonedMatrix);

            [DllImport(LibraryName)]
            internal static extern int GdipDeleteMatrix(IntPtr matrix);

            [DllImport(LibraryName)]
            internal static extern int GdipSetMatrixElements(SafeMatrixHandle matrix, float m11, float m12, float m21, float m22, float dx, float dy);

            [DllImport(LibraryName)]
            internal static extern int GdipMultiplyMatrix(SafeMatrixHandle matrix, SafeMatrixHandle matrix2, MatrixOrder order);

            [DllImport(LibraryName)]
            internal static extern int GdipTranslateMatrix(SafeMatrixHandle matrix, float offsetX, float offsetY, MatrixOrder order);

            [DllImport(LibraryName)]
            internal static extern int GdipScaleMatrix(SafeMatrixHandle matrix, float scaleX, float scaleY, MatrixOrder order);

            [DllImport(LibraryName)]
            internal static extern int GdipRotateMatrix(SafeMatrixHandle matrix, float angle, MatrixOrder order);

            [DllImport(LibraryName)]
            internal static extern int GdipShearMatrix(SafeMatrixHandle matrix, float shearX, float shearY, MatrixOrder order);

            [DllImport(LibraryName)]
            internal static extern int GdipInvertMatrix(SafeMatrixHandle matrix);

            [DllImport(LibraryName)]
            internal static extern int GdipTransformMatrixPoints(SafeMatrixHandle matrix, PointF* pts, int count);

            [DllImport(LibraryName)]
            internal static extern int GdipTransformMatrixPointsI(SafeMatrixHandle matrix, Point* pts, int count);

            [DllImport(LibraryName)]
            internal static extern int GdipVectorTransformMatrixPoints(SafeMatrixHandle matrix, PointF* pts, int count);

            [DllImport(LibraryName)]
            internal static extern int GdipVectorTransformMatrixPointsI(SafeMatrixHandle matrix, Point* pts, int count);

            [DllImport(LibraryName)]
            internal static extern unsafe int GdipGetMatrixElements(SafeMatrixHandle matrix, float* m);

            [DllImport(LibraryName)]
            internal static extern int GdipIsMatrixInvertible(SafeMatrixHandle matrix, out int boolean);

            [DllImport(LibraryName)]
            internal static extern int GdipIsMatrixIdentity(SafeMatrixHandle matrix, out int boolean);

            [DllImport(LibraryName)]
            internal static extern int GdipIsMatrixEqual(SafeMatrixHandle matrix, SafeMatrixHandle matrix2, out int boolean);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipCreateRegion(out SafeRegionHandle region);

#pragma warning disable DLLIMPORTGENANALYZER015 // Use 'GeneratedDllImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time
            // TODO: [DllImportGenerator] Switch to use GeneratedDllImport once we support blittable structs defined in other assemblies.
            [DllImport(LibraryName)]
            internal static extern int GdipCreateRegionRect(ref RectangleF gprectf, out SafeRegionHandle region);

            [DllImport(LibraryName)]
            internal static extern int GdipCreateRegionRectI(ref Rectangle gprect, out SafeRegionHandle region);
#pragma warning restore DLLIMPORTGENANALYZER015 // Use 'GeneratedDllImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time

            [DllImport(LibraryName)]
            internal static extern int GdipCreateRegionPath(SafeGraphicsPathHandle path, out SafeRegionHandle region);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipCreateRegionRgnData(byte[] rgndata, int size, out SafeRegionHandle region);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipCreateRegionHrgn(IntPtr hRgn, out SafeRegionHandle region);

            [DllImport(LibraryName)]
            internal static extern int GdipCloneRegion(SafeRegionHandle region, out SafeRegionHandle clonedRegion);

            [DllImport(LibraryName)]
            internal static extern int GdipDeleteRegion(IntPtr region);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipFillRegion(SafeGraphicsHandle graphics, SafeBrushHandle brush, SafeRegionHandle region);

            [DllImport(LibraryName)]
            internal static extern int GdipSetInfinite(SafeRegionHandle region);

            [DllImport(LibraryName)]
            internal static extern int GdipSetEmpty(SafeRegionHandle region);

            [DllImport(LibraryName)]
            internal static extern int GdipCombineRegionRect(SafeRegionHandle region, ref RectangleF gprectf, CombineMode mode);

            [DllImport(LibraryName)]
            internal static extern int GdipCombineRegionRectI(SafeRegionHandle region, ref Rectangle gprect, CombineMode mode);

            [DllImport(LibraryName)]
            internal static extern int GdipCombineRegionPath(SafeRegionHandle region, SafeGraphicsPathHandle path, CombineMode mode);

            [DllImport(LibraryName)]
            internal static extern int GdipCombineRegionRegion(SafeRegionHandle region, SafeRegionHandle region2, CombineMode mode);

            [DllImport(LibraryName)]
            internal static extern int GdipTranslateRegion(SafeRegionHandle region, float dx, float dy);

            [DllImport(LibraryName)]
            internal static extern int GdipTranslateRegionI(SafeRegionHandle region, int dx, int dy);

            [DllImport(LibraryName)]
            internal static extern int GdipTransformRegion(SafeRegionHandle region, SafeMatrixHandle matrix);

            [DllImport(LibraryName)]
            internal static extern int GdipGetRegionBounds(SafeRegionHandle region, SafeGraphicsHandle graphics, out RectangleF gprectf);

            [DllImport(LibraryName)]
            internal static extern int GdipGetRegionHRgn(SafeRegionHandle region, SafeGraphicsHandle graphics, out IntPtr hrgn);

            [DllImport(LibraryName)]
            internal static extern int GdipIsEmptyRegion(SafeRegionHandle region, SafeGraphicsHandle graphics, out int boolean);

            [DllImport(LibraryName)]
            internal static extern int GdipIsInfiniteRegion(SafeRegionHandle region, SafeGraphicsHandle graphics, out int boolean);

            [DllImport(LibraryName)]
            internal static extern int GdipIsEqualRegion(SafeRegionHandle region, SafeRegionHandle region2, SafeGraphicsHandle graphics, out int boolean);

            [DllImport(LibraryName)]
            internal static extern int GdipGetRegionDataSize(SafeRegionHandle region, out int bufferSize);

            [DllImport(LibraryName)]
            internal static extern int GdipGetRegionData(SafeRegionHandle region, byte[] regionData, int bufferSize, out int sizeFilled);

            [DllImport(LibraryName)]
            internal static extern int GdipIsVisibleRegionPoint(SafeRegionHandle region, float X, float Y, HandleRef graphics, out int boolean);

            [DllImport(LibraryName)]
            internal static extern int GdipIsVisibleRegionPointI(SafeRegionHandle region, int X, int Y, HandleRef graphics, out int boolean);

            [DllImport(LibraryName)]
            internal static extern int GdipIsVisibleRegionRect(SafeRegionHandle region, float X, float Y, float width, float height, HandleRef graphics, out int boolean);

            [DllImport(LibraryName)]
            internal static extern int GdipIsVisibleRegionRectI(SafeRegionHandle region, int X, int Y, int width, int height, HandleRef graphics, out int boolean);

            [DllImport(LibraryName)]
            internal static extern int GdipGetRegionScansCount(SafeRegionHandle region, out int count, SafeMatrixHandle matrix);

            [DllImport(LibraryName)]
            internal static extern int GdipGetRegionScans(SafeRegionHandle region, RectangleF* rects, out int count, SafeMatrixHandle matrix);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipCreateFromHDC(IntPtr hdc, out SafeGraphicsHandle graphics);

            [DllImport(LibraryName)]
            internal static extern int GdipSetClipGraphics(SafeGraphicsHandle graphics, SafeGraphicsHandle srcgraphics, CombineMode mode);

            [DllImport(LibraryName)]
            internal static extern int GdipSetClipRect(SafeGraphicsHandle graphics, float x, float y, float width, float height, CombineMode mode);

            [DllImport(LibraryName)]
            internal static extern int GdipSetClipRectI(SafeGraphicsHandle graphics, int x, int y, int width, int height, CombineMode mode);

            [DllImport(LibraryName)]
            internal static extern int GdipSetClipPath(SafeGraphicsHandle graphics, SafeGraphicsPathHandle path, CombineMode mode);

            [DllImport(LibraryName)]
            internal static extern int GdipSetClipRegion(SafeGraphicsHandle graphics, SafeRegionHandle region, CombineMode mode);

            [DllImport(LibraryName)]
            internal static extern int GdipResetClip(SafeGraphicsHandle graphics);

            [DllImport(LibraryName)]
            internal static extern int GdipTranslateClip(SafeGraphicsHandle graphics, float dx, float dy);

            [DllImport(LibraryName)]
            internal static extern int GdipGetClip(SafeGraphicsHandle graphics, SafeRegionHandle region);

            [DllImport(LibraryName)]
            internal static extern int GdipGetClipBounds(SafeGraphicsHandle graphics, out RectangleF rect);

            [DllImport(LibraryName)]
            internal static extern int GdipIsClipEmpty(SafeGraphicsHandle graphics, out bool result);

            [DllImport(LibraryName)]
            internal static extern int GdipGetVisibleClipBounds(SafeGraphicsHandle graphics, out RectangleF rect);

            [DllImport(LibraryName)]
            internal static extern int GdipIsVisibleClipEmpty(SafeGraphicsHandle graphics, out bool result);

            [DllImport(LibraryName)]
            internal static extern int GdipIsVisiblePoint(SafeGraphicsHandle graphics, float x, float y, out bool result);

            [DllImport(LibraryName)]
            internal static extern int GdipIsVisiblePointI(SafeGraphicsHandle graphics, int x, int y, out bool result);

            [DllImport(LibraryName)]
            internal static extern int GdipIsVisibleRect(SafeGraphicsHandle graphics, float x, float y, float width, float height, out bool result);

            [DllImport(LibraryName)]
            internal static extern int GdipIsVisibleRectI(SafeGraphicsHandle graphics, int x, int y, int width, int height, out bool result);

            [DllImport(LibraryName)]
            internal static extern int GdipFlush(SafeGraphicsHandle graphics, FlushIntention intention);

            [DllImport(LibraryName)]
            internal static extern int GdipGetDC(SafeGraphicsHandle graphics, out IntPtr hdc);

            [DllImport(LibraryName)]
            internal static extern int GdipSetStringFormatMeasurableCharacterRanges(HandleRef format, int rangeCount, [In] [Out] CharacterRange[] range);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipCreateStringFormat(StringFormatFlags options, int language, out IntPtr format);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipStringFormatGetGenericDefault(out IntPtr format);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipStringFormatGetGenericTypographic(out IntPtr format);

            [DllImport(LibraryName)]
            internal static extern int GdipDeleteStringFormat(HandleRef format);

            [DllImport(LibraryName)]
            internal static extern int GdipCloneStringFormat(HandleRef format, out IntPtr newFormat);

            [DllImport(LibraryName)]
            internal static extern int GdipSetStringFormatFlags(HandleRef format, StringFormatFlags options);

            [DllImport(LibraryName)]
            internal static extern int GdipGetStringFormatFlags(HandleRef format, out StringFormatFlags result);

            [DllImport(LibraryName)]
            internal static extern int GdipSetStringFormatAlign(HandleRef format, StringAlignment align);

            [DllImport(LibraryName)]
            internal static extern int GdipGetStringFormatAlign(HandleRef format, out StringAlignment align);

            [DllImport(LibraryName)]
            internal static extern int GdipSetStringFormatLineAlign(HandleRef format, StringAlignment align);

            [DllImport(LibraryName)]
            internal static extern int GdipGetStringFormatLineAlign(HandleRef format, out StringAlignment align);

            [DllImport(LibraryName)]
            internal static extern int GdipSetStringFormatHotkeyPrefix(HandleRef format, HotkeyPrefix hotkeyPrefix);

            [DllImport(LibraryName)]
            internal static extern int GdipGetStringFormatHotkeyPrefix(HandleRef format, out HotkeyPrefix hotkeyPrefix);

            [DllImport(LibraryName)]
            internal static extern int GdipSetStringFormatTabStops(HandleRef format, float firstTabOffset, int count, float[] tabStops);

            [DllImport(LibraryName)]
            internal static extern int GdipGetStringFormatTabStops(HandleRef format, int count, out float firstTabOffset, [In] [Out] float[] tabStops);

            [DllImport(LibraryName)]
            internal static extern int GdipGetStringFormatTabStopCount(HandleRef format, out int count);

            [DllImport(LibraryName)]
            internal static extern int GdipGetStringFormatMeasurableCharacterRangeCount(HandleRef format, out int count);

            [DllImport(LibraryName)]
            internal static extern int GdipSetStringFormatTrimming(HandleRef format, StringTrimming trimming);

            [DllImport(LibraryName)]
            internal static extern int GdipGetStringFormatTrimming(HandleRef format, out StringTrimming trimming);

            [DllImport(LibraryName)]
            internal static extern int GdipSetStringFormatDigitSubstitution(HandleRef format, int langID, StringDigitSubstitute sds);

            [DllImport(LibraryName)]
            internal static extern int GdipGetStringFormatDigitSubstitution(HandleRef format, out int langID, out StringDigitSubstitute sds);

            [DllImport(LibraryName)]
            internal static extern int GdipGetImageDimension(HandleRef image, out float width, out float height);

            [DllImport(LibraryName)]
            internal static extern int GdipGetImageWidth(HandleRef image, out int width);

            [DllImport(LibraryName)]
            internal static extern int GdipGetImageHeight(HandleRef image, out int height);

            [DllImport(LibraryName)]
            internal static extern int GdipGetImageHorizontalResolution(HandleRef image, out float horzRes);

            [DllImport(LibraryName)]
            internal static extern int GdipGetImageVerticalResolution(HandleRef image, out float vertRes);

            [DllImport(LibraryName)]
            internal static extern int GdipGetImageFlags(HandleRef image, out int flags);

            [DllImport(LibraryName)]
            internal static extern int GdipGetImageRawFormat(HandleRef image, ref Guid format);

            [DllImport(LibraryName)]
            internal static extern int GdipGetImagePixelFormat(HandleRef image, out PixelFormat format);

            [DllImport(LibraryName)]
            internal static extern int GdipImageGetFrameCount(HandleRef image, ref Guid dimensionID, out int count);

            [DllImport(LibraryName)]
            internal static extern int GdipImageSelectActiveFrame(HandleRef image, ref Guid dimensionID, int frameIndex);

            [DllImport(LibraryName)]
            internal static extern int GdipImageRotateFlip(HandleRef image, int rotateFlipType);

            [DllImport(LibraryName)]
            internal static extern int GdipGetAllPropertyItems(HandleRef image, uint totalBufferSize, uint numProperties, PropertyItemInternal* allItems);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPropertyCount(HandleRef image, out uint numOfProperty);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPropertyIdList(HandleRef image, uint numOfProperty, int* list);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPropertyItem(HandleRef image, int propid, uint propSize, PropertyItemInternal* buffer);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPropertyItemSize(HandleRef image, int propid, out uint size);

            [DllImport(LibraryName)]
            internal static extern int GdipGetPropertySize(HandleRef image, out uint totalBufferSize, out uint numProperties);

            [DllImport(LibraryName)]
            internal static extern int GdipRemovePropertyItem(HandleRef image, int propid);

            [DllImport(LibraryName)]
            internal static extern int GdipSetPropertyItem(HandleRef image, PropertyItemInternal* item);

            [DllImport(LibraryName)]
            internal static extern int GdipGetImageType(HandleRef image, out int type);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipGetImageType(IntPtr image, out int type);

            [DllImport(LibraryName)]
            internal static extern int GdipDisposeImage(HandleRef image);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipDisposeImage(IntPtr image);

            [GeneratedDllImport(LibraryName, CharSet = CharSet.Unicode, ExactSpelling = true)]
            internal static partial int GdipCreateBitmapFromFile(string filename, out IntPtr bitmap);

            [GeneratedDllImport(LibraryName, CharSet = CharSet.Unicode, ExactSpelling = true)]
            internal static partial int GdipCreateBitmapFromFileICM(string filename, out IntPtr bitmap);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipCreateBitmapFromScan0(int width, int height, int stride, int format, IntPtr scan0, out IntPtr bitmap);

            [DllImport(LibraryName)]
            internal static extern int GdipCreateBitmapFromGraphics(int width, int height, SafeGraphicsHandle graphics, out IntPtr bitmap);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipCreateBitmapFromHBITMAP(IntPtr hbitmap, IntPtr hpalette, out IntPtr bitmap);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipCreateBitmapFromHICON(IntPtr hicon, out IntPtr bitmap);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipCreateBitmapFromResource(IntPtr hresource, IntPtr name, out IntPtr bitmap);

            [DllImport(LibraryName)]
            internal static extern int GdipCreateHBITMAPFromBitmap(HandleRef nativeBitmap, out IntPtr hbitmap, int argbBackground);

            [DllImport(LibraryName)]
            internal static extern int GdipCreateHICONFromBitmap(HandleRef nativeBitmap, out IntPtr hicon);

            [DllImport(LibraryName)]
            internal static extern int GdipCloneBitmapArea(float x, float y, float width, float height, int format, HandleRef srcbitmap, out IntPtr dstbitmap);

            [DllImport(LibraryName)]
            internal static extern int GdipCloneBitmapAreaI(int x, int y, int width, int height, int format, HandleRef srcbitmap, out IntPtr dstbitmap);

            [DllImport(LibraryName)]
            internal static extern int GdipBitmapLockBits(HandleRef bitmap, ref Rectangle rect, ImageLockMode flags, PixelFormat format, [In] [Out] BitmapData lockedBitmapData);

            [DllImport(LibraryName)]
            internal static extern int GdipBitmapUnlockBits(HandleRef bitmap, BitmapData lockedBitmapData);

            [DllImport(LibraryName)]
            internal static extern int GdipBitmapGetPixel(HandleRef bitmap, int x, int y, out int argb);

            [DllImport(LibraryName)]
            internal static extern int GdipBitmapSetPixel(HandleRef bitmap, int x, int y, int argb);

            [DllImport(LibraryName)]
            internal static extern int GdipBitmapSetResolution(HandleRef bitmap, float dpix, float dpiy);

            [DllImport(LibraryName)]
            internal static extern int GdipImageGetFrameDimensionsCount(HandleRef image, out int count);

            [DllImport(LibraryName)]
            internal static extern int GdipImageGetFrameDimensionsList(HandleRef image, Guid* dimensionIDs, int count);

            [GeneratedDllImport(LibraryName)]
            internal static partial int GdipCreateMetafileFromEmf(IntPtr hEnhMetafile, bool deleteEmf, out IntPtr metafile);

#pragma warning disable DLLIMPORTGENANALYZER015 // Use 'GeneratedDllImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time
            // TODO: [DllImportGenerator] Switch to use GeneratedDllImport once we support non-blittable structs.
            [DllImport(LibraryName)]
            internal static extern int GdipCreateMetafileFromWmf(IntPtr hMetafile, bool deleteWmf, WmfPlaceableFileHeader wmfplacealbeHeader, out IntPtr metafile);
#pragma warning restore DLLIMPORTGENANALYZER015 // Use 'GeneratedDllImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time

            [GeneratedDllImport(LibraryName, CharSet = CharSet.Unicode, ExactSpelling = true)]
            internal static partial int GdipCreateMetafileFromFile(string file, out IntPtr metafile);

            [GeneratedDllImport(LibraryName, CharSet = CharSet.Unicode, ExactSpelling = true)]
            internal static partial int GdipRecordMetafile(IntPtr referenceHdc, EmfType emfType, IntPtr pframeRect, MetafileFrameUnit frameUnit, string? description, out IntPtr metafile);

#pragma warning disable DLLIMPORTGENANALYZER015 // Use 'GeneratedDllImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time
            // TODO: [DllImportGenerator] Switch to use GeneratedDllImport once we support blittable structs defined in other assemblies.
            [DllImport(LibraryName, CharSet = CharSet.Unicode, ExactSpelling = true)]
            internal static extern int GdipRecordMetafile(IntPtr referenceHdc, EmfType emfType, ref RectangleF frameRect, MetafileFrameUnit frameUnit, string? description, out IntPtr metafile);

            [DllImport(LibraryName, CharSet = CharSet.Unicode, ExactSpelling = true)]
            internal static extern int GdipRecordMetafileI(IntPtr referenceHdc, EmfType emfType, ref Rectangle frameRect, MetafileFrameUnit frameUnit, string? description, out IntPtr metafile);

            [DllImport(LibraryName, CharSet = CharSet.Unicode, ExactSpelling = true)]
            internal static extern int GdipRecordMetafileFileName(string fileName, IntPtr referenceHdc, EmfType emfType, ref RectangleF frameRect, MetafileFrameUnit frameUnit, string? description, out IntPtr metafile);
#pragma warning restore DLLIMPORTGENANALYZER015 // Use 'GeneratedDllImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time

            [GeneratedDllImport(LibraryName, CharSet = CharSet.Unicode, ExactSpelling = true)]
            internal static partial int GdipRecordMetafileFileName(string fileName, IntPtr referenceHdc, EmfType emfType, IntPtr pframeRect, MetafileFrameUnit frameUnit, string? description, out IntPtr metafile);

#pragma warning disable DLLIMPORTGENANALYZER015 // Use 'GeneratedDllImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time
            // TODO: [DllImportGenerator] Switch to use GeneratedDllImport once we support blittable structs defined in other assemblies.
            [DllImport(LibraryName, CharSet = CharSet.Unicode, ExactSpelling = true)]
            internal static extern int GdipRecordMetafileFileNameI(string fileName, IntPtr referenceHdc, EmfType emfType, ref Rectangle frameRect, MetafileFrameUnit frameUnit, string? description, out IntPtr metafile);
#pragma warning restore DLLIMPORTGENANALYZER015 // Use 'GeneratedDllImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time

            [DllImport(LibraryName)]
            internal static extern int GdipPlayMetafileRecord(HandleRef metafile, EmfPlusRecordType recordType, int flags, int dataSize, byte[] data);

            [DllImport(LibraryName)]
            internal static extern int GdipSaveGraphics(SafeGraphicsHandle graphics, out int state);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawArc(SafeGraphicsHandle graphics, SafePenHandle pen, float x, float y, float width, float height, float startAngle, float sweepAngle);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawArcI(SafeGraphicsHandle graphics, SafePenHandle pen, int x, int y, int width, int height, float startAngle, float sweepAngle);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawLinesI(SafeGraphicsHandle graphics, SafePenHandle pen, Point* points, int count);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawBezier(SafeGraphicsHandle graphics, SafePenHandle pen, float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawEllipse(SafeGraphicsHandle graphics, SafePenHandle pen, float x, float y, float width, float height);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawEllipseI(SafeGraphicsHandle graphics, SafePenHandle pen, int x, int y, int width, int height);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawLine(SafeGraphicsHandle graphics, SafePenHandle pen, float x1, float y1, float x2, float y2);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawLineI(SafeGraphicsHandle graphics, SafePenHandle pen, int x1, int y1, int x2, int y2);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawLines(SafeGraphicsHandle graphics, SafePenHandle pen, PointF* points, int count);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawPath(SafeGraphicsHandle graphics, SafePenHandle pen, SafeGraphicsPathHandle path);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawPie(SafeGraphicsHandle graphics, SafePenHandle pen, float x, float y, float width, float height, float startAngle, float sweepAngle);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawPieI(SafeGraphicsHandle graphics, SafePenHandle pen, int x, int y, int width, int height, float startAngle, float sweepAngle);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawPolygon(SafeGraphicsHandle graphics, SafePenHandle pen, PointF* points, int count);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawPolygonI(SafeGraphicsHandle graphics, SafePenHandle pen, Point* points, int count);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipFillEllipse(SafeGraphicsHandle graphics, SafeBrushHandle brush, float x, float y, float width, float height);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipFillEllipseI(SafeGraphicsHandle graphics, SafeBrushHandle brush, int x, int y, int width, int height);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipFillPolygon(SafeGraphicsHandle graphics, SafeBrushHandle brush, PointF* points, int count, FillMode brushMode);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipFillPolygonI(SafeGraphicsHandle graphics, SafeBrushHandle brush, Point* points, int count, FillMode brushMode);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipFillRectangle(SafeGraphicsHandle graphics, SafeBrushHandle brush, float x, float y, float width, float height);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipFillRectangleI(SafeGraphicsHandle graphics, SafeBrushHandle brush, int x, int y, int width, int height);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipFillRectangles(SafeGraphicsHandle graphics, SafeBrushHandle brush, RectangleF* rects, int count);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipFillRectanglesI(SafeGraphicsHandle graphics, SafeBrushHandle brush, Rectangle* rects, int count);

            [DllImport(LibraryName, CharSet = CharSet.Unicode, SetLastError = true)]
            internal static extern int GdipDrawString(SafeGraphicsHandle graphics, string textString, int length, HandleRef font, ref RectangleF layoutRect, HandleRef stringFormat, SafeBrushHandle brush);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawImageRectI(SafeGraphicsHandle graphics, HandleRef image, int x, int y, int width, int height);

            [DllImport(LibraryName)]
            internal static extern int GdipGraphicsClear(SafeGraphicsHandle graphics, int argb);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawClosedCurve(SafeGraphicsHandle graphics, SafePenHandle pen, PointF* points, int count);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawClosedCurveI(SafeGraphicsHandle graphics, SafePenHandle pen, Point* points, int count);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawClosedCurve2(SafeGraphicsHandle graphics, SafePenHandle pen, PointF* points, int count, float tension);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawClosedCurve2I(SafeGraphicsHandle graphics, SafePenHandle pen, Point* points, int count, float tension);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawCurve(SafeGraphicsHandle graphics, SafePenHandle pen, PointF* points, int count);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawCurveI(SafeGraphicsHandle graphics, SafePenHandle pen, Point* points, int count);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawCurve2(SafeGraphicsHandle graphics, SafePenHandle pen, PointF* points, int count, float tension);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawCurve2I(SafeGraphicsHandle graphics, SafePenHandle pen, Point* points, int count, float tension);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawCurve3(SafeGraphicsHandle graphics, SafePenHandle pen, PointF* points, int count, int offset, int numberOfSegments, float tension);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawCurve3I(SafeGraphicsHandle graphics, SafePenHandle pen, Point* points, int count, int offset, int numberOfSegments, float tension);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipFillClosedCurve(SafeGraphicsHandle graphics, SafeBrushHandle brush, PointF* points, int count);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipFillClosedCurveI(SafeGraphicsHandle graphics, SafeBrushHandle brush, Point* points, int count);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipFillClosedCurve2(SafeGraphicsHandle graphics, SafeBrushHandle brush, PointF* points, int count, float tension, FillMode mode);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipFillClosedCurve2I(SafeGraphicsHandle graphics, SafeBrushHandle brush, Point* points, int count, float tension, FillMode mode);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipFillPie(SafeGraphicsHandle graphics, SafeBrushHandle brush, float x, float y, float width, float height, float startAngle, float sweepAngle);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipFillPieI(SafeGraphicsHandle graphics, SafeBrushHandle brush, int x, int y, int width, int height, float startAngle, float sweepAngle);

            [DllImport(LibraryName, CharSet = CharSet.Unicode)]
            internal static extern int GdipMeasureString(SafeGraphicsHandle graphics, string textString, int length, HandleRef font, ref RectangleF layoutRect, HandleRef stringFormat, ref RectangleF boundingBox, out int codepointsFitted, out int linesFilled);

            [DllImport(LibraryName, CharSet = CharSet.Unicode)]
            internal static extern int GdipMeasureCharacterRanges(SafeGraphicsHandle graphics, string textString, int length, HandleRef font, ref RectangleF layoutRect, HandleRef stringFormat, int characterCount, [In] [Out] IntPtr[] regions);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawImageI(SafeGraphicsHandle graphics, HandleRef image, int x, int y);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawImage(SafeGraphicsHandle graphics, HandleRef image, float x, float y);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawImagePoints(SafeGraphicsHandle graphics, HandleRef image, PointF* points, int count);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawImagePointsI(SafeGraphicsHandle graphics, HandleRef image, Point* points, int count);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawImageRectRectI(SafeGraphicsHandle graphics, HandleRef image, int dstx, int dsty, int dstwidth, int dstheight, int srcx, int srcy, int srcwidth, int srcheight, GraphicsUnit srcunit, HandleRef imageAttributes, Graphics.DrawImageAbort? callback, HandleRef callbackdata);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawImagePointsRect(SafeGraphicsHandle graphics, HandleRef image, PointF* points, int count, float srcx, float srcy, float srcwidth, float srcheight, GraphicsUnit srcunit, HandleRef imageAttributes, Graphics.DrawImageAbort? callback, HandleRef callbackdata);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawImageRectRect(SafeGraphicsHandle graphics, HandleRef image, float dstx, float dsty, float dstwidth, float dstheight, float srcx, float srcy, float srcwidth, float srcheight, GraphicsUnit srcunit, HandleRef imageAttributes, Graphics.DrawImageAbort? callback, HandleRef callbackdata);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawImagePointsRectI(SafeGraphicsHandle graphics, HandleRef image, Point* points, int count, int srcx, int srcy, int srcwidth, int srcheight, GraphicsUnit srcunit, HandleRef imageAttributes, Graphics.DrawImageAbort? callback, HandleRef callbackdata);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawImageRect(SafeGraphicsHandle graphics, HandleRef image, float x, float y, float width, float height);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawImagePointRect(SafeGraphicsHandle graphics, HandleRef image, float x, float y, float srcx, float srcy, float srcwidth, float srcheight, int srcunit);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawImagePointRectI(SafeGraphicsHandle graphics, HandleRef image, int x, int y, int srcx, int srcy, int srcwidth, int srcheight, int srcunit);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawRectangle(SafeGraphicsHandle graphics, SafePenHandle pen, float x, float y, float width, float height);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawRectangleI(SafeGraphicsHandle graphics, SafePenHandle pen, int x, int y, int width, int height);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawRectangles(SafeGraphicsHandle graphics, SafePenHandle pen, RectangleF* rects, int count);

            [DllImport(LibraryName, SetLastError = true)]
            internal static extern int GdipDrawRectanglesI(SafeGraphicsHandle graphics, SafePenHandle pen, Rectangle* rects, int count);

            [DllImport(LibraryName)]
            internal static extern int GdipTransformPoints(SafeGraphicsHandle graphics, int destSpace, int srcSpace, PointF* points, int count);

            [DllImport(LibraryName)]
            internal static extern int GdipTransformPointsI(SafeGraphicsHandle graphics, int destSpace, int srcSpace, Point* points, int count);

            [GeneratedDllImport(LibraryName, CharSet = CharSet.Unicode, ExactSpelling = true)]
            internal static partial int GdipLoadImageFromFileICM(string filename, out IntPtr image);

            [GeneratedDllImport(LibraryName, CharSet = CharSet.Unicode, ExactSpelling = true)]
            internal static partial int GdipLoadImageFromFile(string filename, out IntPtr image);

            [DllImport(LibraryName)]
            internal static extern int GdipGetEncoderParameterListSize(HandleRef image, ref Guid encoder, out int size);

            [DllImport(LibraryName)]
            internal static extern int GdipGetEncoderParameterList(HandleRef image, ref Guid encoder, int size, IntPtr buffer);
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct StartupInput
        {
            public int GdiplusVersion;             // Must be 1

            // public DebugEventProc DebugEventCallback; // Ignored on free builds
            public IntPtr DebugEventCallback;

            public bool SuppressBackgroundThread;     // FALSE unless you're prepared to call
                                                      // the hook/unhook functions properly

            public bool SuppressExternalCodecs;       // FALSE unless you want GDI+ only to use
                                                      // its internal image codecs.

            public static StartupInput GetDefault()
            {
                OperatingSystem os = Environment.OSVersion;
                StartupInput result = default;

                // In Windows 7 GDI+1.1 story is different as there are different binaries per GDI+ version.
                bool isWindows7 = os.Platform == PlatformID.Win32NT && os.Version.Major == 6 && os.Version.Minor == 1;
                result.GdiplusVersion = isWindows7 ? 1 : 2;
                // result.DebugEventCallback = null;
                result.SuppressBackgroundThread = false;
                result.SuppressExternalCodecs = false;
                return result;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct StartupOutput
        {
            // The following 2 fields won't be used.  They were originally intended
            // for getting GDI+ to run on our thread - however there are marshalling
            // dealing with function *'s and what not - so we make explicit calls
            // to gdi+ after the fact, via the GdiplusNotificationHook and
            // GdiplusNotificationUnhook methods.
            public IntPtr hook; //not used
            public IntPtr unhook; //not used.
        }
    }
}
