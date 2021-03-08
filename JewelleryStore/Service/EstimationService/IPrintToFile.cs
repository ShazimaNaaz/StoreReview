using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JewelleryStore.Service.EstimationService
{
    public interface IPrintToFile
    {
        Task<string> SaveToFile(List<string>  message);
    }
}
