    Bridge.define("System.SR", {
        statics: {
            fields: {
                _lock: null
            },
            props: {
                ResourceManager: null
            },
            ctors: {
                init: function () {
                    this._lock = { };
                }
            },
            methods: {
                UsingResourceKeys: function () {
                    return false;
                },
                GetResourceString: function (resourceKey) {
                    return System.SR.GetResourceString$1(resourceKey, "");
                },
                GetResourceString$1: function (resourceKey, defaultString) {
                    var resourceString = null;
                    try {
                        resourceString = System.SR.InternalGetResourceString(resourceKey);
                    }
                    catch ($e1) {
                        $e1 = System.Exception.create($e1);
                        if (Bridge.is($e1, System.Resources.MissingManifestResourceException)) {
                        } else {
                            throw $e1;
                        }
                    }

                    if (defaultString != null && System.String.equals(resourceKey, resourceString, 4)) {
                        return defaultString;
                    }

                    return resourceString;
                },
                InternalGetResourceString: function (key) {
                    if (key == null || key.length === 0) {
                        return key;
                    }

                    return key;

                    // TODO: NotSupported



                    //bool lockTaken = false;
                    //try
                    //{
                    //    Monitor.Enter(_lock, ref lockTaken);

                    //    // Are we recursively looking up the same resource?  Note - our backout code will set
                    //    // the ResourceHelper's currentlyLoading stack to null if an exception occurs.
                    //    if (_currentlyLoading != null && _currentlyLoading.Count > 0 && _currentlyLoading.LastIndexOf(key) != -1)
                    //    {
                    //        // We can start infinitely recursing for one resource lookup,
                    //        // then during our failure reporting, start infinitely recursing again.
                    //        // avoid that.
                    //        if (_infinitelyRecursingCount > 0)
                    //        {
                    //            return key;
                    //        }
                    //        _infinitelyRecursingCount++;

                    //        // Note: our infrastructure for reporting this exception will again cause resource lookup.
                    //        // This is the most direct way of dealing with that problem.
                    //        string message = $"Infinite recursion during resource lookup within {System.CoreLib.Name}.  This may be a bug in {System.CoreLib.Name}, or potentially in certain extensibility points such as assembly resolve events or CultureInfo names.  Resource name: {key}";
                    //        Environment.FailFast(message);
                    //    }
                    //    if (_currentlyLoading == null)
                    //        _currentlyLoading = new List<string>();

                    //    // Call class constructors preemptively, so that we cannot get into an infinite
                    //    // loop constructing a TypeInitializationException.  If this were omitted,
                    //    // we could get the Infinite recursion assert above by failing type initialization
                    //    // between the Push and Pop calls below.
                    //    if (!_resourceManagerInited)
                    //    {
                    //        RuntimeHelpers.RunClassConstructor(typeof(ResourceManager).TypeHandle);
                    //        RuntimeHelpers.RunClassConstructor(typeof(ResourceReader).TypeHandle);
                    //        RuntimeHelpers.RunClassConstructor(typeof(RuntimeResourceSet).TypeHandle);
                    //        RuntimeHelpers.RunClassConstructor(typeof(BinaryReader).TypeHandle);
                    //        _resourceManagerInited = true;
                    //    }

                    //    _currentlyLoading.Add(key); // Push

                    //    if (ResourceManager == null)
                    //    {
                    //        ResourceManager = new ResourceManager(SR.ResourceType);
                    //    }
                    //    string s = ResourceManager.GetString(key, null);
                    //    _currentlyLoading.RemoveAt(_currentlyLoading.Count - 1); // Pop

                    //    Debug.Assert(s != null, "Managed resource string lookup failed.  Was your resource name misspelled?  Did you rebuild mscorlib after adding a resource to resources.txt?  Debug this w/ cordbg and bug whoever owns the code that called SR.GetResourceString.  Resource name was: \"" + key + "\"");
                    //    return s ?? key;
                    //}
                    //catch
                    //{
                    //    if (lockTaken)
                    //    {
                    //        // Backout code - throw away potentially corrupt state
                    //        ResourceManager = null;
                    //        _currentlyLoading = null;
                    //    }
                    //    throw;
                    //}
                    //finally
                    //{
                    //    if (lockTaken)
                    //    {
                    //        Monitor.Exit(_lock);
                    //    }
                    //}
                },
                Format$3: function (resourceFormat, args) {
                    if (args === void 0) { args = []; }
                    if (args != null) {
                        if (System.SR.UsingResourceKeys()) {
                            return (resourceFormat || "") + (args.join(", ") || "");
                        }

                        return System.String.format.apply(System.String, [resourceFormat].concat(args));
                    }

                    return resourceFormat;
                },
                Format: function (resourceFormat, p1) {
                    if (System.SR.UsingResourceKeys()) {
                        return [resourceFormat, p1].join(", ");
                    }

                    return System.String.format(resourceFormat, [p1]);
                },
                Format$1: function (resourceFormat, p1, p2) {
                    if (System.SR.UsingResourceKeys()) {
                        return [resourceFormat, p1, p2].join(", ");
                    }

                    return System.String.format(resourceFormat, p1, p2);
                },
                Format$2: function (resourceFormat, p1, p2, p3) {
                    if (System.SR.UsingResourceKeys()) {
                        return [resourceFormat, p1, p2, p3].join(", ");
                    }
                    return System.String.format(resourceFormat, p1, p2, p3);
                }
            }
        }
    });
