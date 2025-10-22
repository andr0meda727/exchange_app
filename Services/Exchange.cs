using CurrencyExchangeApp.Models;

namespace CurrencyExchangeApp.Services
{
    public class Exchange
    {
        private ExchangeTable table;

        public void SetTable(ExchangeTable table)
        {
            this.table = table;
        }

        public double Convert(string from, string to, double amount)
        {
            if (table == null)
            {
                throw new Exception("Exchange table not loaded");
            }

            // Konwersja z PLN
            if (from.Equals("PLN", StringComparison.OrdinalIgnoreCase))
            {
                var toRate = table.GetRate(to);
                if (toRate == null)
                    throw new Exception($"Currency not found: {to}");

                return (amount * toRate.Multiplier) / toRate.Rate;
            }

            // Konwersja do PLN
            if (to.Equals("PLN", StringComparison.OrdinalIgnoreCase))
            {
                var fromRate = table.GetRate(from);
                if (fromRate == null)
                    throw new Exception($"Currency not found: {from}");

                return (amount * fromRate.Rate) / fromRate.Multiplier;
            }

            // Konwersja między walutami obcymi przez PLN
            var fromRate2 = table.GetRate(from);
            var toRate2 = table.GetRate(to);

            if (fromRate2 == null)
                throw new Exception($"Currency not found: {from}");
            if (toRate2 == null)
                throw new Exception($"Currency not found: {to}");

            double amountInPLN = (amount * fromRate2.Rate) / fromRate2.Multiplier;
            return (amountInPLN * toRate2.Multiplier) / toRate2.Rate;
        }
    }
}