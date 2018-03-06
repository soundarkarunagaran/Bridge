    Bridge.define("System.DateTimeOffset", {
        inherits: function () { return [System.IComparable,System.IFormattable,System.Runtime.Serialization.ISerializable,System.Runtime.Serialization.IDeserializationCallback,System.IComparable$1(System.DateTimeOffset),System.IEquatable$1(System.DateTimeOffset)]; },
        $kind: "struct",
        statics: {
            fields: {
                MaxOffset: System.Int64(0),
                MinOffset: System.Int64(0),
                UnixEpochTicks: System.Int64(0),
                UnixEpochSeconds: System.Int64(0),
                UnixEpochMilliseconds: System.Int64(0),
                minValue: null,
                maxValue: null
            },
            props: {
                Now: {
                    get: function () {
                        return new System.DateTimeOffset.$ctor1(System.DateTime.getNow());
                    }
                },
                UtcNow: {
                    get: function () {
                        return new System.DateTimeOffset.$ctor1(System.DateTime.getUtcNow());
                    }
                }
            },
            ctors: {
                init: function () {
                    this.minValue = new System.DateTimeOffset();
                    this.maxValue = new System.DateTimeOffset();
                    this.MaxOffset = System.Int64([1488826368,117]);
                    this.MinOffset = System.Int64([-1488826368,-118]);
                    this.UnixEpochTicks = System.Int64([-139100160,144670709]);
                    this.UnixEpochSeconds = System.Int64([2006054656,14]);
                    this.UnixEpochMilliseconds = System.Int64([304928768,14467]);
                    this.minValue = new System.DateTimeOffset.$ctor5(System.DateTime.MinTicks, System.TimeSpan.zero);
                    this.maxValue = new System.DateTimeOffset.$ctor5(System.DateTime.MaxTicks, System.TimeSpan.zero);
                }
            },
            methods: {
                compare: function (first, second) {
                    return Bridge.compare(first.UtcDateTime, second.UtcDateTime);
                },
                equals: function (first, second) {
                    return Bridge.equalsT(first.UtcDateTime, second.UtcDateTime);
                },
                fromFileTime: function (fileTime) {
                    return new System.DateTimeOffset.$ctor1(System.DateTime.FromFileTime(fileTime));
                },
                fromUnixTimeSeconds: function (seconds) {
                    var MinSeconds = System.Int64([-2006054656,-15]);
                    var MaxSeconds = System.Int64([-769665,58]);

                    if (seconds.lt(MinSeconds) || seconds.gt(MaxSeconds)) {
                        throw new System.ArgumentOutOfRangeException("seconds", System.String.format(System.Environment.GetResourceString("ArgumentOutOfRange_Range"), MinSeconds, MaxSeconds));
                    }

                    var ticks = seconds.mul(System.Int64(10000000)).add(System.DateTimeOffset.UnixEpochTicks);
                    return new System.DateTimeOffset.$ctor5(ticks, System.TimeSpan.zero);
                },
                fromUnixTimeMilliseconds: function (milliseconds) {
                    var MinMilliseconds = System.Int64([-304928768,-14468]);
                    var MaxMilliseconds = System.Int64([-769664001,58999]);

                    if (milliseconds.lt(MinMilliseconds) || milliseconds.gt(MaxMilliseconds)) {
                        throw new System.ArgumentOutOfRangeException("milliseconds", System.String.format(System.Environment.GetResourceString("ArgumentOutOfRange_Range"), MinMilliseconds, MaxMilliseconds));
                    }

                    var ticks = milliseconds.mul(System.Int64(10000)).add(System.DateTimeOffset.UnixEpochTicks);
                    return new System.DateTimeOffset.$ctor5(ticks, System.TimeSpan.zero);
                },
                parse: function (input) {
                    var offset = { };
                    var dateResult = System.DateTimeParse.parse$1(input, System.Globalization.DateTimeFormatInfo.currentInfo, 0, offset);
                    return new System.DateTimeOffset.$ctor5(System.DateTime.getTicks(dateResult), offset.v);
                },
                parse$1: function (input, formatProvider) {
                    return System.DateTimeOffset.parse$2(input, formatProvider, 0);
                },
                parse$2: function (input, formatProvider, styles) {
                    throw System.NotImplemented.ByDesign;
                    // TODO: NotSupported [DateTimeFormatInfo]
                    //styles = ValidateStyles(styles, "styles");
                    //TimeSpan offset;
                    //DateTime dateResult = DateTimeParse.Parse(input,
                    //                                          DateTimeFormatInfo.GetInstance(formatProvider),
                    //                                          styles,
                    //                                          out offset);
                    //return new DateTimeOffset(dateResult.Ticks, offset);
                },
                parseExact: function (input, format, formatProvider) {
                    return System.DateTimeOffset.parseExact$1(input, format, formatProvider, 0);
                },
                parseExact$1: function (input, format, formatProvider, styles) {
                    throw System.NotImplemented.ByDesign;
                    //TODO: NotSupported [DateTimeFormatInfo]
                    //styles = ValidateStyles(styles, "styles");
                    //TimeSpan offset;
                    //DateTime dateResult = DateTimeParse.ParseExact(input,
                    //                                               format,
                    //                                               DateTimeFormatInfo.GetInstance(formatProvider),
                    //                                               styles,
                    //                                               out offset);
                    //return new DateTimeOffset(dateResult.Ticks, offset);
                },
                tryParse: function (input, result) {
                    var offset = { };
                    var dateResult = { };
                    var parsed = System.DateTimeParse.tryParse$1(input, System.Globalization.DateTimeFormatInfo.currentInfo, 0, dateResult, offset);
                    result.v = new System.DateTimeOffset.$ctor5(System.DateTime.getTicks(dateResult.v), offset.v);
                    return parsed;
                },
                validateOffset: function (offset) {
                    var ticks = offset.getTicks();
                    if (ticks.mod(System.Int64(600000000)).ne(System.Int64(0))) {
                        throw new System.ArgumentException(System.Environment.GetResourceString("Argument_OffsetPrecision"), "offset");
                    }
                    if (ticks.lt(System.DateTimeOffset.MinOffset) || ticks.gt(System.DateTimeOffset.MaxOffset)) {
                        throw new System.ArgumentOutOfRangeException("offset", System.Environment.GetResourceString("Argument_OffsetOutOfRange"));
                    }
                    return System.Int64.clip16(offset.getTicks().div(System.Int64(600000000)));
                },
                validateDate: function (dateTime, offset) {
                    // The key validation is that both the UTC and clock times fit. The clock time is validated
                    // by the DateTime constructor.
                    // This operation cannot overflow because offset should have already been validated to be within
                    // 14 hours and the DateTime instance is more than that distance from the boundaries of Int64.
                    var utcTicks = System.DateTime.getTicks(dateTime).sub(offset.getTicks());
                    if (utcTicks.lt(System.DateTime.MinTicks) || utcTicks.gt(System.DateTime.MaxTicks)) {
                        throw new System.ArgumentOutOfRangeException("offset", System.Environment.GetResourceString("Argument_UTCOutOfRange"));
                    }
                    // make sure the Kind is set to Unspecified
                    //
                    return System.DateTime.create$2(utcTicks, 0);
                },
                op_Implicit: function (dateTime) {
                    return new System.DateTimeOffset.$ctor1(dateTime);
                },
                op_Addition: function (dateTimeOffset, timeSpan) {
                    return new System.DateTimeOffset.$ctor2(System.DateTime.adddt(dateTimeOffset.ClockDateTime, timeSpan), dateTimeOffset.Offset);
                },
                op_Subtraction: function (dateTimeOffset, timeSpan) {
                    return new System.DateTimeOffset.$ctor2(System.DateTime.subdt(dateTimeOffset.ClockDateTime, timeSpan), dateTimeOffset.Offset);
                },
                op_Subtraction$1: function (left, right) {
                    return System.DateTime.subdd(left.UtcDateTime, right.UtcDateTime);
                },
                op_Equality: function (left, right) {
                    return Bridge.equals(left.UtcDateTime, right.UtcDateTime);
                },
                op_Inequality: function (left, right) {
                    return !Bridge.equals(left.UtcDateTime, right.UtcDateTime);
                },
                op_LessThan: function (left, right) {
                    return System.DateTime.lt(left.UtcDateTime, right.UtcDateTime);
                },
                op_LessThanOrEqual: function (left, right) {
                    return System.DateTime.lte(left.UtcDateTime, right.UtcDateTime);
                },
                op_GreaterThan: function (left, right) {
                    return System.DateTime.gt(left.UtcDateTime, right.UtcDateTime);
                },
                op_GreaterThanOrEqual: function (left, right) {
                    return System.DateTime.gte(left.UtcDateTime, right.UtcDateTime);
                },
                getDefaultValue: function () { return new System.DateTimeOffset(); }
            }
        },
        fields: {
            m_dateTime: null,
            m_offsetMinutes: 0
        },
        props: {
            DateTime: {
                get: function () {
                    return this.ClockDateTime;
                }
            },
            UtcDateTime: {
                get: function () {
                    return System.DateTime.specifyKind(this.m_dateTime, 1);
                }
            },
            LocalDateTime: {
                get: function () {
                    return System.DateTime.toLocalTime(this.UtcDateTime);
                }
            },
            ClockDateTime: {
                get: function () {
                    return System.DateTime.create$2(System.DateTime.getTicks((System.DateTime.adddt(this.m_dateTime, this.Offset))), 0);
                }
            },
            Date: {
                get: function () {
                    return System.DateTime.getDate(this.ClockDateTime);
                }
            },
            Day: {
                get: function () {
                    return System.DateTime.getDay(this.ClockDateTime);
                }
            },
            DayOfWeek: {
                get: function () {
                    return System.DateTime.getDayOfWeek(this.ClockDateTime);
                }
            },
            DayOfYear: {
                get: function () { // leap year
                    return System.DateTime.getDayOfYear(this.ClockDateTime);
                }
            },
            Hour: {
                get: function () {
                    return System.DateTime.getHour(this.ClockDateTime);
                }
            },
            Millisecond: {
                get: function () {
                    return System.DateTime.getMillisecond(this.ClockDateTime);
                }
            },
            Minute: {
                get: function () {
                    return System.DateTime.getMinute(this.ClockDateTime);
                }
            },
            Month: {
                get: function () {
                    return System.DateTime.getMonth(this.ClockDateTime);
                }
            },
            Offset: {
                get: function () {
                    return new System.TimeSpan(0, this.m_offsetMinutes, 0);
                }
            },
            Second: {
                get: function () {
                    return System.DateTime.getSecond(this.ClockDateTime);
                }
            },
            Ticks: {
                get: function () {
                    return System.DateTime.getTicks(this.ClockDateTime);
                }
            },
            UtcTicks: {
                get: function () {
                    return System.DateTime.getTicks(this.UtcDateTime);
                }
            },
            TimeOfDay: {
                get: function () {
                    return System.DateTime.getTimeOfDay(this.ClockDateTime);
                }
            },
            Year: {
                get: function () {
                    return System.DateTime.getYear(this.ClockDateTime);
                }
            }
        },
        alias: [
            "compareTo", ["System$IComparable$1$System$DateTimeOffset$compareTo", "System$IComparable$1$compareTo"],
            "equalsT", "System$IEquatable$1$System$DateTimeOffset$equalsT",
            "format", "System$IFormattable$format"
        ],
        ctors: {
            init: function () {
                this.m_dateTime = System.DateTime.getDefaultValue();
            },
            $ctor5: function (ticks, offset) {
                this.$initialize();
                this.m_offsetMinutes = System.DateTimeOffset.validateOffset(offset);
                // Let the DateTime constructor do the range checks
                var dateTime = System.DateTime.create$2(ticks);
                this.m_dateTime = System.DateTimeOffset.validateDate(dateTime, offset);
            },
            $ctor1: function (dateTime) {
                this.$initialize();
                var offset;
                if (System.DateTime.getKind(dateTime) !== 1) {
                    // Local and Unspecified are both treated as Local
                    offset = System.DateTime.subdd(System.DateTime.getNow(), System.DateTime.getUtcNow());

                    // TODO: Revised [TimeZoneInfo not supported]
                    //offset = TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
                } else {
                    offset = new System.TimeSpan(System.Int64(0));
                }
                this.m_offsetMinutes = System.DateTimeOffset.validateOffset(offset);
                this.m_dateTime = System.DateTimeOffset.validateDate(dateTime, offset);
            },
            $ctor2: function (dateTime, offset) {
                this.$initialize();
                if (System.DateTime.getKind(dateTime) === 2) {
                    // TODO: Revised [TimeZoneInfo not supported]
                    //if (offset != TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime)) {
                    if (System.TimeSpan.neq(offset, (System.DateTime.subdd(System.DateTime.getNow(), System.DateTime.getUtcNow())))) {
                        throw new System.ArgumentException(System.Environment.GetResourceString("Argument_OffsetLocalMismatch"), "offset");
                    }
                } else if (System.DateTime.getKind(dateTime) === 1) {
                    if (System.TimeSpan.neq(offset, System.TimeSpan.zero)) {
                        throw new System.ArgumentException(System.Environment.GetResourceString("Argument_OffsetUtcMismatch"), "offset");
                    }
                }
                this.m_offsetMinutes = System.DateTimeOffset.validateOffset(offset);
                this.m_dateTime = System.DateTimeOffset.validateDate(dateTime, offset);
            },
            $ctor4: function (year, month, day, hour, minute, second, offset) {
                this.$initialize();
                this.m_offsetMinutes = System.DateTimeOffset.validateOffset(offset);
                this.m_dateTime = System.DateTimeOffset.validateDate(System.DateTime.create(year, month, day, hour, minute, second), offset);
            },
            $ctor3: function (year, month, day, hour, minute, second, millisecond, offset) {
                this.$initialize();
                this.m_offsetMinutes = System.DateTimeOffset.validateOffset(offset);
                this.m_dateTime = System.DateTimeOffset.validateDate(System.DateTime.create(year, month, day, hour, minute, second, millisecond), offset);
            },
            ctor: function () {
                this.$initialize();
            }
        },
        methods: {
            toOffset: function (offset) {
                return new System.DateTimeOffset.$ctor5(System.DateTime.getTicks((System.DateTime.adddt(this.m_dateTime, offset))), offset);
            },
            add: function (timeSpan) {
                return new System.DateTimeOffset.$ctor2(System.DateTime.add(this.ClockDateTime, timeSpan), this.Offset);
            },
            addDays: function (days) {
                return new System.DateTimeOffset.$ctor2(System.DateTime.addDays(this.ClockDateTime, days), this.Offset);
            },
            addHours: function (hours) {
                return new System.DateTimeOffset.$ctor2(System.DateTime.addHours(this.ClockDateTime, hours), this.Offset);
            },
            addMilliseconds: function (milliseconds) {
                return new System.DateTimeOffset.$ctor2(System.DateTime.addMilliseconds(this.ClockDateTime, milliseconds), this.Offset);
            },
            addMinutes: function (minutes) {
                return new System.DateTimeOffset.$ctor2(System.DateTime.addMinutes(this.ClockDateTime, minutes), this.Offset);
            },
            addMonths: function (months) {
                return new System.DateTimeOffset.$ctor2(System.DateTime.addMonths(this.ClockDateTime, months), this.Offset);
            },
            addSeconds: function (seconds) {
                return new System.DateTimeOffset.$ctor2(System.DateTime.addSeconds(this.ClockDateTime, seconds), this.Offset);
            },
            addTicks: function (ticks) {
                return new System.DateTimeOffset.$ctor2(System.DateTime.addTicks(this.ClockDateTime, ticks), this.Offset);
            },
            addYears: function (years) {
                return new System.DateTimeOffset.$ctor2(System.DateTime.addYears(this.ClockDateTime, years), this.Offset);
            },
            System$IComparable$compareTo: function (obj) {
                if (obj == null) {
                    return 1;
                }
                if (!(Bridge.is(obj, System.DateTimeOffset))) {
                    throw new System.ArgumentException(System.Environment.GetResourceString("Arg_MustBeDateTimeOffset"));
                }

                var objUtc = System.Nullable.getValue(Bridge.cast(Bridge.unbox(obj), System.DateTimeOffset)).UtcDateTime;
                var utc = this.UtcDateTime;
                if (System.DateTime.gt(utc, objUtc)) {
                    return 1;
                }
                if (System.DateTime.lt(utc, objUtc)) {
                    return -1;
                }
                return 0;
            },
            compareTo: function (other) {
                var otherUtc = other.UtcDateTime;
                var utc = this.UtcDateTime;
                if (System.DateTime.gt(utc, otherUtc)) {
                    return 1;
                }
                if (System.DateTime.lt(utc, otherUtc)) {
                    return -1;
                }
                return 0;
            },
            equals: function (obj) {
                if (Bridge.is(obj, System.DateTimeOffset)) {
                    return Bridge.equalsT(this.UtcDateTime, System.Nullable.getValue(Bridge.cast(Bridge.unbox(obj), System.DateTimeOffset)).UtcDateTime);
                }
                return false;
            },
            equalsT: function (other) {
                return Bridge.equalsT(this.UtcDateTime, other.UtcDateTime);
            },
            equalsExact: function (other) {
                //
                // returns true when the ClockDateTime, Kind, and Offset match
                //
                // currently the Kind should always be Unspecified, but there is always the possibility that a future version
                // of DateTimeOffset overloads the Kind field
                //
                return (Bridge.equals(this.ClockDateTime, other.ClockDateTime) && System.TimeSpan.eq(this.Offset, other.Offset) && System.DateTime.getKind(this.ClockDateTime) === System.DateTime.getKind(other.ClockDateTime));
            },
            System$Runtime$Serialization$IDeserializationCallback$onDeserialization: function (sender) {
                try {
                    this.m_offsetMinutes = System.DateTimeOffset.validateOffset(this.Offset);
                    this.m_dateTime = System.DateTimeOffset.validateDate(this.ClockDateTime, this.Offset);
                }
                catch ($e1) {
                    $e1 = System.Exception.create($e1);
                    var e;
                    if (Bridge.is($e1, System.ArgumentException)) {
                        e = $e1;
                        throw new System.Runtime.Serialization.SerializationException.$ctor2(System.Environment.GetResourceString("Serialization_InvalidData"), e);
                    } else {
                        throw $e1;
                    }
                }
            },
            getHashCode: function () {
                return Bridge.getHashCode(this.UtcDateTime);
            },
            subtract$1: function (value) {
                return System.DateTime.subdd(this.UtcDateTime, value.UtcDateTime);
            },
            subtract: function (value) {
                return new System.DateTimeOffset.$ctor2(System.DateTime.subtract(this.ClockDateTime, value), this.Offset);
            },
            toFileTime: function () {
                return System.DateTime.ToFileTime(this.UtcDateTime);
            },
            toUnixTimeSeconds: function () {
                // Truncate sub-second precision before offsetting by the Unix Epoch to avoid
                // the last digit being off by one for dates that result in negative Unix times.
                //
                // For example, consider the DateTimeOffset 12/31/1969 12:59:59.001 +0
                //   ticks            = 621355967990010000
                //   ticksFromEpoch   = ticks - UnixEpochTicks                   = -9990000
                //   secondsFromEpoch = ticksFromEpoch / TimeSpan.TicksPerSecond = 0
                //
                // Notice that secondsFromEpoch is rounded *up* by the truncation induced by integer division,
                // whereas we actually always want to round *down* when converting to Unix time. This happens
                // automatically for positive Unix time values. Now the example becomes:
                //   seconds          = ticks / TimeSpan.TicksPerSecond = 62135596799
                //   secondsFromEpoch = seconds - UnixEpochSeconds      = -1
                //
                // In other words, we want to consistently round toward the time 1/1/0001 00:00:00,
                // rather than toward the Unix Epoch (1/1/1970 00:00:00).
                var seconds = System.DateTime.getTicks(this.UtcDateTime).div(System.Int64(10000000));
                return seconds.sub(System.DateTimeOffset.UnixEpochSeconds);
            },
            toUnixTimeMilliseconds: function () {
                // Truncate sub-millisecond precision before offsetting by the Unix Epoch to avoid
                // the last digit being off by one for dates that result in negative Unix times
                var milliseconds = System.DateTime.getTicks(this.UtcDateTime).div(System.Int64(10000));
                return milliseconds.sub(System.DateTimeOffset.UnixEpochMilliseconds);
            },
            toLocalTime: function () {
                return this.toLocalTime$1(false);
            },
            toLocalTime$1: function (throwOnOverflow) {
                return new System.DateTimeOffset.$ctor1(System.DateTime.toLocalTime(this.UtcDateTime, throwOnOverflow));
            },
            toString: function () {
                return System.DateTime.format(this.DateTime);
                // TODO: NotSupported [DateTimeFormatInfo]
                //Contract.Ensures(Contract.Result<String>() != null);
                //return DateTimeFormat.Format(ClockDateTime, null, DateTimeFormatInfo.CurrentInfo, Offset);
            },
            toString$2: function (format) {
                return System.DateTime.format(this.DateTime, format);
                // TODO: NotSupported [DateTimeFormatInfo]
                //Contract.Ensures(Contract.Result<String>() != null);
                //return DateTimeFormat.Format(ClockDateTime, format, DateTimeFormatInfo.CurrentInfo, Offset);
            },
            toString$1: function (formatProvider) {
                return System.DateTime.format(this.DateTime, null, formatProvider);
                // TODO: NotSupported [DateTimeFormatInfo]
                //Contract.Ensures(Contract.Result<String>() != null);
                //return DateTimeFormat.Format(ClockDateTime, null, DateTimeFormatInfo.GetInstance(formatProvider), Offset);
            },
            format: function (format, formatProvider) {
                return System.DateTime.format(this.DateTime, format, formatProvider);

                // TODO: NotSupported [DateTimeFormatInfo]
                //Contract.Ensures(Contract.Result<String>() != null);
                //return DateTimeFormat.Format(ClockDateTime, format, DateTimeFormatInfo.GetInstance(formatProvider), Offset);
            },
            toUniversalTime: function () {
                return new System.DateTimeOffset.$ctor1(this.UtcDateTime);
            },
            $clone: function (to) {
                var s = to || new System.DateTimeOffset();
                s.m_dateTime = this.m_dateTime;
                s.m_offsetMinutes = this.m_offsetMinutes;
                return s;
            }
        }
    });
