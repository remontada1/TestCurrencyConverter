using System;
using System.Collections.Generic;
using System.Text;

namespace TestCurrencyConverterReaddle
{
    public static class MyExtensions
    {
        public static string UriConstruct(this StringBuilder sb, string to, string from = "USD")
        {
            sb.Append("https://free.currencyconverterapi.com/api/v5/convert?q=");
            sb.Append(from + "_" + to);
            sb.Append("&compact=y");

            string uri = sb.ToString();

            return uri;
        }
        public static string CurrencyConvertCode(this StringBuilder sb, string to, string from = "USD")
        {
            sb.Append(from + "_" + to);

            string convertCode = sb.ToString();

            return convertCode;
        }

    }
}
