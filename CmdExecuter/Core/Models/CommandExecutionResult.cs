namespace CmdExecuter.Core.Models {
    internal abstract class CommandExecutionResult {
        public string Command { get; init; }
        public string Output { get; init; }
    }
}
