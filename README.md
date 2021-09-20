# CmdExecuter

## Description

Lightweight tool that allows to execute **cmd** commands from **.txt** files

## Target

This app is intended for system administrators and people who constantly execute different cmd commands and are tired of entering them one by one.

## Usage

1. Create .txt files with all the commands you want in the order you want them in.
    - Lines that are prefixed with # will be ignored. Use this to disable commands or to write comments.
    - you can create many of them, such as: ***"WindowsRestore.txt"***, ***"CloudSyncing.txt"*** and so on...
2. Place all of them in a folder together with ***CmdExecuter.exe***.
3. The app will scan and read all commands in all files and categorize them by file name
4. then it will let you select which files you want to execute, and it will execute all selected files by order
5. and inform you of the result of every command execution, should you want to, it can produce a detailed report.

## Safety tips

- The application was developed with using multiple files in purpose, use this to separate your commands and execute only what is required.
- Always inspect the files *before* execution to prevent unwanted outcomes.
- Make sure to test all commands beforehand to know the predictable outcome.
- Use absolute paths as this calls on **cmd.exe** which might not recognize the paths otherwise.

## Disclaimer

***Use this application at your own risk.*** I do not guarantee all commands will work with this. Also, As this application redirect output to executes with the systems cmd.exe file, Many commands execute with different behavior, thus it is nearly impossible to test all cases or to guarantee 100% success.

## Downloads

The following link contains 2 zip files which I builded, assembled and synced with this public folder:

1. Regular - Light version (single .exe file), depends on the system being up to date and having .NET runtime installed.
2. FrameworkIndependent - Heavier version (folder), contains all the files required to run this app including the .NET runtime, perfect for using on legacy or client machines in which you can't guarantee the conditions.

[Mega.nz public folder](https://mega.nz/folder/prYATJLK#CXktCXklP7xn00u-M3VDwg)

If you do not want to download, as with any other open source project, you can pull from the *main* branch and build it yourself.
