using CurrencyExchangeApp.Interfaces;
using CurrencyExchangeApp.Models;
using System.Globalization;
using System.Xml.Linq;

namespace CurrencyExchangeApp.Implementations
{
    public class XMLDocument : IDocument
    {
        public ExchangeTable GetTable(string data)
        {
            try
            {
                XDocument doc = XDocument.Parse(data);
                XElement root = doc.Root;

                string tableNumber = root.Element("numer_tabeli")?.Value ?? "";
                string date = root.Element("data_publikacji")?.Value ?? "";

                ExchangeTable table = new ExchangeTable(tableNumber, date);

                var positions = root.Elements("pozycja");

                foreach (var position in positions)
                {
                    string code = position.Element("kod_waluty")?.Value ?? "";
                    string name = position.Element("nazwa_waluty")?.Value ?? "";
                    string rateStr = position.Element("kurs_sredni")?.Value?.Replace(",", ".") ?? "0";
                    string multiplierStr = position.Element("przelicznik")?.Value ?? "1";

                    double rate = double.Parse(rateStr, CultureInfo.InvariantCulture);
                    double multiplier = double.Parse(multiplierStr, CultureInfo.InvariantCulture);

                    table.AddRate(new ExchangeRate(code, name, rate, multiplier));
                }

                return table;
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"XML parsing error: {ex.Message}", ex);
            }
        }
    }
}