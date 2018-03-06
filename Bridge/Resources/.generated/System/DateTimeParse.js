    Bridge.define("System.DateTimeParse", {
        statics: {
            methods: {
                tryParseExact: function (s, format, dtfi, style, result) {
                    return System.DateTime.tryParseExact(s, format, null, result);

                    // TODO: NotSupported
                    //result = DateTime.MinValue;
                    //DateTimeResult resultData = new DateTimeResult();       // The buffer to store the parsing result.
                    //resultData.Init();
                    //if (TryParseExact(s, format, dtfi, style, ref resultData)) {
                    //    result = resultData.parsedDate;
                    //    return true;
                    //}
                    //return false;
                },
                parse: function (s, dtfi, styles) {
                    return System.DateTime.parse(s, dtfi);
                    // TODO: NotSupported
                    //DateTimeResult result = new DateTimeResult();       // The buffer to store the parsing result.
                    //result.Init();
                    //if (TryParse(s, dtfi, styles, ref result))
                    //{
                    //    return result.parsedDate;
                    //}
                    //else
                    //{
                    //    throw GetDateTimeParseException(ref result);
                    //}
                },
                parse$1: function (s, dtfi, styles, offset) {
                    throw System.NotImplemented.ByDesign;

                    // TODO: NotSupported
                    //DateTimeResult result = new DateTimeResult();       // The buffer to store the parsing result.
                    //result.Init();
                    //result.flags |= ParseFlags.CaptureOffset;
                    //if (TryParse(s, dtfi, styles, ref result))
                    //{
                    //    offset = result.timeZoneOffset;
                    //    return result.parsedDate;
                    //}
                    //else
                    //{
                    //    throw GetDateTimeParseException(ref result);
                    //}
                },
                tryParse: function (s, dtfi, styles, result) {
                    return System.DateTime.tryParse(s, null, result);

                    // TODO: NotSupported
                    //result = DateTime.MinValue;
                    //DateTimeResult resultData = new DateTimeResult();       // The buffer to store the parsing result.
                    //resultData.Init();
                    //if (TryParse(s, dtfi, styles, ref resultData)) {
                    //    result = resultData.parsedDate;
                    //    return true;
                    //}
                    //return false;
                },
                tryParse$1: function (s, dtfi, styles, result, offset) {
                    throw System.NotImplemented.ByDesign;
                    // TODO: NotSupported
                    //result = DateTime.MinValue;
                    //offset = TimeSpan.Zero;
                    //DateTimeResult parseResult = new DateTimeResult();       // The buffer to store the parsing result.
                    //parseResult.Init();
                    //parseResult.flags |= ParseFlags.CaptureOffset;
                    //if (TryParse(s, dtfi, styles, ref parseResult)) {
                    //    result = parseResult.parsedDate;
                    //    offset = parseResult.timeZoneOffset;
                    //    return true;
                    //}
                    //return false;
                }
            }
        }
    });
