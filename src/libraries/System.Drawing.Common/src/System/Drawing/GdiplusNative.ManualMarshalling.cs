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
            private static partial int GdipGetPathWorldBounds(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef path, out RectangleF gprectf, IntPtr matrix, IntPtr pen);

            // Passing null SafeHandles in P/Invokes is not supported so we will do it the manual way.
            // TODO: Use a marshaller once it can be used on all frameworks.
            internal static int GdipGetPathWorldBounds(HandleRef path, out RectangleF gprectf, SafeMatrixHandle? matrixOptional, SafePenHandle? penOptional)
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
            private static partial int GdipFlattenPath(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef path, IntPtr matrix, float flatness);

            internal static int GdipFlattenPath(HandleRef path, SafeMatrixHandle? matrixOptional, float flatness)
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
            private static partial int GdipWidenPath(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef path, SafePenHandle pen, IntPtr matrix, float flatness);

            internal static int GdipWidenPath(HandleRef path, SafePenHandle pen, SafeMatrixHandle? matrixOptional, float flatness)
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
            private static partial int GdipWarpPath(
#if NET7_0_OR_GREATER
            [MarshalUsing(typeof(HandleRefMarshaller))]
#endif
            HandleRef path, IntPtr matrix, PointF[] points, int count, float srcX, float srcY, float srcWidth, float srcHeight, WarpMode warpMode, float flatness);

            internal static int GdipWarpPath(HandleRef path, SafeMatrixHandle? matrixOptional, PointF[] points, int count, float srcX, float srcY, float srcWidth, float srcHeight, WarpMode warpMode, float flatness)
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
        }
    }
}
