# unidump
指定したコードポイントに紐づくユニコード情報を標準出力に出力するコマンド

インストール

```
curl -fsSL https://github.com/ukijumotahaneniarukenia/unidump/releases/download/1-0-0/unidump-linux-x64 -o $HOME/.local/bin/unidump

curl -fsSL https://github.com/ukijumotahaneniarukenia/unidump/releases/download/1-0-0/unidump-osx.10.14-x64 -o unidump

curl -fsSL https://github.com/ukijumotahaneniarukenia/unidump/releases/download/1-0-0/unidump-win10-x64 -o unidump.exe
```


実行権限付与

```
chmod 755 $HOME/.local/bin/unidump
```

使い方

```
$ unidump

Usage:

CMD: unidump [codePointStart] [codePointEnd]

```

```
$ unidump 300 400


$ unidump 1234 1254 >1234-1254.tsv
```
