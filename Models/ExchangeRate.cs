namespace CurrencyExchangeApp.Models
{
    public class ExchangeRate
    {
        public string Code { get; }
        public string Name { get; }
        public double Rate { get; }
        public double Multiplier { get; }

        public ExchangeRate(string code, string name, double rate, double multiplier)
        {
            Code = code;
            Name = name;
            Rate = rate;
            Multiplier = multiplier;
        }

        public bool Equals(ExchangeRate other)
        {
            return Code.Equals(other.Code, StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return $"{Name} ({Code}): {Rate:F4}";
        }
    }
}