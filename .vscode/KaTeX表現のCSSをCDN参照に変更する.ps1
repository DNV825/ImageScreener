<#
.synopsis
    KaTeX表現のCSSをCDN参照に変更する

.description
    Markdown Preview EnhancedでHTMLを出力すると以下のようにkatex.min.cssを参照するが、このままでは自分の環境以外で数式表現を確認できない。

        <link rel="stylesheet" href="file:///c:\Users\ユーザー名\.vscode\extensions\shd101wyy.markdown-preview-enhanced-0.6.0\node_modules\@shd101wyy\mume\dependencies\katex\katex.min.css">

    そこで、以下のようにCDN参照に置換する。参照先の版数は、とりあえず決め打ちとする。

        <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/katex@0.13.18/dist/katex.min.css">

.parameter TargetPath
    チェック対象フォルダのパス。Get-ChildItemの-Pathパラメーターに渡すので、".\*"のようにワイルドカードを指定するのが良い。

.example
  カレントディレクトリ配下の.htmlファイルの内容を置換する。

    .\KaTeX表現のCSSをCDN参照に変更する.ps1 .\*

.notes
  2021/09/10  司関 昭秀  新規作成。
#>
param (
    $TargetPath = ".\*"
)

$local = "`"file:///.+katex\.min\.css`""
$cdn = "`"https://cdn.jsdelivr.net/npm/katex@0.13.18/dist/katex.min.css`""

Write-Host -NoNewline "[ ] KaTeX表現のCSSをCDN参照に変更する - 開始 … "

Get-ChildItem -Path $TargetPath -Filter "*.html" -Recurse | foreach {

    $data = Get-Content $_.FullName -Encoding utf8

    if ($data -match $local) {

        $data = $data -replace $local, $cdn
        $data | Out-File $_.FullName -Encoding utf8

    }

}

Write-Host "`r[v] KaTeX表現のCSSをCDN参照に変更する - 開始 … 終了"