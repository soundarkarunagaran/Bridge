Bridge.assembly("TestProject", function ($asm, globals) {
    "use strict";


    var $m = Bridge.setMetadata,
        $n = [TestProject1,System,TestProject2];
    $m($n[0].ExistsIfTest1Defined, function () { return {"att":1048577,"a":2,"m":[{"a":2,"isSynthetic":true,"n":".ctor","t":1,"sn":"ctor"}]}; });
    $m($n[0].ExistsIfTest2Defined, function () { return {"att":1048577,"a":2,"m":[{"a":2,"isSynthetic":true,"n":".ctor","t":1,"sn":"ctor"}]}; });
    $m($n[0].TestClassA, function () { return {"att":1048576,"a":4,"m":[{"a":2,"isSynthetic":true,"n":".ctor","t":1,"sn":"ctor"},{"a":2,"n":"GetMyValue","t":8,"pi":[{"n":"i","pt":$n[1].Int32,"ps":0}],"sn":"GetMyValue","rt":$n[1].String,"p":[$n[1].Int32]},{"a":2,"n":"Value1","t":16,"rt":$n[1].Int32,"g":{"a":2,"n":"get_Value1","t":8,"rt":$n[1].Int32,"fg":"Value1","box":function ($v) { return Bridge.box($v, System.Int32);}},"s":{"a":2,"n":"set_Value1","t":8,"p":[$n[1].Int32],"rt":$n[1].Void,"fs":"Value1"},"fn":"Value1"}]}; });
    $m($n[2].TestClassB, function () { return {"att":1048576,"a":4,"m":[{"a":2,"isSynthetic":true,"n":".ctor","t":1,"sn":"ctor"},{"a":2,"n":"getYourValue","t":8,"pi":[{"n":"MyParameter","pt":$n[1].String,"ps":0}],"sn":"getYourValue","rt":$n[1].Int32,"p":[$n[1].String],"box":function ($v) { return Bridge.box($v, System.Int32);}},{"a":2,"n":"Value1","t":16,"rt":$n[1].Int32,"g":{"a":2,"n":"get_Value1","t":8,"rt":$n[1].Int32,"fg":"Value1","box":function ($v) { return Bridge.box($v, System.Int32);}},"s":{"a":2,"n":"set_Value1","t":8,"p":[$n[1].Int32],"rt":$n[1].Void,"fs":"Value1"},"fn":"Value1"}]}; });
});
