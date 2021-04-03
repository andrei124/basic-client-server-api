using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace REST_API
{
    public class Server
    {
        private readonly HttpListener httpListener;
        private readonly string serverURL;
        private bool running;

        public Server()
        {
            this.httpListener = new HttpListener();
            this.serverURL = Utils.GetServerUrl(Utils.ParseJsonFile(Utils.serverInfoFile));
            this.running = false;
        }

        public string GetUrl()
        {
            return serverURL;
        }

        public void Start()
        {
            Console.WriteLine($"\nServer started at: {serverURL}\n");
            httpListener.Prefixes.Add(serverURL);
            httpListener.Start();
            running = true;
        }

        public void Stop()
        {
            httpListener.Stop();
            running = false;
        }

        public async Task HandleRequests()
        {
            while (running)
            {
                // Obtain a HTTP context after listening for a connection
                HttpListenerContext httpContext = await httpListener.GetContextAsync();

                // Get the request and response instances
                HttpListenerRequest request = httpContext.Request;
                HttpListenerResponse response = httpContext.Response;

                // TODO: Add separate thread to handle the logging operation
                Utils.LogRequest(request, DateTime.Now);

                Console.WriteLine("Request received");

                // Prepare HTTP server response
                string message = "Reply message!!!";
                response.StatusCode = 200;
                response.StatusDescription = "OK - Server responded";
                response.ContentEncoding = Encoding.UTF8;
                byte[] responseBody = Encoding.UTF8.GetBytes(message);

                // Send response back to client
                await response.OutputStream.WriteAsync(responseBody);

                // Close the response
                response.Close();
            }
        }
    }
}