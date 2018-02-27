namespace System.Globalization
{
    [Bridge.External]
    [Bridge.Reflectable]
    public class CultureNotFoundException : ArgumentException, Bridge.IBridgeClass
    {
        public extern CultureNotFoundException();

        [Bridge.Template("new System.Globalization.CultureNotFoundException(null, null, {message})")]
        public extern CultureNotFoundException(string message);

        [Bridge.Template("new System.Globalization.CultureNotFoundException(null, null, {message}, {innerException})")]
        public extern CultureNotFoundException(string message, Exception innerException);

        [Bridge.Template("new System.Globalization.CultureNotFoundException({paramName}, null, {message})")]
        public extern CultureNotFoundException(string paramName, string message);

        [Bridge.Template("new System.Globalization.CultureNotFoundException(null, {invalidCultureName}, {message}, {innerException})")]
        public extern CultureNotFoundException(string message, string invalidCultureName, Exception innerException);

        [Bridge.Template("new System.Globalization.CultureNotFoundException({paramName}, {invalidCultureName}, {message})")]
        public extern CultureNotFoundException(string paramName, string invalidCultureName, string message);

        [Bridge.Template("new System.Globalization.CultureNotFoundException(null, null, {message}, {innerException}, {invalidCultureId})")]
        public extern CultureNotFoundException(string message, int invalidCultureId, Exception innerException);

        [Bridge.Template("new System.Globalization.CultureNotFoundException({paramName}, null, {message}, null, {invalidCultureId})")]
        public extern CultureNotFoundException(string paramName, int invalidCultureId, string message);

        public extern string InvalidCultureName
        {
            [Bridge.Template("getInvalidCultureName()")]
            get;
        }

        public extern int? InvalidCultureId
        {
            [Bridge.Template("getInvalidCultureId()")]
            get;
        }
    }
}