using MinecraftConnectionBE.JsonProperty;
using MinecraftConnectionBE.Model;
using System;
using System.Text.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace MinecraftConnectionBE.Services
{
    public class SendCommandService : WebSocketBehavior
    {
        protected override void OnError(ErrorEventArgs e)
        {
            Console.WriteLine(e);
        }

        protected override void OnOpen()
        {
            Send(MakeSubscribe());
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            var json = JsonSerializer.Deserialize<ResponceJson>(e.Data);
            if (json.body.message == SendCommandModel.Statement)
            {
                Sessions.Broadcast(MakeCommand());
                json = JsonSerializer.Deserialize<ResponceJson>(e.Data);
                Console.WriteLine(json.body.message);
            }
        }

        private string MakeCommand()
        {
            var json = new CommandRequestJson
            {
                body = new CommandRequestJson.Body
                {
                    origin = new CommandRequestJson.Body.Origin
                    {
                        type = "player"
                    },
                    commandLine = SendCommandModel.Command,
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

        private string MakeSubscribe()
        {
            var json = new EventSubscribeJson
            {
                body = new EventSubscribeJson.Body
                {
                    eventName = SendCommandModel.Trigger,
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

    }
}
