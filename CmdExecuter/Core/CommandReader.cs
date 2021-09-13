using System.Collections.Generic;

namespace CmdExecuter.Core
{
    public class CommandReader
    {
        /// <summary>
        /// Saves the relative file path to the commands list
        /// </summary>
        public string RelativeFilePath { get; init; }

        /// <summary>
        /// The result from the file will be saved here after processing
        /// </summary>
        /// <remarks>
        /// Before processing this list will be <c>null</c>
        /// </remarks>
        public List<string> Results { get; private set; }

        /// <summary>
        /// Initializes the relative file path property
        /// </summary>
        /// <param name="relativeFilePath">Relative file path without extension</param>
        public CommandReader(string relativeFilePath)
        {
            RelativeFilePath = relativeFilePath;
        }

        //TODO: Implement rest [OneOf, Success, Error, IO operations)
    }
}
