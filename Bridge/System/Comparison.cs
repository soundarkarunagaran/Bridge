
namespace System
{
    [Bridge.Name("Function")]
    [Bridge.External]
    public delegate int Comparison<in T>(T x, T y);
}