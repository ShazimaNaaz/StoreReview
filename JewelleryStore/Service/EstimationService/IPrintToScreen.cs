using System;
using System.Threading.Tasks;

namespace JewelleryStore.Service.EstimationService
{
    public interface IPrintToScreen
    {
        Task<string> ShowPopup(String billingMessage);
    }
}
