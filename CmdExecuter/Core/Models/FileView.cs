using System.Collections.Generic;

namespace CmdExecuter.Core.Models {
    public struct FileView {
        public string Name { get; init; }
        public List<string> Lines { get; init; }

        public FileView(string name, List<string> lines) {
            Name = name;
            Lines = lines;
        }
    }
}
