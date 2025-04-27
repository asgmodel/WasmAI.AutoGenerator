

using AutoGenerator;
using AutoGenerator.ApiFolder;
using maind.Test;
using static AutoGenerator.Test;


string projectPath = Directory.GetCurrentDirectory().Split("bin")[0];

ApiFolderGenerator.Build(projectPath,nameRoot:"ApiCore");



TestClass.Main();



