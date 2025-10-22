using CurrencyExchangeApp.Implementations;
using CurrencyExchangeApp.Interfaces;
using CurrencyExchangeApp.UI;
using System.Text;

namespace CurrencyExchangeApp
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            // Obsługa kodowania ISO
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            try
            {
                IRemoteRepository repository = new RESTRepository();
                IEncoder encoder = new ISOEncoder();
                IDocument documentParser = new XMLDocument();
                string url = "https://static.nbp.pl/dane/kursy/xml/LastA.xml";

                ExchangeApp.Configure(repository, encoder, documentParser, url);

                ConsoleUI ui = new ConsoleUI();
                await ui.RunAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Application didnt start. Press any key to exit");
            }
        }
    }
}