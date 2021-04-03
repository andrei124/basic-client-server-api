using System;
using System.Threading.Tasks;

namespace REST_Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Client httpClient = new Client("http://localhost:8500/example","May StackOverflow ever be with you");
            
            Task<string> retrieveResponse = httpClient.ExecuteRequest();
            string response = await retrieveResponse;

            Console.WriteLine($"Response from Server: {response}");
        }
    }
}