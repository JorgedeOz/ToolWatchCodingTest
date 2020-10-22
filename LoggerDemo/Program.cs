using System;
using LoggerLibrary;
using System.Threading;

namespace LoggerDemo
{
    class Program
    {
        private static string Tag = typeof(Program).FullName;
        static void Main(string[] args)
        {
            var logger = new Logger(){ OverwriteLog = true };
            try{
                logger.LogInfo(Tag,"Initializing program...");
                Thread.Sleep(1500);
                logger.LogInfo(Tag,"Initialization finished");
                Thread.Sleep(1500);
                var processes = new Processes(logger);
            }catch(Exception ex){
                logger.LogError(Tag,"An error ocurred on main class", ex);
            }
            Thread.Sleep(1500);
            logger.LogInfo(Tag,"Finished!");
            //logger.PushLogsToFile(@"c:\logs\mylog.log");            
        }
    }
}
