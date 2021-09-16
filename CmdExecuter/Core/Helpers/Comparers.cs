using CmdExecuter.Core.Models;

using System.Collections.Generic;

namespace CmdExecuter.Core.Helpers {
    internal static class Comparers {
        public static Comparer<FileView> FileViewComparer = Comparer<FileView>.Create((x1,x2) => x1.FileName.CompareTo(x2.FileName));

        public static Comparer<FileExecutionOutput> FileExecutionOutputComparer = Comparer<FileExecutionOutput>.Create((x1, x2) => x1.FileName.CompareTo(x2.FileName));
    }
}
