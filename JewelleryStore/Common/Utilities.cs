using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace JewelleryStore.Common
{
    public static class Utilities
    {
        public static string SaveBillToFile(List<string> billDetails)
        {
            string message = string.Empty;
            try
            {
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "bill.txt");
                File.WriteAllLines(fileName, billDetails);

                var text = File.ReadAllLines(fileName);
                
                if(text !=null)
                {
                    message = "A bill of " + text[text.Length-1] + " is generated";
                }
                return message;
            }
            catch (Exception ex)
            {
                Debug.Write("Error in SaveBillToFile in Utilities" + ex.StackTrace);
                return message;
            }
        }
    }
}
