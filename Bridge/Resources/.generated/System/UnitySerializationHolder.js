    Bridge.define("System.UnitySerializationHolder", {
        inherits: [System.Runtime.Serialization.ISerializable,System.Runtime.Serialization.IObjectReference],
        statics: {
            fields: {
                NullUnity: 0
            },
            ctors: {
                init: function () {
                    this.NullUnity = 2;
                }
            }
        },
        alias: ["GetRealObject", "System$Runtime$Serialization$IObjectReference$GetRealObject"],
        methods: {
            GetRealObject: function (context) {
                throw System.NotImplemented.ByDesign;
                // TODO: NotSupported
                //if (_unityType != NullUnity)
                //{
                //    throw new ArgumentException(SR.Format("Type '{0}' is not deserializable.", _data ?? "UnityType"));
                //    // TODO: SR
                //    //throw new ArgumentException(SR.Format(SR.Argument_InvalidUnity, _data ?? "UnityType"));
                //}

                //return DBNull.Value;
            }
        }
    });
