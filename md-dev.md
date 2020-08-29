```
$ git clone https://github.com/ukijumotahaneniarukenia/unidump.git


$ cd unidump/


$ dotnet new console


$ echo '/bin/* /obj/*' | xargs -n1 >.gitignore

$ dotnet add package UnicodeInformation --version 2.5.0

$ time echo linux-x64 osx.10.14-x64 win10-x64 | xargs -n1 | xargs -t -I@ dotnet publish -c Release -r @ --self-contained


$ ls -lh $HOME/media/*

$ rm -rf $HOME/media/*


$ find -type f | grep -P unidump | grep Release| grep publish |teip -Gog '(?<!\.[a-zA-Z]+)$|exe$' -- awk '{print $1,1}' | grep 1$| awk '$0=$1' | ruby -F'/' -anle 'p $F[4],$_'|xargs -n2|awk '{print "mkdir -p $HOME/media/"$1"; cp "$2" $HOME/media/"$1"/unidump-"$1}' | bash


$ tree -ugh $HOME/media
/home/aine/media
├── [aine     aine     4.0K]  linux-x64
│   └── [aine     aine      37M]  unidump-linux-x64
├── [aine     aine     4.0K]  osx.10.14-x64
│   └── [aine     aine      33M]  unidump-osx.10.14-x64
└── [aine     aine     4.0K]  win10-x64
    └── [aine     aine      28M]  unidump-win10-x64

3 directories, 3 files
```
