using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JewelleryStore.Service.EstimationService
{
    public interface IPrintToPaper
    {
         Task<string> PrintToPaper(List<string> billingMessage);
    }
}
