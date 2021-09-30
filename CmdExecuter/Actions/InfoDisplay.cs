using System;

using static CmdExecuter.Core.UI;

namespace CmdExecuter.Actions {
    internal class InfoDisplay {
        public InfoDisplay() { }

        public void Display() {
            Print("Usage instructions:", TitleColor);
            NewLine();
            Print(new string[] { "1. ", "Create a folder named ", "Resources", "." },
                new ConsoleColor[] { HighlightColor, BaseColor, HighlightColor, BaseColor });
            Print(new string[] { "2. ", "Create .txt files with all the commands you want in the order you want them in." },
                new ConsoleColor[] { HighlightColor, BaseColor });
            Print(new string[] { "\t- ", "Lines that are prefixed with ", "#", " will be ignored. Use this to disable commands or to write comments." },
                new ConsoleColor[] { HighlightColor, BaseColor, HighlightColor, BaseColor });
            Print(new string[] { "\t- ", "you can create many of them, such as: ", "WindowsRestore.txt ", "and", " CloudSyncing.txt", " and so on..." },
                new ConsoleColor[] { HighlightColor, BaseColor, HighlightColor, BaseColor, HighlightColor, BaseColor });
            Print(new string[] { "3. ", "Place all of them in the ", "Resources", " folder you just created and run ", "CmdExecuter.exe" },
                new ConsoleColor[] { HighlightColor, BaseColor, HighlightColor, BaseColor, HighlightColor });

            Print(new string[] { "4. ", "The app will scan and read all commands in all files and categorize them by file name" },
                new ConsoleColor[] { HighlightColor, BaseColor });
            Print(new string[] { "5. ", "then it will let you select which files you want to execute, and it will execute all selected files." },
                new ConsoleColor[] { HighlightColor, BaseColor });
            Print(new string[] { "6. ", "and inform you of the result of every command execution, should you want to, it can produce a detailed report." },
                new ConsoleColor[] { HighlightColor, BaseColor });
            NewLine();

            Console.BackgroundColor = HighlightColor;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Make sure to use this application only to execute commands that don't require further input as it redirects the output streams and may cause unforeseen bugs or crashes");
            Console.ResetColor();
            NewLine();

            Print("Safety tips:", TitleColor);
            NewLine();
            Print(new string[] { "- ", "The application was developed with using multiple files in purpose, use this to separate your commands and execute only what is required." },
                new ConsoleColor[] { HighlightColor, BaseColor });
            Print(new string[] { "- ", "Always inspect the files and test commands before execution to prevent unwanted outcomes." },
                new ConsoleColor[] { HighlightColor, BaseColor });
            Print(new string[] { "- ", "Use absolute paths as this calls on cmd.exe which might not recognize the paths otherwise." },
                new ConsoleColor[] { HighlightColor, BaseColor });

            NewLine();
            Print("For more information, go the GitHub repository:", TitleColor);
            Print("https://github.com/dusrdev/CmdExecuter", ConsoleColor.Magenta);

            NewLine();
            Print("Errors:", TitleColor);
            Print("If you encounter any errors feel free to post them in the repository or email me at:");
            Print("dusrdev@gmail.com", ConsoleColor.Magenta);
            NewLine();

            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Disclaimer: Use this application in your own risk");
            Console.ResetColor();
            NewLine();
        }
    }
}
