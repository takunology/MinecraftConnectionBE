using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MinecraftConnectionBE.Base
{
    public abstract class MCWebSocket
    {
        protected HttpListener listener;
        protected CancellationTokenSource cts;
        protected bool isRunning;

        public MCWebSocket(string uri, ushort port)
        {
            listener = new HttpListener();
            listener.Prefixes.Add($"http://{uri}:{port}/");
            cts = new CancellationTokenSource();
            isRunning = false;
            StartAsync().Wait();
        }

        public async Task StartAsync()
        {
            listener.Start();
            isRunning = true;
            Console.WriteLine("WebSocket server started.");

            while (isRunning)
            {
                try
                {
                    var context = await listener.GetContextAsync();
                    if (context.Request.IsWebSocketRequest)
                    {
                        await ProcessWebSocketRequestAsync(context);
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                        context.Response.Close();
                    }
                }
                catch (HttpListenerException)
                {
                    // HttpListener has been stopped
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            listener.Close();
            Console.WriteLine("WebSocket server stopped.");
        }

        public void Stop()
        {
            isRunning = false;
            cts.Cancel();
        }

        protected abstract Task ProcessWebSocketRequestAsync(HttpListenerContext context);

        protected abstract Task SendCommandAsync(WebSocket webSocket, string command);

        protected abstract Task<string> ReceiveCommandAsync(WebSocket webSocket);
    }
}
