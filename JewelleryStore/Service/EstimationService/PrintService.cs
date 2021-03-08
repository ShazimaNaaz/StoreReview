using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JewelleryStore.Common;
using JewelleryStore.Logging;

namespace JewelleryStore.Service.EstimationService
{
    public class PrintService : IPrintService
    {
        IPrintToScreen _printToScreen;
        IPrintToFile _printToFile;
        IPrintToPaper _printToPaper;
        IloggerService _logger;

        public PrintService(IPrintToScreen printToScreen, IPrintToFile printToFile, IPrintToPaper printToPaper, IloggerService loggerService)
        {
            _printToScreen = printToScreen;
            _printToFile = printToFile;
            _printToPaper = printToPaper;
            _logger = loggerService;
        }

        public async Task<string> PrintBill(BillMode billMode, List<string> billingMessage )
        {
            string message = string.Empty;
            try
            {
                if (billMode.Equals(BillMode.PrintToFile))
                {
                    message = await _printToFile.SaveToFile(billingMessage);
                }
                else if (billMode.Equals(BillMode.PrintToPaper))
                {
                    message = await _printToPaper.PrintToPaper(billingMessage);
                }
                else
                {
                    message = await _printToScreen.ShowPopup(billingMessage.FirstOrDefault());
                }
                
            }
            catch (Exception ex)
            {
                await _logger.LogRecord("Error in PrintService  : PrintBill ", LogType.Error, ex);
            }
            return message;
        }
    }
}
