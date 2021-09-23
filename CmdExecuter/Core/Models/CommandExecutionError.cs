namespace CmdExecuter.Core.Models {
    internal class CommandExecutionError : CommandExecutionResult {
        public CommandExecutionError(string command, string errorOutput) {
            Command = command;
            SuccessfulOutput = null;
            ErrorOutput = errorOutput;
        }
    }
}
