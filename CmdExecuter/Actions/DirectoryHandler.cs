using CmdExecuter.Core.Components;

using Spectre.Console;

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
                AnsiConsole.Markup("[white]Resources directory wasn't found, creating directory [darkslategray1]-->[/] [/]");
                Creator.CreateResourcesDirectory().Switch(
                    success => {
                        AnsiConsole.MarkupLine("[springgreen1]Success[/]");
                        AnsiConsole.MarkupLine("");
                        AnsiConsole.MarkupLine("[white]Now add command files and re-launch the application.[/]");
                    },
                    error => {
                        AnsiConsole.MarkupLine("[#990000]Fail[/]");
                        AnsiConsole.MarkupLine("");
                        AnsiConsole.MarkupLine("[white]Please create a directory inside the current directory and call it [springgreen1]Resources[/], and add your command files inside, then relaunch the application.[/]");
                    });
                AnsiConsole.MarkupLine("");
                return false;
            }

            if (Creator.IsResourcesEmpty()) {
                AnsiConsole.MarkupLine("[white]Resources directory is [#990000]Empty[/], add command files and re-launch the application.[/]");
                AnsiConsole.MarkupLine("");
                return false;
            }

            return true;
        }
    }
}
