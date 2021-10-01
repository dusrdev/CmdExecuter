using CmdExecuter.Core.Components;
using CmdExecuter.Core.Models;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using static CmdExecuter.Core.UI;

namespace CmdExecuter.Actions {
    internal class FileHandler {
        private List<FileView> Files { get; set; }

        private List<FileView> SelectedFiles { get; set; }

        private List<FileExecutionOutput> FileOutputs { get; set; }

        private Stopwatch Watch { get; init; }

        private string PathToResources { get; init; }

        private int Successes = 0;
        private int Errors = 0;

        private string ComputerName { get; set; }
        private string RoundedSuccessRate { get; set; }
        private string ExecutionTime { get; set; }

        public FileHandler(string pathToResources) {
            PathToResources = pathToResources;
            Watch = new Stopwatch();
        }

        /// <summary>
        /// Gets a list of the selected file names
        /// </summary>
        public void ScanForFiles() {
            ComputerName = Environment.MachineName;
            FileReader reader = new();
            var fileRead = Task.Run(() => reader.ReadLinesFromAllTextFilesInDirectoryAsync(PathToResources));
            fileRead.Wait();

            fileRead.Result.Switch(success => {
                Print(success.Message);
                NewLine();
                Files = reader.Results;
                SelectFiles();
            },
            error => {
                Print(error.Message, ConsoleColor.Red);
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
            List<string> fileNames = new();

            foreach (var file in Files) {
                fileNames.Add(file.FileName);
            }

            if (Files.Count is 0) {
                Print("No files were found...", ConsoleColor.Red);
                return;
            }

            var selectedFileNames = MultiSelection("Files found:", fileNames);

            SelectedFiles = new();

            foreach (var fileName in selectedFileNames) {
                var file = Files.Find(f => f.FileName == fileName);
                SelectedFiles.Add(file);
            }

            Files = null;
        }


        /// <summary>
        /// Executes all commands
        /// </summary>
        public void Execute() {
            NewLine();
            Print("Selected files in order of execution:", ConsoleColor.Cyan);
            foreach (var file in SelectedFiles) {
                Print($"\t{file.FileName}");
            }
            NewLine();
            if (!Confirm("Confirm execution")) {
                return;
            }

            NewLine();

            FileOutputs = new();
            Print("Beginning Execution.", ConsoleColor.Magenta);
            NewLine();

            Watch.Start();

            foreach (var file in SelectedFiles) {
                FileExecutionOutput fileOutput = new(file.FileName);

                Print(new string[] { "== ", "Executing ", $"{file.Commands.Length} ", "commands in file: ", $"{file.FileName}" },
                    new ConsoleColor[] { ConsoleColor.Magenta, BaseColor, ConsoleColor.Green, BaseColor, ConsoleColor.Green });
                NewLine();

                foreach (var command in file.Commands) {

                    Print(new string[] { "==== ", "Executing command:" },
                        new ConsoleColor[] { ConsoleColor.Magenta, BaseColor });
                    Print(new string[] { "-------- ", $"{command}" },
                        new ConsoleColor[] { BaseColor, ConsoleColor.Cyan });
                    Print("---------------->  ", ConsoleColor.Magenta, false);

                    var executionResult = new CommandExecuter(command).Execute();

                    executionResult.Switch(
                        success => {
                            Print("Success", ConsoleColor.Green);
                            fileOutput.AddResult(success);
                            Successes++;
                        },
                        error => {
                            Print("Failed", ConsoleColor.Red);
                            fileOutput.AddResult(error);
                            Errors++;
                        },
                        mix => {
                            Print("Mixed output", ConsoleColor.Cyan);
                            fileOutput.AddResult(mix);
                            Successes++;
                            Errors++;
                        });
                    NewLine();
                }

                FileOutputs.Add(fileOutput);
            }

            ExecutionTime = Watch.Stop();

            PromptToExportReport();
        }

        /// <summary>
        /// Gives the user the option to export errors to report or dismiss and follows up on selection.
        /// </summary>
        private void PromptToExportReport() {
            switch ((Successes, Errors)) {
                case (0, 0): {
                        Print("Nothing has been executed...", ConsoleColor.Red);
                        break;
                    }
                case (_, 0): {
                        Print(new string[] { "All commands have executed ", "successfully" },
                            new ConsoleColor[] { BaseColor, ConsoleColor.Green });
                        break;
                    }
                case (0, _): {
                        Print(new string[] { "All commands have ", "failed..." },
                            new ConsoleColor[] { BaseColor, ConsoleColor.Red });
                        break;
                    }
                default: {
                        Print(new string[] { "There are ", "mixed ", "results, some commands were ", "successful ", "and some ", "failed", "." },
                            new ConsoleColor[] { BaseColor, ConsoleColor.Cyan, BaseColor, ConsoleColor.Green, BaseColor, ConsoleColor.Red, BaseColor });
                        break;
                    }
            }
            NewLine();
            DisplayStatistics();
            NewLine();

            if (Confirm("Do you want to export detailed HTML report to folder")) {
                ExportReport();
            }
        }

        /// <summary>
        /// Attempts to export execution errors
        /// </summary>
        private void ExportReport() {
            NewLine();
            var reportExporter = new ReportExporter(ComputerName, FileOutputs, ExecutionTime, RoundedSuccessRate);
            reportExporter.CreateReport();
            var exportResult = reportExporter.Export();
            exportResult.Switch(
                success => {
                    Print(success.Message, ConsoleColor.Green);
                },
                error => {
                    Print(error.Message, ConsoleColor.Red);
                });
            NewLine();
        }

        /// <summary>
        /// Displays success rate at the end of all execution
        /// </summary>
        private void DisplayStatistics() {
            int total = Successes + Errors;

            if (total == 0) {
                return;
            }

            decimal SuccessRate = ((decimal)Successes / (decimal)total) * 100;
            RoundedSuccessRate = $"{SuccessRate:0.##}";

            Print(new string[] { "Execution time: ", ExecutionTime, ", success rate: ", $"{RoundedSuccessRate}%" },
                new ConsoleColor[] { BaseColor, ConsoleColor.Green, BaseColor, ConsoleColor.Green });
        }
    }
}
