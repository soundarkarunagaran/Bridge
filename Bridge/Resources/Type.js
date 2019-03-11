Bridge.define("System.Type", {

    statics: {       
        $is: function (instance) {
            return instance && instance.constructor === Function;
        }
    }
});