    Bridge.define("System.NotImplemented", {
        statics: {
            props: {
                ByDesign: {
                    get: function () {
                        return new System.NotImplementedException();
                    }
                }
            },
            methods: {
                ByDesignWithMessage: function (message) {
                    return new System.NotImplementedException(message);
                }
            }
        }
    });
