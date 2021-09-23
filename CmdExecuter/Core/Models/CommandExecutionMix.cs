namespace CmdExecuter.Core.Models {
    internal class CommandExecutionMix : CommandExecutionResult {
        public CommandExecutionMix(string command, string successfulOutput, string errorOutput) {
            Command = command;
            SuccessfulOutput = successfulOutput;
            ErrorOutput = errorOutput;
        }
    }
}
