    Bridge.define("System.IO.IOException", {
        inherits: [System.Exception],
        fields: {
            _maybeFullPath: null
        },
        ctors: {
            ctor: function () {
                this.$initialize();
                System.Exception.ctor.call(this, "Arg_IOException");
            },
            $ctor1: function (message) {
                this.$initialize();
                System.Exception.ctor.call(this, message);
            },
            $ctor3: function (message, hresult) {
                this.$initialize();
                System.Exception.ctor.call(this, message);
            },
            $ctor4: function (message, hresult, maybeFullPath) {
                this.$initialize();
                System.Exception.ctor.call(this, message);
                this._maybeFullPath = maybeFullPath;
            },
            $ctor2: function (message, innerException) {
                this.$initialize();
                System.Exception.ctor.call(this, message, innerException);
            }
        }
    });
