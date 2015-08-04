using System;
using System.Collections.Generic;
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
            Instalar(string.Format("cd {0} && msiexec /qb /i {1}", folderToSave, nodeJS), "Node.js");
            Command("npm update -g npm", "Atualizando NPM");
            Command("npm install -g grunt-cli grunt gulp", "Instalando grunt-cli, grunt e gulp");
            Download(WebConfigSettings.GruntLauncher);
            Instalar(string.Format("\"{1}\" /q {0}/{2}", folderToSave, WebConfigSettings.VSIXInstaller, gruntLauncher), "Grunt Launcher");
            Download(WebConfigSettings.TaskRunnerExplorer);
            Instalar(string.Format("\"{1}\" /q {0}/{2}", folderToSave, WebConfigSettings.VSIXInstaller, taskRunnerExplorer), "Task Runner Explorer");
            Download(WebConfigSettings.RubyGems);
            Instalar(string.Format("{0}/{1} /VERYSILENT", folderToSave, rubyGems), "Ruby 2.2.2-p95-x64");
            Command(@"set PATH=%PATH%;C:\Ruby22-x64\bin;", "Adicionando RubyGems nas Variaveis de Ambiente");
            Command("gem install sass scss_lint", "Instalando SASS, SCSS_LINT");
            Console.WriteLine("Digite o path do projeto Web ou da pasta que contém o package.json");
            Command(string.Format("cd {0} && npm install && npm install -g", Console.ReadLine()), "Instalando packages no projeto");
            Console.WriteLine("Abra o Visual Studio 2013 vá em View > Other Windows > Task Runner Explorer\nClique no botao de atualizar e rode a task dev ou dev-build");
            Console.ReadKey();
        }

        private static void Instalar(string command, string description)
        {
            if (!GetInstalledPrograms(description))
            {
                Console.WriteLine("Instalando {0}", description);
                Command(command, description,true);
            }
        }

        private static void Command(string command, string description, bool esperarParaSair = false)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c " + command;
            process.StartInfo = startInfo;
            process.Start(); 
            if (esperarParaSair)
            {
                process.WaitForExit();
            }
        }

        private static void Download(string linkDownload)
        {
            WebClient webClient = new WebClient();
            string[] splitedLink = linkDownload.Split('/');
            string nameToSave = folderToSave + "/" + splitedLink[splitedLink.Length - 1];
            string nameVerify = string.Format("{0}", nameToSave.Split('/')[nameToSave.Split('/').Length - 1]);
            var containsFile = Directory.GetFiles(folderToSave, nameVerify, SearchOption.AllDirectories);
            if (containsFile == null || containsFile.Length < 1)
            {
                Console.WriteLine("Baixando {0}...", splitedLink[splitedLink.Length - 1]);
                webClient.DownloadFile(new Uri(linkDownload), nameToSave);
            }
            else
            {
                Console.WriteLine("Já contém {0}...", splitedLink[splitedLink.Length - 1]);
            }
        }

        public static bool GetInstalledPrograms(string installedItem)
        {
            List<string> programas = InstalledPrograms.GetInstalledPrograms();
            var id = programas.BinarySearch(installedItem);
            string t = string.Empty;
            try
            {
                 t = programas[id];
            }catch(Exception e){
                return false;
            }
            
            if (t == installedItem)
            {
                Console.WriteLine("{0} já está instalado", t);
                return true;
            }
            return false;
        }
    }
}