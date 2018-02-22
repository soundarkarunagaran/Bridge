
namespace System.Globalization
{
    [Bridge.External]
    [Bridge.Reflectable]
    public sealed class NumberFormatInfo : IFormatProvider, ICloneable, Bridge.IBridgeClass
    {
        public extern NumberFormatInfo();

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public static extern NumberFormatInfo InvariantInfo
        {
            get;
        }

        [Bridge.Name("nanSymbol")]
        public extern string NaNSymbol
        {
            get;
            set;
        }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern string NegativeSign
        {
            get;
            set;
        }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern string PositiveSign
        {
            get;
            set;
        }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern string NegativeInfinitySymbol
        {
            get;
            set;
        }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern string PositiveInfinitySymbol
        {
            get;
            set;
        }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern string PercentSymbol
        {
            get;
            set;
        }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern int[] PercentGroupSizes
        {
            get;
            set;
        }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern int PercentDecimalDigits
        {
            get;
            set;
        }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern string PercentDecimalSeparator
        {
            get;
            set;
        }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern string PercentGroupSeparator
        {
            get;
            set;
        }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern int PercentPositivePattern
        {
            get;
            set;
        }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern int PercentNegativePattern
        {
            get;
            set;
        }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern string CurrencySymbol
        {
            get;
            set;
        }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern int[] CurrencyGroupSizes
        {
            get;
            set;
        }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern int CurrencyDecimalDigits
        {
            get;
            set;
        }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern string CurrencyDecimalSeparator
        {
            get;
            set;
        }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern string CurrencyGroupSeparator
        {
            get;
            set;
        }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern int CurrencyPositivePattern
        {
            get;
            set;
        }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern int CurrencyNegativePattern
        {
            get;
            set;
        }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern int[] NumberGroupSizes
        {
            get;
            set;
        }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern int NumberDecimalDigits
        {
            get;
            set;
        }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern string NumberDecimalSeparator
        {
            get;
            set;
        }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern string NumberGroupSeparator
        {
            get;
            set;
        }

        public extern object GetFormat(Type formatType);

        public extern object Clone();

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public static extern NumberFormatInfo CurrentInfo
        {
            get;
        }
    }
}