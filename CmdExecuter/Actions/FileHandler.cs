using CmdExecuter.Core.Components;
using CmdExecuter.Core.Models;

using OneOf;

using Spectre.Console;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmdExecuter.Actions {
    internal class FileHandler {
        private List<FileView> Files { get; set; }

        private List<CommandExecutionError> Errors { get; set; }

        public FileHandler() { }

        /// <summary>
        /// Gets a list of the selected file names
        /// </summary>
        public void SelectFiles() {
            FileReader reader = new();
            var fileRead = Task.Run(() => reader.ReadLinesFromAllTextFilesInDirectoryAsync());

            fileRead.Wait();
            fileRead.Result.Switch(success => {
                AnsiConsole.MarkupLine($"[bold]{success.Message}[/]");
            },
            error => {
                AnsiConsole.MarkupLine($"[bold #990000]{error.Message}[/]");
                return;
            });

            AnsiConsole.MarkupLine(string.Empty);

            var fileNames = reader.Results.Select(f => f.Name);

            var selectedFileNames = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                .Title("[springgreen1]Files found:[/]")
                .HighlightStyle(new Style(foreground: Color.White))
                .InstructionsText("[grey85](Press [springgreen1]<space>[/] to toggle a file, [springgreen1]<enter>[/] to accept)[/]")
                .AddChoices(fileNames));

            Files = reader.Results.FindAll(f => selectedFileNames.Contains(f.Name));
        }

        /// <summary>
        /// Executes all commands
        /// </summary>
        public void Execute() {
            Errors = new();

            AnsiConsole.MarkupLine("[springgreen1]Beginning Execution.[/]");
            AnsiConsole.MarkupLine("");

            foreach (var file in Files) {
                foreach (var command in file.Lines) {
                    AnsiConsole.Markup($"[White]Executing: [darkslategray1]{command}[/][violet]  -->  [/][/]");
                    var execution = Task.Run(() => new CommandExecuter(command).ExecuteAsync());
                    execution.Wait();
                    HandleSingleExecution(execution.Result);
                }
            }

            if (Errors.Any()) {
                PromptToExportReport();
            } else {
                AnsiConsole.MarkupLine("\n[bold]All commands have executed [springgreen1]Successfully[/][/]");
            }
        }

        /// <summary>
        /// Handles a single command execution -> displays result of execution and logs if resulted in error
        /// </summary>
        /// <param name="command">The executed command</param>
        /// <param name="executionResult">Result of execution</param>
        private void HandleSingleExecution(OneOf<Success, CommandExecutionError> executionResult) {
            executionResult.Switch(
                success => {
                    AnsiConsole.MarkupLine("[springgreen1]Successful![/]");
                },
                error => {
                    AnsiConsole.MarkupLine("[#990000]Failed![/]");
                    Errors.Add(error);
                });
        }

        /// <summary>
        /// Gives the user the option to export errors to report or dismiss and follows up on selection.
        /// </summary>
        private void PromptToExportReport() {
            Dictionary<string, ExportOptions> selectionOptions = new();

            _ = selectionOptions.TryAdd(key: "[white]Export [bold]HTML[/] report.[/]", value: ExportOptions.Export);
            _ = selectionOptions.TryAdd("[white]Dismiss.[/]", ExportOptions.Dismiss);

            var selection = AnsiConsole.Prompt(new SelectionPrompt<string>().
                Title("\n[white][bold #990000]Errors have been found, Options[/]:[/]").
                AddChoices(selectionOptions.Keys));

            switch (selectionOptions[selection]) {
                case ExportOptions.Export: {
                        ExportErrors();
                        break;
                    }
                case ExportOptions.Dismiss: {
                        AnsiConsole.MarkupLine("[yellow]Dismissed.[/]");
                        break;
                    }
                default: break;
            }
        }

        /// <summary>
        /// Attempts to export execution errors
        /// </summary>
        private void ExportErrors() {
            var reportExporter = new ErrorExporter(Errors);
            reportExporter.CreateReport();
            var exportResult = reportExporter.Export();
            exportResult.Switch(
                success => {
                    AnsiConsole.MarkupLine($"[springgreen1]{success.Message}[/]");
                },
                error => {
                    AnsiConsole.MarkupLine($"[#990000]{error.Message}[/]");
                });
        }
    }
}
