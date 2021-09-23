using Spectre.Console;

namespace CmdExecuter.Actions {
    internal class InfoDisplay {
        public InfoDisplay() { }

        public void Display() {
            AnsiConsole.Render(new Markup(@"[white][underline springgreen1]Usage Instructions:[/]

[springgreen1]1.[/] Create a folder named ""[springgreen1]Resources[/]"".
[springgreen1]2.[/] Create .txt files with all the commands you want in the order you want them in.
    [springgreen1]-[/] Lines that are prefixed with [springgreen1]#[/] will be ignored. Use this to disable commands or to write comments.
    [springgreen1]-[/] you can create many of them, such as: [deepskyblue1]""WindowsRestore.txt""[/],[deepskyblue1]""CloudSyncing.txt""[/] and so on...
[springgreen1]3.[/] Place all of them in the ""[springgreen1]Resources[/]"" folder you just created and run [bold deepskyblue1]CmdExecuter.exe[/].
[springgreen1]4.[/] The app will scan and read all commands in all files and categorize them by file name
[springgreen1]5.[/] then it will let you select which files you want to execute, and it will execute all selected files by order.
[springgreen1]6.[/] and inform you of the result of every command execution, should you want to, it can produce a detailed report.

[springgreen1 invert]Make sure to use this application only to execute commands that don't require further input
as it redirects the output streams and may cause unforeseen bugs or crashes[/]

[underline springgreen1]Safety tips:[/]

[springgreen1]-[/] The application was developed with using multiple files in purpose, use this to separate
your commands and execute only what is required.
[springgreen1]-[/] Always inspect the files before execution to prevent unwanted outcomes.
[springgreen1]-[/] Make sure to test all commands beforehand to know the predictable outcome.
[springgreen1]-[/] Use absolute paths as this calls on cmd.exe which might not recognize the paths otherwise.

For more information, go the GitHub repository:
[underline violet]https://github.com/dusrdev/CmdExecuter[/]

[underline springgreen1]Errors:[/]
If you encounter any errors feel free to post them in the repository or email me at:
[underline violet]dusrdev@gmail.com[/]


[Invert red]Disclaimer:[/]
Use this application in your own risk.
[/]"
            ));
            AnsiConsole.MarkupLine("");
        }
    }
}
