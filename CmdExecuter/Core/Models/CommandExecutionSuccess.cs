namespace CmdExecuter.Core.Models {
    internal class CommandExecutionSuccess : CommandExecutionResult {
        public CommandExecutionSuccess(string command, string successfulOutput) {
            Command = command;
            SuccessfulOutput = successfulOutput;
            ErrorOutput = null;
        }
    }
}
