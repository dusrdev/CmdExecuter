namespace CmdExecuter.Core.Models {
    internal class CommandExecutionMix {
        public string Command { get; init; }
        public string SuccessOutput { get; init; }
        public string ErrorOutput { get; init; }

        public CommandExecutionMix(string command, string successOutput, string errorOutput) {
            Command = command;
            SuccessOutput = successOutput;
            ErrorOutput = errorOutput;
        }
    }
}
