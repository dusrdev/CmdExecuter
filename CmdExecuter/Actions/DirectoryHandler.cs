using CmdExecuter.Core.Components;

using System;

using static CmdExecuter.Core.UI;

namespace CmdExecuter.Actions {
    internal class DirectoryHandler {
        private DirectoryCreator Creator { get; init; }

        public DirectoryHandler() {
            Creator = new();
        }

        public string GetResourcesPath() {
            return Creator.ResourcesDirectoryPath;
        }

        public bool IsReady() {
            if (!Creator.DoesResourcesDirectoryExist()) {
                Print("Resources directory wasn't found, creating directory ", false);
                Print("--> ", ConsoleColor.Cyan, false);
                Creator.CreateResourcesDirectory().Switch(
                    success => {
                        Print("Success", ConsoleColor.Green);
                        NewLine();
                        Print("Now add command files and re-launch the application.");
                    },
                    error => {
                        Print("Fail", ConsoleColor.Red);
                        NewLine();
                        Print(new string[] { "Please create a directory inside the current directory and call it ", "Resources", ", and add your command files inside, then relaunch the application." },
                            new ConsoleColor[] { BaseColor, HighlightColor, BaseColor });
                    });
                NewLine();
                return false;
            }

            if (Creator.IsResourcesEmpty()) {
                Print(new string[] { "Resources directory is ", "Empty", ", add command files and re-launch the application." },
                    new ConsoleColor[] { BaseColor, HighlightColor, BaseColor });
                NewLine();
                return false;
            }

            return true;
        }
    }
}
