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

        private StringBuilder ErrorOutputBuilder { get; set; }
        private StringBuilder StandardOutputBuilder { get; set; }

        private string ErrorOutput { get; set; }
        private string StandardOutput {  get; set; }

        public CommandExecuter(string command) {
            _command = command;
            ErrorOutputBuilder = new();
            StandardOutputBuilder = new();
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

            StandardOutput = StandardOutputBuilder.Length == 0 ?
                await process.StandardOutput.ReadToEndAsync()
                : StandardOutputBuilder.ToString();

            ErrorOutput = ErrorOutputBuilder.Length == 0 ?
                await process.StandardError.ReadToEndAsync()
                : ErrorOutputBuilder.ToString();

            OneOf <CommandExecutionSuccess, CommandExecutionError> result = process.ExitCode switch {
                0 => StandardOutput.Length switch {
                    0 => new CommandExecutionSuccess(_command, "Execution was successful without any output."),
                    _ => new CommandExecutionError(_command, StandardOutput)
                },
                _ => (StandardOutput.Length, ErrorOutput.Length) switch {
                    (0, _) => new CommandExecutionError(_command, StandardOutput.EmptyAlternative("Execution failed without any output.")),
                    (_, _) => new CommandExecutionError(_command, $"Success:\n{StandardOutput}\nError:{ErrorOutput}"),
                }
            };

            process.Close();
            process.Dispose();

            return result;
        }

        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e) {
            ErrorOutputBuilder.Append(e.Data);
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e) {
            StandardOutputBuilder.Append(e.Data);
        }
    }
}
