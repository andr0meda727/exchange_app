namespace CurrencyExchangeApp.Models
{
    public class ExchangeTable
    {
        public string Id { get; }
        public string Timestamp { get; }
        private List<ExchangeRate> rates;

        public ExchangeTable(string id, string timestamp)
        {
            Id = id;
            Timestamp = timestamp;
            rates = new List<ExchangeRate>();
        }

        public void AddRate(ExchangeRate rate)
        {
            rates.Add(rate);
        }

        public ExchangeRate GetRate(string code)
        {
            return rates.FirstOrDefault(r =>
                r.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<ExchangeRate> GetAllRates()
        {
            return rates;
        }
    }
}