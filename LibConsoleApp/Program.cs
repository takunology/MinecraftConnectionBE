using MinecraftConnectionBE;
using System.Net;

var command = new MinecraftCommands(IPAddress.Parse("127.0.0.1"), 8080);
command.SendCommand("/time set day");
Console.ReadKey(true);
