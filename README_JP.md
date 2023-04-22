# MinecraftConnectionBE
<div>
<img src="https://raw.githubusercontent.com/takunology/MinecraftConnectionBE/main/images/logo.png" width="300" hspace="0" vspace="10">
</div>

![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/MinecraftConnectionBE)
![Nuget](https://img.shields.io/nuget/dt/MinecraftConnectionBE?color=blue)
![](https://img.shields.io/badge/Minecraft%20Version-1.18~-brightgreen)
![GitHub](https://img.shields.io/github/license/takunology/MinecraftConnectionBE)

日本語版は[こちら](https://github.com/takunology/MinecraftConnectionBE/blob/main/README_JP.md)

MinecraftConnectionBE は、MinecraftBE やMinecraft Education 用のコマンド送信ライブラリです。自動化やプログラミング学習などに役立てることができます。Java 版は [MinecraftConnection](https://github.com/takunology/MinecraftConnection) を使用してください。

# 1. 準備
まず、MinecraftBE または Minecraft Education を起動し、設定の項目で「暗号化されたウェブソケットの要求」をオフに設定します。

<img src="https://raw.githubusercontent.com/takunology/MinecraftConnectionBE/main/images/image01_jp.png" width="550" hspace="0" vspace="10">

# 2. プロジェクトの作成
このライブラリは、.NET Standard 2.1 以上を対象としています。ここでは、.NET 6のコンソールアプリケーションを作成する方法を説明します。

NuGet パッケージマネージャーで MinecraftConnectionBE をインストールするか、パッケージマネージャーコンソールまたは dotnet コマンドで以下のコマンドを実行します。

```ps1
Install-Package MinecraftConnectionBE -Version 1.0.0-beta1
```

または

```ps1
dotnet add package MinecraftConnectionBE -v 1.0.0-beta1
```

# 3. サンプルプログラム

プログラムを実行したら、Minecraftの`/connect`コマンドでサーバーに接続するのを忘れないでください。

コマンドの例:

```
/connect 127.0.0.1:8080
```

## 3.1 AIとチャットする
AIとのチャットには、必ず OpenAI API キーが必要です。取得されていない方は、[こちら](https://platform.openai.com/overview)からアクセスし、ご確認ください。

まず、IPアドレスとポート番号を指定し、WebSocketサーバを用意します。次に、インスタンスから `ChatAI()` メソッドを呼び出し、APIキーと会話するプレイヤーID をそれぞれ文字列型として割り当てます。`Console.Readkey()` メソッドを使って、キーが押されるまでサーバを開いた状態を維持します。

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

実行結果：

<img src="https://raw.githubusercontent.com/takunology/MinecraftConnectionBE/main/images/image02_jp.gif" width="550" hspace="0" vspace="10">

## 3.2 コマンドの実行
チャットメッセージをトリガーにして、特定のコマンドを実行することができます。コマンドを実行するには、`SubscribeCommand()` メソッドを使用します。

```cs
using MinecraftConnectionBE;
using System.Net;

var address = IPAddress.Parse("127.0.0.1");
var port = 8080;

var command = new MinecraftCommands(address, port);
command.SubscribeCommand("time set night", "night");
Console.ReadKey();
```

実行結果：

<img src="https://raw.githubusercontent.com/takunology/MinecraftConnectionBE/main/images/image03_jp.gif" width="550" hspace="0" vspace="10">