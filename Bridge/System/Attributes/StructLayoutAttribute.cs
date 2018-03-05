// ==++==
//
//   Copyright (c) Microsoft Corporation.  All rights reserved.
//
// ==--==
/*=============================================================================
**
** Class: CLSCompliantAttribute
**
**
** Purpose: Container for assemblies.
**
**
=============================================================================*/

using System.Runtime.InteropServices;

namespace System
{
    [Bridge.NonScriptable]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    [System.Runtime.InteropServices.ComVisible(true)]
    public sealed class StructLayoutAttribute : Attribute
    {
        public extern StructLayoutAttribute(LayoutKind layoutKind);
        public extern StructLayoutAttribute(short layoutKind);
    }
}
