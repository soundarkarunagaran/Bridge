    Bridge.define("System.IO.StringBuilderCache", {
        statics: {
            fields: {
                MAX_BUILDER_SIZE: 0,
                DEFAULT_CAPACITY: 0,
                t_cachedInstance: null
            },
            ctors: {
                init: function () {
                    this.MAX_BUILDER_SIZE = 260;
                    this.DEFAULT_CAPACITY = 16;
                }
            },
            methods: {
                Acquire: function (capacity) {
                    if (capacity === void 0) { capacity = 16; }
                    if (capacity <= System.IO.StringBuilderCache.MAX_BUILDER_SIZE) {
                        var sb = System.IO.StringBuilderCache.t_cachedInstance;
                        if (sb != null) {
                            if (capacity <= sb.getCapacity()) {
                                System.IO.StringBuilderCache.t_cachedInstance = null;
                                sb.clear();
                                return sb;
                            }
                        }
                    }
                    return new System.Text.StringBuilder("", capacity);
                },
                Release: function (sb) {
                    if (sb.getCapacity() <= System.IO.StringBuilderCache.MAX_BUILDER_SIZE) {
                        System.IO.StringBuilderCache.t_cachedInstance = sb;
                    }
                },
                GetStringAndRelease: function (sb) {
                    var result = sb.toString();
                    System.IO.StringBuilderCache.Release(sb);
                    return result;
                }
            }
        }
    });
