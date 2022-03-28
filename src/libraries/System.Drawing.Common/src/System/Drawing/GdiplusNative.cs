// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.Win32.SafeHandles;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Internal;
using System.Drawing.Text;
using System.Runtime.InteropServices;
#if NET7_0_OR_GREATER
using System.Runtime.InteropServices.GeneratedMarshalling;
#endif

namespace System.Drawing
{
    // Raw function imports for gdiplus
    internal static partial class SafeNativeMethods
    {
        internal static unsafe partial class Gdip
        {
            private const string LibraryName = "gdiplus.dll";

            // Imported functions
            [LibraryImport(LibraryName)]
            private static partial int GdiplusStartup(out IntPtr token, in StartupInputEx input, out StartupOutput output);

            [LibraryImport(LibraryName)]
            internal static partial int GdipBeginContainer(SafeGraphicsHandle graphics, ref RectangleF dstRect, ref RectangleF srcRect, GraphicsUnit unit, out int state);

            [LibraryImport(LibraryName)]
            internal static partial int GdipBeginContainer2(SafeGraphicsHandle graphics, out int state);

            [LibraryImport(LibraryName)]
            internal static partial int GdipBeginContainerI(SafeGraphicsHandle graphics, ref Rectangle dstRect, ref Rectangle srcRect, GraphicsUnit unit, out int state);

            [LibraryImport(LibraryName)]
            internal static partial int GdipEndContainer(SafeGraphicsHandle graphics, int state);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateAdjustableArrowCap(float height, float width, [MarshalAs(UnmanagedType.Bool)] bool isFilled, out IntPtr adjustableArrowCap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetAdjustableArrowCapHeight(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef adjustableArrowCap, out float height);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetAdjustableArrowCapHeight(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef adjustableArrowCap, float height);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetAdjustableArrowCapWidth(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef adjustableArrowCap, float width);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetAdjustableArrowCapWidth(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef adjustableArrowCap, out float width);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetAdjustableArrowCapMiddleInset(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef adjustableArrowCap, float middleInset);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetAdjustableArrowCapMiddleInset(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef adjustableArrowCap, out float middleInset);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetAdjustableArrowCapFillState(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef adjustableArrowCap, [MarshalAs(UnmanagedType.Bool)] bool fillState);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetAdjustableArrowCapFillState(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef adjustableArrowCap, [MarshalAs(UnmanagedType.Bool)] out bool fillState);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetCustomLineCapType(IntPtr customCap, out CustomLineCapType capType);

            [LibraryImport(LibraryName)]
            internal static partial int GdipDeleteCustomLineCap(IntPtr customCap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipDeleteCustomLineCap(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef customCap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCloneCustomLineCap(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef customCap, out IntPtr clonedCap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetCustomLineCapStrokeCaps(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef customCap, LineCap startCap, LineCap endCap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetCustomLineCapStrokeCaps(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef customCap, out LineCap startCap, out LineCap endCap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetCustomLineCapStrokeJoin(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef customCap, LineJoin lineJoin);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetCustomLineCapStrokeJoin(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef customCap, out LineJoin lineJoin);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetCustomLineCapBaseCap(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef customCap, LineCap baseCap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetCustomLineCapBaseCap(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef customCap, out LineCap baseCap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetCustomLineCapBaseInset(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef customCap, float inset);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetCustomLineCapBaseInset(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef customCap, out float inset);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetCustomLineCapWidthScale(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef customCap, float widthScale);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetCustomLineCapWidthScale(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef customCap, out float widthScale);

            [LibraryImport(LibraryName)]
            internal static partial int GdipDeletePathIter(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef pathIter);

            [LibraryImport(LibraryName)]
            internal static partial int GdipPathIterNextSubpath(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef pathIter, out int resultCount, out int startIndex, out int endIndex, [MarshalAs(UnmanagedType.Bool)] out bool isClosed);

            [LibraryImport(LibraryName)]
            internal static partial int GdipPathIterNextSubpathPath(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef pathIter, out int resultCount, SafeGraphicsPathHandle path, out bool isClosed);

            [LibraryImport(LibraryName)]
            internal static partial int GdipPathIterNextPathType(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef pathIter, out int resultCount, out byte pathType, out int startIndex, out int endIndex);

            [LibraryImport(LibraryName)]
            internal static partial int GdipPathIterNextMarker(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef pathIter, out int resultCount, out int startIndex, out int endIndex);

            [LibraryImport(LibraryName)]
            internal static partial int GdipPathIterNextMarkerPath(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef pathIter, out int resultCount, SafeGraphicsPathHandle path);

            [LibraryImport(LibraryName)]
            internal static partial int GdipPathIterGetCount(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef pathIter, out int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipPathIterGetSubpathCount(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef pathIter, out int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipPathIterHasCurve(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef pathIter, [MarshalAs(UnmanagedType.Bool)] out bool hasCurve);

            [LibraryImport(LibraryName)]
            internal static partial int GdipPathIterRewind(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef pathIter);

            [LibraryImport(LibraryName)]
            internal static partial int GdipPathIterEnumerate(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef pathIter, out int resultCount, PointF* points, byte* types, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipPathIterCopyData(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef pathIter, out int resultCount, PointF* points, byte* types, int startIndex, int endIndex);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateHatchBrush(int hatchstyle, int forecol, int backcol, out SafeBrushHandle brush);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetHatchStyle(SafeBrushHandle brush, out int hatchstyle);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetHatchForegroundColor(SafeBrushHandle brush, out int forecol);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetHatchBackgroundColor(SafeBrushHandle brush, out int backcol);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCloneBrush(SafeBrushHandle brush, out SafeBrushHandle clonebrush);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateLineBrush(ref PointF point1, ref PointF point2, int color1, int color2, WrapMode wrapMode, out SafeBrushHandle lineGradient);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateLineBrushI(ref Point point1, ref Point point2, int color1, int color2, WrapMode wrapMode, out SafeBrushHandle lineGradient);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateLineBrushFromRect(ref RectangleF rect, int color1, int color2, LinearGradientMode lineGradientMode, WrapMode wrapMode, out SafeBrushHandle lineGradient);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateLineBrushFromRectI(ref Rectangle rect, int color1, int color2, LinearGradientMode lineGradientMode, WrapMode wrapMode, out SafeBrushHandle lineGradient);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateLineBrushFromRectWithAngle(ref RectangleF rect, int color1, int color2, float angle, bool isAngleScaleable, WrapMode wrapMode, out SafeBrushHandle lineGradient);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateLineBrushFromRectWithAngleI(ref Rectangle rect, int color1, int color2, float angle, bool isAngleScaleable, WrapMode wrapMode, out SafeBrushHandle lineGradient);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetLineColors(SafeBrushHandle brush, int color1, int color2);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetLineColors(SafeBrushHandle brush, int[] colors);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetLineRect(SafeBrushHandle brush, out RectangleF gprectf);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetLineGammaCorrection(SafeBrushHandle brush, out bool useGammaCorrection);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetLineGammaCorrection(SafeBrushHandle brush, bool useGammaCorrection);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetLineSigmaBlend(SafeBrushHandle brush, float focus, float scale);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetLineLinearBlend(SafeBrushHandle brush, float focus, float scale);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetLineBlendCount(SafeBrushHandle brush, out int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetLineBlend(SafeBrushHandle brush, IntPtr blend, IntPtr positions, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetLineBlend(SafeBrushHandle brush, IntPtr blend, IntPtr positions, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetLinePresetBlendCount(SafeBrushHandle brush, out int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetLinePresetBlend(SafeBrushHandle brush, IntPtr blend, IntPtr positions, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetLinePresetBlend(SafeBrushHandle brush, IntPtr blend, IntPtr positions, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetLineWrapMode(SafeBrushHandle brush, int wrapMode);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetLineWrapMode(SafeBrushHandle brush, out int wrapMode);

            [LibraryImport(LibraryName)]
            internal static partial int GdipResetLineTransform(SafeBrushHandle brush);

            [LibraryImport(LibraryName)]
            internal static partial int GdipMultiplyLineTransform(SafeBrushHandle brush, SafeMatrixHandle matrix, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetLineTransform(SafeBrushHandle brush, SafeMatrixHandle matrix);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetLineTransform(SafeBrushHandle brush, SafeMatrixHandle matrix);

            [LibraryImport(LibraryName)]
            internal static partial int GdipTranslateLineTransform(SafeBrushHandle brush, float dx, float dy, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipScaleLineTransform(SafeBrushHandle brush, float sx, float sy, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipRotateLineTransform(SafeBrushHandle brush, float angle, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreatePathGradient(PointF* points, int count, WrapMode wrapMode, out SafeBrushHandle brush);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreatePathGradientI(Point* points, int count, WrapMode wrapMode, out SafeBrushHandle brush);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreatePathGradientFromPath(SafeGraphicsPathHandle path, out SafeBrushHandle brush);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPathGradientCenterColor(SafeBrushHandle brush, out int color);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPathGradientCenterColor(SafeBrushHandle brush, int color);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPathGradientSurroundColorsWithCount(SafeBrushHandle brush, int[] color, ref int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPathGradientSurroundColorsWithCount(SafeBrushHandle brush, int[] argb, ref int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPathGradientCenterPoint(SafeBrushHandle brush, out PointF point);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPathGradientCenterPoint(SafeBrushHandle brush, ref PointF point);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPathGradientRect(SafeBrushHandle brush, out RectangleF gprectf);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPathGradientPointCount(SafeBrushHandle brush, out int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPathGradientSurroundColorCount(SafeBrushHandle brush, out int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPathGradientBlendCount(SafeBrushHandle brush, out int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPathGradientBlend(SafeBrushHandle brush, float[] blend, float[] positions, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPathGradientBlend(SafeBrushHandle brush, IntPtr blend, IntPtr positions, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPathGradientPresetBlendCount(SafeBrushHandle brush, out int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPathGradientPresetBlend(SafeBrushHandle brush, int[] blend, float[] positions, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPathGradientPresetBlend(SafeBrushHandle brush, int[] blend, float[] positions, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPathGradientSigmaBlend(SafeBrushHandle brush, float focus, float scale);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPathGradientLinearBlend(SafeBrushHandle brush, float focus, float scale);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPathGradientWrapMode(SafeBrushHandle brush, int wrapmode);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPathGradientWrapMode(SafeBrushHandle brush, out int wrapmode);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPathGradientTransform(SafeBrushHandle brush, SafeMatrixHandle matrix);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPathGradientTransform(SafeBrushHandle brush, SafeMatrixHandle matrix);

            [LibraryImport(LibraryName)]
            internal static partial int GdipResetPathGradientTransform(SafeBrushHandle brush);

            [LibraryImport(LibraryName)]
            internal static partial int GdipMultiplyPathGradientTransform(SafeBrushHandle brush, SafeMatrixHandle matrix, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipTranslatePathGradientTransform(SafeBrushHandle brush, float dx, float dy, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipScalePathGradientTransform(SafeBrushHandle brush, float sx, float sy, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipRotatePathGradientTransform(SafeBrushHandle brush, float angle, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPathGradientFocusScales(SafeBrushHandle brush, float[] xScale, float[] yScale);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPathGradientFocusScales(SafeBrushHandle brush, float xScale, float yScale);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateImageAttributes(out IntPtr imageattr);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCloneImageAttributes(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageattr, out IntPtr cloneImageattr);

            [LibraryImport(LibraryName)]
            internal static partial int GdipDisposeImageAttributes(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageattr);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetImageAttributesColorMatrix(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageattr, ColorAdjustType type, [MarshalAs(UnmanagedType.Bool)] bool enableFlag,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(ColorMatrix.PinningMarshaller))]
#endif
            ColorMatrix? colorMatrix,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(ColorMatrix.PinningMarshaller))]
#endif
            ColorMatrix? grayMatrix, ColorMatrixFlag flags);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetImageAttributesThreshold(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageattr, ColorAdjustType type, [MarshalAs(UnmanagedType.Bool)] bool enableFlag, float threshold);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetImageAttributesGamma(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageattr, ColorAdjustType type, [MarshalAs(UnmanagedType.Bool)] bool enableFlag, float gamma);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetImageAttributesNoOp(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageattr, ColorAdjustType type, [MarshalAs(UnmanagedType.Bool)] bool enableFlag);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetImageAttributesColorKeys(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageattr, ColorAdjustType type, [MarshalAs(UnmanagedType.Bool)] bool enableFlag, int colorLow, int colorHigh);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetImageAttributesOutputChannel(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageattr, ColorAdjustType type, [MarshalAs(UnmanagedType.Bool)] bool enableFlag, ColorChannelFlag flags);

            [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf16)]
            internal static partial int GdipSetImageAttributesOutputChannelColorProfile(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageattr, ColorAdjustType type, [MarshalAs(UnmanagedType.Bool)] bool enableFlag, string colorProfileFilename);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetImageAttributesRemapTable(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageattr, ColorAdjustType type, [MarshalAs(UnmanagedType.Bool)] bool enableFlag, int mapSize, IntPtr map);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetImageAttributesWrapMode(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageattr, int wrapmode, int argb, [MarshalAs(UnmanagedType.Bool)] bool clamp);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetImageAttributesAdjustedPalette(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageattr, IntPtr palette, ColorAdjustType type);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetImageDecodersSize(out int numDecoders, out int size);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetImageDecoders(int numDecoders, int size, IntPtr decoders);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetImageEncodersSize(out int numEncoders, out int size);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetImageEncoders(int numEncoders, int size, IntPtr encoders);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateSolidFill(int color, out SafeBrushHandle brush);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetSolidFillColor(SafeBrushHandle brush, int color);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetSolidFillColor(SafeBrushHandle brush, out int color);


            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateTexture(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef bitmap, int wrapmode, out SafeBrushHandle texture);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateTexture2(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef bitmap, int wrapmode, float x, float y, float width, float height, out SafeBrushHandle texture);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateTextureIA(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef bitmap,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageAttrib, float x, float y, float width, float height, out SafeBrushHandle texture);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateTexture2I(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef bitmap, int wrapmode, int x, int y, int width, int height, out SafeBrushHandle texture);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateTextureIAI(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef bitmap,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageAttrib, int x, int y, int width, int height, out SafeBrushHandle texture);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetTextureTransform(SafeBrushHandle brush, SafeMatrixHandle matrix);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetTextureTransform(SafeBrushHandle brush, SafeMatrixHandle matrix);

            [LibraryImport(LibraryName)]
            internal static partial int GdipResetTextureTransform(SafeBrushHandle brush);

            [LibraryImport(LibraryName)]
            internal static partial int GdipMultiplyTextureTransform(SafeBrushHandle brush, SafeMatrixHandle matrix, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipTranslateTextureTransform(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef brush, float dx, float dy, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipScaleTextureTransform(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef brush, float sx, float sy, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipRotateTextureTransform(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef brush, float angle, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetTextureWrapMode(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef brush, int wrapMode);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetTextureWrapMode(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef brush, out int wrapMode);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetTextureImage(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef brush, out IntPtr image);
            [LibraryImport(LibraryName)]
            internal static partial int GdipTranslateTextureTransform(SafeBrushHandle brush, float dx, float dy, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipScaleTextureTransform(SafeBrushHandle brush, float sx, float sy, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipRotateTextureTransform(SafeBrushHandle brush, float angle, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetTextureWrapMode(SafeBrushHandle brush, int wrapMode);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetTextureWrapMode(SafeBrushHandle brush, out int wrapMode);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetTextureImage(SafeBrushHandle brush, out IntPtr image);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetFontCollectionFamilyCount(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef fontCollection, out int numFound);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetFontCollectionFamilyList(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef fontCollection, int numSought, IntPtr[] gpfamilies, out int numFound);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCloneFontFamily(IntPtr fontfamily, out IntPtr clonefontfamily);

            [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf16)]
            internal static partial int GdipCreateFontFamilyFromName(string name,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef fontCollection, out IntPtr FontFamily);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetGenericFontFamilySansSerif(out IntPtr fontfamily);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetGenericFontFamilySerif(out IntPtr fontfamily);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetGenericFontFamilyMonospace(out IntPtr fontfamily);

            [LibraryImport(LibraryName)]
            internal static partial int GdipDeleteFontFamily(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef fontFamily);

            [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf16)]
            internal static partial int GdipGetFamilyName(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef family, char* name, int language);

            [LibraryImport(LibraryName)]
            internal static partial int GdipIsStyleAvailable(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef family, FontStyle style, out int isStyleAvailable);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetEmHeight(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef family, FontStyle style, out int EmHeight);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetCellAscent(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef family, FontStyle style, out int CellAscent);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetCellDescent(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef family, FontStyle style, out int CellDescent);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetLineSpacing(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef family, FontStyle style, out int LineSpaceing);

            [LibraryImport(LibraryName)]
            internal static partial int GdipNewInstalledFontCollection(out IntPtr fontCollection);

            [LibraryImport(LibraryName)]
            internal static partial int GdipNewPrivateFontCollection(out IntPtr fontCollection);

            [LibraryImport(LibraryName)]
            internal static partial int GdipDeletePrivateFontCollection(ref IntPtr fontCollection);

            [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf16)]
            internal static partial int GdipPrivateAddFontFile(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef fontCollection, string filename);

            [LibraryImport(LibraryName)]
            internal static partial int GdipPrivateAddMemoryFont(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef fontCollection, IntPtr memory, int length);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateFont(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef fontFamily, float emSize, FontStyle style, GraphicsUnit unit, out IntPtr font);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateFontFromDC(IntPtr hdc, ref IntPtr font);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCloneFont(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef font, out IntPtr cloneFont);

            [LibraryImport(LibraryName)]
            internal static partial int GdipDeleteFont(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef font);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetFamily(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef font, out IntPtr family);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetFontStyle(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef font, out FontStyle style);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetFontSize(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef font, out float size);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetFontHeight(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef font, SafeGraphicsHandle graphics, out float size);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetFontHeightGivenDPI(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef font, float dpi, out float size);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetFontUnit(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef font, out GraphicsUnit unit);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetLogFontW(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef font, SafeGraphicsHandle graphics, ref Interop.User32.LOGFONT lf);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreatePen1(int argb, float width, int unit, out SafePenHandle pen);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreatePen2(SafeBrushHandle brush, float width, int unit, out SafePenHandle pen);

            [LibraryImport(LibraryName)]
            internal static partial int GdipClonePen(SafePenHandle pen, out SafePenHandle clonedPen);

            [LibraryImport(LibraryName)]
            internal static partial int GdipDeletePen(IntPtr pen);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPenMode(SafePenHandle pen, PenAlignment penAlign);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPenMode(SafePenHandle pen, out PenAlignment penAlign);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPenWidth(SafePenHandle pen, float width);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPenWidth(SafePenHandle pen, out float width);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPenLineCap197819(SafePenHandle pen, int startCap, int endCap, int dashCap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPenStartCap(SafePenHandle pen, int startCap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPenEndCap(SafePenHandle pen, int endCap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPenStartCap(SafePenHandle pen, out int startCap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPenEndCap(SafePenHandle pen, out int endCap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPenDashCap197819(SafePenHandle pen, out int dashCap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPenDashCap197819(SafePenHandle pen, int dashCap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPenLineJoin(SafePenHandle pen, int lineJoin);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPenLineJoin(SafePenHandle pen, out int lineJoin);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPenCustomStartCap(SafePenHandle pen,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef customCap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPenCustomStartCap(SafePenHandle pen, out IntPtr customCap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPenCustomEndCap(SafePenHandle pen,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef customCap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPenCustomEndCap(SafePenHandle pen, out IntPtr customCap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPenMiterLimit(SafePenHandle pen, float miterLimit);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPenMiterLimit(SafePenHandle pen, float[] miterLimit);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPenTransform(SafePenHandle pen, SafeMatrixHandle matrix);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPenTransform(SafePenHandle pen, SafeMatrixHandle matrix);

            [LibraryImport(LibraryName)]
            internal static partial int GdipResetPenTransform(SafePenHandle pen);

            [LibraryImport(LibraryName)]
            internal static partial int GdipMultiplyPenTransform(SafePenHandle pen, SafeMatrixHandle matrix, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipTranslatePenTransform(SafePenHandle pen, float dx, float dy, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipScalePenTransform(SafePenHandle pen, float sx, float sy, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipRotatePenTransform(SafePenHandle pen, float angle, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPenColor(SafePenHandle pen, int argb);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPenColor(SafePenHandle pen, out int argb);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPenBrushFill(SafePenHandle pen, SafeBrushHandle brush);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPenBrushFill(SafePenHandle pen, out SafeBrushHandle brush);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPenFillType(SafePenHandle pen, out int pentype);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPenDashStyle(SafePenHandle pen, out int dashstyle);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPenDashStyle(SafePenHandle pen, int dashstyle);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPenDashArray(SafePenHandle pen,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef memorydash, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPenDashOffset(SafePenHandle pen, float[] dashoffset);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPenDashOffset(SafePenHandle pen, float dashoffset);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPenDashCount(SafePenHandle pen, out int dashcount);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPenDashArray(SafePenHandle pen, float[] memorydash, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPenCompoundCount(SafePenHandle pen, out int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPenCompoundArray(SafePenHandle pen, float[] array, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPenCompoundArray(SafePenHandle pen, float[] array, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetWorldTransform(SafeGraphicsHandle graphics, SafeMatrixHandle matrix);

            [LibraryImport(LibraryName)]
            internal static partial int GdipResetWorldTransform(SafeGraphicsHandle graphics);

            [LibraryImport(LibraryName)]
            internal static partial int GdipMultiplyWorldTransform(SafeGraphicsHandle graphics, SafeMatrixHandle matrix, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipTranslateWorldTransform(SafeGraphicsHandle graphics, float dx, float dy, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipScaleWorldTransform(SafeGraphicsHandle graphics, float sx, float sy, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipRotateWorldTransform(SafeGraphicsHandle graphics, float angle, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetWorldTransform(SafeGraphicsHandle graphics, SafeMatrixHandle matrix);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetCompositingMode(SafeGraphicsHandle graphics, CompositingMode compositingMode);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetTextRenderingHint(SafeGraphicsHandle graphics, TextRenderingHint textRenderingHint);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetTextContrast(SafeGraphicsHandle graphics, int textContrast);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetInterpolationMode(SafeGraphicsHandle graphics, InterpolationMode interpolationMode);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetCompositingMode(SafeGraphicsHandle graphics, out CompositingMode compositingMode);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetRenderingOrigin(SafeGraphicsHandle graphics, int x, int y);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetRenderingOrigin(SafeGraphicsHandle graphics, out int x, out int y);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetCompositingQuality(SafeGraphicsHandle graphics, CompositingQuality quality);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetCompositingQuality(SafeGraphicsHandle graphics, out CompositingQuality quality);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetSmoothingMode(SafeGraphicsHandle graphics, SmoothingMode smoothingMode);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetSmoothingMode(SafeGraphicsHandle graphics, out SmoothingMode smoothingMode);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPixelOffsetMode(SafeGraphicsHandle graphics, PixelOffsetMode pixelOffsetMode);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPixelOffsetMode(SafeGraphicsHandle graphics, out PixelOffsetMode pixelOffsetMode);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetTextRenderingHint(SafeGraphicsHandle graphics, out TextRenderingHint textRenderingHint);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetTextContrast(SafeGraphicsHandle graphics, out int textContrast);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetInterpolationMode(SafeGraphicsHandle graphics, out InterpolationMode interpolationMode);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPageUnit(SafeGraphicsHandle graphics, out GraphicsUnit unit);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPageScale(SafeGraphicsHandle graphics, out float scale);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPageUnit(SafeGraphicsHandle graphics, GraphicsUnit unit);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPageScale(SafeGraphicsHandle graphics, float scale);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetDpiX(SafeGraphicsHandle graphics, out float dpi);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetDpiY(SafeGraphicsHandle graphics, out float dpi);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateMatrix(out SafeMatrixHandle matrix);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateMatrix2(float m11, float m12, float m21, float m22, float dx, float dy, out SafeMatrixHandle matrix);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateMatrix3(ref RectangleF rect, PointF* dstplg, out SafeMatrixHandle matrix);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateMatrix3I(ref Rectangle rect, Point* dstplg, out SafeMatrixHandle matrix);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCloneMatrix(SafeMatrixHandle matrix, out SafeMatrixHandle clonedMatrix);

            [LibraryImport(LibraryName)]
            internal static partial int GdipDeleteMatrix(IntPtr matrix);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetMatrixElements(SafeMatrixHandle matrix, float m11, float m12, float m21, float m22, float dx, float dy);

            [LibraryImport(LibraryName)]
            internal static partial int GdipMultiplyMatrix(SafeMatrixHandle matrix, SafeMatrixHandle matrix2, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipTranslateMatrix(SafeMatrixHandle matrix, float offsetX, float offsetY, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipScaleMatrix(SafeMatrixHandle matrix, float scaleX, float scaleY, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipRotateMatrix(SafeMatrixHandle matrix, float angle, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipShearMatrix(SafeMatrixHandle matrix, float shearX, float shearY, MatrixOrder order);

            [LibraryImport(LibraryName)]
            internal static partial int GdipInvertMatrix(SafeMatrixHandle matrix);

            [LibraryImport(LibraryName)]
            internal static partial int GdipTransformMatrixPoints(SafeMatrixHandle matrix, PointF* pts, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipTransformMatrixPointsI(SafeMatrixHandle matrix, Point* pts, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipVectorTransformMatrixPoints(SafeMatrixHandle matrix, PointF* pts, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipVectorTransformMatrixPointsI(SafeMatrixHandle matrix, Point* pts, int count);

            [LibraryImport(LibraryName)]
            internal static unsafe partial int GdipGetMatrixElements(SafeMatrixHandle matrix, float* m);

            [LibraryImport(LibraryName)]
            internal static partial int GdipIsMatrixInvertible(SafeMatrixHandle matrix, out int boolean);

            [LibraryImport(LibraryName)]
            internal static partial int GdipIsMatrixIdentity(SafeMatrixHandle matrix, out int boolean);

            [LibraryImport(LibraryName)]
            internal static partial int GdipIsMatrixEqual(SafeMatrixHandle matrix, SafeMatrixHandle matrix2, out int boolean);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateRegion(out SafeRegionHandle region);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateRegionRect(ref RectangleF gprectf, out SafeRegionHandle region);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateRegionRectI(ref Rectangle gprect, out SafeRegionHandle region);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateRegionPath(SafeGraphicsPathHandle path, out SafeRegionHandle region);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateRegionRgnData(byte[] rgndata, int size, out SafeRegionHandle region);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateRegionHrgn(IntPtr hRgn, out SafeRegionHandle region);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCloneRegion(SafeRegionHandle region, out SafeRegionHandle clonedRegion);

            [LibraryImport(LibraryName)]
            internal static partial int GdipDeleteRegion(IntPtr region);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipFillRegion(SafeGraphicsHandle graphics, SafeBrushHandle brush, SafeRegionHandle region);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetInfinite(SafeRegionHandle region);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetEmpty(SafeRegionHandle region);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCombineRegionRect(SafeRegionHandle region, ref RectangleF gprectf, CombineMode mode);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCombineRegionRectI(SafeRegionHandle region, ref Rectangle gprect, CombineMode mode);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCombineRegionPath(SafeRegionHandle region, SafeGraphicsPathHandle path, CombineMode mode);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCombineRegionRegion(SafeRegionHandle region, SafeRegionHandle region2, CombineMode mode);

            [LibraryImport(LibraryName)]
            internal static partial int GdipTranslateRegion(SafeRegionHandle region, float dx, float dy);

            [LibraryImport(LibraryName)]
            internal static partial int GdipTranslateRegionI(SafeRegionHandle region, int dx, int dy);

            [LibraryImport(LibraryName)]
            internal static partial int GdipTransformRegion(SafeRegionHandle region, SafeMatrixHandle matrix);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetRegionBounds(SafeRegionHandle region, SafeGraphicsHandle graphics, out RectangleF gprectf);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetRegionHRgn(SafeRegionHandle region, SafeGraphicsHandle graphics, out IntPtr hrgn);

            [LibraryImport(LibraryName)]
            internal static partial int GdipIsEmptyRegion(SafeRegionHandle region, SafeGraphicsHandle graphics, out int boolean);

            [LibraryImport(LibraryName)]
            internal static partial int GdipIsInfiniteRegion(SafeRegionHandle region, SafeGraphicsHandle graphics, out int boolean);

            [LibraryImport(LibraryName)]
            internal static partial int GdipIsEqualRegion(SafeRegionHandle region, SafeRegionHandle region2, SafeGraphicsHandle graphics, out int boolean);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetRegionDataSize(SafeRegionHandle region, out int bufferSize);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetRegionData(SafeRegionHandle region, byte[] regionData, int bufferSize, out int sizeFilled);

            [LibraryImport(LibraryName)]
            internal static partial int GdipIsVisibleRegionPoint(SafeRegionHandle region, float X, float Y, SafeGraphicsHandle graphics, out int boolean);

            [LibraryImport(LibraryName)]
            internal static partial int GdipIsVisibleRegionPointI(SafeRegionHandle region, int X, int Y, SafeGraphicsHandle graphics, out int boolean);

            [LibraryImport(LibraryName)]
            internal static partial int GdipIsVisibleRegionRect(SafeRegionHandle region, float X, float Y, float width, float height, SafeGraphicsHandle graphics, out int boolean);

            [LibraryImport(LibraryName)]
            internal static partial int GdipIsVisibleRegionRectI(SafeRegionHandle region, int X, int Y, int width, int height, SafeGraphicsHandle graphics, out int boolean);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetRegionScansCount(SafeRegionHandle region, out int count, SafeMatrixHandle matrix);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetRegionScans(SafeRegionHandle region, RectangleF* rects, out int count, SafeMatrixHandle matrix);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateFromHDC(IntPtr hdc, out SafeGraphicsHandle graphics);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetClipGraphics(SafeGraphicsHandle graphics, SafeGraphicsHandle srcgraphics, CombineMode mode);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetClipRect(SafeGraphicsHandle graphics, float x, float y, float width, float height, CombineMode mode);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetClipRectI(SafeGraphicsHandle graphics, int x, int y, int width, int height, CombineMode mode);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetClipPath(SafeGraphicsHandle graphics, SafeGraphicsPathHandle path, CombineMode mode);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetClipRegion(SafeGraphicsHandle graphics, SafeRegionHandle region, CombineMode mode);

            [LibraryImport(LibraryName)]
            internal static partial int GdipResetClip(SafeGraphicsHandle graphics);

            [LibraryImport(LibraryName)]
            internal static partial int GdipTranslateClip(SafeGraphicsHandle graphics, float dx, float dy);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetClip(SafeGraphicsHandle graphics, SafeRegionHandle region);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetClipBounds(SafeGraphicsHandle graphics, out RectangleF rect);

            [LibraryImport(LibraryName)]
            internal static partial int GdipIsClipEmpty(SafeGraphicsHandle graphics, out bool result);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetVisibleClipBounds(SafeGraphicsHandle graphics, out RectangleF rect);

            [LibraryImport(LibraryName)]
            internal static partial int GdipIsVisibleClipEmpty(SafeGraphicsHandle graphics, out bool result);

            [LibraryImport(LibraryName)]
            internal static partial int GdipIsVisiblePoint(SafeGraphicsHandle graphics, float x, float y, out bool result);

            [LibraryImport(LibraryName)]
            internal static partial int GdipIsVisiblePointI(SafeGraphicsHandle graphics, int x, int y, out bool result);

            [LibraryImport(LibraryName)]
            internal static partial int GdipIsVisibleRect(SafeGraphicsHandle graphics, float x, float y, float width, float height, out bool result);

            [LibraryImport(LibraryName)]
            internal static partial int GdipIsVisibleRectI(SafeGraphicsHandle graphics, int x, int y, int width, int height, out bool result);

            [LibraryImport(LibraryName)]
            internal static partial int GdipFlush(SafeGraphicsHandle graphics, FlushIntention intention);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetDC(SafeGraphicsHandle graphics, out IntPtr hdc);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetStringFormatMeasurableCharacterRanges(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef format, int rangeCount, CharacterRange[] range);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateStringFormat(StringFormatFlags options, int language, out IntPtr format);

            [LibraryImport(LibraryName)]
            internal static partial int GdipStringFormatGetGenericDefault(out IntPtr format);

            [LibraryImport(LibraryName)]
            internal static partial int GdipStringFormatGetGenericTypographic(out IntPtr format);

            [LibraryImport(LibraryName)]
            internal static partial int GdipDeleteStringFormat(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef format);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCloneStringFormat(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef format, out IntPtr newFormat);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetStringFormatFlags(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef format, StringFormatFlags options);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetStringFormatFlags(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef format, out StringFormatFlags result);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetStringFormatAlign(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef format, StringAlignment align);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetStringFormatAlign(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef format, out StringAlignment align);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetStringFormatLineAlign(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef format, StringAlignment align);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetStringFormatLineAlign(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef format, out StringAlignment align);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetStringFormatHotkeyPrefix(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef format, HotkeyPrefix hotkeyPrefix);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetStringFormatHotkeyPrefix(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef format, out HotkeyPrefix hotkeyPrefix);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetStringFormatTabStops(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef format, float firstTabOffset, int count, float[] tabStops);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetStringFormatTabStops(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef format, int count, out float firstTabOffset, float[] tabStops);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetStringFormatTabStopCount(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef format, out int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetStringFormatMeasurableCharacterRangeCount(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef format, out int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetStringFormatTrimming(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef format, StringTrimming trimming);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetStringFormatTrimming(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef format, out StringTrimming trimming);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetStringFormatDigitSubstitution(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef format, int langID, StringDigitSubstitute sds);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetStringFormatDigitSubstitution(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef format, out int langID, out StringDigitSubstitute sds);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetImageDimension(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, out float width, out float height);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetImageWidth(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, out int width);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetImageHeight(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, out int height);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetImageHorizontalResolution(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, out float horzRes);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetImageVerticalResolution(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, out float vertRes);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetImageFlags(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, out int flags);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetImageRawFormat(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, ref Guid format);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetImagePixelFormat(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, out PixelFormat format);

            [LibraryImport(LibraryName)]
            internal static partial int GdipImageGetFrameCount(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, ref Guid dimensionID, out int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipImageSelectActiveFrame(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, ref Guid dimensionID, int frameIndex);

            [LibraryImport(LibraryName)]
            internal static partial int GdipImageRotateFlip(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, int rotateFlipType);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetAllPropertyItems(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, uint totalBufferSize, uint numProperties, PropertyItemInternal* allItems);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPropertyCount(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, out uint numOfProperty);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPropertyIdList(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, uint numOfProperty, int* list);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPropertyItem(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, int propid, uint propSize, PropertyItemInternal* buffer);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPropertyItemSize(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, int propid, out uint size);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPropertySize(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, out uint totalBufferSize, out uint numProperties);

            [LibraryImport(LibraryName)]
            internal static partial int GdipRemovePropertyItem(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, int propid);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPropertyItem(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, PropertyItemInternal* item);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetImageType(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, out int type);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetImageType(IntPtr image, out int type);

            [LibraryImport(LibraryName)]
            internal static partial int GdipDisposeImage(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image);

            [LibraryImport(LibraryName)]
            internal static partial int GdipDisposeImage(IntPtr image);

            [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf16)]
            internal static partial int GdipCreateBitmapFromFile(string filename, out IntPtr bitmap);

            [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf16)]
            internal static partial int GdipCreateBitmapFromFileICM(string filename, out IntPtr bitmap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateBitmapFromScan0(int width, int height, int stride, int format, IntPtr scan0, out IntPtr bitmap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateBitmapFromGraphics(int width, int height, SafeGraphicsHandle graphics, out IntPtr bitmap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateBitmapFromHBITMAP(IntPtr hbitmap, IntPtr hpalette, out IntPtr bitmap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateBitmapFromHICON(IntPtr hicon, out IntPtr bitmap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateBitmapFromResource(IntPtr hresource, IntPtr name, out IntPtr bitmap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateHBITMAPFromBitmap(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef nativeBitmap, out IntPtr hbitmap, int argbBackground);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateHICONFromBitmap(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef nativeBitmap, out IntPtr hicon);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCloneBitmapArea(float x, float y, float width, float height, int format,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef srcbitmap, out IntPtr dstbitmap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCloneBitmapAreaI(int x, int y, int width, int height, int format,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef srcbitmap, out IntPtr dstbitmap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipBitmapLockBits(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef bitmap, ref Rectangle rect, ImageLockMode flags, PixelFormat format,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(BitmapData.PinningMarshaller))]
#endif
            BitmapData lockedBitmapData);

            [LibraryImport(LibraryName)]
            internal static partial int GdipBitmapUnlockBits(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef bitmap,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(BitmapData.PinningMarshaller))]
#endif
            BitmapData lockedBitmapData);

            [LibraryImport(LibraryName)]
            internal static partial int GdipBitmapGetPixel(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef bitmap, int x, int y, out int argb);

            [LibraryImport(LibraryName)]
            internal static partial int GdipBitmapSetPixel(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef bitmap, int x, int y, int argb);

            [LibraryImport(LibraryName)]
            internal static partial int GdipBitmapSetResolution(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef bitmap, float dpix, float dpiy);

            [LibraryImport(LibraryName)]
            internal static partial int GdipImageGetFrameDimensionsCount(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, out int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipImageGetFrameDimensionsList(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, Guid* dimensionIDs, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateMetafileFromEmf(IntPtr hEnhMetafile, [MarshalAs(UnmanagedType.Bool)] bool deleteEmf, out IntPtr metafile);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateMetafileFromWmf(IntPtr hMetafile, [MarshalAs(UnmanagedType.Bool)] bool deleteWmf,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(WmfPlaceableFileHeader.PinningMarshaller))]
#endif
                WmfPlaceableFileHeader wmfplacealbeHeader, out IntPtr metafile);

            [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf16)]
            internal static partial int GdipCreateMetafileFromFile(string file, out IntPtr metafile);

            [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf16)]
            internal static partial int GdipRecordMetafile(IntPtr referenceHdc, EmfType emfType, IntPtr pframeRect, MetafileFrameUnit frameUnit, string? description, out IntPtr metafile);

            [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf16)]
            internal static partial int GdipRecordMetafile(IntPtr referenceHdc, EmfType emfType, ref RectangleF frameRect, MetafileFrameUnit frameUnit, string? description, out IntPtr metafile);

            [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf16)]
            internal static partial int GdipRecordMetafileI(IntPtr referenceHdc, EmfType emfType, ref Rectangle frameRect, MetafileFrameUnit frameUnit, string? description, out IntPtr metafile);

            [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf16)]
            internal static partial int GdipRecordMetafileFileName(string fileName, IntPtr referenceHdc, EmfType emfType, ref RectangleF frameRect, MetafileFrameUnit frameUnit, string? description, out IntPtr metafile);

            [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf16)]
            internal static partial int GdipRecordMetafileFileName(string fileName, IntPtr referenceHdc, EmfType emfType, IntPtr pframeRect, MetafileFrameUnit frameUnit, string? description, out IntPtr metafile);

            [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf16)]
            internal static partial int GdipRecordMetafileFileNameI(string fileName, IntPtr referenceHdc, EmfType emfType, ref Rectangle frameRect, MetafileFrameUnit frameUnit, string? description, out IntPtr metafile);

            [LibraryImport(LibraryName)]
            internal static partial int GdipPlayMetafileRecord(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef metafile, EmfPlusRecordType recordType, int flags, int dataSize, byte[] data);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSaveGraphics(SafeGraphicsHandle graphics, out int state);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawArc(SafeGraphicsHandle graphics, SafePenHandle pen, float x, float y, float width, float height, float startAngle, float sweepAngle);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawArcI(SafeGraphicsHandle graphics, SafePenHandle pen, int x, int y, int width, int height, float startAngle, float sweepAngle);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawLinesI(SafeGraphicsHandle graphics, SafePenHandle pen, Point* points, int count);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawBezier(SafeGraphicsHandle graphics, SafePenHandle pen, float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawEllipse(SafeGraphicsHandle graphics, SafePenHandle pen, float x, float y, float width, float height);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawEllipseI(SafeGraphicsHandle graphics, SafePenHandle pen, int x, int y, int width, int height);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawLine(SafeGraphicsHandle graphics, SafePenHandle pen, float x1, float y1, float x2, float y2);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawLineI(SafeGraphicsHandle graphics, SafePenHandle pen, int x1, int y1, int x2, int y2);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawLines(SafeGraphicsHandle graphics, SafePenHandle pen, PointF* points, int count);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawPath(SafeGraphicsHandle graphics, SafePenHandle pen, SafeGraphicsPathHandle path);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawPie(SafeGraphicsHandle graphics, SafePenHandle pen, float x, float y, float width, float height, float startAngle, float sweepAngle);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawPieI(SafeGraphicsHandle graphics, SafePenHandle pen, int x, int y, int width, int height, float startAngle, float sweepAngle);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawPolygon(SafeGraphicsHandle graphics, SafePenHandle pen, PointF* points, int count);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawPolygonI(SafeGraphicsHandle graphics, SafePenHandle pen, Point* points, int count);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipFillEllipse(SafeGraphicsHandle graphics, SafeBrushHandle brush, float x, float y, float width, float height);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipFillEllipseI(SafeGraphicsHandle graphics, SafeBrushHandle brush, int x, int y, int width, int height);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipFillPolygon(SafeGraphicsHandle graphics, SafeBrushHandle brush, PointF* points, int count, FillMode brushMode);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipFillPolygonI(SafeGraphicsHandle graphics, SafeBrushHandle brush, Point* points, int count, FillMode brushMode);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipFillRectangle(SafeGraphicsHandle graphics, SafeBrushHandle brush, float x, float y, float width, float height);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipFillRectangleI(SafeGraphicsHandle graphics, SafeBrushHandle brush, int x, int y, int width, int height);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipFillRectangles(SafeGraphicsHandle graphics, SafeBrushHandle brush, RectangleF* rects, int count);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipFillRectanglesI(SafeGraphicsHandle graphics, SafeBrushHandle brush, Rectangle* rects, int count);

            [LibraryImport(LibraryName, CharSet = CharSet.Unicode, SetLastError = true)]
            internal static partial int GdipDrawString(SafeGraphicsHandle graphics, string textString, int length,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef font, ref RectangleF layoutRect,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef stringFormat, SafeBrushHandle brush);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawImageRectI(SafeGraphicsHandle graphics,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, int x, int y, int width, int height);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGraphicsClear(SafeGraphicsHandle graphics, int argb);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawClosedCurve(SafeGraphicsHandle graphics, SafePenHandle pen, PointF* points, int count);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawClosedCurveI(SafeGraphicsHandle graphics, SafePenHandle pen, Point* points, int count);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawClosedCurve2(SafeGraphicsHandle graphics, SafePenHandle pen, PointF* points, int count, float tension);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawClosedCurve2I(SafeGraphicsHandle graphics, SafePenHandle pen, Point* points, int count, float tension);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawCurve(SafeGraphicsHandle graphics, SafePenHandle pen, PointF* points, int count);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawCurveI(SafeGraphicsHandle graphics, SafePenHandle pen, Point* points, int count);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawCurve2(SafeGraphicsHandle graphics, SafePenHandle pen, PointF* points, int count, float tension);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawCurve2I(SafeGraphicsHandle graphics, SafePenHandle pen, Point* points, int count, float tension);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawCurve3(SafeGraphicsHandle graphics, SafePenHandle pen, PointF* points, int count, int offset, int numberOfSegments, float tension);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawCurve3I(SafeGraphicsHandle graphics, SafePenHandle pen, Point* points, int count, int offset, int numberOfSegments, float tension);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipFillClosedCurve(SafeGraphicsHandle graphics, SafeBrushHandle brush, PointF* points, int count);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipFillClosedCurveI(SafeGraphicsHandle graphics, SafeBrushHandle brush, Point* points, int count);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipFillClosedCurve2(SafeGraphicsHandle graphics, SafeBrushHandle brush, PointF* points, int count, float tension, FillMode mode);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipFillClosedCurve2I(SafeGraphicsHandle graphics, SafeBrushHandle brush, Point* points, int count, float tension, FillMode mode);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipFillPie(SafeGraphicsHandle graphics, SafeBrushHandle brush, float x, float y, float width, float height, float startAngle, float sweepAngle);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipFillPieI(SafeGraphicsHandle graphics, SafeBrushHandle brush, int x, int y, int width, int height, float startAngle, float sweepAngle);

            [LibraryImport(LibraryName, CharSet = CharSet.Unicode)]
            internal static partial int GdipMeasureString(SafeGraphicsHandle graphics, string textString, int length,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef font, ref RectangleF layoutRect,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef stringFormat, ref RectangleF boundingBox, out int codepointsFitted, out int linesFilled);

            [LibraryImport(LibraryName, CharSet = CharSet.Unicode)]
            internal static partial int GdipMeasureCharacterRanges(SafeGraphicsHandle graphics, string textString, int length,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef font, ref RectangleF layoutRect,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef stringFormat, int characterCount, IntPtr[] region);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawImageI(SafeGraphicsHandle graphics,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, int x, int y);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawImage(SafeGraphicsHandle graphics,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, float x, float y);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawImagePoints(SafeGraphicsHandle graphics,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, PointF* points, int count);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawImagePointsI(SafeGraphicsHandle graphics,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, Point* points, int count);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawImageRectRectI(SafeGraphicsHandle graphics,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, int dstx, int dsty, int dstwidth, int dstheight, int srcx, int srcy, int srcwidth, int srcheight, GraphicsUnit srcunit,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageAttributes, Graphics.DrawImageAbort? callback,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef callbackdata);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawImagePointsRect(SafeGraphicsHandle graphics,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, PointF* points, int count, float srcx, float srcy, float srcwidth, float srcheight, GraphicsUnit srcunit,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageAttributes, Graphics.DrawImageAbort? callback,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef callbackdata);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawImageRectRect(SafeGraphicsHandle graphics,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, float dstx, float dsty, float dstwidth, float dstheight, float srcx, float srcy, float srcwidth, float srcheight, GraphicsUnit srcunit,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageAttributes, Graphics.DrawImageAbort? callback,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef callbackdata);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawImagePointsRectI(SafeGraphicsHandle graphics,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, Point* points, int count, int srcx, int srcy, int srcwidth, int srcheight, GraphicsUnit srcunit,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageAttributes, Graphics.DrawImageAbort? callback,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef callbackdata);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawImageRect(SafeGraphicsHandle graphics,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, float x, float y, float width, float height);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawImagePointRect(SafeGraphicsHandle graphics,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, float x, float y, float srcx, float srcy, float srcwidth, float srcheight, int srcunit);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawImagePointRectI(SafeGraphicsHandle graphics,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, int x, int y, int srcx, int srcy, int srcwidth, int srcheight, int srcunit);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawRectangle(SafeGraphicsHandle graphics, SafePenHandle pen, float x, float y, float width, float height);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawRectangleI(SafeGraphicsHandle graphics, SafePenHandle pen, int x, int y, int width, int height);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawRectangles(SafeGraphicsHandle graphics, SafePenHandle pen, RectangleF* rects, int count);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawRectanglesI(SafeGraphicsHandle graphics, SafePenHandle pen, Rectangle* rects, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipTransformPoints(SafeGraphicsHandle graphics, int destSpace, int srcSpace, PointF* points, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipTransformPointsI(SafeGraphicsHandle graphics, int destSpace, int srcSpace, Point* points, int count);

            [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf16)]
            internal static partial int GdipLoadImageFromFileICM(string filename, out IntPtr image);

            [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf16)]
            internal static partial int GdipLoadImageFromFile(string filename, out IntPtr image);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetEncoderParameterListSize(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, ref Guid encoder, out int size);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetEncoderParameterList(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, ref Guid encoder, int size, IntPtr buffer);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreatePath(int brushMode, out SafeGraphicsPathHandle path);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreatePath2(PointF* points, byte* types, int count, int brushMode, out SafeGraphicsPathHandle path);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreatePath2I(Point* points, byte* types, int count, int brushMode, out SafeGraphicsPathHandle path);

            [LibraryImport(LibraryName)]
            internal static partial int GdipClonePath(SafeGraphicsPathHandle path, out SafeGraphicsPathHandle clonepath);

            [LibraryImport(LibraryName)]
            internal static partial int GdipDeletePath(IntPtr path);

            [LibraryImport(LibraryName)]
            internal static partial int GdipResetPath(SafeGraphicsPathHandle path);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPointCount(SafeGraphicsPathHandle path, out int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPathTypes(SafeGraphicsPathHandle path, byte[] types, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPathPoints(SafeGraphicsPathHandle path, PointF* points, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPathFillMode(SafeGraphicsPathHandle path, out FillMode fillmode);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPathFillMode(SafeGraphicsPathHandle path, FillMode fillmode);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPathData(SafeGraphicsPathHandle path, GpPathData* pathData);

            [LibraryImport(LibraryName)]
            internal static partial int GdipStartPathFigure(SafeGraphicsPathHandle path);

            [LibraryImport(LibraryName)]
            internal static partial int GdipClosePathFigure(SafeGraphicsPathHandle path);

            [LibraryImport(LibraryName)]
            internal static partial int GdipClosePathFigures(SafeGraphicsPathHandle path);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetPathMarker(SafeGraphicsPathHandle path);

            [LibraryImport(LibraryName)]
            internal static partial int GdipClearPathMarkers(SafeGraphicsPathHandle path);

            [LibraryImport(LibraryName)]
            internal static partial int GdipReversePath(SafeGraphicsPathHandle path);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetPathLastPoint(SafeGraphicsPathHandle path, out PointF lastPoint);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathLine(SafeGraphicsPathHandle path, float x1, float y1, float x2, float y2);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathLine2(SafeGraphicsPathHandle path, PointF* points, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathArc(SafeGraphicsPathHandle path, float x, float y, float width, float height, float startAngle, float sweepAngle);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathBezier(SafeGraphicsPathHandle path, float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathBeziers(SafeGraphicsPathHandle path, PointF* points, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathCurve(SafeGraphicsPathHandle path, PointF* points, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathCurve2(SafeGraphicsPathHandle path, PointF* points, int count, float tension);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathCurve3(SafeGraphicsPathHandle path, PointF* points, int count, int offset, int numberOfSegments, float tension);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathClosedCurve(SafeGraphicsPathHandle path, PointF* points, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathClosedCurve2(SafeGraphicsPathHandle path, PointF* points, int count, float tension);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathRectangle(SafeGraphicsPathHandle path, float x, float y, float width, float height);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathRectangles(SafeGraphicsPathHandle path, RectangleF* rects, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathEllipse(SafeGraphicsPathHandle path, float x, float y, float width, float height);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathPie(SafeGraphicsPathHandle path, float x, float y, float width, float height, float startAngle, float sweepAngle);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathPolygon(SafeGraphicsPathHandle path, PointF* points, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathPath(SafeGraphicsPathHandle path, SafeGraphicsPathHandle addingPath, [MarshalAs(UnmanagedType.Bool)] bool connect);

            [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf16)]
            internal static partial int GdipAddPathString(SafeGraphicsPathHandle path, string s, int length,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef fontFamily, int style, float emSize, ref RectangleF layoutRect,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef format);

            [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf16)]
            internal static partial int GdipAddPathStringI(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef fontFamily, int style, float emSize, ref Rectangle layoutRect,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef format);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathLineI(SafeGraphicsPathHandle path, int x1, int y1, int x2, int y2);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathLine2I(SafeGraphicsPathHandle path, Point* points, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathArcI(SafeGraphicsPathHandle path, int x, int y, int width, int height, float startAngle, float sweepAngle);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathBezierI(SafeGraphicsPathHandle path, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathBeziersI(SafeGraphicsPathHandle path, Point* points, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathCurveI(SafeGraphicsPathHandle path, Point* points, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathCurve2I(SafeGraphicsPathHandle path, Point* points, int count, float tension);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathCurve3I(SafeGraphicsPathHandle path, Point* points, int count, int offset, int numberOfSegments, float tension);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathClosedCurveI(SafeGraphicsPathHandle path, Point* points, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathClosedCurve2I(SafeGraphicsPathHandle path, Point* points, int count, float tension);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathRectangleI(SafeGraphicsPathHandle path, int x, int y, int width, int height);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathRectanglesI(SafeGraphicsPathHandle path, Rectangle* rects, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathEllipseI(SafeGraphicsPathHandle path, int x, int y, int width, int height);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathPieI(SafeGraphicsPathHandle path, int x, int y, int width, int height, float startAngle, float sweepAngle);

            [LibraryImport(LibraryName)]
            internal static partial int GdipAddPathPolygonI(SafeGraphicsPathHandle path, Point* points, int count);

            [LibraryImport(LibraryName)]
            internal static partial int GdipTransformPath(SafeGraphicsPathHandle path, SafeMatrixHandle matrix);

            [LibraryImport(LibraryName)]
            internal static partial int GdipIsVisiblePathPoint(SafeGraphicsPathHandle path, float x, float y, SafeGraphicsHandle graphics, [MarshalAs(UnmanagedType.Bool)] out bool result);

            [LibraryImport(LibraryName)]
            internal static partial int GdipIsVisiblePathPointI(SafeGraphicsPathHandle path, int x, int y, SafeGraphicsHandle graphics, [MarshalAs(UnmanagedType.Bool)] out bool result);

            [LibraryImport(LibraryName)]
            internal static partial int GdipIsOutlineVisiblePathPoint(SafeGraphicsPathHandle path, float x, float y, SafePenHandle pen, SafeGraphicsHandle graphics, [MarshalAs(UnmanagedType.Bool)] out bool result);

            [LibraryImport(LibraryName)]
            internal static partial int GdipIsOutlineVisiblePathPointI(SafeGraphicsPathHandle path, int x, int y, SafePenHandle pen, SafeGraphicsHandle graphics, [MarshalAs(UnmanagedType.Bool)] out bool result);

            [LibraryImport(LibraryName)]
            internal static partial int GdipDeleteBrush(IntPtr brush);

            [LibraryImport(LibraryName)]
            internal static partial int GdipLoadImageFromStream(IntPtr stream, IntPtr* image);

            [LibraryImport(LibraryName)]
            internal static partial int GdipLoadImageFromStreamICM(IntPtr stream, IntPtr* image);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCloneImage(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, out IntPtr cloneimage);

            [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf16)]
            internal static partial int GdipSaveImageToFile(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, string filename, ref Guid classId,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef encoderParams);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSaveImageToStream(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, IntPtr stream, Guid* classId,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef encoderParams);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSaveAdd(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef encoderParams);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSaveAddImage(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef newImage,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef encoderParams);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetImageGraphicsContext(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, out SafeGraphicsHandle graphics);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetImageBounds(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, out RectangleF gprectf, out GraphicsUnit unit);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetImageThumbnail(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, int thumbWidth, int thumbHeight, out IntPtr thumbImage, Image.GetThumbnailImageAbort? callback, IntPtr callbackdata);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetImagePalette(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, IntPtr palette, int size);

            [LibraryImport(LibraryName)]
            internal static partial int GdipSetImagePalette(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, IntPtr palette);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetImagePaletteSize(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef image, out int size);

            [LibraryImport(LibraryName)]
            internal static partial int GdipImageForceValidation(IntPtr image);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateFromHDC2(IntPtr hdc, IntPtr hdevice, out SafeGraphicsHandle graphics);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateFromHWND(IntPtr hwnd, out SafeGraphicsHandle graphics);

            [LibraryImport(LibraryName)]
            internal static partial int GdipDeleteGraphics(IntPtr graphics);

            [LibraryImport(LibraryName)]
            internal static partial int GdipReleaseDC(SafeGraphicsHandle graphics, IntPtr hdc);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetNearestColor(SafeGraphicsHandle graphics, ref int color);

            [LibraryImport(LibraryName)]
            internal static partial IntPtr GdipCreateHalftonePalette();

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawBeziers(SafeGraphicsHandle graphics, SafePenHandle pen, PointF* points, int count);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipDrawBeziersI(SafeGraphicsHandle graphics, SafePenHandle pen, Point* points, int count);

            [LibraryImport(LibraryName, SetLastError = true)]
            internal static partial int GdipFillPath(SafeGraphicsHandle graphics, SafeBrushHandle brush, SafeGraphicsPathHandle path);

            [LibraryImport(LibraryName)]
            internal static partial int GdipEnumerateMetafileDestPoint(SafeGraphicsHandle graphics,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef metafile, ref PointF destPoint, Graphics.EnumerateMetafileProc callback, IntPtr callbackdata,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageattributes);

            [LibraryImport(LibraryName)]
            internal static partial int GdipEnumerateMetafileDestPointI(SafeGraphicsHandle graphics,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef metafile, ref Point destPoint, Graphics.EnumerateMetafileProc callback, IntPtr callbackdata,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageattributes);

            [LibraryImport(LibraryName)]
            internal static partial int GdipEnumerateMetafileDestRect(SafeGraphicsHandle graphics,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef metafile, ref RectangleF destRect, Graphics.EnumerateMetafileProc callback, IntPtr callbackdata,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageattributes);

            [LibraryImport(LibraryName)]
            internal static partial int GdipEnumerateMetafileDestRectI(SafeGraphicsHandle graphics,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef metafile, ref Rectangle destRect, Graphics.EnumerateMetafileProc callback, IntPtr callbackdata,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageattributes);

            [LibraryImport(LibraryName)]
            internal static partial int GdipEnumerateMetafileDestPoints(SafeGraphicsHandle graphics,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef metafile, PointF* destPoints, int count, Graphics.EnumerateMetafileProc callback, IntPtr callbackdata,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageattributes);

            [LibraryImport(LibraryName)]
            internal static partial int GdipEnumerateMetafileDestPointsI(SafeGraphicsHandle graphics,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef metafile, Point* destPoints, int count, Graphics.EnumerateMetafileProc callback, IntPtr callbackdata,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageattributes);

            [LibraryImport(LibraryName)]
            internal static partial int GdipEnumerateMetafileSrcRectDestPoint(SafeGraphicsHandle graphics,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef metafile, ref PointF destPoint, ref RectangleF srcRect, GraphicsUnit pageUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackdata,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageattributes);

            [LibraryImport(LibraryName)]
            internal static partial int GdipEnumerateMetafileSrcRectDestPointI(SafeGraphicsHandle graphics,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef metafile, ref Point destPoint, ref Rectangle srcRect, GraphicsUnit pageUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackdata,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageattributes);

            [LibraryImport(LibraryName)]
            internal static partial int GdipEnumerateMetafileSrcRectDestRect(SafeGraphicsHandle graphics,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef metafile, ref RectangleF destRect, ref RectangleF srcRect, GraphicsUnit pageUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackdata,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageattributes);

            [LibraryImport(LibraryName)]
            internal static partial int GdipEnumerateMetafileSrcRectDestRectI(SafeGraphicsHandle graphics,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef metafile, ref Rectangle destRect, ref Rectangle srcRect, GraphicsUnit pageUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackdata,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageattributes);

            [LibraryImport(LibraryName)]
            internal static partial int GdipEnumerateMetafileSrcRectDestPoints(SafeGraphicsHandle graphics,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef metafile, PointF* destPoints, int count, ref RectangleF srcRect, GraphicsUnit pageUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackdata,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageattributes);

            [LibraryImport(LibraryName)]
            internal static partial int GdipEnumerateMetafileSrcRectDestPointsI(SafeGraphicsHandle graphics,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef metafile, Point* destPoints, int count, ref Rectangle srcRect, GraphicsUnit pageUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackdata,
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef imageattributes);

            [LibraryImport(LibraryName)]
            internal static partial int GdipRestoreGraphics(SafeGraphicsHandle graphics, int state);

            [LibraryImport(LibraryName, EntryPoint = "GdipGetMetafileHeaderFromWmf")]
            private static partial int GdipGetMetafileHeaderFromWmf_Internal(IntPtr hMetafile,
#if NET7_0_OR_GREATER
                [MarshalUsing(typeof(WmfPlaceableFileHeader.PinningMarshaller))]
#endif
                WmfPlaceableFileHeader wmfplaceable,
#if NET7_0_OR_GREATER
                [MarshalUsing(typeof(MetafileHeaderWmf.InPlaceMarshaller))]
                ref MetafileHeaderWmf metafileHeaderWmf
#else
                MetafileHeaderWmf metafileHeaderWmf
#endif
                );

            internal static int GdipGetMetafileHeaderFromWmf(IntPtr hMetafile,
                WmfPlaceableFileHeader wmfplaceable,
                MetafileHeaderWmf metafileHeaderWmf
                )
            {
                return GdipGetMetafileHeaderFromWmf_Internal(hMetafile,
                    wmfplaceable,
#if NET7_0_OR_GREATER
                    ref
#endif
                    metafileHeaderWmf
                    );
            }

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetMetafileHeaderFromEmf(IntPtr hEnhMetafile, MetafileHeaderEmf metafileHeaderEmf);

            [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf16)]
            internal static partial int GdipGetMetafileHeaderFromFile(string filename, IntPtr header);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetMetafileHeaderFromStream(IntPtr stream, IntPtr header);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetMetafileHeaderFromMetafile(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef metafile, IntPtr header);

            [LibraryImport(LibraryName)]
            internal static partial int GdipGetHemfFromMetafile(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef metafile, out IntPtr hEnhMetafile);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateMetafileFromStream(IntPtr stream, IntPtr* metafile);

            [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf16)]
            internal static partial int GdipRecordMetafileStream(IntPtr stream, IntPtr referenceHdc, EmfType emfType, RectangleF* frameRect, MetafileFrameUnit frameUnit, string? description, IntPtr* metafile);

            [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf16)]
            internal static partial int GdipRecordMetafileStream(IntPtr stream, IntPtr referenceHdc, EmfType emfType, IntPtr pframeRect, MetafileFrameUnit frameUnit, string? description, IntPtr* metafile);

            [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf16)]
            internal static partial int GdipRecordMetafileStreamI(IntPtr stream, IntPtr referenceHdc, EmfType emfType, Rectangle* frameRect, MetafileFrameUnit frameUnit, string? description, IntPtr* metafile);

            [LibraryImport(LibraryName)]
            internal static partial int GdipComment(SafeGraphicsHandle graphics, int sizeData, byte[] data);

            [LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf16)]
            internal static partial int GdipCreateFontFromLogfontW(IntPtr hdc, ref Interop.User32.LOGFONT lf, out IntPtr font);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateBitmapFromStream(IntPtr stream, IntPtr* bitmap);

            [LibraryImport(LibraryName)]
            internal static partial int GdipCreateBitmapFromStreamICM(IntPtr stream, IntPtr* bitmap);
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct StartupInputEx
        {
            public int GdiplusVersion;             // Must be 1 or 2

            public IntPtr DebugEventCallback;

            public Interop.BOOL SuppressBackgroundThread;     // FALSE unless you're prepared to call
                                                              // the hook/unhook functions properly

            public Interop.BOOL SuppressExternalCodecs;       // FALSE unless you want GDI+ only to use
                                                              // its internal image codecs.
            public int StartupParameters;

            public static StartupInputEx GetDefault()
            {
                OperatingSystem os = Environment.OSVersion;
                StartupInputEx result = default;

                // In Windows 7 GDI+1.1 story is different as there are different binaries per GDI+ version.
                bool isWindows7 = os.Platform == PlatformID.Win32NT && os.Version.Major == 6 && os.Version.Minor == 1;
                result.GdiplusVersion = isWindows7 ? 1 : 2;
                result.SuppressBackgroundThread = Interop.BOOL.FALSE;
                result.SuppressExternalCodecs = Interop.BOOL.FALSE;
                result.StartupParameters = 0;
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
