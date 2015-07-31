using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TaskRunnerInstall
{
    class WebConfigSettings
    {
        public static string GruntLauncher
        {
            get
            {
                return ConfigurationSettings.AppSettings["GruntLauncher"].ToString();
            }
        }

        public static string NodeJS
        {
            get
            {
                return ConfigurationSettings.AppSettings["NodeJS"].ToString();
            }
        }

        public static string TaskRunnerExplorer
        {
            get
            {
                return ConfigurationSettings.AppSettings["TaskRunnerExplorer"].ToString();
            }
        }

        public static string RubyGems
        {
            get
            {
                return ConfigurationSettings.AppSettings["RubyGems"].ToString();
            }
        }

        public static string VSIXInstaller
        {
            get
            {
                return ConfigurationSettings.AppSettings["VSIXInstaller"].ToString();
            }
        }
    }
}
