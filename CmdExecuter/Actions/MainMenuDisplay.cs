using CmdExecuter.Core.Models;

using Spectre.Console;

using System.Collections.Generic;

namespace CmdExecuter.Actions {
    internal class MainMenuDisplay {
        public MainMenuDisplay() { }

        public MainMenuSelection GetSelection() {
            Dictionary<string, MainMenuSelection> selectionOptions = new();

            _ = selectionOptions.TryAdd("Scan folder.", MainMenuSelection.ScanFolder);
            _ = selectionOptions.TryAdd("Display info.", MainMenuSelection.DisplayInfo);

            var selection = AnsiConsole.Prompt(new SelectionPrompt<string>().
                Title("[springgreen1]Options:[/]").
                AddChoices(selectionOptions.Keys));

            AnsiConsole.Clear();
            return selectionOptions[selection];
        }
    }
}
