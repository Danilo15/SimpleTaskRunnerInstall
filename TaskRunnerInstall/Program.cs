using System;
using System.Net;

namespace TaskRunnerInstall
{
    internal class Program
    {
        private static string fileToSave = "C:/Users/{0}/Downloads";

        private static void Main(string[] args)
        {
            fileToSave = string.Format(fileToSave, Environment.UserName);
            Download("https://nodejs.org/dist/v0.12.7/x64/node-v0.12.7-x64.msi");
            Command(string.Format("cd {0} && msiexec /qb /i node-v0.12.7-x64.msi", fileToSave),"Instalando NodeJS");
            Command("npm update -g npm","Atualizando NPM");
            Command("npm install -g grunt-cli grunt gulp", "Instalando grunt-cli, grunt e gulp");
            Download("https://visualstudiogallery.msdn.microsoft.com/dcbc5325-79ef-4b72-960e-0a51ee33a0ff/file/109075/22/Grunt%20Launcher%20v1.7.8.vsix");
            Download("https://visualstudiogallery.msdn.microsoft.com/8e1b4368-4afb-467a-bc13-9650572db708/file/140636/3/TaskRunnerExplorer.vsix");
            Download("http://dl.bintray.com/oneclick/rubyinstaller/rubyinstaller-2.2.2-x64.exe");
            Command(string.Format("{0}/rubyinstaller-2.2.2-x64.exe /VERYSILENT", fileToSave),"Instalando RubyGems");
            Command(@"set PATH=%PATH%;C:\Ruby22-x64\bin;","Adicionando RubyGems nas Variaveis de Ambiente");
            Command("gem install sass thor sasslint", "Instalando SASS, Thor e SASSLINT");
            Console.WriteLine("Digite o path do projeto Web");
            Command(string.Format("cd {0} && npm install && npm install -g",Console.ReadLine()),"Instalando packages no projeto");
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
            string nameToSave = fileToSave + "/" + splitedLink[splitedLink.Length - 1];
            Console.WriteLine("Baixando {0}...", splitedLink[splitedLink.Length - 1]);
            webClient.DownloadFile(new Uri(linkDownload), nameToSave);
        }
    }
}