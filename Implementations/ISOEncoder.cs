using CurrencyExchangeApp.Interfaces;
using System.Text;

namespace CurrencyExchangeApp.Implementations
{
    public class ISOEncoder : IEncoder
    {
        public string Encode(byte[] bytes)
        {
            try
            {
                return Encoding.GetEncoding("ISO-8859-2").GetString(bytes);
            }
            catch
            {
                return Encoding.UTF8.GetString(bytes);
            }
        }
    }
}