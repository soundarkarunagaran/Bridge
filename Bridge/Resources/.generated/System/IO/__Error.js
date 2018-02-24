    Bridge.define("System.IO.__Error", {
        statics: {
            methods: {
                EndOfFile: function () {
                    throw new System.IO.EndOfStreamException.$ctor1("IO.EOF_ReadBeyondEOF");
                },
                FileNotOpen: function () {
                    throw new System.Exception("ObjectDisposed_FileClosed");
                },
                StreamIsClosed: function () {
                    throw new System.Exception("ObjectDisposed_StreamClosed");
                },
                MemoryStreamNotExpandable: function () {
                    throw new System.NotSupportedException("NotSupported_MemStreamNotExpandable");
                },
                ReaderClosed: function () {
                    throw new System.Exception("ObjectDisposed_ReaderClosed");
                },
                ReadNotSupported: function () {
                    throw new System.NotSupportedException("NotSupported_UnreadableStream");
                },
                SeekNotSupported: function () {
                    throw new System.NotSupportedException("NotSupported_UnseekableStream");
                },
                WrongAsyncResult: function () {
                    throw new System.ArgumentException("Arg_WrongAsyncResult");
                },
                EndReadCalledTwice: function () {
                    // Should ideally be InvalidOperationExc but we can't maitain parity with Stream and FileStream without some work
                    throw new System.ArgumentException("InvalidOperation_EndReadCalledMultiple");
                },
                EndWriteCalledTwice: function () {
                    // Should ideally be InvalidOperationExc but we can't maintain parity with Stream and FileStream without some work
                    throw new System.ArgumentException("InvalidOperation_EndWriteCalledMultiple");
                },
                WriteNotSupported: function () {
                    throw new System.NotSupportedException("NotSupported_UnwritableStream");
                },
                WriterClosed: function () {
                    throw new System.Exception("ObjectDisposed_WriterClosed");
                }
            }
        }
    });
