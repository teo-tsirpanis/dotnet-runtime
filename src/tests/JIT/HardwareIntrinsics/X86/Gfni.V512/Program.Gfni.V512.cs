// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;

[assembly:Xunit.ActiveIssue("https://github.com/dotnet/runtime/issues/91392", typeof(TestLibrary.PlatformDetection), nameof(TestLibrary.PlatformDetection.IsMonoLLVMAOT))]
namespace JIT.HardwareIntrinsics.X86._Gfni.V512
{
    public static partial class Program
    {
        static Program()
        {

        }
    }
}
