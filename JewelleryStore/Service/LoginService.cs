using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Serialization;
using JewelleryStore.Logging;
using JewelleryStore.Models;
using JewelleryStore.Pages;

namespace JewelleryStore.Service
{
    public class LoginService : ILoginService
    {
        IloggerService _logger;
        public LoginService(IloggerService loggerService)
        {
            _logger = loggerService;
        }

        public  async Task<Tuple<bool,string>> ValidateAuthentication(string userName, string password)
        {
            try
            {
                #region How to load a text file embedded resource
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(LoginPage)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("JewelleryStore.Common.LoadUsers.xml");

                List<Users> users;
                using (var reader = new System.IO.StreamReader(stream))
                {
                    var serializer = new XmlSerializer(typeof(List<Users>));
                    users = (List<Users>)serializer.Deserialize(reader);
                }
                foreach(var eachUser in users)
                {
                    if(userName.Equals(eachUser.UserName) && password.Equals(eachUser.Password))
                    {
                        return new Tuple<bool, string>(true, eachUser.UserType);
                    }
                }
                    return new Tuple<bool, string>(false, string.Empty);
                #endregion
            }
            catch(Exception ex)
            {
                await _logger.LogRecord("Error in LoginService  : ValidateAuthentication " , LogType.Error, ex);
            }

            return new Tuple<bool, string>(false, string.Empty); 
        }
    }
}
