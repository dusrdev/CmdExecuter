using CmdExecuter.Core.Components;

using Spectre.Console;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CmdExecuter {
    class Program {
        static async Task Main() {
            CancellationTokenSource stopper = new();

            FileReader reader = new();
            var fileRead = await reader.ReadLinesFromAllTextFilesInDirectoryAsync(stopper.Token);

            fileRead.Switch(success => {
                AnsiConsole.MarkupLine($"[bold]{success.Message}[/]");
            },
            error => {
                AnsiConsole.MarkupLine($"[bold red]{error.Message}[/]");
                return;
            });

            AnsiConsole.MarkupLine(string.Empty);

            var fileNames = reader.Results.Select(f => f.Name);

            var fruits = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                .Title("[springgreen1]Files found:[/]")
                .HighlightStyle(new Style(foreground: Color.White))
                .InstructionsText("[grey85](Press [springgreen1]<space>[/] to toggle a file, [springgreen1]<enter>[/] to accept)[/]")
                .AddChoices(fileNames));

            //TODO: Move to a function || class
            //TODO: Complete the rest
        }
    }
}
