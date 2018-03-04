    Bridge.define("System.Globalization.GlobalizationMode", {
        statics: {
            props: {
                Invariant: false
            },
            ctors: {
                init: function () {
                    this.Invariant = System.Globalization.GlobalizationMode.GetGlobalizationInvariantMode();
                }
            },
            methods: {
                GetInvariantSwitchValue: function () {
                    return true;

                    // TODO: NotSupported
                    //bool exist;
                    //bool ret = CLRConfig.GetBoolValue("System.Globalization.Invariant", out exist);
                    //if (!exist)
                    //{
                    //    // Linux doesn't support environment variable names include dots
                    //    string switchValue = Environment.GetEnvironmentVariable("DOTNET_SYSTEM_GLOBALIZATION_INVARIANT");
                    //    if (switchValue != null)
                    //    {
                    //        ret = switchValue.Equals("true", StringComparison.OrdinalIgnoreCase) || switchValue.Equals("1");
                    //    }
                    //}

                    //return ret;
                },
                GetGlobalizationInvariantMode: function () {
                    return System.Globalization.GlobalizationMode.GetInvariantSwitchValue();
                }
            }
        }
    });
