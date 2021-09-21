using OneOf;
using CmdExecuter.Core.Models;
using System.Diagnostics;
using System.Text;

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

        public OneOf<CommandExecutionSuccess, CommandExecutionError, CommandExecutionMix> Execute() {
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

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            process.WaitForExit();

            StandardOutput = StandardOutputBuilder.ToString().Trim();
            ErrorOutput = ErrorOutputBuilder.ToString().Trim();

            OneOf<CommandExecutionSuccess, CommandExecutionError, CommandExecutionMix> result = (StandardOutput.Length, ErrorOutput.Length) switch {
                (0, 0) => new CommandExecutionSuccess(_command, "Execution was successful without any output."),
                (_, 0) => new CommandExecutionSuccess(_command, StandardOutput),
                (0, _) => new CommandExecutionError(_command, ErrorOutput),
                (_, _) => new CommandExecutionMix(_command, StandardOutput, ErrorOutput)
            };

            process.Close();
            process.Dispose();

            return result;
        }

        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e) {
            ErrorOutputBuilder.AppendLine(e.Data);
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e) {
            StandardOutputBuilder.AppendLine(e.Data);
        }
    }
}
