using System;
using System.Net;
using WebSocketSharp.Server;
using MinecraftConnectionBE.Services;
using MinecraftConnectionBE.Model;

namespace MinecraftConnectionBE
{
    public class MinecraftCommands
    {
        private WebSocketServer _server;

        public MinecraftCommands(IPAddress address, int port)
        {
            _server = new WebSocketServer(address, port);
            _server.WaitTime = TimeSpan.FromSeconds(120);
            _server.Start();
            Console.WriteLine($"Listening to {_server.Address}:{_server.Port}");
        }

        private void OnDestroy()
        {
            _server.Stop();
            _server = null;
        }

        private void ServerStop()
        {
            OnDestroy();
        }

        /// <summary>
        /// Use the OpenAI API functionality to start a chat.
        /// </summary>
        /// <param name="apiKey">API Key for OpenAI API</param>
        /// <param name="playerName">Minecraft user id (It is a conversation trigger.)</param>
        public void AIChat(string apiKey, string playerName)
        {
            AIChatModel.PlayerName = playerName;
            AIChatModel.ApiKey = apiKey;
            _server.AddWebSocketService<AIChatService>("/");
        }

        /// <summary>
        /// Executes a command when an event triggers.
        /// </summary>
        /// <param name="command">Minecraft command</param>
        /// <param name="proposal">Triggering statements</param>
        /// <returns></returns>
        public void SubscribeCommand(string command, string statement)
        {
            SendCommandModel.Command = command;
            SendCommandModel.Trigger = Enum.GetName(typeof(MinecraftEvents), MinecraftEvents.PlayerMessage);
            SendCommandModel.Statement = statement;
            _server.AddWebSocketService<SendCommandService>("/");
        }
    }
}
