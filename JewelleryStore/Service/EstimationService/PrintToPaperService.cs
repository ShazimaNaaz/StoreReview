using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JewelleryStore.Logging;

namespace JewelleryStore.Service.EstimationService
{
    public class PrintToPaperService :IPrintToPaper
    {
        IloggerService _logger;
        public PrintToPaperService(IloggerService loggerService)
        {
            _logger = loggerService;
        }
        public async Task<string> PrintToPaper(List<string> billingMessage)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch(Exception ex)
            {
                await _logger.LogRecord("Error in LoginService  : ValidateAuthentication ", LogType.Error, ex);
            }
            return string.Empty;
        }
    }
}
