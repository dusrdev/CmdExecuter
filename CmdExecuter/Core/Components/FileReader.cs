using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using CmdExecuter.Core.Models;

using OneOf;

namespace CmdExecuter.Core.Components
{
    internal class FileReader
    {
        public List<FileView> Results { get; private set;  }

        public FileReader() {
            Results = new List<FileView>();
        }

        /// <summary>
        /// Used to get a list of tuples (fileName, list of lines), it searches all .txt files in current directory.
        /// </summary>
        /// <param name="token"></param>
        public async Task<OneOf<Success, Error>> ReadLinesFromAllTextFilesInDirectoryAsync() {
            string currentDirectory = Directory.GetCurrentDirectory();
            var textFiles = Directory.GetFiles(currentDirectory, "*.txt");

            ConcurrentBag<Task<FileView>> taskBag = new();
            FileView[] files = null;

            try {
                _ = Parallel.ForEach(textFiles, t => taskBag.Add(GetFileAndLinesAsync(t)));
                files = await Task.WhenAll(taskBag);
            } catch (TaskCanceledException) {
                return new Error("Operation canceled.");
            }

            foreach (var file in files) {
                Results.Add(file);
            }

            return Results.Any() ?
                new Success("Successfully retrieved all lines from all text files in current folder.")
                : new Error("No files have been found...");
        }

        /// <summary>
        /// Returns a FileView
        /// </summary>
        /// <param name="filePath">absolute file path</param>
        /// <param name="token"></param>
        private async Task<FileView> GetFileAndLinesAsync(string filePath, CancellationToken token = default) {
            return new(filePath[(filePath.LastIndexOf('\\') + 1)..], new(await File.ReadAllLinesAsync(filePath, token)));
        }
    }
}
