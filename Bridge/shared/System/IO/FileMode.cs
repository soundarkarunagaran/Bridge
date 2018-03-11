// ==++==
//
//   Copyright (c) Microsoft Corporation.  All rights reserved.
//
// ==--==
/*============================================================
**
** <OWNER>Microsoft</OWNER>
**
** Class:  File
**
**
** Purpose: A collection of methods for manipulating Files.
**
**          April 09,2000 (some design refactorization)
**
===========================================================*/
/*
 * https://github.com/Microsoft/referencesource/blob/master/mscorlib/system/io/filemode.cs
 */


using System;
using System.Text;
using System.Diagnostics.Contracts;

namespace System.IO
{
    [Bridge.Enum(Bridge.Emit.Value)]
    public enum FileMode
    {
        CreateNew = 1,
        Create,
        Open,
        OpenOrCreate,
        Truncate,
        Append
    }
}
