using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shahkar.Consumer
{
    public static class ShahkarApiCaller
    {
        static string baseAddress = "https://localhost:44340";
        static HttpClient _client = new HttpClient() { BaseAddress = new Uri(baseAddress)};
       public static async Task<string> GetUserAsync(string phoneNumber)
       {
         
         var response = await _client.GetAsync($"api/UserInfo/GetUserInfo?PhoneNumber={phoneNumber}");
         Console.WriteLine(response.RequestMessage);
         if(response.IsSuccessStatusCode)
         {
               return await response.Content.ReadAsStringAsync();
         }

         return "";


       }
    }
}
