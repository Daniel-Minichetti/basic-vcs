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
    internal class DiffCommand
    {
        private static readonly string RootDir = Path.Combine(Environment.CurrentDirectory, "VCS");
        private static readonly string VersionsDir = Path.Combine(RootDir, "versions");
        private static readonly string DataFile = Path.Combine(RootDir, "data.json");

        public static void Execute(string[] args)
        {
            if (args.Length < 3 ||
                !int.TryParse(args[1], out int version1) ||
                !int.TryParse(args[2], out int version2))
            {
                Console.WriteLine("Usage: diff <version1> <version2>");
                return;
            }

            if (!File.Exists(DataFile))
            {
                Console.WriteLine("No version history found.");
                return;
            }

            string json = File.ReadAllText(DataFile);
            var versions = JsonSerializer.Deserialize<List<FileVersion>>(json);

            string file1 = string.Empty;
            string file2 = string.Empty;

            var v1 = versions?.Find(v => v.VersionNumber == version1);
            var v2 = versions?.Find(v => v.VersionNumber == version2);

            if (v1 == null || v2 == null)
            {
                Console.WriteLine("❗ One or both version numbers not found.");
                return;
            }

            if (string.IsNullOrEmpty(v1.SavedFileName) || string.IsNullOrEmpty(v2.SavedFileName))
            {
                Console.WriteLine("❌ Cannot diff: one or both files have missing filenames.");
                return;
            }

            file1 = Path.Combine(VersionsDir, v1.SavedFileName);
            file2 = Path.Combine(VersionsDir, v2.SavedFileName);

            if (!File.Exists(file1) || !File.Exists(file2))
            {
                Console.WriteLine("One or both files not found in versions folder.");
                return;
            }

            var lines1 = File.ReadAllLines(file1);
            var lines2 = File.ReadAllLines(file2);

            Console.WriteLine($"Diff between version {version1} and {version2}:\n");

            int max = Math.Max(lines1.Length, lines2.Length);
            for (int i = 0; i < max; i++)
            {
                string line1 = i < lines1.Length ? lines1[i] : "";
                string line2 = i < lines2.Length ? lines2[i] : "";

                if (line1 != line2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"- {line1}");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"+ {line2}");

                    Console.ResetColor();
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Diff complete.");
        }

    }
}
