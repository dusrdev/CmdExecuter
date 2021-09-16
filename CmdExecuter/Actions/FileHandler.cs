﻿using CmdExecuter.Core.Components;
using CmdExecuter.Core.Helpers;
using CmdExecuter.Core.Models;

using OneOf;

using Spectre.Console;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmdExecuter.Actions {
    internal class FileHandler {
        private SortedSet<FileView> Files { get; set; }

        private SortedSet<FileExecutionOutput> FileOutputs { get; set; }

        private bool HasErrorOccurred { get; set; }

        private bool LogSuccess { get; set; }

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

            Files = reader.Results;

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

            List<FileView> UnselectedFiles = new();

            foreach (var file in Files) {
                if (!selectedFileNames.Contains(file.FileName)) {
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

            PromptUserToLogSuccess();
            AnsiConsole.MarkupLine("");

            AnsiConsole.MarkupLine("[violet]Beginning Execution.[/]");

            foreach (var file in Files) {
                FileExecutionOutput fileOutput = new(file.FileName);

                AnsiConsole.MarkupLine("");
                AnsiConsole.MarkupLine($"[white][violet]++[/] Executing file: [springgreen1]{file.FileName}[/][/]");
                AnsiConsole.MarkupLine("");

                foreach (var command in file.Commands) {
                    AnsiConsole.Markup($"[White][violet]++++++[/] Executing command: [darkslategray1]{command}[/][violet]  -->  [/][/]");
                    OneOf<CommandExecutionSuccess, CommandExecutionError> executionResult = default;

                    AnsiConsole.Status().SpinnerStyle = new Style(foreground: Color.SpringGreen1);

                    var execution = Task.Run(() => new CommandExecuter(command, LogSuccess).ExecuteAsync());
                    execution.Wait();
                    executionResult = execution.Result;

                    executionResult.Switch(success => {
                        AnsiConsole.MarkupLine("[springgreen1]Successful![/]");
                        if (LogSuccess) {
                            fileOutput.AddSuccess(success);
                        }
                    },
                        error => {
                            AnsiConsole.MarkupLine("[#990000]Failed![/]");
                            fileOutput.AddError(error);
                            HasErrorOccurred = true;
                        });
                }

                FileOutputs.Add(fileOutput);
            }

            if (LogSuccess || HasErrorOccurred) {
                PromptToExportReport();
            } else {
                AnsiConsole.MarkupLine("\n[bold]All commands have executed [springgreen1]Successfully[/][/]");
            }
        }

        /// <summary>
        /// Prompts the user to log success in addition to errors
        /// </summary>
        private void PromptUserToLogSuccess() {
            LogSuccess = AnsiConsole.Confirm("[white]Do you want to log standard output [yellow]in addition[/] to errors?[/]");
        }

        /// <summary>
        /// Gives the user the option to export errors to report or dismiss and follows up on selection.
        /// </summary>
        private void PromptToExportReport() {
            string title = (HasErrorOccurred, LogSuccess) switch {
                (true, false) => "[bold #990000]Errors have been found...[/]",
                (true, true) => "[white][bold #990000]Some[/] errors have been found...[/]",
                (_, _) => "[bold][white]All commands have executed [springgreen1]successfully![/][/][/]"
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
        }
    }
}
