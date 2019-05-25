    Bridge.define("System.String", {
        inherits: [System.IComparable, System.ICloneable, System.Collections.IEnumerable, System.Collections.Generic.IEnumerable$1(System.Char)],

        statics: {
            $is: function (instance) {
                return typeof (instance) === "string";
            },

            charCodeAt: function (str, idx) {
                idx = idx || 0;

                var code = str.charCodeAt(idx),
                    hi,
                    low;

                if (0xD800 <= code && code <= 0xDBFF) {
                    hi = code;
                    low = str.charCodeAt(idx + 1);

                    if (isNaN(low)) {
                        throw new System.Exception("High surrogate not followed by low surrogate");
                    }

                    return ((hi - 0xD800) * 0x400) + (low - 0xDC00) + 0x10000;
                }

                if (0xDC00 <= code && code <= 0xDFFF) {
                    return false;
                }

                return code;
            },

            fromCharCode: function (codePt) {
                if (codePt > 0xFFFF) {
                    codePt -= 0x10000;

                    return String.fromCharCode(0xD800 + (codePt >> 10), 0xDC00 + (codePt & 0x3FF));
                }

                return String.fromCharCode(codePt);
            },

            fromCharArray: function (chars, startIndex, length) {
                if (chars == null) {
                    throw new System.ArgumentNullException.$ctor1("chars");
                }

                if (startIndex < 0) {
                    throw new System.ArgumentOutOfRangeException.$ctor1("startIndex");
                }

                if (length < 0) {
                    throw new System.ArgumentOutOfRangeException.$ctor1("length");
                }

                if (chars.length - startIndex < length) {
                    throw new System.ArgumentOutOfRangeException.$ctor1("startIndex");
                }

                var result = "";

                startIndex = startIndex || 0;
                length = Bridge.isNumber(length) ? length : chars.length;

                if ((startIndex + length) > chars.length) {
                    length = chars.length - startIndex;
                }

                for (var i = 0; i < length; i++) {
                    var ch = chars[i + startIndex] | 0;

                    result += String.fromCharCode(ch);
                }

                return result;
            },

            lastIndexOf: function (s, search, startIndex, count) {
                var index = s.lastIndexOf(search, startIndex);

                return (index < (startIndex - count + 1)) ? -1 : index;
            },

            lastIndexOfAny: function (s, chars, startIndex, count) {
                var length = s.length;

                if (!length) {
                    return -1;
                }

                chars = String.fromCharCode.apply(null, chars);
                startIndex = startIndex || length - 1;
                count = count || length;

                var endIndex = startIndex - count + 1;

                if (endIndex < 0) {
                    endIndex = 0;
                }

                for (var i = startIndex; i >= endIndex; i--) {
                    if (chars.indexOf(s.charAt(i)) >= 0) {
                        return i;
                    }
                }

                return -1;
            },

            isNullOrWhiteSpace: function (s) {
                if (!s) {
                    return true;
                }

                return System.Char.isWhiteSpace(s);
            },

            isNullOrEmpty: function (s) {
                return !s;
            },

            fromCharCount: function (c, count) {
                if (count >= 0) {
                    return String(Array(count + 1).join(String.fromCharCode(c)));
                } else {
                    throw new System.ArgumentOutOfRangeException.$ctor4("count", "cannot be less than zero");
                }
            },

            format: function (format, args) {
                return System.String._format(System.Globalization.CultureInfo.getCurrentCulture(), format, Array.isArray(args) && arguments.length == 2 ? args : Array.prototype.slice.call(arguments, 1));
            },

            formatProvider: function (provider, format, args) {
                return System.String._format(provider, format, Array.isArray(args) && arguments.length == 3 ? args : Array.prototype.slice.call(arguments, 2));
            },

            _format: function (provider, format, args) {
                if (format == null) {
                    throw new System.ArgumentNullException.$ctor1("format");
                }

                var reverse = function (s) {
                    return s.split("").reverse().join("");
                };

                format = reverse(reverse(format.replace(/\{\{/g, function (m) {
                    return String.fromCharCode(1, 1);
                })).replace(/\}\}/g, function (m) {
                    return String.fromCharCode(2, 2);
                }));

                var me = this,
                    _formatRe = /(\{+)((\d+|[a-zA-Z_$]\w+(?:\.[a-zA-Z_$]\w+|\[\d+\])*)(?:\,(-?\d*))?(?:\:([^\}]*))?)(\}+)|(\{+)|(\}+)/g,
                    fn = this.decodeBraceSequence;

                format = format.replace(_formatRe, function (m, openBrace, elementContent, index, align, format, closeBrace, repeatOpenBrace, repeatCloseBrace) {
                    if (repeatOpenBrace) {
                        return fn(repeatOpenBrace);
                    }

                    if (repeatCloseBrace) {
                        return fn(repeatCloseBrace);
                    }

                    if (openBrace.length % 2 === 0 || closeBrace.length % 2 === 0) {
                        return fn(openBrace) + elementContent + fn(closeBrace);
                    }

                    return fn(openBrace, true) + me.handleElement(provider, index, align, format, args) + fn(closeBrace, true);
                });

                return format.replace(/(\x01\x01)|(\x02\x02)/g, function (m) {
                    if (m == String.fromCharCode(1, 1)) {
                        return "{";
                    }

                    if (m == String.fromCharCode(2, 2)) {
                        return "}";
                    }
                });
            },

            handleElement: function (provider, index, alignment, formatStr, args) {
                var value;

                index = parseInt(index, 10);

                if (index > args.length - 1) {
                    throw new System.FormatException.$ctor1("Input string was not in a correct format.");
                }

                value = args[index];

                if (value == null) {
                    value = "";
                }

                if (formatStr && value.$boxed && value.type.$kind === "enum") {
                    value = System.Enum.format(value.type, value.v, formatStr);
                } else if (formatStr && value.$boxed && value.type.format) {
                    value = value.type.format(Bridge.unbox(value, true), formatStr, provider);
                } else if (formatStr && Bridge.is(value, System.IFormattable)) {
                    value = Bridge.format(Bridge.unbox(value, true), formatStr, provider);
                } if (Bridge.isNumber(value)) {
                    value = Bridge.Int.format(value, formatStr, provider);
                } else if (Bridge.isDate(value)) {
                    value = System.DateTime.format(value, formatStr, provider);
                } else {
                    value = "" + Bridge.toString(value);
                }

                if (alignment) {
                    alignment = parseInt(alignment, 10);

                    if (!Bridge.isNumber(alignment)) {
                        alignment = null;
                    }
                }

                return System.String.alignString(Bridge.toString(value), alignment);
            },

            decodeBraceSequence: function (braces, remove) {
                return braces.substr(0, (braces.length + (remove ? 0 : 1)) / 2);
            },

            alignString: function (str, alignment, pad, dir, cut) {
                if (str == null || !alignment) {
                    return str;
                }

                if (!pad) {
                    pad = " ";
                }

                if (Bridge.isNumber(pad)) {
                    pad = String.fromCharCode(pad);
                }

                if (!dir) {
                    dir = alignment < 0 ? 1 : 2;
                }

                alignment = Math.abs(alignment);

                if (cut && (str.length > alignment)) {
                    str = str.substring(0, alignment);
                }

                if (alignment + 1 >= str.length) {
                    switch (dir) {
                        case 2:
                            str = Array(alignment + 1 - str.length).join(pad) + str;
                            break;

                        case 3:
                            var padlen = alignment - str.length,
                                right = Math.ceil(padlen / 2),
                                left = padlen - right;

                            str = Array(left + 1).join(pad) + str + Array(right + 1).join(pad);
                            break;

                        case 1:
                        default:
                            str = str + Array(alignment + 1 - str.length).join(pad);
                            break;
                    }
                }

                return str;
            },

            startsWith: function (str, prefix) {
                if (!prefix.length) {
                    return true;
                }

                if (prefix.length > str.length) {
                    return false;
                }

                return System.String.equals(str.slice(0, prefix.length), prefix, arguments[2]);
            },

            endsWith: function (str, suffix) {
                if (!suffix.length) {
                    return true;
                }

                if (suffix.length > str.length) {
                    return false;
                }

                return System.String.equals(str.slice(str.length - suffix.length, str.length), suffix, arguments[2]);
            },

            contains: function (str, value) {
                if (value == null) {
                    throw new System.ArgumentNullException();
                }

                if (str == null) {
                    return false;
                }

                return str.indexOf(value) > -1;
            },

            indexOfAny: function (str, anyOf) {
                if (anyOf == null) {
                    throw new System.ArgumentNullException();
                }

                if (str == null || str === "") {
                    return -1;
                }

                var startIndex = (arguments.length > 2) ? arguments[2] : 0;

                if (startIndex < 0) {
                    throw new System.ArgumentOutOfRangeException.$ctor4("startIndex", "startIndex cannot be less than zero");
                }

                var length = str.length - startIndex;

                if (arguments.length > 3 && arguments[3] != null) {
                    length = arguments[3];
                }

                if (length < 0) {
                    throw new System.ArgumentOutOfRangeException.$ctor4("length", "must be non-negative");
                }

                if (length > str.length - startIndex) {
                    throw new System.ArgumentOutOfRangeException.$ctor4("length", "Index and length must refer to a location within the string");
                }

                length = startIndex + length;
                anyOf = String.fromCharCode.apply(null, anyOf);

                for (var i = startIndex; i < length; i++) {
                    if (anyOf.indexOf(str.charAt(i)) >= 0) {
                        return i;
                    }
                }

                return -1;
            },

            indexOf: function (str, value) {
                if (value == null) {
                    throw new System.ArgumentNullException();
                }

                if (str == null || str === "") {
                    return -1;
                }

                var startIndex = (arguments.length > 2) ? arguments[2] : 0;

                if (startIndex < 0 || startIndex > str.length) {
                    throw new System.ArgumentOutOfRangeException.$ctor4("startIndex", "startIndex cannot be less than zero and must refer to a location within the string");
                }

                if (value === "") {
                    return (arguments.length > 2) ? startIndex : 0;
                }

                var length = str.length - startIndex;

                if (arguments.length > 3 && arguments[3] != null) {
                    length = arguments[3];
                }

                if (length < 0) {
                    throw new System.ArgumentOutOfRangeException.$ctor4("length", "must be non-negative");
                }

                if (length > str.length - startIndex) {
                    throw new System.ArgumentOutOfRangeException.$ctor4("length", "Index and length must refer to a location within the string");
                }

                var s = str.substr(startIndex, length),
                    index = (arguments.length === 5 && arguments[4] % 2 !== 0) ? s.toLocaleUpperCase().indexOf(value.toLocaleUpperCase()) : s.indexOf(value);

                if (index > -1) {
                    if (arguments.length === 5) {
                        // StringComparison
                        return (System.String.compare(value, s.substr(index, value.length), arguments[4]) === 0) ? index + startIndex : -1;
                    } else {
                        return index + startIndex;
                    }
                }

                return -1;
            },

            equals: function () {
                return System.String.compare.apply(this, arguments) === 0;
            },

            swapCase: function (letters) {
                return letters.replace(/\w/g, function (c) {
                    if (c === c.toLowerCase()) {
                        return c.toUpperCase();
                    } else {
                        return c.toLowerCase();
                    }
                });
            },

            compare: function (strA, strB) {
                if (strA == null) {
                    return (strB == null) ? 0 : -1;
                }

                if (strB == null) {
                    return 1;
                }

                if (arguments.length >= 3) {
                    if (!Bridge.isBoolean(arguments[2])) {
                        // StringComparison
                        switch (arguments[2]) {
                            case 1: // CurrentCultureIgnoreCase
                                return strA.localeCompare(strB, System.Globalization.CultureInfo.getCurrentCulture().name, {
                                    sensitivity: "accent"
                                });
                            case 2: // InvariantCulture
                                return strA.localeCompare(strB, System.Globalization.CultureInfo.invariantCulture.name);
                            case 3: // InvariantCultureIgnoreCase
                                return strA.localeCompare(strB, System.Globalization.CultureInfo.invariantCulture.name, {
                                    sensitivity: "accent"
                                });
                            case 4: // Ordinal
                                return (strA === strB) ? 0 : ((strA > strB) ? 1 : -1);
                            case 5: // OrdinalIgnoreCase
                                return (strA.toUpperCase() === strB.toUpperCase()) ? 0 : ((strA.toUpperCase() > strB.toUpperCase()) ? 1 : -1);
                            case 0: // CurrentCulture
                            default:
                                break;
                        }
                    } else {
                        // ignoreCase
                        if (arguments[2]) {
                            strA = strA.toLocaleUpperCase();
                            strB = strB.toLocaleUpperCase();
                        }

                        if (arguments.length === 4) {
                            // CultureInfo
                            return strA.localeCompare(strB, arguments[3].name);
                        }
                    }
                }

                return strA.localeCompare(strB);
            },

            toCharArray: function (str, startIndex, length) {
                if (startIndex < 0 || startIndex > str.length || startIndex > str.length - length) {
                    throw new System.ArgumentOutOfRangeException.$ctor4("startIndex", "startIndex cannot be less than zero and must refer to a location within the string");
                }

                if (length < 0) {
                    throw new System.ArgumentOutOfRangeException.$ctor4("length", "must be non-negative");
                }

                if (!Bridge.hasValue(startIndex)) {
                    startIndex = 0;
                }

                if (!Bridge.hasValue(length)) {
                    length = str.length;
                }

                var arr = [];

                for (var i = startIndex; i < startIndex + length; i++) {
                    arr.push(str.charCodeAt(i));
                }

                return arr;
            },

            escape: function (str) {
                return str.replace(/[\-\[\]\/\{\}\(\)\*\+\?\.\\\^\$\|]/g, "\\$&");
            },

            replaceAll: function (str, a, b) {
                var reg = new RegExp(System.String.escape(a), "g");

                return str.replace(reg, b);
            },

            insert: function (index, strA, strB) {
                return index > 0 ? (strA.substring(0, index) + strB + strA.substring(index, strA.length)) : (strB + strA);
            },

            remove: function (s, index, count) {
                if (s == null) {
                    throw new System.NullReferenceException();
                }

                if (index < 0) {
                    throw new System.ArgumentOutOfRangeException.$ctor4("startIndex", "StartIndex cannot be less than zero");
                }

                if (count != null) {
                    if (count < 0) {
                        throw new System.ArgumentOutOfRangeException.$ctor4("count", "Count cannot be less than zero");
                    }

                    if (count > s.length - index) {
                        throw new System.ArgumentOutOfRangeException.$ctor4("count", "Index and count must refer to a location within the string");
                    }
                } else {
                    if (index >= s.length) {
                        throw new System.ArgumentOutOfRangeException.$ctor4("startIndex", "startIndex must be less than length of string");
                    }
                }

                if (count == null || ((index + count) > s.length)) {
                    return s.substr(0, index);
                }

                return s.substr(0, index) + s.substr(index + count);
            },

            split: function (s, strings, limit, options) {
                var re = (!Bridge.hasValue(strings) || strings.length === 0) ? new RegExp("\\s", "g") : new RegExp(strings.map(System.String.escape).join("|"), "g"),
                    res = [],
                    m,
                    i;

                for (i = 0; ; i = re.lastIndex) {
                    if (m = re.exec(s)) {
                        if (options !== 1 || m.index > i) {
                            if (res.length === limit - 1) {
                                res.push(s.substr(i));

                                return res;
                            } else {
                                res.push(s.substring(i, m.index));
                            }
                        }
                    } else {
                        if (options !== 1 || i !== s.length) {
                            res.push(s.substr(i));
                        }

                        return res;
                    }
                }
            },

            trimEnd: function (str, chars) {
                return str.replace(chars ? new RegExp("[" + System.String.escape(String.fromCharCode.apply(null, chars)) + "]+$") : /\s*$/, "");
            },

            trimStart: function (str, chars) {
                return str.replace(chars ? new RegExp("^[" + System.String.escape(String.fromCharCode.apply(null, chars)) + "]+") : /^\s*/, "");
            },

            trim: function (str, chars) {
                return System.String.trimStart(System.String.trimEnd(str, chars), chars);
            },

            trimStartZeros: function (str) {
                return str.replace(new RegExp("^[ 0+]+(?=.)"), "");
            },

            concat: function (values) {
                var list = (arguments.length == 1 && Array.isArray(values)) ? values : [].slice.call(arguments),
                    s = "";

                for (var i = 0; i < list.length; i++) {
                    s += list[i] == null ? "" : Bridge.toString(list[i]);
                }

                return s;
            },

            copyTo: function (str, sourceIndex, destination, destinationIndex, count) {
                if (destination == null) {
                    throw new System.ArgumentNullException.$ctor1("destination");
                }

                if (str == null) {
                    throw new System.ArgumentNullException.$ctor1("str");
                }

                if (count < 0) {
                    throw new System.ArgumentOutOfRangeException.$ctor1("count");
                }

                if (sourceIndex < 0) {
                    throw new System.ArgumentOutOfRangeException.$ctor1("sourceIndex");
                }

                if (count > str.length - sourceIndex) {
                    throw new System.ArgumentOutOfRangeException.$ctor1("sourceIndex");
                }

                if (destinationIndex > destination.length - count || destinationIndex < 0) {
                    throw new System.ArgumentOutOfRangeException.$ctor1("destinationIndex");
                }

                if (count > 0) {
                    for (var i = 0; i < count; i++) {
                        destination[destinationIndex + i] = str.charCodeAt(sourceIndex + i);
                    }
                }
            }
        }
    });

    Bridge.Class.addExtend(System.String, [System.IComparable$1(System.String), System.IEquatable$1(System.String)]);
