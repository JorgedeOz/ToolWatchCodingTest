using LoggerLibrary;
using System.Threading;
using System;

namespace LoggerDemo
{
    class Processes
    {      
        private string Tag = typeof(Processes).FullName;
        public Processes(ILogger logger){      
            logger.LogInfo(Tag,"Initializing class...");
            for(int x = 1; x <= 10; x++){
                try{
                    var value = new Random().Next(x*1000);
                    logger.LogDebug(Tag,"Checking process",new {
                        ProcessId = x,
                        Value = value
                    });
                    if(value <= 2000){
                        throw new Exception("The value is lower than 2000!");
                    }
                    if(value % 2 == 0){
                        logger.LogWarning(Tag, "The value is divisible by 2!");
                    }                    
                    Thread.Sleep(1500);
                }catch(Exception ex){
                    logger.LogError(Tag,"An error ocurred while processing", ex);
                }                
            }
        }
    }
}