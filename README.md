# checkTwitch

My English is very very very veeeeeeeery bad!

To be honest, I am writing a document using Google Translate.

Please read the document on this assumption and use it.

I checked only on Windows, but probably Linux will work if .NET Core is installed.

## What is this tool?

This tool checks if Twitch users are broadcasting.

This is probably required:

* .Net Core 2

## How to use this tool.

### Before using this tool, register to twitch and get the client id and client secret.

This is required:

* tokenString: your Twitch client token.
* tokenType: your token type.
* userNameUrl: User ID you want to check if you are Twitch broadcasting.

I will put an example below.

```powershell
dotnet.exe .\checkTwitch.dll check --userNameUrl < Target UserName or URL > --tokenString < Your Token > --tokenType < Your Token type >
```

When broadcasting,

```powershell
2019/01/22 22:00:00     Online
```

When not broadcasting,

```powershell
2019/01/22 22:00:00     Offline
```

When twitch client id is something wrong,

```powershell
Twitch client id error.
Please check the client id.
2019/01/22 22:00:00     Offline
```

Do not you have any tokens yet?

Execute the following command to create a token.

```powershell
dotnet.exe .\checkTwitch.dll MakeToken --clientId < YOur twithc ClientId > --clientSecret < Your Twitch ClientSecret >
```