using System.Threading.Tasks;

using OneOf;
using CmdExecuter.Core.Models;
using System.Diagnostics;
using System.Threading;

namespace CmdExecuter.Core.Components {
    public class CommandExecuter {
        private readonly string _command;

        public CommandExecuter(string command) {
            _command = command;
        }

        public async Task<OneOf<Success, Error>> ExecuteAsync(CancellationToken token = default) {
            Process process = new();
            ProcessStartInfo startInfo = new();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = $"/C {_command}";
            process.StartInfo = startInfo;
            process.Start();
            await process.WaitForExitAsync(token);

            var err = process.StandardError.ReadToEnd();
            var scc = process.StandardOutput.ReadToEnd();

            OneOf<Success, Error> result = (err, scc) switch {
                ("", _) => new Success(scc),
                (_, "") => new Error(err),
                (_, _) => new Success("Execution finished with no output."),
            };

            process.Dispose();

            return result;
        }
    }
}
