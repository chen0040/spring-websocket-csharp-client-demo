# spring-websocket-csharp-client-demo

Demo of connecting C# client to spring web application via websocket

# Usage

### WebSocket Spring Boot Server

git clone this project, run the "./make.ps1" powershell script in the project root directory to build spring-boot-application.jar
into the "bin" folder.

Run the following command to start the spring-boot-application at http://localhost:8080

```bash
java -jar bin/spring-boot-application.jar
```

The spring-boot-application defines an end point at http://localhost:8080/my-ws and sends a ping message to any connected client that subscribe to its topic "/topics/event" every 10 seconds. the angularjs demo can be viewed by navigating to http://localhost:8080 on your web browser.

### C-Sharp WebSocket Client

Now cd to csharp-client-proj (a modified version from https://github.com/huhuhong/websocket-csharp-net-stomp-client) and open the csharp-client-proj.sln in your Visual Studio IDE and run the Program.cs. The source code for the C# web socket client is shown below:

```cs 
using WebSocketSharp;
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
```

The C# codes opens a connection to http://localhost:8080/my-ws and once the connection is openned sends to messages to spring-boot-application via the socket to subscribe to the "/topics/event" topic, which then allows the client code to receive message from the spring-boot-application.

