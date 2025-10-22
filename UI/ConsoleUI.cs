using System.Globalization;

namespace CurrencyExchangeApp.UI
{
    public class ConsoleUI
    {
        private readonly ExchangeApp app;

        public ConsoleUI(ExchangeApp app)
        {
            this.app = app ?? throw new ArgumentNullException(nameof(app));
        }

        public async Task RunAsync()
        {
            try
            {
                Console.WriteLine("Loading exchange rates from NBP");
                await app.LoadRatesAsync();
                Console.WriteLine("Rates loaded\n");
            }
            catch (Exception ex)
            {
                ShowResult($"Error during rates loading: {ex.Message}");
            }

            bool running = true;
            while (running)
            {
                DisplayMenu();
                string choice = Console.ReadLine()?.Trim() ?? "";

                switch (choice)
                {
                    case "1":
                        ConvertCurrency();
                        break;
                    case "2":
                        ShowCurrencies();
                        break;
                    case "3":
                        await ReloadRatesAsync();
                        break;
                    case "4":
                        running = false;
                        break;
                    default:
                        ShowResult("Invalid option");
                        break;
                }
            }
        }

        private void DisplayMenu()
        {
            Console.WriteLine("\n====================================");
            Console.WriteLine("    CURRENCY EXCHANGE CALCULATOR    ");
            Console.WriteLine("====================================");
            Console.WriteLine("1. Convert currency");
            Console.WriteLine("2. Show available currencies");
            Console.WriteLine("3. Reload rates");
            Console.WriteLine("4. Exit");
            Console.WriteLine("====================================");
            Console.Write("Select option: ");
        }

        private void ShowResult(string result)
        {
            Console.WriteLine($"\n>>> {result}");
        }

        private void ConvertCurrency()
        {
            try
            {
                Console.Write("\nEnter source currency (e.g., USD, EUR, PLN): ");
                string from = Console.ReadLine().Trim().ToUpper();

                Console.Write("Enter target currency (e.g., USD, EUR, PLN): ");
                string to = Console.ReadLine().Trim().ToUpper();

                Console.Write("Enter amount: ");
                if (!double.TryParse(Console.ReadLine().Replace(",", "."), CultureInfo.InvariantCulture, out double amount))
                {
                    ShowResult("Invalid amount");
                    return;
                }

                double result = app.Convert(from, to, amount);
                ShowResult($"{amount:F2} {from} = {result:F2} {to}");
            }
            catch (Exception ex)
            {
                ShowResult($"Error: {ex.Message}");
            }
        }

        private void ShowCurrencies()
        {
            var table = app.GetTable();
            if (table == null)
            {
                ShowResult("Rates didnt load properly. Try reloading (option 3)");
                return;
            }

            Console.WriteLine("\nAvailable currencies:");
            Console.WriteLine("PLN - base currency");
            foreach (var rate in table.GetAllRates())
            {
                Console.WriteLine(rate);
            }
        }

        private async Task ReloadRatesAsync()
        {
            try
            {
                Console.WriteLine("\nReloading rates");
                await app.LoadRatesAsync();
                ShowResult("Rates reloaded");
            }
            catch (Exception ex)
            {
                ShowResult($"Error reloading rates: {ex.Message}");
            }
        }
    }
}