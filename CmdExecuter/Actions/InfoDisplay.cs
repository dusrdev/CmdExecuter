using Spectre.Console;

namespace CmdExecuter.Actions {
    internal class InfoDisplay {
        public InfoDisplay() { }

        public void Display() {
            AnsiConsole.Render(new Markup(string.Join('\n', new string[] {
                "[white][yellow]Usage Instructions:[/]",
                "",
                "[springgreen1]1.[/] Create .txt files with all the commands you want in the order you want them in.",
                "  [springgreen1]*[/] Lines that are prefixed with [springgreen1]#[/] will be ignored. Use this to disable commands or to write comments.",
                "  [springgreen1]*[/] you can create many of them, such as: [lightsteelblue1]\"WindowsRestore.txt\"[/], [lightsteelblue1]\"CloudSyncing.txt\"[/] and so on...",
                "[springgreen1]2.[/] Place all of them in a folder together with [bold deepskyblue1]CmdExecuter.exe[/].",
                "[springgreen1]3.[/] The app will scan and read all commands in all files and categorize them by file name",
                "[springgreen1]4.[/] then it will let you select which files you want to execute, and it will execute all selected files by order",
                "[springgreen1]5.[/] and inform you of the result of every command execution, should you want to, it can produce a detailed report.",
                "",
                "For more information, go the GitHub repository:",
                "[violet]https://github.com/dusrdev/CmdExecuter[/]\n[/]",
            })));
            AnsiConsole.MarkupLine("");
        }
    }
}
