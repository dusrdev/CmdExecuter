namespace CmdExecuter.Actions {
    internal class FileScannerDisplay {
        private string PathToResources { get; init; }
        public FileScannerDisplay(string pathToResources) {
            PathToResources = pathToResources;
        }

        public void Display() {
            var handler = new FileHandler(PathToResources);
            handler.ScanForFiles();
            handler.Execute();
        }
    }
}
