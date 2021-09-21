using CmdExecuter.Core.Components;
using CmdExecuter.Core.Helpers;
using CmdExecuter.Core.Models;

using OneOf;

using Spectre.Console;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace CmdExecuter.Actions {
    internal class FileHandler {
        private SortedSet<FileView> Files { get; set; }

        private SortedSet<FileExecutionOutput> FileOutputs { get; set; }

        private int Successes = 0;
        private int Errors = 0;

        public FileHandler() { }

        /// <summary>
        /// Gets a list of the selected file names
        /// </summary>
        public void ScanForFiles() {
            FileReader reader = new();
            var fileRead = Task.Run(() => reader.ReadLinesFromAllTextFilesInDirectoryAsync());
            fileRead.Wait();

            fileRead.Result.Switch(success => {
                AnsiConsole.MarkupLine($"[bold]{success.Message}[/]");
                AnsiConsole.MarkupLine(string.Empty);
                Files = reader.Results;
                SelectFiles();
            },
            error => {
                AnsiConsole.MarkupLine($"[bold #990000]{error.Message}[/]");
                return;
            });
        }

        /// <summary>
        /// Selects the files the user wants to execute
        /// </summary>
        /// <remarks>
        /// Is only executed if <c>ScanForFiles()</c> finds files...
        /// </remarks>
        private void SelectFiles() {
            SortedSet<string> fileNames = new();

            foreach (var file in Files) {
                fileNames.Add(file.FileName);
            }

            var selectedFileNames = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                .Title("[springgreen1]Files found:[/]")
                .HighlightStyle(new Style(foreground: Color.White))
                .InstructionsText("[grey85](Press [springgreen1]<space>[/] to toggle a file, [springgreen1]<enter>[/] to accept)[/]")
                .AddChoices(fileNames));

            HashSet<string> selected = new(selectedFileNames);

            List<FileView> UnselectedFiles = new();

            foreach (var file in Files) {
                if (!selected.Contains(file.FileName)) {
                    UnselectedFiles.Add(file);
                }
            }

            foreach (var file in UnselectedFiles) {
                _ = Files.Remove(file);
            }
        }


        /// <summary>
        /// Executes all commands
        /// </summary>
        public void Execute() {
            FileOutputs = new(Comparers.FileExecutionOutputComparer);

            AnsiConsole.MarkupLine("");

            AnsiConsole.MarkupLine("[violet]Beginning Execution.[/]");

            foreach (var file in Files) {
                FileExecutionOutput fileOutput = new(file.FileName);

                AnsiConsole.MarkupLine("");
                AnsiConsole.MarkupLine($"[white][violet]++[/] Executing [springgreen1]{file.Commands.Length}[/] commands in file: [springgreen1]{file.FileName}[/][/]");
                AnsiConsole.MarkupLine("");

                for (int i = 0; i < file.Commands.Length; i++) {
                    AnsiConsole.Markup($"[White][violet]++++++[/] Executing command: [darkslategray1]{i + 1}[/][violet]  -->  [/][/]");

                    AnsiConsole.Status().SpinnerStyle = new Style(foreground: Color.SpringGreen1);

                    var executionResult = new CommandExecuter(file.Commands[i]).Execute();

                    executionResult.Switch(success => {
                        AnsiConsole.MarkupLine("[springgreen1]Success[/]");
                        fileOutput.AddResult(success);
                        Successes++;
                    },
                        error => {
                            AnsiConsole.MarkupLine("[#990000]Fail[/]");
                            fileOutput.AddResult(error);
                            Errors++;
                        },
                        mix => {
                            AnsiConsole.MarkupLine("[darkslategray1]Mixed output[/]");
                            fileOutput.AddResult(mix);
                            Successes++;
                            Errors++;
                        });
                }

                FileOutputs.Add(fileOutput);
            }

            PromptToExportReport();
        }

        /// <summary>
        /// Gives the user the option to export errors to report or dismiss and follows up on selection.
        /// </summary>
        private void PromptToExportReport() {
            string title = (Successes, Errors) switch {
                (0, 0) => "[bold #990000]Nothing has been executed...[/]",
                (_, 0) => "[bold white]All commands have executed [springgreen1]successfully[/][/]",
                (0, _) => "[white]all commands have [#990000]failed[/]...[/]",
                (_, _) => "[bold white]There are [darkslategray1]mixed[/] results, some commands were [springgreen1]successful[/] and some [#990000]failed[/].[/]"
            };

            AnsiConsole.MarkupLine("");
            AnsiConsole.MarkupLine(title);

            AnsiConsole.MarkupLine("");

            if (AnsiConsole.Confirm("[white]Do you want to export detailed [yellow]HTML[/] report to folder?[/]")) {
                ExportReport();
            }
        }

        /// <summary>
        /// Attempts to export execution errors
        /// </summary>
        private void ExportReport() {
            AnsiConsole.MarkupLine("");
            var reportExporter = new ReportExporter(FileOutputs);
            reportExporter.CreateReport();
            var exportResult = reportExporter.Export();
            exportResult.Switch(
                success => {
                    AnsiConsole.MarkupLine($"[springgreen1]{success.Message}[/]");
                },
                error => {
                    AnsiConsole.MarkupLine($"[#990000]{error.Message}[/]");
                });
            AnsiConsole.MarkupLine("");
        }
    }
}
