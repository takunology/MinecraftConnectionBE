# MinecraftConnectionBE
<div>
<img src="https://raw.githubusercontent.com/takunology/MinecraftConnectionBE/main/images/logo.png" width="300" hspace="0" vspace="10">
</div>

![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/MinecraftConnectionBE)
![Nuget](https://img.shields.io/nuget/dt/MinecraftConnectionBE?color=blue)
![](https://img.shields.io/badge/Minecraft%20Version-1.18~-brightgreen)
![GitHub](https://img.shields.io/github/license/takunology/MinecraftConnectionBE)

日本語版は[こちら](https://github.com/takunology/MinecraftConnectionBE/blob/main/README_JP.md)

MinecraftConnectionBE is a command sending library for MinecraftBE or Minecraft Education. It can be useful for automation and programming learning.

# 1. Preparation
First, launch MinecraftBE or Minecraft Education. In the settings section, set "Require Encrypted Websockets" to Off.

<img src="https://raw.githubusercontent.com/takunology/MinecraftConnectionBE/main/images/image01_en.png" width="550" hspace="0" vspace="10">

# 2. Create Project
This library is intended for .NET Standard 2.1 and above. This section describes how to create a .NET 6 console application.

Install MinecraftConnectionBE with the NuGet package manager, or run the following command in the package manager console.

```ps1
Install-Package MinecraftConnectionBE -Version 1.0.0-beta1
```

or

```ps1
dotnet add package MinecraftConnectionBE -v 1.0.0-beta1
```

# 3. Sample Programs

After running the program, use the `/connect` command in Minecraft to connect to the server.

For example:

```
/connect 127.0.0.1:8080
```

## 3.1 Chat with AI
An OpenAI API key is always required to chat with an AI. If you have not obtained one, please click [here](https://platform.openai.com/overview) to access and confirm it.

First, specify the IP address and port number to prepare the WebSocket server. Next, call the `ChatAI()` method from the instance and assign the API key and the player ID to be conversed with, each as a string type. Using the `Console.Readkey()` method, the server is open until a key is pressed.

```cs
using MinecraftConnectionBE;
using System.Net;

var address = IPAddress.Parse("127.0.0.1");
var port = 8080;
var apiKey = "<Your API key>";

var command = new MinecraftCommands(address, port);
command.AIChat(apiKey, "<Your ID>");
Console.ReadKey();
```

Result: 

<img src="https://raw.githubusercontent.com/takunology/MinecraftConnectionBE/main/images/image02_en.png" width="550" hspace="0" vspace="10">

## 3.2 Send command
You can trigger a chat message to execute a specific command. Use the `SubscribeCommand()` method to execute the command.

```cs
using MinecraftConnectionBE;
using System.Net;

var address = IPAddress.Parse("127.0.0.1");
var port = 8080;

var command = new MinecraftCommands(address, port);
command.SubscribeCommand("time set night", "night");
Console.ReadKey();
```

Result:

<img src="https://raw.githubusercontent.com/takunology/MinecraftConnectionBE/main/images/image03_en.gif" width="550" hspace="0" vspace="10">