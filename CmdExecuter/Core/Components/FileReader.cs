using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using CmdExecuter.Core.Helpers;
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

        public async Task<OneOf<Success, Error>> ReadLinesFromAllTextFilesInDirectoryAsync(string directory, CancellationToken token = default) {
            var textFiles = Directory.GetFiles(directory, "*.txt");

            ConcurrentBag<Task<FileView>> taskBag = new();
            FileView[] files = null;

            try {
                ParallelOptions options = new() {
                    CancellationToken = token
                };
                _ = Parallel.ForEach(textFiles, options, t => taskBag.Add(GetFileAndLinesAsync(t, token)));
                files = await Task.WhenAll(taskBag);
            } catch (TaskCanceledException) {
                return new Error("Operation canceled.");
            }

            foreach (var file in files) {
                Results.Add(file);
            }

            Results.Sort(Comparers.FileViewComparer);

            return Results.Any() ?
                new Success("Successfully retrieved all lines from all text files in current folder.")
                : new Error("No files have been found...");
        }

        private async Task<FileView> GetFileAndLinesAsync(string filePath, CancellationToken token = default) {
            return new(Helper.GetFileName(filePath), await File.ReadAllLinesAsync(filePath, token));
        }
    }
}
