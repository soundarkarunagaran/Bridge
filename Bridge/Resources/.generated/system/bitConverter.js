    Bridge.define("System.BitConverter", {
        statics: {
            fields: {
                isLittleEndian: false,
                arg_ArrayPlusOffTooSmall: null
            },
            ctors: {
                init: function () {
                    this.isLittleEndian = System.BitConverter.getIsLittleEndian();
                    this.arg_ArrayPlusOffTooSmall = "Destination array is not long enough to copy all the items in the collection. Check array index and length.";
                }
            },
            methods: {
                getBytes: function (value) {
                    return value ? System.Array.init([1], System.Byte) : System.Array.init([0], System.Byte);
                },
                getBytes$1: function (value) {
                    return System.BitConverter.getBytes$3(Bridge.Int.sxs(value & 65535));
                },
                getBytes$3: function (value) {
                    var view = System.BitConverter.view(2);

                    throw new System.NotImplementedException("Removed Bridge.Html5 dependency from Bridge.");

                    /* 
                    view.SetInt16(0, value);

                    return GetViewBytes(view);
                    */
                },
                getBytes$4: function (value) {
                    var view = System.BitConverter.view(4);

                    throw new System.NotImplementedException("Removed Bridge.Html5 dependency from Bridge.");

                    /* 
                    view.SetInt32(0, value);

                    return GetViewBytes(view);
                    */
                },
                getBytes$5: function (value) {
                    var view = System.BitConverter.getView(value);

                    return System.BitConverter.getViewBytes(view);
                },
                getBytes$7: function (value) {
                    var view = System.BitConverter.view(2);

                    throw new System.NotImplementedException("Removed Bridge.Html5 dependency from Bridge.");
                    /* 
                    view.SetUint16(0, value);

                    return GetViewBytes(view);
                    */
                },
                getBytes$8: function (value) {
                    var view = System.BitConverter.view(4);

                    throw new System.NotImplementedException("Removed Bridge.Html5 dependency from Bridge.");
                    /* 
                    view.SetUint32(0, value);

                    return GetViewBytes(view);
                    */
                },
                getBytes$9: function (value) {
                    var view = System.BitConverter.getView(System.Int64.clip64(value));

                    return System.BitConverter.getViewBytes(view);
                },
                getBytes$6: function (value) {
                    var view = System.BitConverter.view(4);

                    throw new System.NotImplementedException("Removed Bridge.Html5 dependency from Bridge.");

                    /* 
                    view.SetFloat32(0, value);

                    return GetViewBytes(view);
                    */
                },
                getBytes$2: function (value) {
                    if (isNaN(value)) {
                        if (System.BitConverter.isLittleEndian) {
                            return System.Array.init([0, 0, 0, 0, 0, 0, 248, 255], System.Byte);
                        } else {
                            return System.Array.init([255, 248, 0, 0, 0, 0, 0, 0], System.Byte);
                        }
                    }

                    var view = System.BitConverter.view(8);

                    throw new System.NotImplementedException("Removed Bridge.Html5 dependency from Bridge.");

                    /* 
                    view.SetFloat64(0, value);

                    return GetViewBytes(view);
                    */
                },
                toChar: function (value, startIndex) {
                    return ((System.BitConverter.toInt16(value, startIndex)) & 65535);
                },
                toInt16: function (value, startIndex) {
                    System.BitConverter.checkArguments(value, startIndex, 2);

                    var view = System.BitConverter.view(2);

                    System.BitConverter.setViewBytes(view, value, -1, startIndex);

                    throw new System.NotImplementedException("Removed Bridge.Html5 dependency from Bridge.");
                    //return view.GetInt16(0);
                },
                toInt32: function (value, startIndex) {
                    System.BitConverter.checkArguments(value, startIndex, 4);

                    var view = System.BitConverter.view(4);

                    System.BitConverter.setViewBytes(view, value, -1, startIndex);

                    throw new System.NotImplementedException("Removed Bridge.Html5 dependency from Bridge.");
                    //return view.GetInt32(0);
                },
                toInt64: function (value, startIndex) {
                    System.BitConverter.checkArguments(value, startIndex, 8);

                    var low = System.BitConverter.toInt32(value, startIndex);
                    var high = System.BitConverter.toInt32(value, ((startIndex + 4) | 0));

                    if (System.BitConverter.isLittleEndian) {
                        return System.Int64([low, high]);
                    }

                    return System.Int64([high, low]);
                },
                toUInt16: function (value, startIndex) {
                    return ((System.BitConverter.toInt16(value, startIndex)) & 65535);
                },
                toUInt32: function (value, startIndex) {
                    return ((System.BitConverter.toInt32(value, startIndex)) >>> 0);
                },
                toUInt64: function (value, startIndex) {
                    var l = System.BitConverter.toInt64(value, startIndex);

                    return System.UInt64([l.value.low, l.value.high]);
                },
                toSingle: function (value, startIndex) {
                    System.BitConverter.checkArguments(value, startIndex, 4);

                    var view = System.BitConverter.view(4);

                    System.BitConverter.setViewBytes(view, value, -1, startIndex);

                    throw new System.NotImplementedException("Removed Bridge.Html5 dependency from Bridge.");
                    //return view.GetFloat32(0);
                },
                toDouble: function (value, startIndex) {
                    System.BitConverter.checkArguments(value, startIndex, 8);

                    var view = System.BitConverter.view(8);

                    System.BitConverter.setViewBytes(view, value, -1, startIndex);

                    throw new System.NotImplementedException("Removed Bridge.Html5 dependency from Bridge.");
                    //return view.GetFloat64(0);
                },
                toString$2: function (value, startIndex, length) {
                    if (value == null) {
                        throw new System.ArgumentNullException("value");
                    }

                    if (startIndex < 0 || startIndex >= value.length && startIndex > 0) { // Don't throw for a 0 length array.
                        throw new System.ArgumentOutOfRangeException("startIndex");
                    }

                    if (length < 0) {
                        throw new System.ArgumentOutOfRangeException("length");
                    }

                    if (startIndex > ((value.length - length) | 0)) {
                        throw new System.ArgumentException(System.BitConverter.arg_ArrayPlusOffTooSmall);
                    }

                    if (length === 0) {
                        return "";
                    }

                    if (length > (715827882)) {
                        // (Int32.MaxValue / 3) == 715,827,882 Bytes == 699 MB
                        throw new System.ArgumentOutOfRangeException("length", Bridge.toString((715827882)));
                    }

                    var chArrayLength = Bridge.Int.mul(length, 3);

                    var chArray = System.Array.init(chArrayLength, 0, System.Char);
                    var i = 0;
                    var index = startIndex;
                    for (i = 0; i < chArrayLength; i = (i + 3) | 0) {
                        var b = value[System.Array.index(Bridge.identity(index, (index = (index + 1) | 0)), value)];
                        chArray[System.Array.index(i, chArray)] = System.BitConverter.getHexValue(((Bridge.Int.div(b, 16)) | 0));
                        chArray[System.Array.index(((i + 1) | 0), chArray)] = System.BitConverter.getHexValue(b % 16);
                        chArray[System.Array.index(((i + 2) | 0), chArray)] = 45;
                    }

                    // We don't need the last '-' character
                    return System.String.fromCharArray(chArray, 0, ((chArray.length - 1) | 0));
                },
                toString: function (value) {
                    if (value == null) {
                        throw new System.ArgumentNullException("value");
                    }

                    return System.BitConverter.toString$2(value, 0, value.length);
                },
                toString$1: function (value, startIndex) {
                    if (value == null) {
                        throw new System.ArgumentNullException("value");
                    }

                    return System.BitConverter.toString$2(value, startIndex, ((value.length - startIndex) | 0));
                },
                toBoolean: function (value, startIndex) {
                    System.BitConverter.checkArguments(value, startIndex, 1);

                    return (value[System.Array.index(startIndex, value)] === 0) ? false : true;
                },
                doubleToInt64Bits: function (value) {
                    var view = System.BitConverter.view(8);

                    throw new System.NotImplementedException("Removed Bridge.Html5 dependency from Bridge.");

                    /* 
                    view.SetFloat64(0, value);

                    return CreateLong(view.GetInt32(4), view.GetInt32(0));
                    */
                },
                int64BitsToDouble: function (value) {
                    var view = System.BitConverter.getView(value);

                    throw new System.NotImplementedException("Removed Bridge.Html5 dependency from Bridge.");
                    //return view.GetFloat64(0);
                },
                getHexValue: function (i) {
                    if (i < 10) {
                        return ((((i + 48) | 0)) & 65535);
                    }

                    return ((((((i - 10) | 0) + 65) | 0)) & 65535);
                },
                getViewBytes: function (view, count, startIndex) {
                    if (count === void 0) { count = -1; }
                    if (startIndex === void 0) { startIndex = 0; }
                    if (count === -1) {
                        throw new System.NotImplementedException("Removed Bridge.Html5 dependency from Bridge.");
                        //count = view.ByteLength;
                    }

                    var r = System.Array.init(count, 0, System.Byte);

                    if (System.BitConverter.isLittleEndian) {
                        /* 
                        for (int i = count - 1; i >= 0; i--)
                        {
                           r[i] = view.GetUint8(startIndex++);
                        }
                        */
                    } else {
                        throw new System.NotImplementedException("Removed Bridge.Html5 dependency from Bridge.");
                        /* 
                        for (int i = 0; i < count; i++)
                        {
                           r[i] = view.GetUint8(startIndex++);
                        }
                        */
                    }

                    return r;
                },
                setViewBytes: function (view, value, count, startIndex) {
                    if (count === void 0) { count = -1; }
                    if (startIndex === void 0) { startIndex = 0; }
                    if (count === -1) {
                        throw new System.NotImplementedException("Removed Bridge.Html5 dependency from Bridge.");
                        //count = view.ByteLength;
                    }

                    if (System.BitConverter.isLittleEndian) {
                        throw new System.NotImplementedException("Removed Bridge.Html5 dependency from Bridge.");
                        /* for (int i = count - 1; i >= 0; i--)
                        {
                           view.SetUint8(i, value[startIndex++]);
                        }*/
                    } else {
                        throw new System.NotImplementedException("Removed Bridge.Html5 dependency from Bridge.");
                        /* 
                        for (int i = 0; i < count; i++)
                        {
                           view.SetUint8(i, value[startIndex++]);
                        }
                        */
                    }
                },
                view: function (length) {
                    throw new System.NotImplementedException("Removed Bridge.Html5 dependency from Bridge.");

                    /* 
                    var buffer = new ArrayBuffer(length);
                    var view = new DataView(buffer);

                    return view;
                    */
                },
                getView: function (value) {
                    throw new System.NotImplementedException("Removed Bridge.Html5 dependency from Bridge.");

                    /* 
                    var view = View(8);

                    view.SetInt32(4, GetLongLow(value));
                    view.SetInt32(0, GetLongHigh(value));

                    return view;
                    */
                },
                getIsLittleEndian: function () {
                    throw new System.NotImplementedException("Removed Bridge.Html5 dependency from Bridge.");

                    /* 
                    var view = View(2);

                    view.SetUint8(0, 0xAA);
                    view.SetUint8(1, 0xBB);

                    if (view.GetUint16(0) == 0xAABB)
                    {
                       return true;
                    }

                    return false;
                    */
                },
                checkArguments: function (value, startIndex, size) {
                    if (value == null) {
                        throw new System.ArgumentNullException("null");
                    }

                    if (System.Int64((startIndex >>> 0)).gte(System.Int64(value.length))) {
                        throw new System.ArgumentOutOfRangeException("startIndex");
                    }

                    if (startIndex > ((value.length - size) | 0)) {
                        throw new System.ArgumentException(System.BitConverter.arg_ArrayPlusOffTooSmall);
                    }
                }
            }
        }
    });
