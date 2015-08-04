using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskRunnerInstall
{
    public static class InstalledPrograms
    {
        private static string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";

        public static List<string> GetInstalledPrograms()
        {
            var result = new List<string>();
            result.AddRange(GetInstalledProgramsFromRegistry(RegistryView.Registry32, RegistryHive.LocalMachine));
            result.AddRange(GetInstalledProgramsFromRegistry(RegistryView.Registry64, RegistryHive.LocalMachine));

            result.Sort();
            result = result.Distinct().ToList();
            return result;
        }

        private static IEnumerable<string> GetInstalledProgramsFromRegistry(RegistryView registryView, RegistryHive currentMachine)
        {
            var result = new List<string>();
            try { 
            using (RegistryKey key = RegistryKey.OpenBaseKey(currentMachine, registryView).OpenSubKey(registry_key))
            {
                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        if (IsProgramVisible(subkey))
                        {
                            result.Add((string)subkey.GetValue("DisplayName"));
                        }
                    }
                }
            }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: {0}", e.Message);
            }
            return result;
        }

        private static bool IsProgramVisible(RegistryKey subkey)
        {
            var name = (string)subkey.GetValue("DisplayName");
            var releaseType = (string)subkey.GetValue("ReleaseType");
            //var unistallString = (string)subkey.GetValue("UninstallString");
            var systemComponent = subkey.GetValue("SystemComponent");
            var parentName = (string)subkey.GetValue("ParentDisplayName");

            return
                !string.IsNullOrEmpty(name)
                && string.IsNullOrEmpty(releaseType)
                && string.IsNullOrEmpty(parentName)
                && (systemComponent == null);
        }
    }
}