using Bridge;
using Bridge.Html5;
using System;
using System.Collections.Generic;

namespace Test.BridgeIssues.N3386
{
    /// <summary>
    /// #3386 - the test here consists in checking whether a method with
    /// default parameter values as float/double NaN can be built.
    /// </summary>
    public class N3386
    {
        public static void ConstantNaN(
            float f = float.NaN,
            double d = double.NaN)
        {
        }
    }
}
