using System;
using System.IO;
using System.Net;

namespace TaskRunnerInstall
{
    internal class Program
    {
        private static string folderToSave = "C:/Users/{0}/Downloads";

        private static void Main(string[] args)
        {
            string gruntLauncher = WebConfigSettings.GruntLauncher.Split('/')[WebConfigSettings.GruntLauncher.Split('/').Length - 1],
                   taskRunnerExplorer = WebConfigSettings.TaskRunnerExplorer.Split('/')[WebConfigSettings.TaskRunnerExplorer.Split('/').Length - 1],
                   nodeJS = WebConfigSettings.NodeJS.Split('/')[WebConfigSettings.NodeJS.Split('/').Length - 1],
                   rubyGems = WebConfigSettings.RubyGems.Split('/')[WebConfigSettings.RubyGems.Split('/').Length - 1];

            folderToSave = string.Format(folderToSave, Environment.UserName);
            Download(WebConfigSettings.NodeJS);
            Command(string.Format("cd {0} && msiexec /qb /i {1}", folderToSave, nodeJS), "Instalando NodeJS");
            Command("npm update -g npm", "Atualizando NPM");
            Command("npm install -g grunt-cli grunt gulp", "Instalando grunt-cli, grunt e gulp");
            Download(WebConfigSettings.GruntLauncher);
            Command(string.Format("\"{1}\" /q {0}/{2}", folderToSave, WebConfigSettings.VSIXInstaller, gruntLauncher), "Instalando Grunt Launcher");
            Download(WebConfigSettings.TaskRunnerExplorer);
            Command(string.Format("\"{1}\" /q {0}/{2}", folderToSave, WebConfigSettings.VSIXInstaller, taskRunnerExplorer), "Instalando Task Runner Explorer");
            Download(WebConfigSettings.RubyGems);
            Command(string.Format("{0}/{1} /VERYSILENT", folderToSave, rubyGems), "Instalando RubyGems");
            Command(@"set PATH=%PATH%;C:\Ruby22-x64\bin;", "Adicionando RubyGems nas Variaveis de Ambiente");
            Command("gem install sass thor sasslint", "Instalando SASS, Thor e SASSLINT");
            Console.WriteLine("Digite o path do projeto Web");
            Command(string.Format("cd {0} && npm install && npm install -g", Console.ReadLine()), "Instalando packages no projeto");
            Console.WriteLine("Abra o Visual Studio 2013 vá em View > Other Windows > Task Runner Explorer\nClique no botao de atualizar e rode a task dev ou dev-build");
            Console.ReadKey();
        }

        private static void Command(string command, string description)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c " + command;
            process.StartInfo = startInfo;
            Console.WriteLine(description);
            process.Start();
        }

        private static void Download(string linkDownload)
        {
            WebClient webClient = new WebClient();
            string[] splitedLink = linkDownload.Split('/');
            string nameToSave = folderToSave + "/" + splitedLink[splitedLink.Length - 1];
            string nameVerify = string.Format("{0}", nameToSave.Split('/')[nameToSave.Split('/').Length - 1]);
            var containsFile = Directory.GetFiles(folderToSave, nameVerify, SearchOption.AllDirectories);
            if(containsFile == null){
                Console.WriteLine("Baixando {0}...", splitedLink[splitedLink.Length - 1]);
                webClient.DownloadFile(new Uri(linkDownload), nameToSave);
            }else{
                Console.WriteLine("Já contém {0}...", splitedLink[splitedLink.Length - 1]);
            }
        }
    }
}