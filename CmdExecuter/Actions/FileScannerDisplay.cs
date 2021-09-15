namespace CmdExecuter.Actions {
    internal class FileScannerDisplay {
        public FileScannerDisplay() { }

        public void Display() {
            var handler = new FileHandler();
            handler.SelectFiles();
            handler.Execute();
        }
    }
}
