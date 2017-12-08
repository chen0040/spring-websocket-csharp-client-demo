using System;
using System.Collections.Generic;
using System.Linq;
using WebSocketSharp;
using System.Text;
using System.Threading.Tasks;

namespace csharp_client_proj
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ws = new WebSocket("ws://localhost:8080/my-ws/websocket"))
            {
                ws.OnOpen += (sender, e) => 
                Console.WriteLine("Spring says: open");
                ws.OnError += (sender, e) =>
                Console.WriteLine("Error: " + e.Message);
                ws.OnMessage += (sender, e) =>
                Console.WriteLine("Spring says: " + e.Data);

                ws.Connect();
               
                //ws.Send("SUBSCRIBE id:sub-0 destination:/topic/mytopic\0");
                Console.ReadKey(true);
            }
        }
    }
}
