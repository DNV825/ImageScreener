# トップページ

パンくずリスト：トップページ

更新者：DNV825
更新日：2021/11/20(Sat) 17:16

## 目的

本プロジェクトの目的は以下の通り。C\#の勉強も兼ねるため、一気に完成させるのではなく少しずつ実現していく。

1. 2007年ごろに作った「ふるいわけ.htm」と同等の機能をC\# & WPF（※1）で再現する。「ふるいわけ.htm」については [「ふるいわけ.htm」とは](./sec/「ふるいわけ.htm」とは.html) を参照。
1. 出来上がったものをブラッシュアップする。

※1：[MAUI](https://github.com/dotnet/maui)というのもこれから出てくるらしいが、とりあえずWPFでやってみる。

## 対応方針

対応方針は以下の通り。具体的な内容については要求仕様書を参照すること。

| バージョン | 要求仕様書へのリンク | 実現内容の概要 |
| --- | --- | --- |
| v0.1.0 | [要求仕様書 v0.1.0](./sec/要求仕様書v0.1.0.html) | C\# & WPFでウィンドウを表示する。 |
| v0.2.0 | [要求仕様書 v0.2.0](./sec/要求仕様書v0.2.0.html) | ウィンドウ内に表示用パーツを配置する。 |
| v0.3.0 | [要求仕様書 v0.3.0](./sec/要求仕様書v0.3.0.html) | 「ふるいわけ.htm」のフォルダ追加機能をC\# & WPFで再現する。 |
| v0.4.0 | [要求仕様書 v0.4.0](./sec/要求仕様書v0.4.0.html) | 「ふるいわけ.htm」のフォルダリスト表示機能をC\# & WPFで再現する。 |
| v0.5.0 | [要求仕様書 v0.5.0](./sec/要求仕様書v0.5.0.html) | 「ふるいわけ.htm」のチェックボックス・ファイル名・画像表示機能をC\# & WPFで再現する。 |
| v0.6.0 | [要求仕様書 v0.6.0](./sec/要求仕様書v0.6.0.html) | 「ふるいわけ.htm」のチェック済み画像移動機能をC\# & WPFで再現する。 |
| v0.7.0 | [要求仕様書 v0.7.0](./sec/要求仕様書v0.7.0.html) | 「ふるいわけ.htm」の元画像表示機能をC\# & WPFで再現する。 |
| v0.8.0 | [要求仕様書 v0.8.0](./sec/要求仕様書v0.8.0.html) | これまでに挙げた改善点について対応するか検討し、反映する。 |
| v0.9.0 | [要求仕様書 v0.9.0](./sec/要求仕様書v0.9.0.html) | 単独のexeファイルを作成する。 |
| v1.0.0 | [要求仕様書 v1.0.0](./sec/要求仕様書v1.0.0.html) | アプリケーションにアイコンを設定し、単独で動作させる。 |
| v1.1.0 | [要求仕様書 v1.1.0](./sec/要求仕様書v1.1.0.html) | フォルダリストをダブルクリックすると、選択したフォルダをカレントフォルダとする。 |
| v1.2.0 | [要求仕様書 v1.2.0](./sec/要求仕様書v1.2.0.html) | 画像をすべて同時に表示するのではなく1枚ずつ表示する。 |

## 課題

v1.0.0までの状況を踏まえた課題は以下の通り。v1.2.0ですべての課題について対処を完了した（対応しないと決めたものも含む。）

| 番号 | 対応予定の版 | 達成した版 | 課題 |
| --- | --- | --- | --- |
| 1 | v0.9.0 | v0.9.0 | 出力するファイルを1つのexeファイルにまとめる。 |
| 2 | v1.0.0 | v1.0.0 | アプリケーションのアイコンを追加する。 |
| 3 | v1.0.0 | なし | xUnitで単体試験を作成する。参考：<https://qiita.com/kojimadev/items/c451196fb703cbf99e86><br>⇒（v1.1.0）現状、単体テストにふさわしい構造になっていないのであまり意味がない。<br>⇒（v1.2.0）このアプリとしては対応なしとする。 |
| 4 | （未定） | v1.2.0 | F5キー押下で表示する画像を再読み込みする。 |
| 5 | （未定） | v1.2.0 | 画像をすべて同時に表示するのではなく1枚ずつ表示する。<br>⇒（v1.1.0）描画中はマウスカーソルを砂時計表示しようと思ったが、これも非同期で処理しなければ画面に反映されないようだ。<br>⇒（v1.2.0）ステータスバーに進捗表示を出すようにした。その結果、偶然にも画像を1枚ずつ表示できるようになった。 |
| 6 | （未定） | なし | ウイルスバスターにランサムウェア扱いされないようにする。<br>⇒（v1.2.0）開発者側としてはどうしようもない。対応なしとする。 |
| 7 | （未定） | なし | MemoryStreamの役割と、それを使ったバージョンを検討する。<br>⇒（v1.2.0）ファイルを保存しない場合に使う。なので、画像ファイルをFileStreamで読み込み、そのバイナリをMemoryStreamへコピーし、FileStreamを閉じることもできるだろう。そうすると、いまストリームを開きっぱなしにしている処理をなくせるかもしれない。しかし、現状動作しているものは変えたくないので何もしない。 |
| 8 | （未定） | v1.1.0 | 元画像表示を自前でウィンドウを作ることで表示する（もしくは、cmdウィンドウを表示しない。）<br>⇒（v1.1.0）cmdウィンドウを表示しない方式で対応した。 |
| 9 | （未定） | なし | Property/PublishProfiles/FolderProfile.pubxmlについて調査する。参考：<br><https://devadjust.exblog.jp/27784413/><br><https://docs.microsoft.com/ja-jp/aspnet/core/host-and-deploy/visual-studio-publish-profiles?view=aspnetcore-6.0><br>⇒（v1.2.0）調査完了。調査結果は《発行プロファイルについて》に記載する。 |
| 10 | （未定） | v1.1.0 | フォルダリストをダブルクリックすると、選択したフォルダをカレントフォルダとして処理する。 |

### 発行プロファイルについて

発行（publish）を行うコマンドは`dotnet publish`であるが、その時だけ利用する設定ファイルを作成することができる。この設定ファイルを「発行プロファイル」と呼び、所定のフォルダへ配置することで発行時に簡単に読み込ませることができる。

> 参照：[プロファイルを発行する](https://docs.microsoft.com/ja-jp/aspnet/core/host-and-deploy/visual-studio-publish-profiles?view=aspnetcore-6.0#publish-profiles)
>
> Visual Studio の発行ツールでは、発行プロファイルについて説明する `Properties/PublishProfiles/{PROFILE NAME}.pubxml` という MSBuild ファイルが作成されます。 .pubxml ファイルは:
>
> - 発行構成の設定を含み、発行プロセスによって使用されます。
> - 変更して、ビルドと発行プロセスをカスタマイズできます。

`Properties\PublishProperties`フォルダに任意のファイル名で配置すればよいので、このプロジェクトとしては`C:\workspace\development\project\ImageScreener\ImageScreener\Properties\PublishProfiles\ImageScreener.pubxml`のように配置を行った。

発行プロファイルの中身は以下の通り。

```xml
<Project>
  <PropertyGroup>
    <!-- 以下の3行で発行（publish）時に単一のexeファイル出力にできる。 -->
    <PublishSingleFile>true</PublishSingleFile>
    <SelfContained>false</SelfContained>
    <RuntimeIdentifier>win-x64</RuntimeIdentifier>
  </PropertyGroup>
</Project>
```

この状態で以下のコマンドを実行すると、単一のexeファイルを発行することができる。

```shell
C:\workspace\development\project\ImageScreener>dotnet publish /p:PublishProfile=ImageScreener
```

ちなみに、Visual Studioで発行プロファイルを作成すると、以下のようにファイルが生成されるらしい（参照：[フォルダーの発行の例](https://docs.microsoft.com/ja-jp/aspnet/core/host-and-deploy/visual-studio-publish-profiles?view=aspnetcore-6.0#folder-publish-example)）

```xml
<?xml version="1.0" encoding="utf-8"?>
<!--
This file is used by the publish/package process of your Web project.
You can customize the behavior of this process by editing this 
MSBuild file.
-->
<Project ToolsVersion="4.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <WebPublishMethod>FileSystem</WebPublishMethod>
    <PublishProvider>FileSystem</PublishProvider>
    <LastUsedBuildConfiguration>Release</LastUsedBuildConfiguration>
    <LastUsedPlatform>Any CPU</LastUsedPlatform>
    <SiteUrlToLaunchAfterPublish />
    <LaunchSiteAfterPublish>True</LaunchSiteAfterPublish>
    <ExcludeApp_Data>False</ExcludeApp_Data>
    <PublishFramework>netcoreapp1.1</PublishFramework>
    <ProjectGuid>c30c453c-312e-40c4-aec9-394a145dee0b</ProjectGuid>
    <publishUrl>\\r8\Release\AdminWeb</publishUrl>
    <DeleteExistingFiles>False</DeleteExistingFiles>
  </PropertyGroup>
</Project>
```
