using Spectre.Console;

namespace CmdExecuter.Actions {
    internal class InfoDisplay {
        public InfoDisplay() { }

        public void Display() {
            AnsiConsole.Render(new Markup(string.Join('\n', new string[] {
                "[white][bold deepskyblue1]CmdExecuter[/] by [bold springgreen1]David Shnayder[/]",
                "",
                "[yellow]Overview:[/]",
                "This is an open source application, developed in c# .NET 5.",
                "This application is a very useful tool primarily targeting system administrators.",
                "",
                "If you have ever had the need to execute many CMD (command prompt) commands and it was uncomfortable for you",
                "because you had to remember all the commands and their order and so on, then this is for you.",
                "",
                "[yellow]Usage Instructions:[/]",
                "Create .txt files with all the commands you want in the order you want them in.",
                "Write every command like you would in CMD, if you want to disable/ignore a command, prefix it with [#990000]#[/]",
                "you can create many of them, such as: [lightsteelblue1]\"WindowsRestore.txt\"[/], [lightsteelblue1]\"CloudSyncing.txt\"[/] and so on...",
                "Place all of them in a folder together with [bold deepskyblue1]CmdExecuter.exe[/].",
                "The app will scan and read all commands in all files and categorize them by file name",
                "then it will let you select which files you want to execute, and it will execute all selected files by order",
                "and inform you of the result of every command execution.",
                "",
                "[yellow]Credits:[/]",
                "For convenience and better user experience this app uses [violet]2[/] third-party packages: [violet]SpectreConsole[/] and [violet]OneOf[/].[/]\n",
            })));
            AnsiConsole.MarkupLine("");
        }
    }
}
