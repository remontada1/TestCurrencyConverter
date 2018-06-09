using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Text;
namespace TestCurrencyConverterReaddle
{
    class Program
    {
        static void Main(string[] args)
        {
            GetCurrencyRate(10, "UAH");            
            Console.ReadLine();
        }

        private static void GetCurrencyRate(int amount, string to, string from = "USD")
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var uriBuilder = new StringBuilder();

                uriBuilder.Append("https://free.currencyconverterapi.com/api/v5/convert?q=");
                uriBuilder.Append(from + "_" + to);
                uriBuilder.Append("&compact=y");
                string uri = uriBuilder.ToString();

                var response = client.GetAsync(uri).Result;
                string res = "";
                using (HttpContent content = response.Content)
                {
                    Task<string> result = content.ReadAsStringAsync();
                    res = result.Result;
                }
                JObject jObject = JObject.Parse(res);

                string currencyCode = from + "_" + to;

                decimal value = (decimal)jObject[currencyCode]["val"];
                decimal output = value * amount;

                Console.WriteLine(output);
            }
        }
    }
}
