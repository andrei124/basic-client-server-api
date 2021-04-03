using System;

namespace REST_API
{
    public class ServerLogEntry
    {
        public string Uri { get; set; }
        public DateTime Timestamp { get; set; }
        public string Content { get; set; }

        public ServerLogEntry() { }
        public ServerLogEntry(string uri, DateTime timestamp, string content)
        {
            Uri = uri;
            Timestamp = timestamp;
            Content = content;
        }
    }
}