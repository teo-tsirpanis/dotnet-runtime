// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.Win32.SafeHandles;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
#if NET7_0_OR_GREATER
using System.Runtime.InteropServices.GeneratedMarshalling;
#endif

namespace System.Drawing
{
    internal static partial class SafeNativeMethods
    {
        internal static unsafe partial class Gdip
        {
            [GeneratedDllImport(LibraryName)]
            private static partial int GdipGetPathWorldBounds(SafeGraphicsPathHandle path, out RectangleF gprectf, IntPtr matrix, IntPtr pen);

            // Passing null SafeHandles in P/Invokes is not supported so we will do it the manual way.
            // TODO: Use a marshaller once it can be used on all frameworks.
            internal static int GdipGetPathWorldBounds(SafeGraphicsPathHandle path, out RectangleF gprectf, SafeMatrixHandle? matrixOptional, SafePenHandle? penOptional)
            {
                bool releasePen = false;
                bool releaseMatrix = false;
                try
                {
                    IntPtr nativePen = IntPtr.Zero;
                    if (penOptional != null)
                    {
                        penOptional.DangerousAddRef(ref releasePen);
                        nativePen = penOptional.DangerousGetHandle();
                    }

                    IntPtr nativeMatrix = IntPtr.Zero;
                    if (matrixOptional != null)
                    {
                        matrixOptional.DangerousAddRef(ref releaseMatrix);
                        nativeMatrix = matrixOptional.DangerousGetHandle();
                    }

                    return GdipGetPathWorldBounds(path, out gprectf, nativeMatrix, nativePen);
                }
                finally
                {
                    if (releasePen)
                    {
                        penOptional!.DangerousRelease();
                    }

                    if (releaseMatrix)
                    {
                        matrixOptional!.DangerousRelease();
                    }
                }
            }


            [GeneratedDllImport(LibraryName)]
            private static partial int GdipFlattenPath(SafeGraphicsPathHandle path, IntPtr matrix, float flatness);

            internal static int GdipFlattenPath(SafeGraphicsPathHandle path, SafeMatrixHandle? matrixOptional, float flatness)
            {
                bool releaseMatrix = false;
                try
                {
                    IntPtr nativeMatrix = IntPtr.Zero;
                    if (matrixOptional != null)
                    {
                        matrixOptional.DangerousAddRef(ref releaseMatrix);
                        nativeMatrix = matrixOptional.DangerousGetHandle();
                    }

                    return GdipFlattenPath(path, nativeMatrix, flatness);
                }
                finally
                {
                    if (releaseMatrix)
                    {
                        matrixOptional!.DangerousRelease();
                    }
                }
            }

            [GeneratedDllImport(LibraryName)]
            private static partial int GdipWidenPath(SafeGraphicsPathHandle path, SafePenHandle pen, IntPtr matrix, float flatness);

            internal static int GdipWidenPath(SafeGraphicsPathHandle path, SafePenHandle pen, SafeMatrixHandle? matrixOptional, float flatness)
            {
                bool releaseMatrix = false;
                try
                {
                    IntPtr nativeMatrix = IntPtr.Zero;
                    if (matrixOptional != null)
                    {
                        matrixOptional.DangerousAddRef(ref releaseMatrix);
                        nativeMatrix = matrixOptional.DangerousGetHandle();
                    }

                    return GdipWidenPath(path, pen, nativeMatrix, flatness);
                }
                finally
                {
                    if (releaseMatrix)
                    {
                        matrixOptional!.DangerousRelease();
                    }
                }
            }

            [GeneratedDllImport(LibraryName)]
            private static partial int GdipWarpPath(SafeGraphicsPathHandle path, IntPtr matrix, PointF[] points, int count, float srcX, float srcY, float srcWidth, float srcHeight, WarpMode warpMode, float flatness);

            internal static int GdipWarpPath(SafeGraphicsPathHandle path, SafeMatrixHandle? matrixOptional, PointF[] points, int count, float srcX, float srcY, float srcWidth, float srcHeight, WarpMode warpMode, float flatness)
            {
                bool releaseMatrix = false;
                try
                {
                    IntPtr nativeMatrix = IntPtr.Zero;
                    if (matrixOptional != null)
                    {
                        matrixOptional.DangerousAddRef(ref releaseMatrix);
                        nativeMatrix = matrixOptional.DangerousGetHandle();
                    }

                    return GdipWarpPath(path, nativeMatrix, points, count, srcX, srcY, srcWidth, srcHeight, warpMode, flatness);
                }
                finally
                {
                    if (releaseMatrix)
                    {
                        matrixOptional!.DangerousRelease();
                    }
                }
            }

            [GeneratedDllImport(LibraryName)]
            private static partial int GdipCreatePathIter(out IntPtr pathIter, IntPtr nativePath);

            internal static int GdipCreatePathIter(out IntPtr pathIter, SafeGraphicsPathHandle? pathOptional)
            {
                bool releasePath = false;
                try
                {
                    IntPtr nativePath = IntPtr.Zero;
                    if (pathOptional != null)
                    {
                        pathOptional.DangerousAddRef(ref releasePath);
                        nativePath = pathOptional.DangerousGetHandle();
                    }

                    return GdipCreatePathIter(out pathIter, nativePath);
                }
                finally
                {
                    if (releasePath)
                    {
                        pathOptional!.DangerousRelease();
                    }
                }
            }

            [GeneratedDllImport(LibraryName)]
            private static partial int GdipCreateCustomLineCap(IntPtr fillpath, IntPtr strokepath, LineCap baseCap, float baseInset, out IntPtr customCap);

            internal static int GdipCreateCustomLineCap(SafeGraphicsPathHandle? fillPathOptional, SafeGraphicsPathHandle? strokePathOptional, LineCap baseCap, float baseInset, out IntPtr customCap)
            {
                bool releaseFillPath = false;
                bool releaseStrokePath = false;
                try
                {
                    IntPtr nativeFillPath = IntPtr.Zero;
                    if (fillPathOptional != null)
                    {
                        fillPathOptional.DangerousAddRef(ref releaseFillPath);
                        nativeFillPath = fillPathOptional.DangerousGetHandle();
                    }

                    IntPtr nativeStrokePath = IntPtr.Zero;
                    if (strokePathOptional != null)
                    {
                        strokePathOptional.DangerousAddRef(ref releaseStrokePath);
                        nativeStrokePath = strokePathOptional.DangerousGetHandle();
                    }

                    return GdipCreateCustomLineCap(nativeFillPath, nativeStrokePath, baseCap, baseInset, out customCap);
                }
                finally
                {
                    if (releaseFillPath)
                    {
                        fillPathOptional!.DangerousRelease();
                    }

                    if (releaseStrokePath)
                    {
                        strokePathOptional!.DangerousRelease();
                    }
                }
            }
        }
    }
}
