using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JewelleryStore.Logging
{
    public interface IloggerService
    {
        Task LogRecord(string message, LogType logLevel, Exception exc = null, Dictionary<string, string> inputs = null);
        Task LogRecord(string message, LogType logLevel, MessageObject errorMessage, Exception exc = null, Dictionary<string, string> inputs = null);
    }
    public enum LogType
    {
        Info,
        Warn,
        Error
    }
    public class MessageObject
    {
        public string Title
        {
            get;
            set;
        }
        public string Message
        {
            get;
            set;
        } = String.Empty;
        public string OkButtonText
        {
            get;
            set;
        } = "OK";
        public Action OkClickEvent { get; set; }
    }
}
