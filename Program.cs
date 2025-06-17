using Fleck;

var server = new WebSocketServer("ws://0.0.0.0:5050");
var allSockets = new List<IWebSocketConnection>();

server.Start(socket =>
{
    socket.OnOpen = () =>
    {
        Console.WriteLine("Client connected");
        allSockets.Add(socket);
    };

    socket.OnClose = () =>
    {
        Console.WriteLine("Client disconnected");
        allSockets.Remove(socket);
    };

    socket.OnMessage = message =>
    {
        Console.WriteLine("Received: " + message);
        foreach (var s in allSockets)
        {
            s.Send(message);
        }
    };
});

Console.WriteLine("WebSocket Server started on ws://0.0.0.0:5050");
Task.Delay(-1).Wait();
Console.ReadLine();
