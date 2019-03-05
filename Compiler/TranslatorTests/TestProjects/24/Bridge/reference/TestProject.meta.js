Bridge.assembly("TestProject", function ($asm, globals) {
    "use strict";

    define("MyModule", function () {
        var MyModule = { };

        Bridge.init();
        return MyModule;
    });

});
