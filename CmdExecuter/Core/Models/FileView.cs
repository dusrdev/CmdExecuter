using System.Collections.Generic;

namespace CmdExecuter.Core.Models {
    internal struct FileView {
        public string FileName { get; init; }
        public string[] Commands { get; init; }

        public FileView(string fileName, string[] commands) {
            FileName = fileName;
            Commands = FilterCommands(commands);
        }

        /// <summary>
        /// Filters commands to allow disabling commands in a file
        /// </summary>
        /// <param name="commands"></param>
        private static string[] FilterCommands(string[] commands) {
            List<string> result = new();
            foreach (string command in commands) {
                if (!command.StartsWith("#")) {
                    result.Add(command);
                }
            }
            return result.ToArray();
        }
    }
}
