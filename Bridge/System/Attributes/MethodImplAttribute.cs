// ==++==
//
//   Copyright (c) Microsoft Corporation.  All rights reserved.
//
// ==--==

namespace System.Runtime.CompilerServices
{
    using System;
    using System.Reflection;

    // This Enum matchs the miImpl flags defined in corhdr.h. It is used to specify
    // certain method properties.

    [Bridge.NonScriptable]
    [Bridge.External]
    [Flags]
    public enum MethodImplOptions
    {
        Unmanaged = 0x0004,
        NoInlining = 0x0008,
        ForwardRef = 0x0010,
        Synchronized = 0x0020,
        NoOptimization = 0x0040,
        PreserveSig = 0x0080,
        AggressiveInlining = 0x0100,
        InternalCall = 0x1000
    }

    [Bridge.NonScriptable]
    [Bridge.External]
    public enum MethodCodeType
    {
        IL = MethodImplAttributes.IL,
        Native = MethodImplAttributes.Native,
        OPTIL = MethodImplAttributes.OPTIL,
        Runtime = MethodImplAttributes.Runtime
    }

    // Custom attribute to specify additional method properties.
    [Bridge.NonScriptable]
    [Bridge.External]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor, Inherited = false)]
    sealed public class MethodImplAttribute : Attribute
    {
        public MethodCodeType MethodCodeType;

        public MethodImplAttribute(MethodImplOptions methodImplOptions)
        {
            Value = methodImplOptions;
        }

        public MethodImplAttribute(short value)
        {
            Value = (MethodImplOptions)value;
        }

        public MethodImplAttribute()
        {
        }

        public MethodImplOptions Value { get; }
    }
}
