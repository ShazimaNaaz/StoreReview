using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;

namespace JewelleryStore.Logging
{
    public class LoggerService : IloggerService
    {
        public LoggerService()
        {
        }

        public async Task LogRecord(string message, LogType logLevel, Exception exc = null, Dictionary<string, string> inputs = null)
        {
            Dictionary<string, string> properties = new Dictionary<string, string>();
            properties.Add("Message", message);
            if (inputs != null && inputs.Count > 0)
            {
                properties.Add("Input Params", String.Join(";", inputs.Select(x => x.Key + ":" + x.Value).ToArray()));
            }

            System.Diagnostics.Debug.WriteLine(message + " " + exc.Message + "===" + exc.InnerException?.Message);
            await Task.Run(() => Crashes.TrackError(exc, properties));
        }

        public async Task LogRecord(string message, LogType logLevel, MessageObject errorMessage, Exception exc = null, Dictionary<string, string> inputs = null)
        {
            if (errorMessage != null)
            {
                App.DisplayUIAlert(errorMessage);
            }

            await LogRecord(message, logLevel, exc, inputs);
        }
    }
}