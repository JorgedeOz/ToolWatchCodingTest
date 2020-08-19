using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace LoggerLibrary
{

    public enum LogLevel {
        INFO,
        DEBUG,
        WARNING,
        ERROR
    }

    /// <summary>
    /// Logger class exposing methods to log with a level (INFO,DEBUG,WARNING,ERROR)
    /// </summary>
    public class Logger : ILogger
    {
        /// <summary>
        /// Sets a date time format for the logs, defaults to "yyyy-MM-ddTHH:mm:ss:ffff"
        /// </summary>
        public string DateTimeFormat {get; set;} = "yyyy-MM-ddTHH:mm:ss:ffff";        

        /// <summary>
        /// Set the path of the log file to be created, defaults to the base directory of the parent application
        /// </summary>
        public string LogPath {get; set;} = System.AppContext.BaseDirectory;

        /// <summary>
        /// Set the file name of the log file to be created, defaults applicationName.log
        /// </summary>
        public string LogFileName {get; set;} = AppDomain.CurrentDomain.FriendlyName + ".log";
        private string LogFilePath {get {
            return $@"{LogPath}\{LogFileName}";
        }}

        /// <summary>
        /// Enables logging to file, defaults to true
        /// </summary>
        public bool LogToFile {get; set;} = true;

        /// <summary>
        /// Defines if old log file will be overwritten, defaults to true
        /// </summary>
        public bool OverwriteLog {get; set;} = true;
        private bool Overwritten = false;
        private List<string> Logs = new List<string>();

        /// <summary>
        /// Constructor: Initializes the logger with default options
        /// </summary>
        public Logger(){
        }

        /// <summary>
        /// Log the desired message to console and file system if enabled
        /// </summary>
        private void Log(string tag, LogLevel level, string message, object data = null,  Exception exception = null){
            var dateTime = DateTime.Now.ToString(this.DateTimeFormat);
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"[{dateTime}]");
            stringBuilder.Append($"[{level}]");
            stringBuilder.Append($"[{tag}]");
            stringBuilder.Append($"[{message}]");
            if(data != null){
                stringBuilder.Append($"[{JsonSerializer.Serialize(data)}]");
            }
            if(exception != null){
                stringBuilder.Append($"[{exception.ToString()}]");
            }
            var log = stringBuilder.ToString(); 
            Logs.Add(log);
            Console.WriteLine(log);
            if(LogToFile)  WriteLogToFile(log);            
        }

        /// <summary>
        /// Method to append the a log to a file
        /// </summary>
        private void WriteLogToFile(string log, string newPath = null, bool overwrite = false){
            var filePath = LogFilePath;
            if(!string.IsNullOrEmpty(newPath)){
                filePath = newPath;
            }
            if((OverwriteLog && Overwritten == false) || (overwrite == true && Overwritten == false)){
                File.Delete(filePath);                
                Overwritten = true;
            }                       
            var directory = Path.GetDirectoryName(filePath);
            if(!Directory.Exists(directory)){
                Directory.CreateDirectory(directory);
            }                             
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(log);
            }
        }

        /// <summary>
        /// Log a INFO level message <br />
        /// Paramameters <br />
        /// tag: The identifier for the log <br />
        /// message: The message to be logged <br />
        /// </summary>                
        public void LogInfo(string tag, string message){
            Log(tag, LogLevel.INFO,message);
        }

        /// <summary>
        /// Log a DEBUG level message <br />
        /// Paramameters <br />
        /// tag: The identifier for the log <br />
        /// message: The message to be logged <br />
        /// data: The object to be debug, it will serialize to a JSON string <br />
        /// </summary>                
        public void LogDebug(string tag,string message, object data){
            Log(tag,LogLevel.DEBUG,message, data);
        }

        /// <summary>
        /// Log a WARNING level message <br />
        /// Paramameters <br />
        /// tag: The identifier for the log <br />
        /// message: The message to be logged <br />        
        /// </summary>            
        public void LogWarning(string tag,string message){
            Log(tag,LogLevel.WARNING,message);
        }

        /// <summary>
        /// Log a ERROR level message <br />
        /// Paramameters <br />
        /// tag: The identifier for the log <br />
        /// message: The message to be logged <br />       
        /// ex: The exception to be logged <br />
        /// </summary>          
        public void LogError(string tag,string message, Exception ex){
            Log(tag,LogLevel.ERROR,message, exception: ex);
        }

        /// <summary>
        /// Saves all logs for the session in file system <br />
        /// Paramameters <br />
        /// filePath (optional): If provided the logs will be saved on the desired file path <br />
        /// overwrite (optional): Overwrites the file if it exists, defaults to true <br />               
        /// </summary>          
        public void PushLogsToFile(string filePath = null, bool overwrite = true){         
            Overwritten = false;   
            foreach(var log in Logs){
                WriteLogToFile(log, newPath: filePath, overwrite: overwrite);
            }                            
            Logs = new List<string>();
        }
    }
}
