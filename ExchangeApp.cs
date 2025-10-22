using CurrencyExchangeApp.Interfaces;
using CurrencyExchangeApp.Models;
using CurrencyExchangeApp.Services;

namespace CurrencyExchangeApp
{
    public class ExchangeApp
    {
        private static ExchangeApp instance;
        private static readonly object lockObj = new object();

        private IRemoteRepository repository;
        private IEncoder encoder;
        private IDocument document;
        private string dataSourceUrl;

        private ExchangeTable table;
        private readonly Exchange exchange;

        private ExchangeApp(IRemoteRepository repo, IEncoder enc, IDocument doc, string url)
        {
            repository = repo;
            encoder = enc;
            document = doc;
            dataSourceUrl = url;
            exchange = new Exchange();
        }

        public static void Configure(IRemoteRepository repo, IEncoder enc, IDocument doc, string url)
        {
            if (instance != null)
            {
                throw new InvalidOperationException("ExchangeApp Singleton is already configured.");
            }
            lock (lockObj)
            {
                if (instance == null)
                {
                    instance = new ExchangeApp(repo, enc, doc, url);
                }
            }
        }

        public static ExchangeApp Instance
        {
            get
            {
                if (instance == null)
                {
                    throw new InvalidOperationException("ExchangeApp has not been configured. Call Configure() at startup.");
                }
                return instance;
            }
        }

        public async Task LoadRatesAsync()
        {
            byte[] rawData = await repository.GetAsync(dataSourceUrl);
            string encodedData = encoder.Encode(rawData);
            table = document.GetTable(encodedData);
            exchange.SetTable(table);
        }

        public ExchangeTable GetTable()
        {
            return table;
        }

        public double Convert(string from, string to, double amount)
        {
            return exchange.Convert(from, to, amount);
        }

        public void SetDocument(IDocument document)
        {
            this.document = document;
        }
    }
}