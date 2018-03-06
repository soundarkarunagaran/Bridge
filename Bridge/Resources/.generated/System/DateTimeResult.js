    Bridge.define("System.DateTimeResult", {
        $kind: "struct",
        statics: {
            methods: {
                getDefaultValue: function () { return new System.DateTimeResult(); }
            }
        },
        fields: {
            year: 0,
            month: 0,
            day: 0,
            hour: 0,
            minute: 0,
            second: 0,
            fraction: 0,
            era: 0,
            flags: 0,
            timeZoneOffset: null,
            calendar: null,
            parsedDate: null,
            failure: 0,
            failureMessageID: null,
            failureMessageFormatArgument: null,
            failureArgumentName: null
        },
        ctors: {
            init: function () {
                this.timeZoneOffset = new System.TimeSpan();
                this.parsedDate = System.DateTime.getDefaultValue();
            },
            ctor: function () {
                this.$initialize();
            }
        },
        methods: {
            init: function () {
                this.year = -1;
                this.month = -1;
                this.day = -1;
                this.fraction = -1;
                this.era = -1;
            },
            setDate: function (year, month, day) {
                this.year = year;
                this.month = month;
                this.day = day;
            },
            setFailure: function (failure, failureMessageID, failureMessageFormatArgument) {
                this.failure = failure;
                this.failureMessageID = failureMessageID;
                this.failureMessageFormatArgument = failureMessageFormatArgument;
            },
            setFailure$1: function (failure, failureMessageID, failureMessageFormatArgument, failureArgumentName) {
                this.failure = failure;
                this.failureMessageID = failureMessageID;
                this.failureMessageFormatArgument = failureMessageFormatArgument;
                this.failureArgumentName = failureArgumentName;
            },
            getHashCode: function () {
                var h = Bridge.addHash([5374321750, this.year, this.month, this.day, this.hour, this.minute, this.second, this.fraction, this.era, this.flags, this.timeZoneOffset, this.calendar, this.parsedDate, this.failure, this.failureMessageID, this.failureMessageFormatArgument, this.failureArgumentName]);
                return h;
            },
            equals: function (o) {
                if (!Bridge.is(o, System.DateTimeResult)) {
                    return false;
                }
                return Bridge.equals(this.year, o.year) && Bridge.equals(this.month, o.month) && Bridge.equals(this.day, o.day) && Bridge.equals(this.hour, o.hour) && Bridge.equals(this.minute, o.minute) && Bridge.equals(this.second, o.second) && Bridge.equals(this.fraction, o.fraction) && Bridge.equals(this.era, o.era) && Bridge.equals(this.flags, o.flags) && Bridge.equals(this.timeZoneOffset, o.timeZoneOffset) && Bridge.equals(this.calendar, o.calendar) && Bridge.equals(this.parsedDate, o.parsedDate) && Bridge.equals(this.failure, o.failure) && Bridge.equals(this.failureMessageID, o.failureMessageID) && Bridge.equals(this.failureMessageFormatArgument, o.failureMessageFormatArgument) && Bridge.equals(this.failureArgumentName, o.failureArgumentName);
            },
            $clone: function (to) {
                var s = to || new System.DateTimeResult();
                s.year = this.year;
                s.month = this.month;
                s.day = this.day;
                s.hour = this.hour;
                s.minute = this.minute;
                s.second = this.second;
                s.fraction = this.fraction;
                s.era = this.era;
                s.flags = this.flags;
                s.timeZoneOffset = this.timeZoneOffset;
                s.calendar = this.calendar;
                s.parsedDate = this.parsedDate;
                s.failure = this.failure;
                s.failureMessageID = this.failureMessageID;
                s.failureMessageFormatArgument = this.failureMessageFormatArgument;
                s.failureArgumentName = this.failureArgumentName;
                return s;
            }
        }
    });
