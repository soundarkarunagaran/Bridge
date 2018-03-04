    Bridge.define("System.Globalization.Calendar", {
        inherits: [System.ICloneable],
        statics: {
            fields: {
                TicksPerMillisecond: System.Int64(0),
                TicksPerSecond: System.Int64(0),
                TicksPerMinute: System.Int64(0),
                TicksPerHour: System.Int64(0),
                TicksPerDay: System.Int64(0),
                MillisPerSecond: 0,
                MillisPerMinute: 0,
                MillisPerHour: 0,
                MillisPerDay: 0,
                DaysPerYear: 0,
                DaysPer4Years: 0,
                DaysPer100Years: 0,
                DaysPer400Years: 0,
                DaysTo10000: 0,
                MaxMillis: System.Int64(0),
                CurrentEra: 0
            },
            ctors: {
                init: function () {
                    this.TicksPerMillisecond = System.Int64(10000);
                    this.TicksPerSecond = System.Int64(10000000);
                    this.TicksPerMinute = System.Int64(600000000);
                    this.TicksPerHour = System.Int64([1640261632,8]);
                    this.TicksPerDay = System.Int64([711573504,201]);
                    this.MillisPerSecond = 1000;
                    this.MillisPerMinute = 60000;
                    this.MillisPerHour = 3600000;
                    this.MillisPerDay = 86400000;
                    this.DaysPerYear = 365;
                    this.DaysPer4Years = 1461;
                    this.DaysPer100Years = 36524;
                    this.DaysPer400Years = 146097;
                    this.DaysTo10000 = 3652059;
                    this.MaxMillis = System.Int64([-464735232,73466]);
                    this.CurrentEra = 0;
                }
            },
            methods: {
                ReadOnly: function (calendar) {
                    if (calendar == null) {
                        throw new System.ArgumentNullException("calendar");
                    }
                    if (calendar.IsReadOnly) {
                        return (calendar);
                    }

                    var clonedCalendar = Bridge.cast((Bridge.clone(calendar)), System.Globalization.Calendar);
                    clonedCalendar.SetReadOnlyState(true);

                    return (clonedCalendar);
                },
                CheckAddResult: function (ticks, minValue, maxValue) {
                    if (ticks.lt(System.DateTime.getTicks(minValue)) || ticks.gt(System.DateTime.getTicks(maxValue))) {
                        throw new System.ArgumentException(System.String.formatProvider(System.Globalization.CultureInfo.invariantCulture, System.SR.Format$1("The result is out of the supported range for this calendar. The result should be between {0} (Gregorian date) and {1} (Gregorian date), inclusive.", Bridge.box(minValue, System.DateTime, System.DateTime.format), Bridge.box(maxValue, System.DateTime, System.DateTime.format)), null));
                    }
                },
                GetSystemTwoDigitYearSetting: function (CalID, defaultYearValue) {
                    // TODO: Revised [Revised to Invarient 2029]
                    //int twoDigitYearMax = CalendarData.GetTwoDigitYearMax(CalID);
                    var twoDigitYearMax = 2029;
                    if (twoDigitYearMax < 0) {
                        twoDigitYearMax = defaultYearValue;
                    }
                    return (twoDigitYearMax);
                }
            }
        },
        fields: {
            _isReadOnly: false,
            twoDigitYearMax: 0
        },
        props: {
            MinSupportedDateTime: {
                get: function () {
                    return (System.DateTime.getMinValue());
                }
            },
            MaxSupportedDateTime: {
                get: function () {
                    return (System.DateTime.getMaxValue());
                }
            },
            AlgorithmType: {
                get: function () {
                    return 0;
                }
            },
            ID: {
                get: function () {
                    return 0;
                }
            },
            BaseCalendarID: {
                get: function () {
                    return this.ID;
                }
            },
            IsReadOnly: {
                get: function () {
                    return (this._isReadOnly);
                }
            },
            CurrentEraValue: {
                get: function () {
                    throw System.NotImplemented.ByDesign;
                    // TODO: NotSupported
                    //if (_currentEraValue == -1)
                    //{
                    //    Debug.Assert(BaseCalendarID != CalendarId.UNINITIALIZED_VALUE, "[Calendar.CurrentEraValue] Expected a real calendar ID");
                    //    _currentEraValue = CalendarData.GetCalendarData(BaseCalendarID).iCurrentEra;
                    //}
                    //return (_currentEraValue);
                }
            },
            DaysInYearBeforeMinSupportedYear: {
                get: function () {
                    return 365;
                }
            },
            TwoDigitYearMax: {
                get: function () {
                    return (this.twoDigitYearMax);
                },
                set: function (value) {
                    this.VerifyWritable();
                    this.twoDigitYearMax = value;
                }
            }
        },
        alias: ["Clone", "System$ICloneable$clone"],
        ctors: {
            init: function () {
                this._isReadOnly = false;
                this.twoDigitYearMax = -1;
            },
            ctor: function () {
                this.$initialize();
                //Do-nothing constructor.
            }
        },
        methods: {
            Clone: function () {
                var o = Bridge.clone(this);
                Bridge.cast(o, System.Globalization.Calendar).SetReadOnlyState(false);
                return (o);
            },
            VerifyWritable: function () {
                if (this._isReadOnly) {
                    // TODO: SR
                    //throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
                    throw new System.InvalidOperationException("Instance is read-only.");
                }
            },
            SetReadOnlyState: function (readOnly) {
                this._isReadOnly = readOnly;
            },
            Add: function (time, value, scale) {
                // From ECMA CLI spec, Partition III, section 3.27:
                //
                // If overflow occurs converting a floating-point type to an integer, or if the floating-point value 
                // being converted to an integer is a NaN, the value returned is unspecified. 
                //
                // Based upon this, this method should be performing the comparison against the double
                // before attempting a cast. Otherwise, the result is undefined.
                var tempMillis = (value * scale + (value >= 0 ? 0.5 : -0.5));
                if (!((tempMillis > -315537897600000.0) && (tempMillis < 315537897600000.0))) {
                    // TODO: SR
                    //throw new ArgumentOutOfRangeException(nameof(value), SR.ArgumentOutOfRange_AddValue);
                    throw new System.ArgumentOutOfRangeException("value", "Value to add was out of range.");
                }

                var millis = Bridge.Int.clip64(tempMillis);
                var ticks = System.DateTime.getTicks(time).add(millis.mul(System.Globalization.Calendar.TicksPerMillisecond));
                System.Globalization.Calendar.CheckAddResult(ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
                return (System.DateTime.create$2(ticks));
            },
            AddMilliseconds: function (time, milliseconds) {
                return (this.Add(time, milliseconds, 1));
            },
            AddDays: function (time, days) {
                return (this.Add(time, days, System.Globalization.Calendar.MillisPerDay));
            },
            AddHours: function (time, hours) {
                return (this.Add(time, hours, System.Globalization.Calendar.MillisPerHour));
            },
            AddMinutes: function (time, minutes) {
                return (this.Add(time, minutes, System.Globalization.Calendar.MillisPerMinute));
            },
            AddSeconds: function (time, seconds) {
                return this.Add(time, seconds, System.Globalization.Calendar.MillisPerSecond);
            },
            AddWeeks: function (time, weeks) {
                return (this.AddDays(time, Bridge.Int.mul(weeks, 7)));
            },
            GetDaysInMonth: function (year, month) {
                return (this.GetDaysInMonth$1(year, month, System.Globalization.Calendar.CurrentEra));
            },
            GetDaysInYear: function (year) {
                return (this.GetDaysInYear$1(year, System.Globalization.Calendar.CurrentEra));
            },
            GetHour: function (time) {
                return (System.Int64.clip32((System.DateTime.getTicks(time).div(System.Globalization.Calendar.TicksPerHour)).mod(System.Int64(24))));
            },
            GetMilliseconds: function (time) {
                return System.Int64.toNumber((System.DateTime.getTicks(time).div(System.Globalization.Calendar.TicksPerMillisecond)).mod(System.Int64(1000)));
            },
            GetMinute: function (time) {
                return (System.Int64.clip32((System.DateTime.getTicks(time).div(System.Globalization.Calendar.TicksPerMinute)).mod(System.Int64(60))));
            },
            GetMonthsInYear: function (year) {
                return (this.GetMonthsInYear$1(year, System.Globalization.Calendar.CurrentEra));
            },
            GetSecond: function (time) {
                return (System.Int64.clip32((System.DateTime.getTicks(time).div(System.Globalization.Calendar.TicksPerSecond)).mod(System.Int64(60))));
            },
            GetFirstDayWeekOfYear: function (time, firstDayOfWeek) {
                var dayOfYear = (this.GetDayOfYear(time) - 1) | 0; // Make the day of year to be 0-based, so that 1/1 is day 0.
                // Calculate the day of week for the first day of the year.
                // dayOfWeek - (dayOfYear % 7) is the day of week for the first day of this year.  Note that
                // this value can be less than 0.  It's fine since we are making it positive again in calculating offset.
                var dayForJan1 = (this.GetDayOfWeek(time) - (dayOfYear % 7)) | 0;
                var offset = (((((dayForJan1 - firstDayOfWeek) | 0) + 14) | 0)) % 7;
                return (((((Bridge.Int.div((((dayOfYear + offset) | 0)), 7)) | 0) + 1) | 0));
            },
            GetWeekOfYearFullDays: function (time, firstDayOfWeek, fullDays) {
                var dayForJan1;
                var offset;
                var day;

                var dayOfYear = (this.GetDayOfYear(time) - 1) | 0; // Make the day of year to be 0-based, so that 1/1 is day 0.
                //
                // Calculate the number of days between the first day of year (1/1) and the first day of the week.
                // This value will be a positive value from 0 ~ 6.  We call this value as "offset".
                //
                // If offset is 0, it means that the 1/1 is the start of the first week.
                //     Assume the first day of the week is Monday, it will look like this:
                //     Sun      Mon     Tue     Wed     Thu     Fri     Sat
                //     12/31    1/1     1/2     1/3     1/4     1/5     1/6
                //              +--> First week starts here.
                //
                // If offset is 1, it means that the first day of the week is 1 day ahead of 1/1.
                //     Assume the first day of the week is Monday, it will look like this:
                //     Sun      Mon     Tue     Wed     Thu     Fri     Sat
                //     1/1      1/2     1/3     1/4     1/5     1/6     1/7
                //              +--> First week starts here.
                //
                // If offset is 2, it means that the first day of the week is 2 days ahead of 1/1.
                //     Assume the first day of the week is Monday, it will look like this:
                //     Sat      Sun     Mon     Tue     Wed     Thu     Fri     Sat
                //     1/1      1/2     1/3     1/4     1/5     1/6     1/7     1/8
                //                      +--> First week starts here.



                // Day of week is 0-based.
                // Get the day of week for 1/1.  This can be derived from the day of week of the target day.
                // Note that we can get a negative value.  It's ok since we are going to make it a positive value when calculating the offset.
                dayForJan1 = (this.GetDayOfWeek(time) - (dayOfYear % 7)) | 0;

                // Now, calculate the offset.  Subtract the first day of week from the dayForJan1.  And make it a positive value.
                offset = (((((firstDayOfWeek - dayForJan1) | 0) + 14) | 0)) % 7;
                if (offset !== 0 && offset >= fullDays) {
                    //
                    // If the offset is greater than the value of fullDays, it means that
                    // the first week of the year starts on the week where Jan/1 falls on.
                    //
                    offset = (offset - 7) | 0;
                }
                //
                // Calculate the day of year for specified time by taking offset into account.
                //
                day = (dayOfYear - offset) | 0;
                if (day >= 0) {
                    //
                    // If the day of year value is greater than zero, get the week of year.
                    //
                    return (((((Bridge.Int.div(day, 7)) | 0) + 1) | 0));
                }
                //
                // Otherwise, the specified time falls on the week of previous year.
                // Call this method again by passing the last day of previous year.
                //
                // the last day of the previous year may "underflow" to no longer be a valid date time for
                // this calendar if we just subtract so we need the subclass to provide us with 
                // that information
                if (System.DateTime.lte(time, System.DateTime.addDays(this.MinSupportedDateTime, dayOfYear))) {
                    return this.GetWeekOfYearOfMinSupportedDateTime(firstDayOfWeek, fullDays);
                }
                return (this.GetWeekOfYearFullDays(System.DateTime.addDays(time, ((-(((dayOfYear + 1) | 0))) | 0)), firstDayOfWeek, fullDays));
            },
            GetWeekOfYearOfMinSupportedDateTime: function (firstDayOfWeek, minimumDaysInFirstWeek) {
                var dayOfYear = (this.GetDayOfYear(this.MinSupportedDateTime) - 1) | 0; // Make the day of year to be 0-based, so that 1/1 is day 0.
                var dayOfWeekOfFirstOfYear = (this.GetDayOfWeek(this.MinSupportedDateTime) - dayOfYear % 7) | 0;

                // Calculate the offset (how many days from the start of the year to the start of the week)
                var offset = (((((firstDayOfWeek + 7) | 0) - dayOfWeekOfFirstOfYear) | 0)) % 7;
                if (offset === 0 || offset >= minimumDaysInFirstWeek) {
                    // First of year falls in the first week of the year
                    return 1;
                }

                var daysInYearBeforeMinSupportedYear = (this.DaysInYearBeforeMinSupportedYear - 1) | 0; // Make the day of year to be 0-based, so that 1/1 is day 0.
                var dayOfWeekOfFirstOfPreviousYear = (((dayOfWeekOfFirstOfYear - 1) | 0) - (daysInYearBeforeMinSupportedYear % 7)) | 0;

                // starting from first day of the year, how many days do you have to go forward
                // before getting to the first day of the week?
                var daysInInitialPartialWeek = (((((firstDayOfWeek - dayOfWeekOfFirstOfPreviousYear) | 0) + 14) | 0)) % 7;
                var day = (daysInYearBeforeMinSupportedYear - daysInInitialPartialWeek) | 0;
                if (daysInInitialPartialWeek >= minimumDaysInFirstWeek) {
                    // If the offset is greater than the minimum Days in the first week, it means that
                    // First of year is part of the first week of the year even though it is only a partial week
                    // add another week
                    day = (day + 7) | 0;
                }

                return (((((Bridge.Int.div(day, 7)) | 0) + 1) | 0));
            },
            GetWeekOfYear: function (time, rule, firstDayOfWeek) {
                if (firstDayOfWeek < 0 || firstDayOfWeek > 6) {
                    throw new System.ArgumentOutOfRangeException("firstDayOfWeek", System.SR.Format$1("Valid values are between {0} and {1}, inclusive.", Bridge.box(System.DayOfWeek.Sunday, System.DayOfWeek, System.Enum.toStringFn(System.DayOfWeek)), Bridge.box(System.DayOfWeek.Saturday, System.DayOfWeek, System.Enum.toStringFn(System.DayOfWeek))));
                }
                switch (rule) {
                    case 0: 
                        return (this.GetFirstDayWeekOfYear(time, firstDayOfWeek));
                    case 1: 
                        return (this.GetWeekOfYearFullDays(time, firstDayOfWeek, 7));
                    case 2: 
                        return (this.GetWeekOfYearFullDays(time, firstDayOfWeek, 4));
                }
                throw new System.ArgumentOutOfRangeException("rule", System.SR.Format$1("Valid values are between {0} and {1}, inclusive.", Bridge.box(0, System.Globalization.CalendarWeekRule, System.Enum.toStringFn(System.Globalization.CalendarWeekRule)), Bridge.box(2, System.Globalization.CalendarWeekRule, System.Enum.toStringFn(System.Globalization.CalendarWeekRule))));
            },
            IsLeapDay: function (year, month, day) {
                return (this.IsLeapDay$1(year, month, day, System.Globalization.Calendar.CurrentEra));
            },
            IsLeapMonth: function (year, month) {
                return (this.IsLeapMonth$1(year, month, System.Globalization.Calendar.CurrentEra));
            },
            GetLeapMonth: function (year) {
                return (this.GetLeapMonth$1(year, System.Globalization.Calendar.CurrentEra));
            },
            GetLeapMonth$1: function (year, era) {
                if (!this.IsLeapYear$1(year, era)) {
                    return 0;
                }

                var monthsCount = this.GetMonthsInYear$1(year, era);
                for (var month = 1; month <= monthsCount; month = (month + 1) | 0) {
                    if (this.IsLeapMonth$1(year, month, era)) {
                        return month;
                    }
                }

                return 0;
            },
            IsLeapYear: function (year) {
                return (this.IsLeapYear$1(year, System.Globalization.Calendar.CurrentEra));
            },
            ToDateTime: function (year, month, day, hour, minute, second, millisecond) {
                return (this.ToDateTime$1(year, month, day, hour, minute, second, millisecond, System.Globalization.Calendar.CurrentEra));
            },
            TryToDateTime: function (year, month, day, hour, minute, second, millisecond, era, result) {
                result.v = System.DateTime.getMinValue();
                try {
                    result.v = this.ToDateTime$1(year, month, day, hour, minute, second, millisecond, era);
                    return true;
                }
                catch ($e1) {
                    $e1 = System.Exception.create($e1);
                    if (Bridge.is($e1, System.ArgumentException)) {
                        return false;
                    } else {
                        throw $e1;
                    }
                }
            },
            IsValidYear: function (year, era) {
                return (year >= this.GetYear(this.MinSupportedDateTime) && year <= this.GetYear(this.MaxSupportedDateTime));
            },
            IsValidMonth: function (year, month, era) {
                return (this.IsValidYear(year, era) && month >= 1 && month <= this.GetMonthsInYear$1(year, era));
            },
            IsValidDay: function (year, month, day, era) {
                return (this.IsValidMonth(year, month, era) && day >= 1 && day <= this.GetDaysInMonth$1(year, month, era));
            },
            ToFourDigitYear: function (year) {
                if (year < 0) {
                    throw new System.ArgumentOutOfRangeException("year", "Non-negative number required.");
                    // TODO: SR
                    //SR.ArgumentOutOfRange_NeedNonNegNum);
                }
                if (year < 100) {
                    return (((Bridge.Int.mul((((((Bridge.Int.div(this.TwoDigitYearMax, 100)) | 0) - (year > this.TwoDigitYearMax % 100 ? 1 : 0)) | 0)), 100) + year) | 0));
                }
                // If the year value is above 100, just return the year value.  Don't have to do
                // the TwoDigitYearMax comparison.
                return (year);
            }
        }
    });
