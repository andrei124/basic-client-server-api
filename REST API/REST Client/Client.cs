using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace REST_Client
{
    public class Client
    {
        private readonly HttpClient client;
        private readonly Uri serverUri;
        private readonly HttpContent payload;

        public Client(string serverUri, string payload)
        {
            client = new HttpClient();
            this.serverUri = new Uri(serverUri);
            this.payload = new ByteArrayContent(Encoding.UTF8.GetBytes(payload));
        }

        public async Task<string> ExecuteRequest()
        {
            Task<HttpResponseMessage> responseTask = client.PostAsync(serverUri, payload);
            HttpResponseMessage response = await responseTask;
            HttpContent responseContent = response.Content;
            byte[] responseBytes = await responseContent.ReadAsByteArrayAsync();
            return Encoding.UTF8.GetString(responseBytes);
        }
    }
}