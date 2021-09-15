﻿namespace CmdExecuter.Core.Models {
    public class CommandExecutionError {
        public string Command { get; init; }
        public string Error {  get; init; }

        public CommandExecutionError(string command, string error) {
            Command = command;
            Error = error;
        }
    }
}