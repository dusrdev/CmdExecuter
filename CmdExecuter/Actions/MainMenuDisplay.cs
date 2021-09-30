using CmdExecuter.Core.Models;

using System.Collections.Generic;

using static CmdExecuter.Core.UI;

namespace CmdExecuter.Actions {
    internal class MainMenuDisplay {
        public MainMenuDisplay() { }

        public MainMenuSelection GetSelection() {
            Dictionary<string, MainMenuSelection> selectionOptions = new();

            _ = selectionOptions.TryAdd("Scan folder.", MainMenuSelection.ScanFolder);
            _ = selectionOptions.TryAdd("Display info.", MainMenuSelection.DisplayInfo);

            var selected = Selection("Main menu:", selectionOptions.Keys);

            Clear();
            return selectionOptions[selected];
        }
    }
}
