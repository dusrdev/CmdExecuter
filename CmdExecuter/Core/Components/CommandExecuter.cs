using System.Threading.Tasks;

using OneOf;
using CmdExecuter.Core.Models;
using System.Diagnostics;
using System.Threading;
using System.Text;

namespace CmdExecuter.Core.Components {
    internal class CommandExecuter {
        private readonly string _command;

        private StringBuilder OutputErrors { get; set; }

        public CommandExecuter(string command) {
            _command = command;
        }

        public async Task<OneOf<Success, CommandExecutionError>> ExecuteAsync(CancellationToken token = default) {
            Process process = new();
            ProcessStartInfo startInfo = new();
            OutputErrors = new();
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

            //var err = process.StandardError.ReadToEnd();
            //var scc = process.StandardOutput.ReadToEnd();

            OneOf<Success, CommandExecutionError> result = OutputErrors.Length switch {
                0 => new Success(""),
                _ => new CommandExecutionError(_command, OutputErrors.ToString()),
            };

            //process.Dispose();
            process.Close();

            return result;
        }

        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e) {
            OutputErrors.Append(e.Data);
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e) {
        }
    }
}
