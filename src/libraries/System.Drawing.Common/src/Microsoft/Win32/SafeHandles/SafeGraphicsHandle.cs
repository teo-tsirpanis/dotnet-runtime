// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using Gdip = System.Drawing.SafeNativeMethods.Gdip;

namespace Microsoft.Win32.SafeHandles
{
    internal class SafeGraphicsHandle : SafeGdiPlusHandle
    {
        public SafeGraphicsHandle(IntPtr preexistingHandle, bool ownsHandle) : base(ownsHandle)
        {
            SetHandle(preexistingHandle);
        }

        public SafeGraphicsHandle() : base(true)
        {
        }

        protected override int ReleaseHandleImpl() => Gdip.GdipDeleteGraphics(handle);
    }
}
