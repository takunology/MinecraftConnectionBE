using MinecraftConnectionBE.Base;
using System;
using System.Net.WebSockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;
using System.Security.Cryptography.X509Certificates;
using System.Timers;
using System.Runtime.CompilerServices;
using MinecraftConnectionBE.JsonProperty;
using System.Text.Json;
using MinecraftConnectionBE.Commands;

namespace MinecraftConnectionBE
{
    public class MinecraftCommands : WebSocketBehavior
    {
        private WebSocketServer _server;

        public MinecraftCommands(IPAddress address, int port)
        {
            _server = new WebSocketServer(address, port);
            _server.WaitTime = TimeSpan.FromSeconds(120);
            _server.AddWebSocketService<SendCommandService>("/");
            _server.Start();
        }

        private void OnDestroy()
        {
            _server.Stop();
            _server = null;
        }

        public void ServerStop()
        {
            OnDestroy();
        }

        protected string MakeCommand(string command)
        {
            var json = new CommandRequestJson
            {
                body = new CommandRequestJson.Body
                {
                    origin = new CommandRequestJson.Body.Origin
                    {
                        type = "player"
                    },
                    commandLine = command,
                },
                header = new CommandRequestJson.Header
                {
                    requestId = Guid.NewGuid().ToString(),
                    messagePurpose = "commandRequest",
                    messageType = "commandRequest"
                }
            };

            return JsonSerializer.Serialize(json);
        }

        // ここの引数、将来的に列挙体にしたい
        protected string MakeSubscribe(string eventName)
        {
            var json = new EventSubscribeJson
            {
                body = new EventSubscribeJson.Body
                {
                    eventName = eventName,
                },
                header = new EventSubscribeJson.Header
                {
                    requestId = Guid.NewGuid().ToString(),
                    messagePurpose = "subscribe",
                    messageType = "commandRequest"
                },
            };
            return JsonSerializer.Serialize(json);
        }

        public void SendCommand(string command)
        {
            Send(MakeCommand(command));
        }

        protected override void OnOpen()
        {
            Send(MakeSubscribe("PlayerMessage"));
#if DEBUG
            Console.WriteLine("Connected.");
#endif
        }
    }
}
