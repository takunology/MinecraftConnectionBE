using MinecraftConnectionBE.JsonProperty;
using OpenAI_API;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace MinecraftConnectionBE.Commands
{
    internal class SendCommandService : WebSocketBehavior
    {
        private string _json;
        private string apiKey = "";

        public void SetCommand(string json)
        {
            _json = json;
        }

        protected override void OnError(ErrorEventArgs e)
        {
            Console.WriteLine(e);
        }

        protected override async void OnMessage(MessageEventArgs e)
        {
            var json = JsonSerializer.Deserialize<ResponceJson>(e.Data);
            if(json.body.sender == "classmall_teache")
            {
                var api = new OpenAIAPI(apiKey);
                var chat = api.Chat.CreateConversation();
                var prompt = json.body.message;
                chat.AppendUserInput(prompt);
#if DEBUG
                Console.WriteLine(prompt);
#endif
                var result = await chat.GetResponseFromChatbotAsync();
                Sessions.Broadcast(MakeCommand($"/say {result}"));
            }
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

    }
}
