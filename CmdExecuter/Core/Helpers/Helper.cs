using System.Text;

namespace CmdExecuter.Core.Helpers {
    internal static class Helper {
        /// <summary>
        /// Returns the file name without extension from an absolute path
        /// </summary>
        /// <param name="path">Absolute path</param>
        public static string GetFileName(string path) {
            if (string.IsNullOrEmpty(path) || !path.Contains('\\')) {
                return string.Empty;
            }
            return path[(path.LastIndexOf('\\') + 1)..path.LastIndexOf('.')];
        }


        /// <summary>
        /// Returns the string in the string builder if not empty, or alternative if it is empty.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="alternative"></param>
        public static string EmptyAlternative(this StringBuilder builder, string alternative) {
            if (string.IsNullOrEmpty(builder.ToString())) {
                return alternative;
            }
            return builder.ToString();
        }
    }
}
