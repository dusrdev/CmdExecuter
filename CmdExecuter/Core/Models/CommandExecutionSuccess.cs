namespace CmdExecuter.Core.Models {
    internal class CommandExecutionSuccess : CommandExecutionResult {
        public CommandExecutionSuccess(string command, string output) {
            Command = command;
            Output = output;
        }
    }
}
