using System.Collections.Generic;

namespace CmdExecuter.Core.Models {
    internal class FileExecutionOutput {
        public string FileName { get; init; }
        public List<CommandExecutionSuccess> Successes { get; private set; }
        public List<CommandExecutionError> Errors { get; private set; }

        public bool HasSuccesses { get; private set; }
        public bool HasErrors { get; private set; }

        public FileExecutionOutput(string fileName) {
            FileName = fileName;
        }

        public void AddSuccess(CommandExecutionSuccess success) {
            if (Successes is null) {
                Successes = new List<CommandExecutionSuccess>();
                HasSuccesses = true;
            }

            Successes.Add(success);
        }

        public void AddError(CommandExecutionError error) {
            if (Errors is null) {
                Errors = new List<CommandExecutionError>();
                HasErrors = true;
            }

            Errors.Add(error);
        }
    }
}
