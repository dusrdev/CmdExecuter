using System.Collections.Generic;

namespace CmdExecuter.Core.Models {
    internal struct FileView {
        public string Name { get; init; }
        public List<string> Lines { get; init; }

        public FileView(string name, List<string> lines) {
            Name = name;
            Lines = FilterCommands(lines);
        }

        /// <summary>
        /// Filters commands to allow disabling commands in a file
        /// </summary>
        /// <param name="commands"></param>
        private static List<string> FilterCommands(List<string> commands) {
            return commands.FindAll(c => !c.StartsWith("#"));
        }
    }
}
