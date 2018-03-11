    Bridge.define("System.ThrowHelper", {
        statics: {
            methods: {
                ThrowArrayTypeMismatchException: function () {
                    throw System.NotImplemented.ByDesign;
                    // TODO: NotSupported
                    //throw new ArrayTypeMismatchException();
                },
                ThrowInvalidTypeWithPointersNotSupported: function (targetType) {
                    // TODO: SR
                    //throw new ArgumentException(SR.Format(SR.Argument_InvalidTypeWithPointersNotSupported, targetType));
                    throw new System.ArgumentException.$ctor1(System.SR.Format("Cannot use type '{0}'. Only value types without pointers or references are supported.", targetType));
                },
                ThrowIndexOutOfRangeException: function () {
                    throw new System.IndexOutOfRangeException.ctor();
                },
                ThrowArgumentOutOfRangeException: function () {
                    throw new System.ArgumentOutOfRangeException.ctor();
                },
                ThrowArgumentOutOfRangeException$1: function (argument) {
                    throw new System.ArgumentOutOfRangeException.$ctor1(System.ThrowHelper.GetArgumentName(argument));
                },
                ThrowArgumentOutOfRangeException$2: function (argument, resource) {
                    throw System.ThrowHelper.GetArgumentOutOfRangeException(argument, resource);
                },
                ThrowArgumentOutOfRangeException$3: function (argument, paramNumber, resource) {
                    throw System.ThrowHelper.GetArgumentOutOfRangeException$1(argument, paramNumber, resource);
                },
                ThrowArgumentException_DestinationTooShort: function () {
                    // TODO: SR
                    //throw new ArgumentException(SR.Argument_DestinationTooShort);
                    throw new System.ArgumentException.$ctor1("Destination is too short.");
                },
                ThrowArgumentException_OverlapAlignmentMismatch: function () {
                    // TODO: SR
                    //throw new ArgumentException(SR.Argument_OverlapAlignmentMismatch);
                    throw new System.ArgumentException.$ctor1("Overlapping spans have mismatching alignment.");
                },
                ThrowArgumentOutOfRange_IndexException: function () {
                    throw System.ThrowHelper.GetArgumentOutOfRangeException(System.ExceptionArgument.index, System.ExceptionResource.ArgumentOutOfRange_Index);
                },
                ThrowIndexArgumentOutOfRange_NeedNonNegNumException: function () {
                    throw System.ThrowHelper.GetArgumentOutOfRangeException(System.ExceptionArgument.index, System.ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
                },
                ThrowLengthArgumentOutOfRange_ArgumentOutOfRange_NeedNonNegNum: function () {
                    throw System.ThrowHelper.GetArgumentOutOfRangeException(System.ExceptionArgument.$length, System.ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
                },
                ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index: function () {
                    throw System.ThrowHelper.GetArgumentOutOfRangeException(System.ExceptionArgument.startIndex, System.ExceptionResource.ArgumentOutOfRange_Index);
                },
                ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count: function () {
                    throw System.ThrowHelper.GetArgumentOutOfRangeException(System.ExceptionArgument.count, System.ExceptionResource.ArgumentOutOfRange_Count);
                },
                ThrowWrongKeyTypeArgumentException: function (T, key, targetType) {
                    // Generic key to move the boxing to the right hand side of throw
                    throw System.ThrowHelper.GetWrongKeyTypeArgumentException(key, targetType);
                },
                ThrowWrongValueTypeArgumentException: function (T, value, targetType) {
                    // Generic key to move the boxing to the right hand side of throw
                    throw System.ThrowHelper.GetWrongValueTypeArgumentException(value, targetType);
                },
                GetAddingDuplicateWithKeyArgumentException: function (key) {
                    // TODO: SR
                    //return new ArgumentException(SR.Format(SR.Argument_AddingDuplicateWithKey, key));
                    return new System.ArgumentException.$ctor1(System.SR.Format("An item with the same key has already been added. Key: {0}", key));
                },
                ThrowAddingDuplicateWithKeyArgumentException: function (T, key) {
                    // Generic key to move the boxing to the right hand side of throw
                    throw System.ThrowHelper.GetAddingDuplicateWithKeyArgumentException(key);
                },
                ThrowKeyNotFoundException: function (T, key) {
                    // Generic key to move the boxing to the right hand side of throw
                    throw System.ThrowHelper.GetKeyNotFoundException(key);
                },
                ThrowArgumentException: function (resource) {
                    throw System.ThrowHelper.GetArgumentException(resource);
                },
                ThrowArgumentException$1: function (resource, argument) {
                    throw System.ThrowHelper.GetArgumentException$1(resource, argument);
                },
                GetArgumentNullException: function (argument) {
                    return new System.ArgumentNullException.$ctor1(System.ThrowHelper.GetArgumentName(argument));
                },
                ThrowArgumentNullException: function (argument) {
                    throw System.ThrowHelper.GetArgumentNullException(argument);
                },
                ThrowArgumentNullException$2: function (resource) {
                    throw new System.ArgumentNullException.$ctor1(System.ThrowHelper.GetResourceString(resource));
                },
                ThrowArgumentNullException$1: function (argument, resource) {
                    throw new System.ArgumentNullException.$ctor3(System.ThrowHelper.GetArgumentName(argument), System.ThrowHelper.GetResourceString(resource));
                },
                ThrowInvalidOperationException: function (resource) {
                    throw System.ThrowHelper.GetInvalidOperationException(resource);
                },
                ThrowInvalidOperationException$1: function (resource, e) {
                    throw new System.InvalidOperationException.$ctor2(System.ThrowHelper.GetResourceString(resource), e);
                },
                ThrowInvalidOperationException_OutstandingReferences: function () {
                    System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.Memory_OutstandingReferences);
                },
                ThrowSerializationException: function (resource) {
                    throw System.NotImplemented.ByDesign;

                    // TODO: NotSupported
                    //throw new SerializationException(GetResourceString(resource));
                },
                ThrowSecurityException: function (resource) {
                    throw System.NotImplemented.ByDesign;

                    // TODO: NotSupported
                    //throw new System.Security.SecurityException(GetResourceString(resource));
                },
                ThrowRankException: function (resource) {
                    throw new System.RankException.$ctor1(System.ThrowHelper.GetResourceString(resource));
                },
                ThrowNotSupportedException$1: function (resource) {
                    throw new System.NotSupportedException.$ctor1(System.ThrowHelper.GetResourceString(resource));
                },
                ThrowNotSupportedException: function () {
                    throw new System.NotSupportedException.ctor();
                },
                ThrowUnauthorizedAccessException: function (resource) {
                    throw System.NotImplemented.ByDesign;

                    // TODO: NotSupported
                    //throw new UnauthorizedAccessException(GetResourceString(resource));
                },
                ThrowObjectDisposedException$1: function (objectName, resource) {
                    throw System.NotImplemented.ByDesign;

                    // TODO: NotSupported
                    //throw new ObjectDisposedException(objectName, GetResourceString(resource));
                },
                ThrowObjectDisposedException: function (resource) {
                    throw System.NotImplemented.ByDesign;

                    // TODO: NotSupported
                    //throw new ObjectDisposedException(null, GetResourceString(resource));
                },
                ThrowObjectDisposedException_MemoryDisposed: function () {
                    throw System.NotImplemented.ByDesign;

                    // TODO: NotSupported
                    //throw new ObjectDisposedException("OwnedMemory<T>", GetResourceString(ExceptionResource.MemoryDisposed));
                },
                ThrowAggregateException: function (exceptions) {
                    throw new System.AggregateException(null, exceptions);
                },
                ThrowOutOfMemoryException: function () {
                    throw new System.OutOfMemoryException.ctor();
                },
                ThrowArgumentException_Argument_InvalidArrayType: function () {
                    throw System.ThrowHelper.GetArgumentException(System.ExceptionResource.Argument_InvalidArrayType);
                },
                ThrowInvalidOperationException_InvalidOperation_EnumNotStarted: function () {
                    throw System.ThrowHelper.GetInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumNotStarted);
                },
                ThrowInvalidOperationException_InvalidOperation_EnumEnded: function () {
                    throw System.ThrowHelper.GetInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumEnded);
                },
                ThrowInvalidOperationException_EnumCurrent: function (index) {
                    throw System.ThrowHelper.GetInvalidOperationException_EnumCurrent(index);
                },
                ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion: function () {
                    throw System.ThrowHelper.GetInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumFailedVersion);
                },
                ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen: function () {
                    throw System.ThrowHelper.GetInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumOpCantHappen);
                },
                ThrowInvalidOperationException_InvalidOperation_NoValue: function () {
                    throw System.ThrowHelper.GetInvalidOperationException(System.ExceptionResource.InvalidOperation_NoValue);
                },
                ThrowArraySegmentCtorValidationFailedExceptions: function (array, offset, count) {
                    throw System.ThrowHelper.GetArraySegmentCtorValidationFailedException(array, offset, count);
                },
                GetArraySegmentCtorValidationFailedException: function (array, offset, count) {
                    if (array == null) {
                        return System.ThrowHelper.GetArgumentNullException(System.ExceptionArgument.array);
                    }
                    if (offset < 0) {
                        return System.ThrowHelper.GetArgumentOutOfRangeException(System.ExceptionArgument.offset, System.ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
                    }
                    if (count < 0) {
                        return System.ThrowHelper.GetArgumentOutOfRangeException(System.ExceptionArgument.count, System.ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
                    }

                    return System.ThrowHelper.GetArgumentException(System.ExceptionResource.Argument_InvalidOffLen);
                },
                GetArgumentException: function (resource) {
                    return new System.ArgumentException.$ctor1(System.ThrowHelper.GetResourceString(resource));
                },
                GetArgumentException$1: function (resource, argument) {
                    return new System.ArgumentException.$ctor3(System.ThrowHelper.GetResourceString(resource), System.ThrowHelper.GetArgumentName(argument));
                },
                GetInvalidOperationException: function (resource) {
                    return new System.InvalidOperationException.$ctor1(System.ThrowHelper.GetResourceString(resource));
                },
                GetWrongKeyTypeArgumentException: function (key, targetType) {
                    // TODO: SR
                    //return new ArgumentException(SR.Format(SR.Arg_WrongType, key, targetType), nameof(key));
                    return new System.ArgumentException.$ctor3(System.SR.Format$1("The value \"{0}\" is not of type \"{1}\" and cannot be used in this generic collection.", key, targetType), "key");
                },
                GetWrongValueTypeArgumentException: function (value, targetType) {
                    // TODO: SR
                    //return new ArgumentException(SR.Format(SR.Arg_WrongType, value, targetType), nameof(value));
                    return new System.ArgumentException.$ctor3(System.SR.Format$1("The value \"{0}\" is not of type \"{1}\" and cannot be used in this generic collection.", value, targetType), "value");
                },
                GetKeyNotFoundException: function (key) {
                    // TODO: SR
                    //return new KeyNotFoundException(SR.Format(SR.Arg_KeyNotFoundWithKey, key.ToString()));
                    return new System.Collections.Generic.KeyNotFoundException.$ctor1(System.SR.Format("The given key '{0}' was not present in the dictionary.", Bridge.toString(key)));
                },
                GetArgumentOutOfRangeException: function (argument, resource) {
                    return new System.ArgumentOutOfRangeException.$ctor4(System.ThrowHelper.GetArgumentName(argument), System.ThrowHelper.GetResourceString(resource));
                },
                GetArgumentOutOfRangeException$1: function (argument, paramNumber, resource) {
                    return new System.ArgumentOutOfRangeException.$ctor4((System.ThrowHelper.GetArgumentName(argument) || "") + "[" + (Bridge.toString(paramNumber) || "") + "]", System.ThrowHelper.GetResourceString(resource));
                },
                GetInvalidOperationException_EnumCurrent: function (index) {
                    return System.ThrowHelper.GetInvalidOperationException(index < 0 ? System.ExceptionResource.InvalidOperation_EnumNotStarted : System.ExceptionResource.InvalidOperation_EnumEnded);
                },
                IfNullAndNullsAreIllegalThenThrow: function (T, value, argName) {
                    // Note that default(T) is not equal to null for value types except when T is Nullable<U>.
                    if (!(Bridge.getDefaultValue(T) == null) && value == null) {
                        System.ThrowHelper.ThrowArgumentNullException(argName);
                    }
                },
                GetArgumentName: function (argument) {

                    return System.Enum.toString(System.ExceptionArgument, argument);
                },
                GetResourceString: function (resource) {

                    return System.SR.GetResourceString(System.Enum.toString(System.ExceptionResource, resource));
                },
                ThrowNotSupportedExceptionIfNonNumericType: function (T) {
                    if (!Bridge.referenceEquals(T, System.Byte) && !Bridge.referenceEquals(T, System.SByte) && !Bridge.referenceEquals(T, System.Int16) && !Bridge.referenceEquals(T, System.UInt16) && !Bridge.referenceEquals(T, System.Int32) && !Bridge.referenceEquals(T, System.UInt32) && !Bridge.referenceEquals(T, System.Int64) && !Bridge.referenceEquals(T, System.UInt64) && !Bridge.referenceEquals(T, System.Single) && !Bridge.referenceEquals(T, System.Double)) {
                        // TODO: SR
                        //throw new NotSupportedException(SR.Arg_TypeNotSupported);
                        throw new System.NotSupportedException.$ctor1("Specified type is not supported");
                    }
                }
            }
        }
    });
