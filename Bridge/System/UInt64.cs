
namespace System
{
    [Bridge.External]
    [Bridge.Reflectable]
    public struct UInt64 : IComparable, IComparable<UInt64>, IEquatable<UInt64>, IFormattable
    {
        private extern UInt64(int i);

        [Bridge.Convention]
        public const ulong MinValue = 0;

        [Bridge.Convention]
        public const ulong MaxValue = 18446744073709551615;

        [Bridge.Template("System.UInt64.parse({s})")]
        public static extern ulong Parse(string s);

        [Bridge.Template("System.UInt64.tryParse({s}, {result})")]
        public static extern bool TryParse(string s, out ulong result);

        public extern string ToString(int radix);

        public extern string Format(string format);

        public extern string Format(string format, IFormatProvider provider);

        public extern string ToString(string format);

        public extern string ToString(string format, IFormatProvider provider);

        public extern int CompareTo(ulong other);

        public extern int CompareTo(object obj);

        public extern bool Equals(ulong other);

        //[Bridge.Template("System.UInt64.lift({value})")]
        public static extern implicit operator ulong (byte value);

        //[Bridge.Template("System.UInt64.lift({value})")]
        public static extern implicit operator ulong (sbyte value);

        //[Bridge.Template("System.UInt64.lift({value})")]
        public static extern implicit operator ulong (short value);

        //[Bridge.Template("System.UInt64.lift({value})")]
        public static extern implicit operator ulong (ushort value);

        //[Bridge.Template("System.UInt64.lift({value})")]
        public static extern implicit operator ulong (char value);

        //[Bridge.Template("System.UInt64.lift({value})")]
        public static extern implicit operator ulong (int value);

        //[Bridge.Template("System.UInt64.lift({value})")]
        public static extern implicit operator ulong (uint value);

        //[Bridge.Template("System.UInt64.lift(Bridge.Int.clipu64({value}))")]
        public static extern explicit operator ulong (float value);

        //[Bridge.Template("System.UInt64.lift(Bridge.Int.clipu64({value}))")]
        public static extern explicit operator ulong (double value);

        //[Bridge.Template("System.Int64.lift({value})")]
        //public static extern explicit operator ulong(long value);

        //[Bridge.Template("System.Int64.clip8({value})")]
        public static extern explicit operator byte (ulong value);

        //[Bridge.Template("System.Int64.clipu8({value})")]
        public static extern explicit operator sbyte (ulong value);

        //[Bridge.Template("System.Int64.clipu16({value})")]
        public static extern explicit operator char (ulong value);

        //[Bridge.Template("System.Int64.clip16({value})")]
        public static extern explicit operator short (ulong value);

        //[Bridge.Template("System.Int64.clipu16({value})")]
        public static extern explicit operator ushort (ulong value);

        //[Bridge.Template("System.Int64.clip32({value})")]
        public static extern explicit operator int (ulong value);

        //[Bridge.Template("System.Int64.clipu32({value})")]
        public static extern explicit operator uint (ulong value);

        //[Bridge.Template("System.UInt64.lift({value})")]
        //public static extern explicit operator long(ulong value);

        //[Bridge.Template("System.UInt64.toNumber({value})")]
        public static extern explicit operator float (ulong value);

        //[Bridge.Template("System.UInt64.toNumber({value})")]
        public static extern explicit operator double (ulong value);
    }
}