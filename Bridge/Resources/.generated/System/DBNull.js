    Bridge.define("System.DBNull", {
        inherits: [System.Runtime.Serialization.ISerializable,System.IConvertible],
        statics: {
            fields: {
                Value: null
            },
            ctors: {
                init: function () {
                    this.Value = new System.DBNull();
                }
            }
        },
        alias: [
            "ToString", "System$IConvertible$ToString",
            "GetTypeCode", "System$IConvertible$GetTypeCode"
        ],
        ctors: {
            ctor: function () {
                this.$initialize();
            }
        },
        methods: {
            toString: function () {
                return "";
            },
            ToString: function (provider) {
                return "";
            },
            GetTypeCode: function () {
                return System.TypeCode.DBNull;
            },
            System$IConvertible$ToBoolean: function (provider) {
                throw new System.InvalidCastException.$ctor1("Object cannot be cast from DBNull to other types.");
                // TODO: SR
                //throw new InvalidCastException(SR.InvalidCast_FromDBNull);
            },
            System$IConvertible$ToChar: function (provider) {
                throw new System.InvalidCastException.$ctor1("Object cannot be cast from DBNull to other types.");
                // TODO: SR
                //throw new InvalidCastException(SR.InvalidCast_FromDBNull);
            },
            System$IConvertible$ToSByte: function (provider) {
                throw new System.InvalidCastException.$ctor1("Object cannot be cast from DBNull to other types.");
                // TODO: SR
                //throw new InvalidCastException(SR.InvalidCast_FromDBNull);
            },
            System$IConvertible$ToByte: function (provider) {
                throw new System.InvalidCastException.$ctor1("Object cannot be cast from DBNull to other types.");
                // TODO: SR
                //throw new InvalidCastException(SR.InvalidCast_FromDBNull);
            },
            System$IConvertible$ToInt16: function (provider) {
                throw new System.InvalidCastException.$ctor1("Object cannot be cast from DBNull to other types.");
                // TODO: SR
                //throw new InvalidCastException(SR.InvalidCast_FromDBNull);
            },
            System$IConvertible$ToUInt16: function (provider) {
                throw new System.InvalidCastException.$ctor1("Object cannot be cast from DBNull to other types.");
                // TODO: SR
                //throw new InvalidCastException(SR.InvalidCast_FromDBNull);
            },
            System$IConvertible$ToInt32: function (provider) {
                throw new System.InvalidCastException.$ctor1("Object cannot be cast from DBNull to other types.");
                // TODO: SR
                //throw new InvalidCastException(SR.InvalidCast_FromDBNull);
            },
            System$IConvertible$ToUInt32: function (provider) {
                throw new System.InvalidCastException.$ctor1("Object cannot be cast from DBNull to other types.");
                // TODO: SR
                //throw new InvalidCastException(SR.InvalidCast_FromDBNull);
            },
            System$IConvertible$ToInt64: function (provider) {
                throw new System.InvalidCastException.$ctor1("Object cannot be cast from DBNull to other types.");
                // TODO: SR
                //throw new InvalidCastException(SR.InvalidCast_FromDBNull);
            },
            System$IConvertible$ToUInt64: function (provider) {
                throw new System.InvalidCastException.$ctor1("Object cannot be cast from DBNull to other types.");
                // TODO: SR
                //throw new InvalidCastException(SR.InvalidCast_FromDBNull);
            },
            System$IConvertible$ToSingle: function (provider) {
                throw new System.InvalidCastException.$ctor1("Object cannot be cast from DBNull to other types.");
                // TODO: SR
                //throw new InvalidCastException(SR.InvalidCast_FromDBNull);
            },
            System$IConvertible$ToDouble: function (provider) {
                throw new System.InvalidCastException.$ctor1("Object cannot be cast from DBNull to other types.");
                // TODO: SR
                //throw new InvalidCastException(SR.InvalidCast_FromDBNull);
            },
            System$IConvertible$ToDecimal: function (provider) {
                throw new System.InvalidCastException.$ctor1("Object cannot be cast from DBNull to other types.");
                // TODO: SR
                //throw new InvalidCastException(SR.InvalidCast_FromDBNull);
            },
            System$IConvertible$ToDateTime: function (provider) {
                throw new System.InvalidCastException.$ctor1("Object cannot be cast from DBNull to other types.");
                // TODO: SR
                //throw new InvalidCastException(SR.InvalidCast_FromDBNull);
            },
            System$IConvertible$ToType: function (type, provider) {
                return System.Convert.defaultToType(Bridge.cast(this, System.IConvertible), type, provider);
            }
        }
    });
