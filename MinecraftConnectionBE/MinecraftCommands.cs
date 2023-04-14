using MinecraftConnectionBE.Base;
using System;
using System.Net.WebSockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MinecraftConnectionBE
{
    public class MinecraftCommands : MCWebSocket
    {
        public MinecraftCommands(string url, ushort port) : base(url, port)
        {
          
        }

        protected override async Task ProcessWebSocketRequestAsync(HttpListenerContext context)
        {
            var webSocketContext = await context.AcceptWebSocketAsync(subProtocol: null);
            var webSocket = webSocketContext.WebSocket;

            Console.WriteLine("WebSocket connection established.");

            while (webSocket.State == WebSocketState.Open)
            {
                // wait for a command from the client
                string command = await ReceiveCommandAsync(webSocket);
                if (command == null)
                {
                    break;
                }

                Console.WriteLine($"Received command: {command}");

                // send the command to the Minecraft server and wait for the response
                await SendCommandAsync(webSocket, command);
                var response = await ReceiveCommandAsync(webSocket);
                Console.WriteLine($"Response: {response}");
            }

            Console.WriteLine("WebSocket connection closed.");
        }

        protected override async Task SendCommandAsync(WebSocket webSocket, string command)
        {
            // Send the command to the Minecraft server
            // ...

            // Simulate the response from the Minecraft server
            await Task.Delay(1000);

            // Send the response to the client
            string response = "OK";
            var buffer = Encoding.UTF8.GetBytes(response);
            await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        protected override async Task<string> ReceiveCommandAsync(WebSocket webSocket)
        {
            var buffer = new byte[1024];
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

            Console.WriteLine("Response: " + message);

            return message;
        }
    }
}
