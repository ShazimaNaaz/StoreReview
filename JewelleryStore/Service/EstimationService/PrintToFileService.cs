using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using JewelleryStore.Logging;

namespace JewelleryStore.Service.EstimationService
{
    public class PrintToFileService : IPrintToFile
    {
        IloggerService _logger;
        public PrintToFileService(IloggerService loggerService)
        {
            _logger = loggerService;
        }

        public async Task<string> SaveToFile(List<string> billDetails)
        {
            string message = string.Empty;
            try
            {
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "bill.txt");
                File.WriteAllLines(fileName, billDetails);
                var text = File.ReadAllLines(fileName);
                if (text != null)
                {
                    message = "A bill of " + text[text.Length - 1] + " is generated";
                }
                return message;
            }
            catch (Exception ex)
            {
                await _logger.LogRecord("Error in PrintToFileService  : SaveToFile ", LogType.Error, ex);
                return message;
            }
        }
    }
}
