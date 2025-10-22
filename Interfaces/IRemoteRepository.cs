namespace CurrencyExchangeApp.Interfaces
{
    public interface IRemoteRepository
    {
        Task<byte[]> GetAsync(string url);
    }
}