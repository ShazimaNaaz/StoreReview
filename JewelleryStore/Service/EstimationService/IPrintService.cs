using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JewelleryStore.Common;

namespace JewelleryStore.Service.EstimationService
{
    public interface IPrintService
    {
        Task<string> PrintBill(BillMode billMode, List<string> billingMessage =null);
    }
}
