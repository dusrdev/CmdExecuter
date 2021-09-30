using System;
using System.Collections.Generic;

namespace CmdExecuter.Core {
    internal static class UI {
        /// <summary>
        /// The base color of all texts
        /// </summary>
        /// <remarks>By default is <c>ConsoleColor.White</c></remarks>
        public static ConsoleColor BaseColor { get; set; } = ConsoleColor.White;

        /// <summary>
        /// The title color for advanced input options like selection or multi-selection
        /// </summary>
        /// <remarks>By default is <c>ConsoleColor.Cyan</c></remarks>
        public static ConsoleColor TitleColor { get; set; } = ConsoleColor.Cyan;

        /// <summary>
        /// The highlight color
        /// </summary>
        /// <remarks>By default is <c>ConsoleColor.Green</c></remarks>
        public static ConsoleColor HighlightColor { get; set; } = ConsoleColor.Green;

        /// <summary>
        /// The color of user inputs when requested from this class
        /// </summary>
        /// <remarks>By default is <c>ConsoleColor.Green</c></remarks>
        public static ConsoleColor InputColor { get; set; } = ConsoleColor.Green;

        public static void Print(object o, bool endWithNewLine = true) {
            Print(o, BaseColor, endWithNewLine);
        }

        public static void Print(object o, ConsoleColor color, bool endWithNewLine = true) {
            Console.ResetColor();
            Console.ForegroundColor = color;
            if (endWithNewLine) {
                Console.WriteLine(o);
            } else {
                Console.Write(o);
            }
            Console.ResetColor();
        }

        public static void Print(object[] objects, ConsoleColor[] colors, bool endWithNewLine = true) {
            if (objects is null || colors is null || objects.Length != colors.Length) {
                throw new ArgumentException("Invalid parameters");
            }
            Console.ResetColor();
            for (int i = 0; i < objects.Length; i++) {
                Console.ForegroundColor = colors[i];
                Console.Write(objects[i]);
            }
            if (endWithNewLine) {
                Console.WriteLine();
            }
            Console.ResetColor();
        }

        public static void RequestAnyInput(string message) {
            Print(message, BaseColor, false);
            Console.ForegroundColor = InputColor;
            _ = Console.Read();
        }

        public static bool Confirm(string message) {
            Print(new string[] { message, "? ", "[", "y", "/", "n", "]: " }, new ConsoleColor[] { BaseColor, HighlightColor, BaseColor, HighlightColor, BaseColor, HighlightColor, BaseColor }, false);
            Console.ForegroundColor = InputColor;
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input) || input is "y" or "Y") {
                return true;
            }
            return false;
        }

        public static string Selection(string title, IEnumerable<string> choices) {
            Print(title, TitleColor);
            Dictionary<int, string> dict = new();
            int i = 1;
            foreach (string choice in choices) {
                Print(new string[] { $"\t{i}", $". {choice}" }, new ConsoleColor[] { HighlightColor, BaseColor });
                dict.Add(i, choice);
                i++;
            }
            Console.ForegroundColor = BaseColor;
            Console.WriteLine();
            Print("Enter your choice: ", BaseColor, false);

            Console.ForegroundColor = InputColor;

            string input = Console.ReadLine();

            input = input.Trim();

            if (string.IsNullOrEmpty(input)) {
                throw new ArgumentNullException(nameof(input));
            }
            if (!int.TryParse(input, out int result)) {
                throw new ArgumentException(nameof(input));
            }
            if (!dict.ContainsKey(result)) {
                throw new ArgumentOutOfRangeException(nameof(input));
            }

            return dict[result];
        }

        public static List<string> MultiSelection(string title, IEnumerable<string> choices) {
            Print(title, TitleColor);
            Dictionary<int, string> dict = new();
            int i = 1;
            foreach (string choice in choices) {
                Print(new string[] { $"\t{i}", $". {choice}" }, new ConsoleColor[] { HighlightColor, BaseColor });
                dict.Add(i, choice);
                i++;
            }
            Console.ForegroundColor = BaseColor;
            Console.WriteLine();
            Print("Enter your choices separated with spaces: ", BaseColor, false);

            List<string> results = new();

            Console.ForegroundColor = InputColor;

            string input = Console.ReadLine();

            string[] selected = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            foreach (string choice in selected) {
                var trimmed = choice.Trim();
                if (string.IsNullOrEmpty(trimmed)) {
                    throw new ArgumentNullException(nameof(choice));
                }
                if (!int.TryParse(trimmed, out int num)) {
                    throw new ArgumentException(nameof(choice));
                }
                if (!dict.ContainsKey(num)) {
                    throw new ArgumentOutOfRangeException(nameof(choice));
                }
                results.Add(dict[num]);
            }

            Console.ResetColor();
            return results;
        }

        public static void Clear() {
            Console.Clear();
        }

        public static void NewLine() {
            Console.WriteLine();
        }
    }
}
