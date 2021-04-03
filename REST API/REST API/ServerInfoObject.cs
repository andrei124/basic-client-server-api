namespace REST_API
{
    public class ServerInfoObject
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Protocol { get; set; }

        public ServerInfoObject() { }

        public ServerInfoObject(string host, int port, string protocol)
        {
            Host = host;
            Port = port;
            Protocol = protocol;
        }

        public override string ToString()
        {
            return $"Server info\n " +
                   $"Host name: {Host}\n " +
                   $"Port: {Port}\n " +
                   $"Protocol used: {Protocol}";
        }
    }
}