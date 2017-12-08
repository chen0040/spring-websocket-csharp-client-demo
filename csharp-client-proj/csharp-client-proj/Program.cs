using System;
using System.Collections.Generic;
using System.Linq;
using WebSocketSharp;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ws = new WebSocket("ws://localhost:8080/my-ws/websocket"))
            {
                int clientId = 999;

                ws.OnOpen += (sender, e) =>
                {
                    Console.WriteLine("Spring says: open");
                    StompMessageSerializer serializer = new StompMessageSerializer();

                    var connect = new StompMessage("CONNECT");
                    connect["accept-version"] = "1.1";
                    connect["heart-beat"] = "10000,10000";
                    ws.Send(serializer.Serialize(connect));

                    
                    
                    var sub = new StompMessage("SUBSCRIBE");
                    sub["id"] = "sub-" + clientId;
                    sub["destination"] = "/topics/event";
                    ws.Send(serializer.Serialize(sub));
                    
                };
                
                ws.OnError += (sender, e) =>
                Console.WriteLine("Error: " + e.Message);
                ws.OnMessage += (sender, e) =>
                Console.WriteLine("Spring says: " + e.Data);

                ws.Connect();
               
                Console.ReadKey(true);
            }
        }
    }
}
