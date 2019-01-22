# checkTwitch

windowsの環境(しかも自分の環境しか知らないです)でしかこのツールが動くか確認してないですが、.NET Core をインストールすればLinuxでも動くと思います。多分。

## そもそもこれって何するツールなの？

Twitchで対象のユーザーが放送中か調べるものです。

windowsユーザはdotnet.exeが入っていれば動きますが、.NET Frameworkをインストールしなくても入っているのかどうかわからないです。
動かなかったら.NET Framework入れてみてください。

## このツールの使い方

### ツール使う前にまずTwitchに登録してclient idを取得してね。

実行時に必要なのは2つです:

* clientId: Twitchに登録して手に入れたTwitch client id
* urlOrUserName: 放送中かどうか調べたいurlかユーザーネーム

例:

```powershell
dotnet.exe .\checkTwitch.dll --clientId < your twitch client id > --urlOrUserNam < url or username ex:twitchjp or https://www.twitch.tv/twitchjp >
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

## めっちゃ大事な注意

別のチャンネルをホスティングしてる場合オフラインの表記がでます！オフラインで間違いではないと思いますが使いづらいのでこれあとでいい感じに直します。