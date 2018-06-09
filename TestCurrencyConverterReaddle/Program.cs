using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Text;
namespace TestCurrencyConverterReaddle
{

    enum CurrencyCodes
    {
        UAH,
        EUR,
        GBP
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Type amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            if (amount < 0)
            {
                throw new IndexOutOfRangeException("Amount value must be greater than zero.");
            }

            Console.WriteLine("Type currency code");
            string currencyCodeTo = Console.ReadLine();

            var sbForUri = new StringBuilder();
            string uri = MyExtensions.UriConstruct(sbForUri, currencyCodeTo);

            var sbForCurrencyConvertCode = new StringBuilder();
            string currencyCodePair = MyExtensions.CurrencyConvertCode(sbForCurrencyConvertCode, currencyCodeTo);
            

            decimal output = GetCurrencyRate(amount, uri, currencyCodePair);

            Console.WriteLine("{0} : {1}", currencyCodePair, output);
            Console.ReadLine();
        }

        private static decimal GetCurrencyRate(decimal amount, string uri, string currencyCodePair)
        {

            

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync(uri).Result;
                string res = "";

                using (HttpContent content = response.Content)
                {
                    Task<string> result = content.ReadAsStringAsync();
                    res = result.Result;
                }
                JObject jObject = JObject.Parse(res);


                decimal value = (decimal)jObject[currencyCodePair]["val"];

                decimal output = value * amount;

                return output;
            }


        }
    }
}
