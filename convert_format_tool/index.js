"use strict";
var __createBinding = (this && this.__createBinding) || (Object.create ? (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    var desc = Object.getOwnPropertyDescriptor(m, k);
    if (!desc || ("get" in desc ? !m.__esModule : desc.writable || desc.configurable)) {
      desc = { enumerable: true, get: function() { return m[k]; } };
    }
    Object.defineProperty(o, k2, desc);
}) : (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    o[k2] = m[k];
}));
var __setModuleDefault = (this && this.__setModuleDefault) || (Object.create ? (function(o, v) {
    Object.defineProperty(o, "default", { enumerable: true, value: v });
}) : function(o, v) {
    o["default"] = v;
});
var __importStar = (this && this.__importStar) || function (mod) {
    if (mod && mod.__esModule) return mod;
    var result = {};
    if (mod != null) for (var k in mod) if (k !== "default" && Object.prototype.hasOwnProperty.call(mod, k)) __createBinding(result, mod, k);
    __setModuleDefault(result, mod);
    return result;
};
Object.defineProperty(exports, "__esModule", { value: true });
const child_process = __importStar(require("child_process"));
const fs = __importStar(require("fs"));
const originCodeDir = "../Assets";
const targetCodeDir = "../../";
const tmpFile = "./file_info.txt";
let fileInfos;
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
    if (fileInfo == "")
        continue;
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
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiaW5kZXguanMiLCJzb3VyY2VSb290IjoiIiwic291cmNlcyI6WyJpbmRleC50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7Ozs7O0FBQUEsNkRBQStDO0FBQy9DLHVDQUF5QjtBQUd6QixNQUFNLGFBQWEsR0FBRyxXQUFXLENBQUM7QUFDbEMsTUFBTSxhQUFhLEdBQUcsUUFBUSxDQUFBO0FBQzlCLE1BQU0sT0FBTyxHQUFHLGlCQUFpQixDQUFBO0FBQ2pDLElBQUksU0FBaUIsQ0FBQztBQUV0QixDQUFDO0lBQ0csc0RBQXNEO0lBRXRELGtCQUFrQjtJQUNsQiwwR0FBMEc7SUFDMUcsa0JBQWtCO0lBQ2xCLHFDQUFxQztJQUNyQyxXQUFXO0lBQ1gsa0JBQWtCO0lBQ2xCLGtEQUFrRDtJQUNsRCwrREFBK0Q7SUFDL0QsNkJBQTZCO0FBQ2pDLENBQUM7QUFFRCxTQUFTLEdBQUcsRUFBRSxDQUFDLFlBQVksQ0FBQyxPQUFPLEVBQUUsRUFBRSxRQUFRLEVBQUUsT0FBTyxFQUFFLENBQUMsQ0FBQztBQUU1RCxJQUFJLFlBQVksR0FBRyxTQUFTLENBQUMsS0FBSyxDQUFDLElBQUksQ0FBQyxDQUFDO0FBQ3pDLEtBQUssSUFBSSxRQUFRLElBQUksWUFBWSxFQUFFLENBQUM7SUFDaEMsT0FBTyxDQUFDLEdBQUcsQ0FBQyxRQUFRLENBQUMsQ0FBQztJQUN0QixJQUFJLFFBQVEsSUFBSSxFQUFFO1FBQUUsU0FBUztJQUM3QixJQUFJLElBQUksR0FBRyxRQUFRLENBQUMsS0FBSyxDQUFDLEdBQUcsQ0FBQyxDQUFDO0lBQy9CLElBQUksUUFBUSxHQUFHLElBQUksQ0FBQyxDQUFDLENBQUMsQ0FBQyxDQUFDLGlDQUFpQztJQUN6RCxJQUFJLFNBQVMsR0FBRyxJQUFJLENBQUMsQ0FBQyxDQUFDLENBQUM7SUFDeEIsSUFBSSxhQUFhLEdBQUcsYUFBYSxDQUFDLFFBQVEsQ0FBQyxTQUFTLFFBQVEsR0FBRyxFQUFFLEVBQUUsUUFBUSxFQUFFLE9BQU8sRUFBRSxDQUFDLENBQUM7SUFDeEYsSUFBSSxhQUFhLElBQUksUUFBUSxFQUFFLENBQUM7UUFDNUIsT0FBTyxDQUFDLEdBQUcsQ0FBQyxxQkFBcUIsUUFBUSxFQUFFLENBQUMsQ0FBQztRQUM3QyxTQUFTO0lBQ2IsQ0FBQztJQUNELElBQUksT0FBTyxHQUFHLEtBQUssQ0FBQztJQUNwQixJQUFJLFNBQVMsQ0FBQyxPQUFPLENBQUMsWUFBWSxDQUFDLElBQUksQ0FBQyxJQUFJLGFBQWEsQ0FBQyxPQUFPLENBQUMsWUFBWSxDQUFDLEdBQUcsQ0FBQyxFQUFFLENBQUM7UUFDbEYsTUFBTSxPQUFPLEdBQUcsRUFBRSxDQUFDLFlBQVksQ0FBQyxRQUFRLENBQUMsQ0FBQztRQUMxQyxZQUFZO1FBQ1osTUFBTSxHQUFHLEdBQUcsTUFBTSxDQUFDLElBQUksQ0FBQyxDQUFDLElBQUksRUFBRSxJQUFJLEVBQUUsSUFBSSxDQUFDLENBQUMsQ0FBQztRQUM1QyxrQkFBa0I7UUFDbEIsTUFBTSxPQUFPLEdBQUcsTUFBTSxDQUFDLE1BQU0sQ0FBQyxDQUFDLEdBQUcsRUFBRSxPQUFPLENBQUMsQ0FBQyxDQUFDO1FBQzlDLEVBQUUsQ0FBQyxhQUFhLENBQUMsUUFBUSxFQUFFLE9BQU8sQ0FBQyxDQUFDO1FBQ3BDLE9BQU8sQ0FBQyxHQUFHLENBQUMsc0JBQXNCLFFBQVEsRUFBRSxDQUFDLENBQUM7UUFDOUMsT0FBTyxHQUFHLElBQUksQ0FBQztJQUNuQixDQUFDO0lBQ0QsSUFBSSxTQUFTLENBQUMsT0FBTyxDQUFDLDRCQUE0QixDQUFDLElBQUksQ0FBQyxJQUFJLGFBQWEsQ0FBQyxPQUFPLENBQUMsNEJBQTRCLENBQUMsR0FBRyxDQUFDLEVBQUUsQ0FBQztRQUNsSCxNQUFNLE9BQU8sR0FBRyxFQUFFLENBQUMsWUFBWSxDQUFDLFFBQVEsRUFBRSxFQUFFLFFBQVEsRUFBRSxPQUFPLEVBQUUsQ0FBQyxDQUFDO1FBQ2pFLElBQUksT0FBTyxHQUFHLE9BQU8sQ0FBQyxPQUFPLENBQUMsS0FBSyxFQUFFLE1BQU0sQ0FBQyxDQUFDO1FBQzdDLEVBQUUsQ0FBQyxhQUFhLENBQUMsUUFBUSxFQUFFLE9BQU8sQ0FBQyxDQUFDO1FBQ3BDLE9BQU8sQ0FBQyxHQUFHLENBQUMsbUNBQW1DLFFBQVEsRUFBRSxDQUFDLENBQUM7UUFDM0QsT0FBTyxHQUFHLElBQUksQ0FBQztJQUNuQixDQUFDO0lBQ0QsSUFBSSxDQUFDLE9BQU8sRUFBRSxDQUFDO1FBQ1gsT0FBTyxDQUFDLEdBQUcsQ0FBQyxxQkFBcUIsUUFBUSxFQUFFLENBQUMsQ0FBQztJQUNqRCxDQUFDO0FBQ0wsQ0FBQyJ9