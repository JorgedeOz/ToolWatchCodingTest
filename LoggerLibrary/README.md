# LoggerLibrary

LoggerLibrary is a Class Library tool for logging messages to console or file system, it is based on .netstandard2.1 

## Installation

Import the DLL file directly into references of your project, or include a reference to the project

```
<ItemGroup>
  <ProjectReference Include="..\LoggerLibrary\LoggerLibrary.csproj" />
</ItemGroup>
```

## Options

The Logger class comes with some options that needs to be provided as optional \
- DateTimeFormat: Sets a date time format for the logs, defaults to "yyyy-MM-ddTHH:mm:ss:ffff"
- LogPath: Set the path of the log file to be created, defaults to the base directory of the parent application 
- LogFileName: Set the file name of the log file to be created, defaults applicationName.log 
- LogToFile: Enables logging to file, defaults to true 
- OverwriteLog: Defines if old log file will be overwritten, defaults to true


## Usage

```
using System;
using LoggerLibrary;

namespace LoggerDemo
{
    class Program
    {
        private static string Tag = typeof(Program).FullName;
        static void Main(string[] args)
        {
            var logger = new Logger();
            logger.LogInfo(Tag,"This is a info log message");
        }
    }
}


```
