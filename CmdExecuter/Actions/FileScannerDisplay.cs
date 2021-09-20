namespace CmdExecuter.Actions {
    internal class FileScannerDisplay {
        public FileScannerDisplay() { }

        public void Display() {
            var handler = new FileHandler();
            handler.ScanForFiles();
            handler.Execute();
        }
    }
}
