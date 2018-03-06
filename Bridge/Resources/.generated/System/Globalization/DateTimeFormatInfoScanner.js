    Bridge.define("System.Globalization.DateTimeFormatInfoScanner", {
        statics: {
            fields: {
                MonthPostfixChar: 0,
                IgnorableSymbolChar: 0,
                CJKYearSuff: null,
                CJKMonthSuff: null,
                CJKDaySuff: null,
                KoreanYearSuff: null,
                KoreanMonthSuff: null,
                KoreanDaySuff: null,
                KoreanHourSuff: null,
                KoreanMinuteSuff: null,
                KoreanSecondSuff: null,
                CJKHourSuff: null,
                ChineseHourSuff: null,
                CJKMinuteSuff: null,
                CJKSecondSuff: null,
                s_knownWords: null
            },
            props: {
                KnownWords: {
                    get: function () {
                        if (System.Globalization.DateTimeFormatInfoScanner.s_knownWords == null) {
                            var temp = new (System.Collections.Generic.Dictionary$2(System.String,System.String))();
                            // Add known words into the hash table.

                            // Skip these special symbols.                        
                            temp.add("/", "");
                            temp.add("-", "");
                            temp.add(".", "");
                            // Skip known CJK suffixes.
                            temp.add(System.Globalization.DateTimeFormatInfoScanner.CJKYearSuff, "");
                            temp.add(System.Globalization.DateTimeFormatInfoScanner.CJKMonthSuff, "");
                            temp.add(System.Globalization.DateTimeFormatInfoScanner.CJKDaySuff, "");
                            temp.add(System.Globalization.DateTimeFormatInfoScanner.KoreanYearSuff, "");
                            temp.add(System.Globalization.DateTimeFormatInfoScanner.KoreanMonthSuff, "");
                            temp.add(System.Globalization.DateTimeFormatInfoScanner.KoreanDaySuff, "");
                            temp.add(System.Globalization.DateTimeFormatInfoScanner.KoreanHourSuff, "");
                            temp.add(System.Globalization.DateTimeFormatInfoScanner.KoreanMinuteSuff, "");
                            temp.add(System.Globalization.DateTimeFormatInfoScanner.KoreanSecondSuff, "");
                            temp.add(System.Globalization.DateTimeFormatInfoScanner.CJKHourSuff, "");
                            temp.add(System.Globalization.DateTimeFormatInfoScanner.ChineseHourSuff, "");
                            temp.add(System.Globalization.DateTimeFormatInfoScanner.CJKMinuteSuff, "");
                            temp.add(System.Globalization.DateTimeFormatInfoScanner.CJKSecondSuff, "");

                            System.Globalization.DateTimeFormatInfoScanner.s_knownWords = temp;
                        }
                        return (System.Globalization.DateTimeFormatInfoScanner.s_knownWords);
                    }
                }
            },
            ctors: {
                init: function () {
                    this.MonthPostfixChar = 57344;
                    this.IgnorableSymbolChar = 57345;
                    this.CJKYearSuff = "年";
                    this.CJKMonthSuff = "月";
                    this.CJKDaySuff = "日";
                    this.KoreanYearSuff = "년";
                    this.KoreanMonthSuff = "월";
                    this.KoreanDaySuff = "일";
                    this.KoreanHourSuff = "시";
                    this.KoreanMinuteSuff = "분";
                    this.KoreanSecondSuff = "초";
                    this.CJKHourSuff = "時";
                    this.ChineseHourSuff = "时";
                    this.CJKMinuteSuff = "分";
                    this.CJKSecondSuff = "秒";
                }
            },
            methods: {
                skipWhiteSpacesAndNonLetter: function (pattern, currentIndex) {
                    while (currentIndex < pattern.length) {
                        var ch = pattern.charCodeAt(currentIndex);
                        if (ch === 92) {
                            // Escaped character. Look ahead one character.
                            currentIndex = (currentIndex + 1) | 0;
                            if (currentIndex < pattern.length) {
                                ch = pattern.charCodeAt(currentIndex);
                                if (ch === 39) {
                                    // Skip the leading single quote.  We will
                                    // stop at the first letter.
                                    continue;
                                }
                                // Fall thru to check if this is a letter.
                            } else {
                                // End of string
                                break;
                            }
                        }
                        if (System.Char.isLetter(ch) || ch === 39 || ch === 46) {
                            break;
                        }
                        // Skip the current char since it is not a letter.
                        currentIndex = (currentIndex + 1) | 0;
                    }
                    return (currentIndex);
                },
                scanRepeatChar: function (pattern, ch, index, count) {
                    count.v = 1;
                    while (((index = (index + 1) | 0)) < pattern.length && pattern.charCodeAt(index) === ch) {
                        count.v = (count.v + 1) | 0;
                    }
                    // Return the updated position.
                    return (index);
                },
                getFormatFlagGenitiveMonth: function (monthNames, genitveMonthNames, abbrevMonthNames, genetiveAbbrevMonthNames) {
                    // If we have different names in regular and genitive month names, use genitive month flag.
                    return ((!System.Globalization.DateTimeFormatInfoScanner.equalStringArrays(monthNames, genitveMonthNames) || !System.Globalization.DateTimeFormatInfoScanner.equalStringArrays(abbrevMonthNames, genetiveAbbrevMonthNames)) ? 1 : 0);
                },
                getFormatFlagUseSpaceInMonthNames: function (monthNames, genitveMonthNames, abbrevMonthNames, genetiveAbbrevMonthNames) {
                    var formatFlags = 0;
                    formatFlags |= (System.Globalization.DateTimeFormatInfoScanner.arrayElementsBeginWithDigit(monthNames) || System.Globalization.DateTimeFormatInfoScanner.arrayElementsBeginWithDigit(genitveMonthNames) || System.Globalization.DateTimeFormatInfoScanner.arrayElementsBeginWithDigit(abbrevMonthNames) || System.Globalization.DateTimeFormatInfoScanner.arrayElementsBeginWithDigit(genetiveAbbrevMonthNames) ? 32 : 0);

                    formatFlags |= (System.Globalization.DateTimeFormatInfoScanner.arrayElementsHaveSpace(monthNames) || System.Globalization.DateTimeFormatInfoScanner.arrayElementsHaveSpace(genitveMonthNames) || System.Globalization.DateTimeFormatInfoScanner.arrayElementsHaveSpace(abbrevMonthNames) || System.Globalization.DateTimeFormatInfoScanner.arrayElementsHaveSpace(genetiveAbbrevMonthNames) ? 4 : 0);
                    return (formatFlags);
                },
                getFormatFlagUseSpaceInDayNames: function (dayNames, abbrevDayNames) {
                    return ((System.Globalization.DateTimeFormatInfoScanner.arrayElementsHaveSpace(dayNames) || System.Globalization.DateTimeFormatInfoScanner.arrayElementsHaveSpace(abbrevDayNames)) ? 16 : 0);
                },
                getFormatFlagUseHebrewCalendar: function (calID) {
                    return (calID === 8 ? 10 : 0);
                },
                equalStringArrays: function (array1, array2) {
                    // Shortcut if they're the same array
                    if (Bridge.referenceEquals(array1, array2)) {
                        return true;
                    }

                    // This is effectively impossible
                    if (array1.length !== array2.length) {
                        return false;
                    }

                    // Check each string 
                    for (var i = 0; i < array1.length; i = (i + 1) | 0) {
                        if (!System.String.equals(array1[System.Array.index(i, array1)], array2[System.Array.index(i, array2)])) {
                            return false;
                        }
                    }

                    return true;
                },
                arrayElementsHaveSpace: function (array) {
                    for (var i = 0; i < array.length; i = (i + 1) | 0) {
                        // it is faster to check for space character manually instead of calling IndexOf
                        // so we don't have to go to native code side.
                        for (var j = 0; j < array[System.Array.index(i, array)].length; j = (j + 1) | 0) {
                            if (System.Char.isWhiteSpace(String.fromCharCode(array[System.Array.index(i, array)].charCodeAt(j)))) {
                                return true;
                            }
                        }
                    }

                    return false;
                },
                arrayElementsBeginWithDigit: function (array) {
                    for (var i = 0; i < array.length; i = (i + 1) | 0) {
                        // it is faster to check for space character manually instead of calling IndexOf
                        // so we don't have to go to native code side.
                        if (array[System.Array.index(i, array)].length > 0 && array[System.Array.index(i, array)].charCodeAt(0) >= 48 && array[System.Array.index(i, array)].charCodeAt(0) <= 57) {
                            var index = 1;
                            while (index < array[System.Array.index(i, array)].length && array[System.Array.index(i, array)].charCodeAt(index) >= 48 && array[System.Array.index(i, array)].charCodeAt(index) <= 57) {
                                // Skip other digits.
                                index = (index + 1) | 0;
                            }
                            if (index === array[System.Array.index(i, array)].length) {
                                return (false);
                            }

                            if (index === ((array[System.Array.index(i, array)].length - 1) | 0)) {
                                // Skip known CJK month suffix.
                                // CJK uses month name like "1\x6708", since \x6708 is a known month suffix,
                                // we don't need the UseDigitPrefixInTokens since it is slower.
                                switch (array[System.Array.index(i, array)].charCodeAt(index)) {
                                    case 26376: 
                                    case 50900:  // CJKMonthSuff // KoreanMonthSuff
                                        return (false);
                                }
                            }

                            if (index === ((array[System.Array.index(i, array)].length - 4) | 0)) {
                                // Skip known CJK month suffix.
                                // Starting with Windows 8, the CJK months for some cultures looks like: "1' \x6708'" 
                                // instead of just "1\x6708"
                                if (array[System.Array.index(i, array)].charCodeAt(index) === 39 && array[System.Array.index(i, array)].charCodeAt(((index + 1) | 0)) === 32 && array[System.Array.index(i, array)].charCodeAt(((index + 2) | 0)) === 26376 && array[System.Array.index(i, array)].charCodeAt(((index + 3) | 0)) === 39) {
                                    return (false);
                                }
                            }
                            return (true);
                        }
                    }

                    return false;
                }
            }
        },
        fields: {
            m_dateWords: null,
            _ymdFlags: 0
        },
        ctors: {
            init: function () {
                this.m_dateWords = new (System.Collections.Generic.List$1(System.String)).ctor();
                this._ymdFlags = System.Globalization.DateTimeFormatInfoScanner.FoundDatePattern.None;
            }
        },
        methods: {
            addDateWordOrPostfix: function (formatPostfix, str) {
                if (str.length > 0) {
                    // Some cultures use . like an abbreviation
                    if (System.String.equals(str, ".")) {
                        this.addIgnorableSymbols(".");
                        return;
                    }
                    var words = { };
                    if (System.Globalization.DateTimeFormatInfoScanner.KnownWords.tryGetValue(str, words) === false) {
                        if (this.m_dateWords == null) {
                            this.m_dateWords = new (System.Collections.Generic.List$1(System.String)).ctor();
                        }
                        if (Bridge.referenceEquals(formatPostfix, "MMMM")) {
                            // Add the word into the ArrayList as "\xfffe" + real month postfix.
                            var temp = String.fromCharCode(System.Globalization.DateTimeFormatInfoScanner.MonthPostfixChar) + (str || "");
                            if (!this.m_dateWords.contains(temp)) {
                                this.m_dateWords.add(temp);
                            }
                        } else {
                            if (!this.m_dateWords.contains(str)) {
                                this.m_dateWords.add(str);
                            }
                            if (str.charCodeAt(((str.length - 1) | 0)) === 46) {
                                // Old version ignore the trialing dot in the date words. Support this as well.
                                var strWithoutDot = str.substr(0, ((str.length - 1) | 0));
                                if (!this.m_dateWords.contains(strWithoutDot)) {
                                    this.m_dateWords.add(strWithoutDot);
                                }
                            }
                        }
                    }
                }
            },
            addDateWords: function (pattern, index, formatPostfix) {
                // Skip any whitespaces so we will start from a letter.
                var newIndex = System.Globalization.DateTimeFormatInfoScanner.skipWhiteSpacesAndNonLetter(pattern, index);
                if (newIndex !== index && formatPostfix != null) {
                    // There are whitespaces. This will not be a postfix.
                    formatPostfix = null;
                }
                index = newIndex;

                // This is the first char added into dateWord.  
                // Skip all non-letter character.  We will add the first letter into DateWord.
                var dateWord = new System.Text.StringBuilder();
                // We assume that date words should start with a letter. 
                // Skip anything until we see a letter.

                while (index < pattern.length) {
                    var ch = pattern.charCodeAt(index);
                    if (ch === 39) {
                        // We have seen the end of quote.  Add the word if we do not see it before, 
                        // and break the while loop.                    
                        this.addDateWordOrPostfix(formatPostfix, dateWord.toString());
                        index = (index + 1) | 0;
                        break;
                    } else if (ch === 92) {
                        //
                        // Escaped character.  Look ahead one character
                        //

                        // Skip escaped backslash.
                        index = (index + 1) | 0;
                        if (index < pattern.length) {
                            dateWord.append(String.fromCharCode(pattern.charCodeAt(index)));
                            index = (index + 1) | 0;
                        }
                    } else if (System.Char.isWhiteSpace(String.fromCharCode(ch))) {
                        // Found a whitespace.  We have to add the current date word/postfix.
                        this.addDateWordOrPostfix(formatPostfix, dateWord.toString());
                        if (formatPostfix != null) {
                            // Done with postfix.  The rest will be regular date word.
                            formatPostfix = null;
                        }
                        // Reset the dateWord.
                        dateWord.setLength(0);
                        index = (index + 1) | 0;
                    } else {
                        dateWord.append(String.fromCharCode(ch));
                        index = (index + 1) | 0;
                    }
                }
                return (index);
            },
            addIgnorableSymbols: function (text) {
                if (this.m_dateWords == null) {
                    // Create the date word array.
                    this.m_dateWords = new (System.Collections.Generic.List$1(System.String)).ctor();
                }
                // Add the ignorable symbol into the ArrayList.
                var temp = String.fromCharCode(System.Globalization.DateTimeFormatInfoScanner.IgnorableSymbolChar) + (text || "");
                if (!this.m_dateWords.contains(temp)) {
                    this.m_dateWords.add(temp);
                }
            },
            scanDateWord: function (pattern) {
                // Check if we have found all of the year/month/day pattern.
                this._ymdFlags = System.Globalization.DateTimeFormatInfoScanner.FoundDatePattern.None;

                var i = 0;
                while (i < pattern.length) {
                    var ch = pattern.charCodeAt(i);
                    var chCount = { };

                    switch (ch) {
                        case 39: 
                            // Find a beginning quote.  Search until the end quote.
                            i = this.addDateWords(pattern, ((i + 1) | 0), null);
                            break;
                        case 77: 
                            i = System.Globalization.DateTimeFormatInfoScanner.scanRepeatChar(pattern, 77, i, chCount);
                            if (chCount.v >= 4) {
                                if (i < pattern.length && pattern.charCodeAt(i) === 39) {
                                    i = this.addDateWords(pattern, ((i + 1) | 0), "MMMM");
                                }
                            }
                            this._ymdFlags |= System.Globalization.DateTimeFormatInfoScanner.FoundDatePattern.FoundMonthPatternFlag;
                            break;
                        case 121: 
                            i = System.Globalization.DateTimeFormatInfoScanner.scanRepeatChar(pattern, 121, i, chCount);
                            this._ymdFlags |= System.Globalization.DateTimeFormatInfoScanner.FoundDatePattern.FoundYearPatternFlag;
                            break;
                        case 100: 
                            i = System.Globalization.DateTimeFormatInfoScanner.scanRepeatChar(pattern, 100, i, chCount);
                            if (chCount.v <= 2) {
                                // Only count "d" & "dd".
                                // ddd, dddd are day names.  Do not count them.
                                this._ymdFlags |= System.Globalization.DateTimeFormatInfoScanner.FoundDatePattern.FoundDayPatternFlag;
                            }
                            break;
                        case 92: 
                            // Found a escaped char not in a quoted string.  Skip the current backslash
                            // and its next character.
                            i = (i + 2) | 0;
                            break;
                        case 46: 
                            if (this._ymdFlags === System.Globalization.DateTimeFormatInfoScanner.FoundDatePattern.FoundYMDPatternFlag) {
                                // If we find a dot immediately after the we have seen all of the y, m, d pattern.
                                // treat it as a ignroable symbol.  Check for comments in AddIgnorableSymbols for
                                // more details.
                                this.addIgnorableSymbols(".");
                                this._ymdFlags = System.Globalization.DateTimeFormatInfoScanner.FoundDatePattern.None;
                            }
                            i = (i + 1) | 0;
                            break;
                        default: 
                            if (this._ymdFlags === System.Globalization.DateTimeFormatInfoScanner.FoundDatePattern.FoundYMDPatternFlag && !System.Char.isWhiteSpace(String.fromCharCode(ch))) {
                                // We are not seeing "." after YMD. Clear the flag.
                                this._ymdFlags = System.Globalization.DateTimeFormatInfoScanner.FoundDatePattern.None;
                            }
                            // We are not in quote.  Skip the current character.
                            i = (i + 1) | 0;
                            break;
                    }
                }
            },
            getDateWordsOfDTFI: function (dtfi) {
                // Enumarate all LongDatePatterns, and get the DateWords and scan for month postfix.
                var datePatterns = dtfi.getAllDateTimePatterns(68);
                var i;

                // Scan the long date patterns
                for (i = 0; i < datePatterns.length; i = (i + 1) | 0) {
                    this.scanDateWord(datePatterns[System.Array.index(i, datePatterns)]);
                }

                // Scan the short date patterns
                datePatterns = dtfi.getAllDateTimePatterns(100);
                for (i = 0; i < datePatterns.length; i = (i + 1) | 0) {
                    this.scanDateWord(datePatterns[System.Array.index(i, datePatterns)]);
                }
                // Scan the YearMonth patterns.
                datePatterns = dtfi.getAllDateTimePatterns(121);
                for (i = 0; i < datePatterns.length; i = (i + 1) | 0) {
                    this.scanDateWord(datePatterns[System.Array.index(i, datePatterns)]);
                }

                // Scan the month/day pattern
                this.scanDateWord(dtfi.monthDayPattern);

                // Scan the long time patterns.
                datePatterns = dtfi.getAllDateTimePatterns(84);
                for (i = 0; i < datePatterns.length; i = (i + 1) | 0) {
                    this.scanDateWord(datePatterns[System.Array.index(i, datePatterns)]);
                }

                // Scan the short time patterns.
                datePatterns = dtfi.getAllDateTimePatterns(116);
                for (i = 0; i < datePatterns.length; i = (i + 1) | 0) {
                    this.scanDateWord(datePatterns[System.Array.index(i, datePatterns)]);
                }

                var result = null;
                if (this.m_dateWords != null && this.m_dateWords.Count > 0) {
                    result = System.Array.init(this.m_dateWords.Count, null, System.String);
                    for (i = 0; i < this.m_dateWords.Count; i = (i + 1) | 0) {
                        result[System.Array.index(i, result)] = this.m_dateWords.getItem(i);
                    }
                }
                return (result);
            }
        }
    });
