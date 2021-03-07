using System;
using System.Threading.Tasks;

namespace JewelleryStore.Service
{
    public interface ILoginService
    {
        Task<Tuple<bool, string>> ValidateAuthentication(string Username, string Password);
    }
}
