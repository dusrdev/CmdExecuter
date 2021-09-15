using CmdExecuter.Actions;
using CmdExecuter.Core.Models;

using Spectre.Console;

using System.Threading.Tasks;

namespace CmdExecuter {
    class Program {
        static void Main() {
            DisplayMainMenu();
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
        private static void DisplayFileScanner() {
            new FileScannerDisplay().Display();
        }

        /// <summary>
        /// Displays main menu
        /// </summary>
        private static void DisplayMainMenu() {
            var selectedOption = new MainMenuDisplay().GetSelection();

            switch (selectedOption) {
                case MainMenuSelection.ScanFolder: {
                        DisplayFileScanner();
                        break;
                    }
                case MainMenuSelection.DisplayInfo: {
                        DisplayInfo();
                        PromptToReturnToMainMenu();
                        break;
                    }
                default: break;
            }
        }

        /// <summary>
        /// Prompts the user to return to main menu
        /// </summary>
        private static void PromptToReturnToMainMenu() {
            if (AnsiConsole.Confirm("[white]Return to main menu?[/]")) {
                AnsiConsole.Clear();
                DisplayMainMenu();
            }
        }
    }
}
