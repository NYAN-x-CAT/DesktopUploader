using System;

namespace DesktopUploader
{
    public static class ConsoleHelper
    {
        public static void Info()
        {
            Console.Title = "Desktop Uplaoder v1.0";
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("[#]Desktop Uploader v1.0: Search And Upload Files");
            Console.WriteLine("[#]Developed: NYAN CAT@hf");
            Console.WriteLine("[#]Website: GitHub.com/NYAN-x-CAT\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            Banner();
        }

        public static void Banner()
        {
            Console.Write(@"
________            ______ _____               
___  __ \______________  /___  /______________ 
__  / / /  _ \_  ___/_  //_/  __/  __ \__  __ \
_  /_/ //  __/(__  )_  ,<  / /_ / /_/ /_  /_/ /
/_____/ \___//____/ /_/|_| \__/ \____/_  .___/ 
                                      /_/      ");
            Console.Write(@"
_____  __       ______            _________            
__  / / /__________  /___________ ______  /____________
_  / / /___  __ \_  /_  __ \  __ `/  __  /_  _ \_  ___/
/ /_/ / __  /_/ /  / / /_/ / /_/ // /_/ / /  __/  /    
\____/  _  .___//_/  \____/\__,_/ \__,_/  \___//_/     
        /_/                                            

");
        }

        public static void DeletePrevConsoleLine()
        {
            if (Console.CursorTop == 0) return;
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
    }
}
