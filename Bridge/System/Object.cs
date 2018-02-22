
namespace System
{
    [Bridge.External]
    [Bridge.IgnoreCast]
    [Bridge.Constructor("{ }")]
    public class Object
    {
        public virtual extern object this[string name]
        {
            [Bridge.External]
            get;
            [Bridge.External]
            set;
        }

        [Bridge.Template("Bridge.toString({this})")]
        public virtual extern string ToString();

        public virtual extern string ToLocaleString();

        public virtual extern object ValueOf();

        public virtual extern bool HasOwnProperty(object v);

        public virtual extern bool IsPrototypeOf(object v);

        public virtual extern bool PropertyIsEnumerable(object v);

        [Bridge.Template("<self>{this:type}")]
        public extern Type GetType();

        [Bridge.Template("Bridge.referenceEquals({a}, {b})")]
        public static extern bool ReferenceEquals(object a, object b);

        [Bridge.Template("Bridge.equals({this}, {o})")]
        public virtual extern bool Equals(object o);

        [Bridge.Template("Bridge.equals({a}, {b})")]
        public static extern bool Equals(object a, object b);

        [Bridge.Template("Bridge.getHashCode({this})")]
        public virtual extern int GetHashCode();

        [Bridge.Template("Object.keys({obj})")]
        [Bridge.Unbox(true)]
        public static extern string[] Keys(object obj);

        [Bridge.Template("Object.getOwnPropertyNames({obj})")]
        [Bridge.Unbox(true)]
        public static extern string[] GetOwnPropertyNames(object obj);

        [Bridge.Template("{T}.prototype")]
        public static extern dynamic GetPrototype<T>();

        public readonly Type Constructor;

#pragma warning disable 169
        private readonly Type ctor;
#pragma warning restore 169

        [Bridge.Template("{this}")]
        public virtual extern dynamic ToDynamic();
    }

    [Bridge.External]
    public static class ObjectExtensions
    {
        [Bridge.Template("{0}")]
        [Bridge.Unbox(true)]
        public static extern T As<T>(this object obj);

        [Bridge.Template("Bridge.cast({obj}, {T})")]
        public static extern T Cast<T>(this object obj);

        [Bridge.Template("Bridge.as({obj}, {T})")]
        public static extern T TryCast<T>(this object obj) where T : class;

        [Bridge.Template("Bridge.is({obj}, {T})")]
        public static extern bool Is<T>(this object obj);
    }
}