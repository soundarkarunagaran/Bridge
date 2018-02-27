/*
 * https://github.com/Microsoft/referencesource/blob/master/mscorlib/system/io/seekorigin.cs
 */

namespace System.IO
{
    /// <summary>
    /// Provides seek reference points.  To seek to the end of a stream, call stream.Seek(0, SeekOrigin.End).
    /// </summary>
    [Bridge.Reflectable]
    [Bridge.Convention]
    public enum SeekOrigin
    {
        Begin,
        Current,
        End
    }
}
