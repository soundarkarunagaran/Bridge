namespace System
{
    [Bridge.External]
    [Bridge.IgnoreGeneric]
    [Bridge.IgnoreCast]
    [Bridge.Name("System.Object")]
    public sealed class Tuple<T1>
    {
        [Bridge.Template("{ item1: {item1} }")]
        public extern Tuple(T1 item1);

        public extern T1 Item1
        {
            [Bridge.Template("item1")]
            get;
        }

        [Bridge.Template("Bridge.objectEquals({this}, {o})")]
        public override extern bool Equals(object o);

        [Bridge.Template("Bridge.getHashCode({this}, false, true)")]
        public override extern int GetHashCode();
    }

    [Bridge.External]
    [Bridge.IgnoreGeneric]
    [Bridge.IgnoreCast]
    [Bridge.Name("System.Object")]
    public sealed class Tuple<T1, T2>
    {
        [Bridge.Template("{ item1: {item1}, item2: {item2} }")]
        public extern Tuple(T1 item1, T2 item2);

        public extern T1 Item1
        {
            [Bridge.Template("item1")]
            get;
        }

        public extern T2 Item2
        {
            [Bridge.Template("item2")]
            get;
        }

        [Bridge.Template("Bridge.objectEquals({this}, {o})")]
        public override extern bool Equals(object o);

        [Bridge.Template("Bridge.getHashCode({this}, false, true)")]
        public override extern int GetHashCode();
    }

    [Bridge.External]
    [Bridge.IgnoreGeneric]
    [Bridge.IgnoreCast]
    [Bridge.Name("System.Object")]
    public sealed class Tuple<T1, T2, T3>
    {
        [Bridge.Template("{ item1: {item1}, item2: {item2}, item3: {item3} }")]
        public extern Tuple(T1 item1, T2 item2, T3 item3);

        public extern T1 Item1
        {
            [Bridge.Template("item1")]
            get;
        }

        public extern T2 Item2
        {
            [Bridge.Template("item2")]
            get;
        }

        public extern T3 Item3
        {
            [Bridge.Template("item3")]
            get;
        }

        [Bridge.Template("Bridge.objectEquals({this}, {o})")]
        public override extern bool Equals(object o);

        [Bridge.Template("Bridge.getHashCode({this}, false, true)")]
        public override extern int GetHashCode();
    }

    [Bridge.External]
    [Bridge.IgnoreGeneric]
    [Bridge.IgnoreCast]
    [Bridge.Name("System.Object")]
    public sealed class Tuple<T1, T2, T3, T4>
    {
        [Bridge.Template("{ item1: {item1}, item2: {item2}, item3: {item3}, item4: {item4} }")]
        public extern Tuple(T1 item1, T2 item2, T3 item3, T4 item4);

        public extern T1 Item1
        {
            [Bridge.Template("item1")]
            get;
        }

        public extern T2 Item2
        {
            [Bridge.Template("item2")]
            get;
        }

        public extern T3 Item3
        {
            [Bridge.Template("item3")]
            get;
        }

        public extern T4 Item4
        {
            [Bridge.Template("item4")]
            get;
        }

        [Bridge.Template("Bridge.objectEquals({this}, {o})")]
        public override extern bool Equals(object o);

        [Bridge.Template("Bridge.getHashCode({this}, false, true)")]
        public override extern int GetHashCode();
    }

    [Bridge.External]
    [Bridge.IgnoreGeneric]
    [Bridge.IgnoreCast]
    [Bridge.Name("System.Object")]
    public sealed class Tuple<T1, T2, T3, T4, T5>
    {
        [Bridge.Template("{ item1: {item1}, item2: {item2}, item3: {item3}, item4: {item4}, item5: {item5} }")]
        public extern Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5);

        public extern T1 Item1
        {
            [Bridge.Template("item1")]
            get;
        }

        public extern T2 Item2
        {
            [Bridge.Template("item2")]
            get;
        }

        public extern T3 Item3
        {
            [Bridge.Template("item3")]
            get;
        }

        public extern T4 Item4
        {
            [Bridge.Template("item4")]
            get;
        }

        public extern T5 Item5
        {
            [Bridge.Template("item5")]
            get;
        }

        [Bridge.Template("Bridge.objectEquals({this}, {o})")]
        public override extern bool Equals(object o);

        [Bridge.Template("Bridge.getHashCode({this}, false, true)")]
        public override extern int GetHashCode();
    }

    [Bridge.External]
    [Bridge.IgnoreGeneric]
    [Bridge.IgnoreCast]
    [Bridge.Name("System.Object")]
    public sealed class Tuple<T1, T2, T3, T4, T5, T6>
    {
        [Bridge.Template("{ item1: {item1}, item2: {item2}, item3: {item3}, item4: {item4}, item5: {item5}, item6: {item6} }")]
        public extern Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6);

        public extern T1 Item1
        {
            [Bridge.Template("item1")]
            get;
        }

        public extern T2 Item2
        {
            [Bridge.Template("item2")]
            get;
        }

        public extern T3 Item3
        {
            [Bridge.Template("item3")]
            get;
        }

        public extern T4 Item4
        {
            [Bridge.Template("item4")]
            get;
        }

        public extern T5 Item5
        {
            [Bridge.Template("item5")]
            get;
        }

        public extern T6 Item6
        {
            [Bridge.Template("item6")]
            get;
        }

        [Bridge.Template("Bridge.objectEquals({this}, {o})")]
        public override extern bool Equals(object o);

        [Bridge.Template("Bridge.getHashCode({this}, false, true)")]
        public override extern int GetHashCode();
    }

    [Bridge.External]
    [Bridge.IgnoreGeneric]
    [Bridge.IgnoreCast]
    [Bridge.Name("System.Object")]
    public sealed class Tuple<T1, T2, T3, T4, T5, T6, T7>
    {
        [Bridge.Template("{ item1: {item1}, item2: {item2}, item3: {item3}, item4: {item4}, item5: {item5}, item6: {item6}, item7: {item7} }")]
        public extern Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7);

        public extern T1 Item1
        {
            [Bridge.Template("item1")]
            get;
        }

        public extern T2 Item2
        {
            [Bridge.Template("item2")]
            get;
        }

        public extern T3 Item3
        {
            [Bridge.Template("item3")]
            get;
        }

        public extern T4 Item4
        {
            [Bridge.Template("item4")]
            get;
        }

        public extern T5 Item5
        {
            [Bridge.Template("item5")]
            get;
        }

        public extern T6 Item6
        {
            [Bridge.Template("item6")]
            get;
        }

        public extern T7 Item7
        {
            [Bridge.Template("item7")]
            get;
        }

        [Bridge.Template("Bridge.objectEquals({this}, {o})")]
        public override extern bool Equals(object o);

        [Bridge.Template("Bridge.getHashCode({this}, false, true)")]
        public override extern int GetHashCode();
    }

    [Bridge.External]
    [Bridge.IgnoreGeneric]
    [Bridge.IgnoreCast]
    [Bridge.Name("System.Object")]
    public sealed class Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>
    {
        [Bridge.Template("{ item1: {item1}, item2: {item2}, item3: {item3}, item4: {item4}, item5: {item5}, item6: {item6}, item7: {item7}, rest: {rest} }")]
        public extern Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest);

        public extern T1 Item1
        {
            [Bridge.Template("item1")]
            get;
        }

        public extern T2 Item2
        {
            [Bridge.Template("item2")]
            get;
        }

        public extern T3 Item3
        {
            [Bridge.Template("item3")]
            get;
        }

        public extern T4 Item4
        {
            [Bridge.Template("item4")]
            get;
        }

        public extern T5 Item5
        {
            [Bridge.Template("item5")]
            get;
        }

        public extern T6 Item6
        {
            [Bridge.Template("item6")]
            get;
        }

        public extern T7 Item7
        {
            [Bridge.Template("item7")]
            get;
        }

        public extern TRest Rest
        {
            [Bridge.Template("rest")]
            get;
        }

        [Bridge.Template("Bridge.objectEquals({this}, {o})")]
        public override extern bool Equals(object o);

        [Bridge.Template("Bridge.getHashCode({this}, false, true)")]
        public override extern int GetHashCode();
    }

    [Bridge.External]
    [Bridge.IgnoreGeneric]
    [Bridge.IgnoreCast]
    [Bridge.Name("System.Object")]
    public static class Tuple
    {
        [Bridge.Template("{ item1: {item1} }")]
        public static extern Tuple<T1> Create<T1>(T1 item1);

        [Bridge.Template("{ item1: {item1}, item2: {item2} }")]
        public static extern Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2);

        [Bridge.Template("{ item1: {item1}, item2: {item2}, item3: {item3} }")]
        public static extern Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3);

        [Bridge.Template("{ item1: {item1}, item2: {item2}, item3: {item3}, item4: {item4} }")]
        public static extern Tuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4);

        [Bridge.Template("{ item1: {item1}, item2: {item2}, item3: {item3}, item4: {item4}, item5: {item5} }")]
        public static extern Tuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5);

        [Bridge.Template("{ item1: {item1}, item2: {item2}, item3: {item3}, item4: {item4}, item5: {item5}, item6: {item6} }")]
        public static extern Tuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6);

        [Bridge.Template("{ item1: {item1}, item2: {item2}, item3: {item3}, item4: {item4}, item5: {item5}, item6: {item6}, item7: {item7} }")]
        public static extern Tuple<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7);

        [Bridge.Template("{ item1: {item1}, item2: {item2}, item3: {item3}, item4: {item4}, item5: {item5}, item6: {item6}, item7: {item7}, rest: {rest} }")]
        public static extern Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> Create<T1, T2, T3, T4, T5, T6, T7, TRest>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest);
    }
}