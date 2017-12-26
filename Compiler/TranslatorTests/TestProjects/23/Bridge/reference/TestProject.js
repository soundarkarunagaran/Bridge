/**
 * @compiler Bridge.NET 16.6.1
 */
Bridge.assembly("TestProject", function ($asm, globals) {
    "use strict";

    Bridge.define("TestProject.Other", {
        statics: {
            methods: {
                Create: function (test) {
                    var other = new TestProject.Other();
                    other.Test = test;
                    return other;
                }
            }
        },
        props: {
            Test: 0
        },
        ctors: {
            ctor: function () {
                this.$initialize();
            }
        }
    });

    Bridge.define("TestProject.Setup", {
        main: function Main () {
            var test = new NewName();
            test.Field++;
            console.log(test.Field);
            (test.Field += (1 * 2) | 0);
            test.Property = (test.Property + 2) | 0;
            console.log(test.Property);
            var x = $asm.$.TestProject.Setup.f1;
            test.addEvent(x);
            test.removeEvent(x);
            console.log(test._Field);
        }
    });

    Bridge.ns("TestProject.Setup", $asm.$);

    Bridge.apply($asm.$.TestProject.Setup, {
        f1: function () {
            console.log("Hello World");
        }
    });

    Bridge.define("NewName", {
        fields: {
            _Field: 0
        },
        props: {
            Property: 0,
            Property2: 0
        },
        methods: {
            addEvent: function (value) {
                throw new System.NotImplementedException();
            },
            removeEvent: function (value) {
                throw new System.NotImplementedException();
            },
            MethodWithReturn: function () {
                return 0;
            },
            Method: function () { },
            MethodWithArgs: function (a, b) { },
            MethodWithArgs: function (a, b, c) { }
        }
    });

    Bridge.define("TestProject.TestAttribute", {
        inherits: [System.Attribute],
        ctors: {
            ctor: function (test) {
                this.$initialize();
                System.Attribute.ctor.call(this);
            }
        }
    });
});
