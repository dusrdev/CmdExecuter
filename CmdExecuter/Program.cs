using CmdExecuter.Actions;
using CmdExecuter.Core.Models;

using Spectre.Console;

namespace CmdExecuter {
    class Program {
        static void Main() {
            System.Console.Title = "CmdExecuter - by David Shnayder";

            var directoryHandler = new DirectoryHandler();

            if (directoryHandler.IsReady()) {
                DisplayMainMenu(directoryHandler.GetResourcesPath());
            }

            AnsiConsole.Markup("[white]Press any key to exit... [/]");
            System.Console.ReadKey();
        }

        /// <summary>
        /// Displays application info
        /// </summary>
        private static void DisplayInfo() {
            new InfoDisplay().Display();
        }

        /// <summary>
        /// Displays file scanner menu
        /// </summary>
        private static void DisplayFileScanner(string pathToResources) {
            new FileScannerDisplay(pathToResources).Display();
        }

        /// <summary>
        /// Displays main menu
        /// </summary>
        private static void DisplayMainMenu(string pathToResources) {
            var selectedOption = new MainMenuDisplay().GetSelection();

            switch (selectedOption) {
                case MainMenuSelection.ScanFolder: {
                        DisplayFileScanner(pathToResources);
                        break;
                    }
                case MainMenuSelection.DisplayInfo: {
                        DisplayInfo();
                        PromptToReturnToMainMenu(pathToResources);
                        break;
                    }
                default: break;
            }
        }

        /// <summary>
        /// Prompts the user to return to main menu
        /// </summary>
        private static void PromptToReturnToMainMenu(string pathToResources) {
            if (AnsiConsole.Confirm("[white]Return to main menu?[/]")) {
                AnsiConsole.Clear();
                DisplayMainMenu(pathToResources);
            }
        }
    }
}
