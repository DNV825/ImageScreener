# ImageScreener

カレントフォルダに存在する画像をサブフォルダへ移動するファイル整理アプリ。

## Dependency

- .NET 5 以降（<https://dotnet.microsoft.com/download>）

## Setup

ソースコードをダウンロードし、任意のフォルダに配置する。その後、以下のコマンドを実行して発行（publish）する。

```shell
C:\User\任意のフォルダ\ImageScreener> dotnet publish /p:PublishProfile=ImageScreener
```

## Usage

1. アプリを起動する。
    - アプリの置いてあるフォルダ内にある画像ファイル（拡張子「jpg」・「jpeg」・「png」・「gif」）を最大100件表示する。
    - アプリの置いてあるフォルダ内にあるサブフォルダもリスト表示する。
1. 移動させたい画像を選択する。
    - チェックボックスをチェックすることで移動対象にできる。
    - 画像をクリックすると、エクスプローラーに紐づいたアプリで画像を開くことができる。
1. 移動先サブフォルダを選択する。
    - 適切な移動先サブフォルダが存在しない場合、新たに作成することもできる。
    - エクスプローラーでファイル移動やフォルダ作成を行った場合、F5キーを押下することで表示画像とフォルダリストを最新状態に更新できる。
    - サブフォルダリストの項目をダブルクリックすると、画像表示対象フォルダを選択したフォルダへ変更することができる。
1. ［フォルダへ移動］ボタンを押下する。
    - 「2」の画像を「3」のフォルダへ移動することができる。
    - ウイルスバスターに検知されることがある。その場合は許可を出せばよい。
1. すべての画像を分類するまで「2」～「4」を繰り返す。

## License

- [ImageScreener](https://github.com/DNV825/ImageScreener), [WTFPL-2.0](http://www.wtfpl.net/)

```text
        DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE 
                    Version 2, December 2004 

 Copyright (C) 2004 Sam Hocevar <sam@hocevar.net> 

 Everyone is permitted to copy and distribute verbatim or modified 
 copies of this license document, and changing it is allowed as long 
 as the name is changed. 

            DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE 
   TERMS AND CONDITIONS FOR COPYING, DISTRIBUTION AND MODIFICATION 

  0. You just DO WHAT THE FUCK YOU WANT TO.
```

## Authors

- [ImageScreener](https://github.com/DNV825/ImageScreener), DNV825

## References

1. 出井 秀行, C\#コードレシピ集, 株式会社技術評論社, 2021/08/27 初版 第1刷, ISBN 978-297-12265-2
