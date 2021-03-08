using System;
using System.Threading.Tasks;
using JewelleryStore.Logging;

namespace JewelleryStore.Service.EstimationService
{
    public class PrintToScreenService : IPrintToScreen
    {
        IloggerService _logger;
        public PrintToScreenService(IloggerService loggerService)
        {
            _logger = loggerService;
        }

        public async Task<string> ShowPopup(string billingMessage)
        {
            string message = "";
            try
            {
                message = "Your total amount for the gold purchased is" + billingMessage;
            }
            catch (Exception ex)
            {
                await _logger.LogRecord("Error in PrintToScreenService  : ShowPopup ", LogType.Error, ex);
            }
            return message;
        }
    }
}
