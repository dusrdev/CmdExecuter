namespace CmdExecuter.Core.Models {
    internal abstract class CommandExecutionResult {
        public string Command { get; init; }
        public string SuccessfulOutput { get; init; }
        public string ErrorOutput { get; init; }
    }
}
