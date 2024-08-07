import * as child_process from 'child_process';
import * as fs from 'fs';
import * as path from 'path';

const originCodeDir = "../Assets";
const targetCodeDir = "../../"
const tmpFile = "./file_info.txt"
let fileInfos: string;

{
    // if (fs.existsSync(tmpFile)) fs.unlinkSync(tmpFile);

    // // 使用exec方法执行脚本
    // child_process.execSync(`cd "${__dirname}" && find ${originCodeDir} -type f -name "*.cs" -exec bash -c '
    //     for file do
    //         file "$file" >> ${tmpFile}
    //     done
    //     ' bash {} +
    //     `, { encoding: "utf8", stdio: 'inherit' });
    // fileInfos = fs.readFileSync(tmpFile, { encoding: "utf-8" });
    // // fs.unlinkSync(tmpFile);
}

fileInfos = fs.readFileSync(tmpFile, { encoding: "utf-8" });

var fileInfoList = fileInfos.split('\n');
for (var fileInfo of fileInfoList) {
    console.log(fileInfo);
    if (fileInfo == "") continue;
    var info = fileInfo.split(":");
    var filePath = info[0]; // path.join(__dirname, info[0]);
    var oldFormat = info[1];
    var newFileFormat = child_process.execSync(`file "${filePath}"`, { encoding: 'utf-8' });
    if (newFileFormat == fileInfo) {
        console.log(`file not changed: ${filePath}`);
        continue;
    }
    let changed = false;
    if (oldFormat.indexOf('(with BOM)') >= 0 && newFileFormat.indexOf('(with BOM)') < 0) {
        const oldData = fs.readFileSync(filePath);
        // BOM头的字节序列
        const bom = Buffer.from([0xef, 0xbb, 0xbf]);
        // 将BOM头添加到文件内容的开头
        const newData = Buffer.concat([bom, oldData]);
        fs.writeFileSync(filePath, newData);
        console.log(`add 'BOM' to file: ${filePath}`);
        changed = true;
    }
    if (oldFormat.indexOf('with CRLF line terminators') >= 0 && newFileFormat.indexOf('with CRLF line terminators') < 0) {
        const oldData = fs.readFileSync(filePath, { encoding: 'utf-8' });
        var newData = oldData.replace(/\n/g, '\r\n');
        fs.writeFileSync(filePath, newData);
        console.log(`convert 'crlf' to 'lf' to file: ${filePath}`);
        changed = true;
    }
    if (!changed) {
        console.log(`file not changed: ${filePath}`);
    }
}
