// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.Globalization
{
    [Bridge.Convention(Member = Bridge.ConventionMember.Field | Bridge.ConventionMember.Method, Notation = Bridge.Notation.None)]
    internal sealed partial class GlobalizationMode
    {
        private static bool GetGlobalizationInvariantMode()
        {
            return GetInvariantSwitchValue();
        }
    }
}
