using CurrencyExchangeApp.Interfaces;

namespace CurrencyExchangeApp.Implementations
{
    public class RESTRepository : IRemoteRepository
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<byte[]> GetAsync(string url)
        {
            try
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to fetch data: {ex.Message}");
            }
        }
    }
}