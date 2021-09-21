﻿using OneOf;

using System.Collections.Generic;

namespace CmdExecuter.Core.Models {
    internal class FileExecutionOutput {
        public string FileName { get; init; }

        public List<OneOf<CommandExecutionSuccess, CommandExecutionError>> Results { get; private set; }

        public FileExecutionOutput(string fileName) {
            FileName = fileName;
            Results = new();
        }

        public void AddResult(OneOf<CommandExecutionSuccess, CommandExecutionError> result) {
            Results.Add(result);
        }
    }
}
