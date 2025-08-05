using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Basic_Version_Control.Models;

namespace Basic_Version_Control.Commands
{
    internal class RestoreCommand
    {
        private static readonly string RootDir = Path.Combine(Environment.CurrentDirectory, "VCS");
        private static readonly string VersionsDir = Path.Combine(RootDir, "versions");
        private static readonly string DataFile = Path.Combine(RootDir, "data.json"); 

        public static void Execute(string[] args)
        {
            if (args.Length < 2 || !int.TryParse(args[1], out int versionNumber))
            {
                Console.WriteLine("Usage: restore <version-number>");
                return;
            }

            if (!File.Exists(DataFile))
            {
                Console.WriteLine("No version history was found.");
                return; 
            }

            string json = File.ReadAllText(DataFile); 
            var versions = JsonSerializer.Deserialize<List<FileVersion>>(json);

            var targetVersion = versions?.Find(v => v.VersionNumber == versionNumber); 

            if (targetVersion == null)
            {
                Console.WriteLine($"Version {versionNumber} not found.");
                return;
            }

            
            if (string.IsNullOrEmpty(targetVersion.OriginalFilename))
            {
                Console.WriteLine("Cannot restore: original filename is missing.");
                return;
            }
            string restorePath = targetVersion.OriginalFilename;

            if (string.IsNullOrEmpty(targetVersion.SavedFileName))
            {
                Console.WriteLine("Cannot restore: saved file name is missing.");
                return;
            }
            string sourcePath = Path.Combine(VersionsDir, targetVersion.SavedFileName);

            if (!File.Exists (sourcePath))
            {
                Console.WriteLine($"Version file missing: {sourcePath}");
                return;
            }

            File.Copy(sourcePath, restorePath, true); 
            Console.WriteLine($"Restored {restorePath} to version {versionNumber}");
        }
    }
}
