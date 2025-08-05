using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Version_Control.Commands
{
    internal class InitCommand
    {
        private static readonly string RootDir = Path.Combine(Environment.CurrentDirectory, "VCS");
        private static readonly string VersionsDir = Path.Combine(RootDir, "versions");
        private static readonly string DataFile = Path.Combine(RootDir, "data.json");

        public static void Execute()
        {
            Directory.CreateDirectory(RootDir);      // Always attempt to create (harmless if exists)
            Directory.CreateDirectory(VersionsDir);  // Now will definitely be created
          
            if (!File.Exists(DataFile))
            {
                File.WriteAllText(DataFile, "[]");   // Only create data file if missing
            }

            Console.WriteLine("VCS initialized (or updated)");
            Console.WriteLine($"Folder: {RootDir}");
            Console.WriteLine($"Versions folder: {VersionsDir}");
        }
    }
}
