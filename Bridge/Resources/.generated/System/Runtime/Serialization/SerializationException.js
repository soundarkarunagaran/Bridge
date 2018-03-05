    Bridge.define("System.Runtime.Serialization.SerializationException", {
        inherits: [System.SystemException],
        statics: {
            fields: {
                s_nullMessage: null
            },
            ctors: {
                init: function () {
                    this.s_nullMessage = "Serialization error.";
                }
            }
        },
        ctors: {
            ctor: function () {
                this.$initialize();
                System.SystemException.ctor.call(this, System.Runtime.Serialization.SerializationException.s_nullMessage);
                this.HResult = -2146233076;
            },
            $ctor1: function (message) {
                this.$initialize();
                System.SystemException.ctor.call(this, message);
                this.HResult = -2146233076;
            },
            $ctor2: function (message, innerException) {
                this.$initialize();
                System.SystemException.ctor.call(this, message, innerException);
                this.HResult = -2146233076;
            }
        }
    });
