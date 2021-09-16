using System.Threading.Tasks;

using OneOf;
using CmdExecuter.Core.Models;
using System.Diagnostics;
using System.Threading;
using System.Text;
using CmdExecuter.Core.Helpers;

namespace CmdExecuter.Core.Components {
    internal class CommandExecuter {
        private readonly string _command;

        private StringBuilder ErrorOutput { get; set; }
        private StringBuilder StandardOutput { get; set; }

        private bool LogSuccess { get; init; }

        public CommandExecuter(string command, bool logSuccess) {
            _command = command;
            LogSuccess = logSuccess;
            ErrorOutput = new();
            StandardOutput = new();
        }

        public async Task<OneOf<CommandExecutionSuccess, CommandExecutionError>> ExecuteAsync(CancellationToken token = default) {
            Process process = new();
            ProcessStartInfo startInfo = new();

            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = $"/C {_command}";
            startInfo.UseShellExecute = false;
            process.StartInfo = startInfo;
            process.OutputDataReceived += Process_OutputDataReceived;
            process.ErrorDataReceived += Process_ErrorDataReceived;
            process.Start();
            await process.WaitForExitAsync(token);

            OneOf<CommandExecutionSuccess, CommandExecutionError> result = process.ExitCode switch {
                0 => StandardOutput.Length switch {
                    0 => new CommandExecutionSuccess(_command, "Execution was successful without any output."),
                    _ => new CommandExecutionError(_command, StandardOutput.ToString())
                },
                _ => (StandardOutput.Length, ErrorOutput.Length) switch {
                    (0, _) => new CommandExecutionError(_command, ErrorOutput.EmptyAlternative("Execution failed without any output.")),
                    (_, _) => new CommandExecutionError(_command, $"Success:\n{StandardOutput}\nError:{ErrorOutput}"),
                }
            };

            process.Close();
            process.Dispose();

            return result;
        }

        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e) {
            ErrorOutput.Append(e.Data);
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e) {
            if (LogSuccess) {
                StandardOutput.Append(e.Data);
            }
        }
    }
}
