    Bridge.define("System.ThrowHelper", {
        statics: {
            methods: {
                throwArrayTypeMismatchException: function () {
                    throw System.NotImplemented.ByDesign;
                    // TODO: NotSupported
                    //throw new ArrayTypeMismatchException();
                },
                throwInvalidTypeWithPointersNotSupported: function (targetType) {
                    // TODO: SR
                    //throw new ArgumentException(SR.Format(SR.Argument_InvalidTypeWithPointersNotSupported, targetType));
                    throw new System.ArgumentException(System.SR.format("Cannot use type '{0}'. Only value types without pointers or references are supported.", targetType));
                },
                throwIndexOutOfRangeException: function () {
                    throw new System.IndexOutOfRangeException();
                },
                throwArgumentOutOfRangeException: function () {
                    throw new System.ArgumentOutOfRangeException();
                },
                throwArgumentOutOfRangeException$1: function (argument) {
                    throw new System.ArgumentOutOfRangeException(System.ThrowHelper.getArgumentName(argument));
                },
                throwArgumentOutOfRangeException$2: function (argument, resource) {
                    throw System.ThrowHelper.getArgumentOutOfRangeException(argument, resource);
                },
                throwArgumentOutOfRangeException$3: function (argument, paramNumber, resource) {
                    throw System.ThrowHelper.getArgumentOutOfRangeException$1(argument, paramNumber, resource);
                },
                throwArgumentException_DestinationTooShort: function () {
                    // TODO: SR
                    //throw new ArgumentException(SR.Argument_DestinationTooShort);
                    throw new System.ArgumentException("Destination is too short.");
                },
                throwArgumentException_OverlapAlignmentMismatch: function () {
                    // TODO: SR
                    //throw new ArgumentException(SR.Argument_OverlapAlignmentMismatch);
                    throw new System.ArgumentException("Overlapping spans have mismatching alignment.");
                },
                throwArgumentOutOfRange_IndexException: function () {
                    throw System.ThrowHelper.getArgumentOutOfRangeException(System.ExceptionArgument.index, System.ExceptionResource.ArgumentOutOfRange_Index);
                },
                throwIndexArgumentOutOfRange_NeedNonNegNumException: function () {
                    throw System.ThrowHelper.getArgumentOutOfRangeException(System.ExceptionArgument.index, System.ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
                },
                throwLengthArgumentOutOfRange_ArgumentOutOfRange_NeedNonNegNum: function () {
                    throw System.ThrowHelper.getArgumentOutOfRangeException(System.ExceptionArgument.$length, System.ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
                },
                throwStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index: function () {
                    throw System.ThrowHelper.getArgumentOutOfRangeException(System.ExceptionArgument.startIndex, System.ExceptionResource.ArgumentOutOfRange_Index);
                },
                throwCountArgumentOutOfRange_ArgumentOutOfRange_Count: function () {
                    throw System.ThrowHelper.getArgumentOutOfRangeException(System.ExceptionArgument.count, System.ExceptionResource.ArgumentOutOfRange_Count);
                },
                throwWrongKeyTypeArgumentException: function (T, key, targetType) {
                    // Generic key to move the boxing to the right hand side of throw
                    throw System.ThrowHelper.getWrongKeyTypeArgumentException(key, targetType);
                },
                throwWrongValueTypeArgumentException: function (T, value, targetType) {
                    // Generic key to move the boxing to the right hand side of throw
                    throw System.ThrowHelper.getWrongValueTypeArgumentException(value, targetType);
                },
                getAddingDuplicateWithKeyArgumentException: function (key) {
                    // TODO: SR
                    //return new ArgumentException(SR.Format(SR.Argument_AddingDuplicateWithKey, key));
                    return new System.ArgumentException(System.SR.format("An item with the same key has already been added. Key: {0}", key));
                },
                throwAddingDuplicateWithKeyArgumentException: function (T, key) {
                    // Generic key to move the boxing to the right hand side of throw
                    throw System.ThrowHelper.getAddingDuplicateWithKeyArgumentException(key);
                },
                throwKeyNotFoundException: function (T, key) {
                    // Generic key to move the boxing to the right hand side of throw
                    throw System.ThrowHelper.getKeyNotFoundException(key);
                },
                throwArgumentException: function (resource) {
                    throw System.ThrowHelper.getArgumentException(resource);
                },
                throwArgumentException$1: function (resource, argument) {
                    throw System.ThrowHelper.getArgumentException$1(resource, argument);
                },
                getArgumentNullException: function (argument) {
                    return new System.ArgumentNullException(System.ThrowHelper.getArgumentName(argument));
                },
                throwArgumentNullException: function (argument) {
                    throw System.ThrowHelper.getArgumentNullException(argument);
                },
                throwArgumentNullException$2: function (resource) {
                    throw new System.ArgumentNullException(System.ThrowHelper.getResourceString(resource));
                },
                throwArgumentNullException$1: function (argument, resource) {
                    throw new System.ArgumentNullException(System.ThrowHelper.getArgumentName(argument), System.ThrowHelper.getResourceString(resource));
                },
                throwInvalidOperationException: function (resource) {
                    throw System.ThrowHelper.getInvalidOperationException(resource);
                },
                throwInvalidOperationException$1: function (resource, e) {
                    throw new System.InvalidOperationException(System.ThrowHelper.getResourceString(resource), e);
                },
                throwInvalidOperationException_OutstandingReferences: function () {
                    System.ThrowHelper.throwInvalidOperationException(System.ExceptionResource.Memory_OutstandingReferences);
                },
                throwSerializationException: function (resource) {
                    throw System.NotImplemented.ByDesign;

                    // TODO: NotSupported
                    //throw new SerializationException(GetResourceString(resource));
                },
                throwSecurityException: function (resource) {
                    throw System.NotImplemented.ByDesign;

                    // TODO: NotSupported
                    //throw new System.Security.SecurityException(GetResourceString(resource));
                },
                throwRankException: function (resource) {
                    throw new System.RankException(System.ThrowHelper.getResourceString(resource));
                },
                throwNotSupportedException$1: function (resource) {
                    throw new System.NotSupportedException(System.ThrowHelper.getResourceString(resource));
                },
                throwNotSupportedException: function () {
                    throw new System.NotSupportedException();
                },
                throwUnauthorizedAccessException: function (resource) {
                    throw System.NotImplemented.ByDesign;

                    // TODO: NotSupported
                    //throw new UnauthorizedAccessException(GetResourceString(resource));
                },
                throwObjectDisposedException$1: function (objectName, resource) {
                    throw System.NotImplemented.ByDesign;

                    // TODO: NotSupported
                    //throw new ObjectDisposedException(objectName, GetResourceString(resource));
                },
                throwObjectDisposedException: function (resource) {
                    throw System.NotImplemented.ByDesign;

                    // TODO: NotSupported
                    //throw new ObjectDisposedException(null, GetResourceString(resource));
                },
                throwObjectDisposedException_MemoryDisposed: function () {
                    throw System.NotImplemented.ByDesign;

                    // TODO: NotSupported
                    //throw new ObjectDisposedException("OwnedMemory<T>", GetResourceString(ExceptionResource.MemoryDisposed));
                },
                throwAggregateException: function (exceptions) {
                    throw new System.AggregateException(null, exceptions);
                },
                throwOutOfMemoryException: function () {
                    throw new System.OutOfMemoryException();
                },
                throwArgumentException_Argument_InvalidArrayType: function () {
                    throw System.ThrowHelper.getArgumentException(System.ExceptionResource.Argument_InvalidArrayType);
                },
                throwInvalidOperationException_InvalidOperation_EnumNotStarted: function () {
                    throw System.ThrowHelper.getInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumNotStarted);
                },
                throwInvalidOperationException_InvalidOperation_EnumEnded: function () {
                    throw System.ThrowHelper.getInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumEnded);
                },
                throwInvalidOperationException_EnumCurrent: function (index) {
                    throw System.ThrowHelper.getInvalidOperationException_EnumCurrent(index);
                },
                throwInvalidOperationException_InvalidOperation_EnumFailedVersion: function () {
                    throw System.ThrowHelper.getInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumFailedVersion);
                },
                throwInvalidOperationException_InvalidOperation_EnumOpCantHappen: function () {
                    throw System.ThrowHelper.getInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumOpCantHappen);
                },
                throwInvalidOperationException_InvalidOperation_NoValue: function () {
                    throw System.ThrowHelper.getInvalidOperationException(System.ExceptionResource.InvalidOperation_NoValue);
                },
                throwArraySegmentCtorValidationFailedExceptions: function (array, offset, count) {
                    throw System.ThrowHelper.getArraySegmentCtorValidationFailedException(array, offset, count);
                },
                getArraySegmentCtorValidationFailedException: function (array, offset, count) {
                    if (array == null) {
                        return System.ThrowHelper.getArgumentNullException(System.ExceptionArgument.array);
                    }
                    if (offset < 0) {
                        return System.ThrowHelper.getArgumentOutOfRangeException(System.ExceptionArgument.offset, System.ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
                    }
                    if (count < 0) {
                        return System.ThrowHelper.getArgumentOutOfRangeException(System.ExceptionArgument.count, System.ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
                    }

                    return System.ThrowHelper.getArgumentException(System.ExceptionResource.Argument_InvalidOffLen);
                },
                getArgumentException: function (resource) {
                    return new System.ArgumentException(System.ThrowHelper.getResourceString(resource));
                },
                getArgumentException$1: function (resource, argument) {
                    return new System.ArgumentException(System.ThrowHelper.getResourceString(resource), System.ThrowHelper.getArgumentName(argument));
                },
                getInvalidOperationException: function (resource) {
                    return new System.InvalidOperationException(System.ThrowHelper.getResourceString(resource));
                },
                getWrongKeyTypeArgumentException: function (key, targetType) {
                    // TODO: SR
                    //return new ArgumentException(SR.Format(SR.Arg_WrongType, key, targetType), nameof(key));
                    return new System.ArgumentException(System.SR.format$1("The value \"{0}\" is not of type \"{1}\" and cannot be used in this generic collection.", key, targetType), "key");
                },
                getWrongValueTypeArgumentException: function (value, targetType) {
                    // TODO: SR
                    //return new ArgumentException(SR.Format(SR.Arg_WrongType, value, targetType), nameof(value));
                    return new System.ArgumentException(System.SR.format$1("The value \"{0}\" is not of type \"{1}\" and cannot be used in this generic collection.", value, targetType), "value");
                },
                getKeyNotFoundException: function (key) {
                    // TODO: SR
                    //return new KeyNotFoundException(SR.Format(SR.Arg_KeyNotFoundWithKey, key.ToString()));
                    return new System.Collections.Generic.KeyNotFoundException(System.SR.format("The given key '{0}' was not present in the dictionary.", Bridge.toString(key)));
                },
                getArgumentOutOfRangeException: function (argument, resource) {
                    return new System.ArgumentOutOfRangeException(System.ThrowHelper.getArgumentName(argument), System.ThrowHelper.getResourceString(resource));
                },
                getArgumentOutOfRangeException$1: function (argument, paramNumber, resource) {
                    return new System.ArgumentOutOfRangeException((System.ThrowHelper.getArgumentName(argument) || "") + "[" + (Bridge.toString(paramNumber) || "") + "]", System.ThrowHelper.getResourceString(resource));
                },
                getInvalidOperationException_EnumCurrent: function (index) {
                    return System.ThrowHelper.getInvalidOperationException(index < 0 ? System.ExceptionResource.InvalidOperation_EnumNotStarted : System.ExceptionResource.InvalidOperation_EnumEnded);
                },
                ifNullAndNullsAreIllegalThenThrow: function (T, value, argName) {
                    // Note that default(T) is not equal to null for value types except when T is Nullable<U>.
                    if (!(Bridge.getDefaultValue(T) == null) && value == null) {
                        System.ThrowHelper.throwArgumentNullException(argName);
                    }
                },
                getArgumentName: function (argument) {

                    return System.Enum.toString(System.ExceptionArgument, argument);
                },
                getResourceString: function (resource) {

                    return System.SR.getResourceString(System.Enum.toString(System.ExceptionResource, resource));
                },
                throwNotSupportedExceptionIfNonNumericType: function (T) {
                    if (!Bridge.referenceEquals(T, System.Byte) && !Bridge.referenceEquals(T, System.SByte) && !Bridge.referenceEquals(T, System.Int16) && !Bridge.referenceEquals(T, System.UInt16) && !Bridge.referenceEquals(T, System.Int32) && !Bridge.referenceEquals(T, System.UInt32) && !Bridge.referenceEquals(T, System.Int64) && !Bridge.referenceEquals(T, System.UInt64) && !Bridge.referenceEquals(T, System.Single) && !Bridge.referenceEquals(T, System.Double)) {
                        // TODO: SR
                        //throw new NotSupportedException(SR.Arg_TypeNotSupported);
                        throw new System.NotSupportedException("Specified type is not supported");
                    }
                }
            }
        }
    });
