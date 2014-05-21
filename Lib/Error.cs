using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
namespace IntellidateLib
{
    /// <summary>
    /// The error log class defines to write errors into event viewer. 
    /// </summary>
    public class Error
    {

        public string MethodName { get; set; }

        public string ClassName { get; set; }

        public DateTime TimeStamp { get; set; }

        public Exception Ex { get; set; }



        /// <summary>
        /// The error log
        /// </summary>
        /// <param name="ex">The exception object</param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        public void LogError(Exception ex, string className, string methodName)
        {
            try
            {
                //Creating object for EventLog class 
                EventLog eventLog = new EventLog(Constants.EventLogName);
                //Assigning class name as lventlog source

                Error logError = new Error
                {
                    ClassName = className,
                    MethodName = methodName,
                    Ex = ex,
                    TimeStamp = DateTime.Now
                };

                eventLog.Source = className;
                //to write the event as  method name exception
                eventLog.WriteEntry(JsonConvert.SerializeObject(logError), EventLogEntryType.FailureAudit);
            }
            catch (Exception)
            {
                // Do nothing
                // Log in text file
                string m_FilePath = "c:\\Logs\\" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".log";
                string m_Error = @"Class Name: " + className + "\n" +
                                "Method Name: " + methodName + "\n" + 
                                JsonConvert.SerializeObject(ex);
                try { File.WriteAllText(m_FilePath, m_Error); }
                catch (Exception){}
            }
        }

        /// <summary>
        /// The method to log the user defined exception
        /// </summary>
        /// <param name="errorMessage">The error messsage</param>
        /// <param name="className">The class name</param>
        /// <param name="methodName">The method name</param>
        public void LogError(string errorMessage, string className, string methodName)
        {
            try
            {
                Exception ex = new Exception(errorMessage);
                LogError(ex, className, methodName);
            }
            catch (Exception)
            {
                // Do nothing
            }
        }


        /// <summary>
        /// This method is to log any success situations
        /// </summary>
        /// <param name="message">The free text message</param>
        public void LogSuccess(string message)
        {
            try
            {
                //Creating object for EventLog class 
                EventLog eventLog = new EventLog(Constants.EventLogName);
                //Assigning class name as lventlog source
                eventLog.Source = "";
                //to write the event as  method name exception
                eventLog.WriteEntry(message, EventLogEntryType.SuccessAudit);
            }
            catch (Exception)
            {
                // Do nothing
            }
        }
    }
}
