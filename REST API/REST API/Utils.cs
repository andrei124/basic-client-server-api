using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;

namespace REST_API
{
    public class Utils
    {
        public static readonly string serverLogFile = "/home/andrei/basic-rest-api/REST API/REST API/server_log.txt";
        public static readonly string serverInfoFile = "/home/andrei/basic-rest-api/REST API/REST API/server_info.json";


        /// <summary>
        /// Utility function for parsing a JSON file into a list of Server Information Objects
        /// </summary>
        /// <param name="filename">The path of the JSON input file</param>
        /// <returns>List of Server Information Objects</returns>
        public static ServerInfoObject ParseJsonFile(string filename)
        {
            using StreamReader sr = new StreamReader(filename);
            string serverInfoJson = sr.ReadToEnd();
            ServerInfoObject serverInfoObjects =
                JsonSerializer.Deserialize<ServerInfoObject>(serverInfoJson);
            Console.WriteLine(serverInfoObjects);
            return serverInfoObjects;
        }

        /// <summary>
        /// Utility function for logging a HTTP request received by the server
        /// </summary>
        /// <param name="request">The HTTP request as sent by the user</param>
        /// <param name="timestamp">The timestamp at which the request was made</param>
        public static void LogRequest(HttpListenerRequest request, DateTime timestamp)
        {
            string requestSummaryAsJson = HttpRequestToJson(request, timestamp);

            using StreamWriter sw = File.AppendText(serverLogFile);
            sw.WriteLine(requestSummaryAsJson);
            sw.WriteLine();
        }

        /// <summary>
        /// Utility function for converting a HTTP request summary information in JSON format
        /// </summary>
        /// <param name="request">The HTTP request to be processed</param>
        /// <param name="timestamp">The timestamp at which the request was recorded</param>
        /// <returns>String representing request summary in JSON format</returns>
        public static string HttpRequestToJson(HttpListenerRequest request, DateTime timestamp)
        {
            // Get the request URI
            Uri requestUri = request.Url;

            // Get the request body as byte[]
            string content;
            using (StreamReader sr = new StreamReader(request.InputStream, Encoding.UTF8))
            {
                content = sr.ReadToEnd();
            }
            byte[] requestBody = Encoding.UTF8.GetBytes(content);
            
            // MemoryStream ms = new MemoryStream();
            // request.InputStream.CopyToAsync(ms);
            // byte[] requestBody = ms.ToArray();

            ServerLogEntry logEntry =
                new ServerLogEntry(requestUri.ToString(), timestamp, Encoding.UTF8.GetString(requestBody));

            return JsonSerializer.Serialize(logEntry);
        }

        public static string GetServerUrl(ServerInfoObject serverInfo)
        {
            return $"{serverInfo.Protocol}://{serverInfo.Host}:{serverInfo.Port}/";
        }
    }
}