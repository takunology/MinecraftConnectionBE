using MinecraftConnectionBE.JsonProperty;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace MinecraftConnectionBE.Base
{
    public class MCWebSocket : WebSocketBehavior
    {
        


        protected override void OnOpen()
        {
#if DEBUG
            Console.WriteLine("Connected.");
#endif
        }

        protected override void OnError(ErrorEventArgs e)
        {
            Console.WriteLine(e);
        }

        protected override void OnMessage(MessageEventArgs e)
        {
#if DEBUG
            Console.WriteLine(e.Data);
#endif
        }
    }    
}
