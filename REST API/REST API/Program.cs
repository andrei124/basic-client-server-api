using System.Threading.Tasks;

namespace REST_API
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.Start();
            
            Task runServer = server.HandleRequests();
            runServer.GetAwaiter().GetResult();
        }
    }
}