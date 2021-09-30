using CmdExecuter.Actions;
using CmdExecuter.Core.Models;

using static CmdExecuter.Core.UI;

namespace CmdExecuter {
    class Program {
        static void Main() {
            System.Console.Title = "CmdExecuter - by David Shnayder";

            var directoryHandler = new DirectoryHandler();

            if (directoryHandler.IsReady()) {
                //DisplayFileScanner(directoryHandler.GetResourcesPath());
                DisplayMainMenu(directoryHandler.GetResourcesPath());
            }

            RequestAnyInput("Press any key to exit...");
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
            if (Confirm("Return to main menu")) {
                Clear();
                DisplayMainMenu(pathToResources);
            }
        }
    }
}
