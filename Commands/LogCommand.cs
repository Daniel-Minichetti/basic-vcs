using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basic_Version_Control.Models;
using System.Text.Json;

namespace Basic_Version_Control.Commands
{
    internal class LogCommand
    {
        private static readonly string RootDir = Path.Combine(Environment.CurrentDirectory, "VCS");
        private static readonly string DataFile = Path.Combine(RootDir, "data.json"); 

        public static void Execute()
        {
            if (!File.Exists(DataFile))
            {
                Console.WriteLine("No version history found. Run 'init' and 'commit' first.");
                return;
            }

            string json = File.ReadAllText(DataFile); 
            var versions = JsonSerializer.Deserialize<List<FileVersion>>(json);

            if (versions == null || versions.Count == 0)
            {
                Console.WriteLine("No commits are found yet.");
                return;
            }

            Console.WriteLine("Commit Log:"); 
            foreach (var v in versions)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Version {v.VersionNumber}");
                Console.ResetColor();

                Console.WriteLine($"  File: {v.OriginalFilename}");
                Console.WriteLine($"  Message: \"{v.CommitMessage}\"");

                string localTime = v.Timestamp.ToLocalTime().ToString("MMM d, yyyy, h:mm tt");
                Console.WriteLine($"  Timestamp: {localTime}");

                Console.WriteLine($"  Author: {v.Author} <{v.Email}>");
                Console.WriteLine(new string('-', 44));
            }
        }
    }
}
