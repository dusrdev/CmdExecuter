# CmdExecuter

**THE APPLICATION IS STILL NOT IN STABLE RELEASE AND REMAINS LARGELY UNTESTED, USE AT YOUR OWN DISCRETION!**

## Description

Lightweight tool that allows the user to execute **cmd** commands from **.txt** files

## Target

This app is intended for system administrators and people who constantly execute different cmd commands and are tired of entering them one by one.

## Usage

1. Create a folder named "Resources".
2. Create .txt files with all the commands you want in the order you want them in.
    - Lines that are prefixed with **"\#"** will be ignored. Use this to disable commands or to write comments.
    - Empty lines will also be ignored.
    - you can create many of them, such as: **"WindowsRestore.txt"**, **"CloudSyncing.txt"** and so on...
3. Place all of them in the "Resources" folder and run **CmdExecuter.exe**.
4. The app will scan and read all commands in all files and categorize them by file name
5. then it will let you select which files you want to execute, and it will execute all the commands of the selected files by order
    - All commands are executed separately so commands like **"cd .."** will not be remembered, if you want to execute commands that should be linked, write them in the same line connected with **"&"** to chain them.
6. and inform you of the result of every command execution, should you want to, it can produce a detailed report.

## Safety tips

- The application was developed with using multiple files in purpose, use this to separate your commands and execute only what is required.
- Always inspect the files **before** execution to prevent unwanted outcomes.
- Make sure to test all commands beforehand to know the predictable outcome.
- Use absolute paths as this calls on **cmd.exe** which might not recognize the paths otherwise.

## Disclaimer

**Use this application at your own risk.** I do not guarantee all commands will work with this. Also, As this application redirect output to executes with the systems cmd.exe file, Many commands execute with different behavior, thus it is nearly impossible to test all cases or to guarantee 100% success.

## Downloads

The following link contains 2 zip files which I compiled, assembled and synced with this public folder:

1. Regular - Light version (single .exe file), depends on the system being up to date and having .NET runtime installed.
2. FrameworkIndependent - Heavier version (folder), contains all the files required to run this app including the .NET runtime, perfect for using on legacy or client machines in which you can't guarantee the conditions.

[Mega.nz public folder](https://mega.nz/folder/prYATJLK#CXktCXklP7xn00u-M3VDwg)

- **Funny note:** I am actually using this application to automate zipping and uploading the releases of itself to the public folder.

If you do not want to download, as with any other open source project, you can pull from the **main** branch and build it yourself.

## FAQ

- Why should I use this application instead of **".bat** files?
This doesn't require you to know .bat commands and syntax rather simply the commands themselves, as well as having a wonderful interface thanks to [Spectre Console](https://spectreconsole.net/).
- I want to request a feature / Add a feature that I coded myself
Feel free to contact me via [email](dusrdev@gmail.com).
- Which version is best to use?
Both versions are compiled from the exact same source code, but the **Regular** version is lighter and should work faster, I recommend the **FrameworkIndependent** version only if compatibility across all computers is necessary.
- I am experiencing issues or errors, what should I do?
Firstly you should use the export report feature and see if the output can help you understand why, If it doesn't help, try manually to execute the command that fails using cmd and see what is the result. If both have been done and the error persists, it might be caused by the app, so please submit the issue here or send to my [email](dusrdev@gmail.com), make sure to add as much info as possible so that I can recreate the issue.
