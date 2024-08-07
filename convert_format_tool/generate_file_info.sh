#!/bin/bash


if [ -f file_info.txt ]; then
  rm -f file_info.txt
fi

# 递归地为当前目录及其子目录中的所有.cs文件添加BOM头
find ../Assets/ -type f -name "*.cs" -exec bash -c '
  for file do
	file "$file" >> file_info.txt
  done
' bash {} +

cat file_info.txt


