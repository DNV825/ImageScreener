<#
.synopsis
    KaTeX�\����CSS��CDN�Q�ƂɕύX����

.description
    Markdown Preview Enhanced��HTML���o�͂���ƈȉ��̂悤��katex.min.css���Q�Ƃ��邪�A���̂܂܂ł͎����̊��ȊO�Ő����\�����m�F�ł��Ȃ��B

        <link rel="stylesheet" href="file:///c:\Users\���[�U�[��\.vscode\extensions\shd101wyy.markdown-preview-enhanced-0.6.0\node_modules\@shd101wyy\mume\dependencies\katex\katex.min.css">

    �����ŁA�ȉ��̂悤��CDN�Q�Ƃɒu������B�Q�Ɛ�̔Ő��́A�Ƃ肠�������ߑł��Ƃ���B

        <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/katex@0.13.18/dist/katex.min.css">

.parameter TargetPath
    �`�F�b�N�Ώۃt�H���_�̃p�X�BGet-ChildItem��-Path�p�����[�^�[�ɓn���̂ŁA".\*"�̂悤�Ƀ��C���h�J�[�h���w�肷��̂��ǂ��B

.example
  �J�����g�f�B���N�g���z����.html�t�@�C���̓��e��u������B

    .\KaTeX�\����CSS��CDN�Q�ƂɕύX����.ps1 .\*

.notes
  2021/09/10  �i�� ���G  �V�K�쐬�B
#>
param (
    $TargetPath = ".\*"
)

$local = "`"file:///.+katex\.min\.css`""
$cdn = "`"https://cdn.jsdelivr.net/npm/katex@0.13.18/dist/katex.min.css`""

Write-Host -NoNewline "[ ] KaTeX�\����CSS��CDN�Q�ƂɕύX���� - �J�n �c "

Get-ChildItem -Path $TargetPath -Filter "*.html" -Recurse | foreach {

    $data = Get-Content $_.FullName -Encoding utf8

    if ($data -match $local) {

        $data = $data -replace $local, $cdn
        $data | Out-File $_.FullName -Encoding utf8

    }

}

Write-Host "`r[v] KaTeX�\����CSS��CDN�Q�ƂɕύX���� - �J�n �c �I��"