using MinecraftConnectionBE.JsonProperty;
using MinecraftConnectionBE.Model;
using OpenAI_API;
using OpenAI_API.Chat;
using System;
using System.Text.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace MinecraftConnectionBE.Services
{
    public class AIChatService : WebSocketBehavior
    {
        private static OpenAIAPI _api = new OpenAIAPI(AIChatModel.ApiKey);
        private static Conversation _chat = _api.Chat.CreateConversation();

        protected override void OnError(ErrorEventArgs e)
        {
            Console.WriteLine(e);
        }

        protected override void OnOpen()
        {
            Send(MakeSubscribe("PlayerMessage"));
        }

        protected override async void OnMessage(MessageEventArgs e)
        {
            var json = JsonSerializer.Deserialize<ResponceJson>(e.Data);
            if(json.body.sender == AIChatModel.PlayerName)
            {
                var prompt = json.body.message;
                _chat.AppendUserInput(prompt);
#if DEBUG
                Console.WriteLine(prompt);
#endif
                var result = await _chat.GetResponseFromChatbotAsync();
#if DEBUG
                Console.WriteLine($"AI>{result}");
#endif
                Sessions.Broadcast(MakeCommand($"/say {result}"));
            }
        }

        private string MakeCommand(string command)
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

        private string MakeSubscribe(string eventName)
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
