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
                return ConfigurationManager.AppSettings["GruntLauncher"].ToString();
            }
        }

        public static string NodeJS
        {
            get
            {
                return ConfigurationManager.AppSettings["NodeJS"].ToString();
            }
        }

        public static string TaskRunnerExplorer
        {
            get
            {
                return ConfigurationManager.AppSettings["TaskRunnerExplorer"].ToString();
            }
        }

        public static string RubyGems
        {
            get
            {
                return ConfigurationManager.AppSettings["RubyGems"].ToString();
            }
        }

        public static string VSIXInstaller
        {
            get
            {
                return ConfigurationManager.AppSettings["VSIXInstaller"].ToString();
            }
        }
    }
}
