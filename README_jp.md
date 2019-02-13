# checkTwitch

windowsの環境(しかも自分の環境しか知らないです)でしかこのツールが動くか確認してないですが、.NET Core をインストールすればLinuxでも動くと思います。多分。

## そもそもこれって何するツールなの？

Twitchで対象のユーザーが放送中か調べるものです。

windowsユーザはdotnet.exeが入っていれば動きますが、.NET Frameworkをインストールしなくても入っているのかどうかわからないです。
動かなかったら.NET Framework入れてみてください。

## このツールの使い方

### ツール使う前にまずTwitchに登録してclient idとclient secretを取得してね。

実行時に必要なのは2つです:

* tokenString: Twitchで発行したトークン(まだ持っていない場合後述のコマンドで発行できます。)
* tokenType: トークンの種類(Bearerしか使わないです。)
* userNameUrl: 調べたいユーザ名かURL

例:

```powershell
dotnet.exe .\checkTwitch.dll check --userNameUrl < Target UserName or URL > --tokenString < Your Token > --tokenType < Your Token type >
```

放送してたら、

```powershell
2019/01/22 22:00:00     Online
```

放送してなかったら、

```powershell
2019/01/22 22:00:00     Offline
```

なんかクライアントIDがおかしい場合、

```powershell
Twitch client id error.
Please check the client id.
2019/01/22 22:00:00     Offline
```

トークンを持っていない場合以下のコマンドで発行できます。

```powershell
dotnet.exe .\checkTwitch.dll MakeToken --clientId < YOur twithc ClientId > --clientSecret < Your Twitch ClientSecret >
```