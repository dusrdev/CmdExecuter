namespace CmdExecuter.Core.Models {
    internal class CommandExecutionError : CommandExecutionResult {
        public CommandExecutionError(string command, string output) {
            Command = command;
            Output = output;
        }
    }
}
