using CurrencyExchangeApp.Models;

namespace CurrencyExchangeApp.Interfaces
{
    public interface IDocument
    {
        ExchangeTable GetTable(string data);
    }
}