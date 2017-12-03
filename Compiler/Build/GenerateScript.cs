using Bridge.Contract;
using Bridge.Translator;
using Bridge.Translator.Logging;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.IO;
using System.Linq;

namespace Bridge.Build
{
    public class BridgeCompilerTask : Task
    {
        [Required]
        public ITaskItem Assembly
        {
            get;
            set;
        }

        public string OutputPath
        {
            get;
            set;
        }

        public string OutDir
        {
            get;
            set;
        }

        [Required]
        public string ProjectPath
        {
            get;
            set;
        }

        [Required]
        public string AssembliesPath
        {
            get;
            set;
        }

        [Required]
        public string AssemblyName
        {
            get;
            set;
        }

        [Required]
        public ITaskItem[] Sources
        {
            get;
            set;
        }

        public string CheckForOverflowUnderflow
        {
            get;
            set;
        }

        public bool NoCore
        {
            get;
            set;
        }

        public string Platform
        {
            get;
            set;
        }

        [Required]
        public string Configuration
        {
            get;
            set;
        }

        [Required]
        public string OutputType
        {
            get;
            set;
        }

        public string DefineConstants
        {
            get;
            set;
        }

        [Required]
        public string RootNamespace
        {
            get;
            set;
        }

#if DEBUG

        /// <summary>
        /// Attaches the process to a debugger once a build event is triggered. If false/absent, does nothing.
        /// This option is similar to Builder.exe's '-d'
        /// </summary>
        public bool AttachDebugger
        {
            get;
            set;
        }

#endif

        private class Executor : MarshalByRefObject
        {
            public bool Execute(dynamic options, TaskLoggingHelper log)
            {
                return (bool)typeof(Bridge.Translator.TranslatorProcessor).GetMethod("TaskRun").Invoke(null, new object[] { options, log });
            }
        }

        public override bool Execute()
        {
#if DEBUG
            if (AttachDebugger)
            {
                System.Diagnostics.Debugger.Launch();
            };
#endif

            AppDomain ad = null;
            try
            {
                var setup = new AppDomainSetup { ApplicationBase = Path.GetDirectoryName(typeof(BridgeCompilerTask).Assembly.Location) };
                ad = AppDomain.CreateDomain("BridgeCompilerTask", null, setup);
                var executor = (Executor)ad.CreateInstanceAndUnwrap(typeof(Executor).Assembly.FullName, typeof(Executor).FullName);
                return executor.Execute(this.GetBridgeOptions(), Log);
            }
            finally
            {
                if (ad != null)
                    AppDomain.Unload(ad);
            }
        }

        private Bridge.Translator.BridgeOptions GetBridgeOptions()
        {
            var bridgeOptions = new Bridge.Translator.BridgeOptions()
            {
                ProjectLocation = this.ProjectPath,
                OutputLocation = this.OutputPath,
                DefaultFileName = Path.GetFileName(this.Assembly.ItemSpec),
                BridgeLocation = Path.Combine(this.AssembliesPath, "Bridge.dll"),
                Rebuild = false,
                ExtractCore = !NoCore,
                Folder = null,
                Recursive = false,
                Lib = null,
                NoCompilation = false,
                NoTimeStamp = null,
                FromTask = true,
                Name = "",
                Sources = GetSources()
            };

            bridgeOptions.ProjectProperties = new ProjectProperties()
            {
                AssemblyName = this.AssemblyName,
                OutputPath = this.OutputPath,
                OutDir = this.OutDir,
                RootNamespace = this.RootNamespace,
                Configuration = this.Configuration,
                Platform = this.Platform,
                DefineConstants = this.DefineConstants,
                CheckForOverflowUnderflow = GetCheckForOverflowUnderflow(),
                OutputType = this.OutputType
            };

            return bridgeOptions;
        }

        private string GetSources()
        {
            if (this.Sources != null && this.Sources.Length > 0)
            {
                var result = string.Join(";", this.Sources.Select(x => x.ItemSpec));

                return result;
            }

            return null;
        }

        private bool? GetCheckForOverflowUnderflow()
        {
            if (this.CheckForOverflowUnderflow == null)
            {
                return null;
            }

            bool b;

            if (bool.TryParse(this.CheckForOverflowUnderflow, out b))
            {
                return b;
            }

            return null;
        }
    }
}