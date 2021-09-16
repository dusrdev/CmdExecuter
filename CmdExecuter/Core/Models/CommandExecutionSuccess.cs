namespace CmdExecuter.Core.Models {
    internal class CommandExecutionSuccess {
        public string Command { get; init; }
        public string Message {  get; init; }

        public CommandExecutionSuccess(string command, string message) {
            Command = command;
            Message = message;
        }
    }
}
