using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Basic_Version_Control.Models;
using Basic_Version_Control.Utils;

namespace Basic_Version_Control.Commands
{
    internal class CommitCommand
    {
        private static readonly string RootDir = Path.Combine(Environment.CurrentDirectory, "VCS");
        private static readonly string VersionsDir = Path.Combine(RootDir, "versions");
        private static readonly string DataFile = Path.Combine(RootDir, "data.json");

        public static void Execute(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: commit <file> \"<commit message>\"");
                return;
            }

            string targetFile = args[1];
            string commitMessage = args[2];

            var config = ConfigHelper.LoadConfig(Environment.CurrentDirectory);
            string author = config.GetValueOrDefault("author", "Unknown");
            string email = config.GetValueOrDefault("email", "unknown@example.com");

            foreach (var arg in args)
            {
                if (arg.StartsWith("--author="))
                    author = arg.Split("=", 2)[1];
                if (arg.StartsWith("--email="))
                    email = arg.Split("=", 2)[1];
            }


            if (!File.Exists(targetFile))
            {
                Console.WriteLine($"File not found: {targetFile}");
                return;
            }


            // Load ignore list and debug
            var ignored = IgnoreHelper.LoadIgnoredFiles(Environment.CurrentDirectory);
            var fileNameOnly = Path.GetFileName(targetFile);

            if (ignored.Contains(fileNameOnly))
            {
                Console.WriteLine($"File '{fileNameOnly}' is ignored by .vcsignore.");
                return;
            }


            // Read existing versions
            List<FileVersion> versions = new List<FileVersion>();
            if (File.Exists(DataFile))
            {
                string json = File.ReadAllText(DataFile);
                versions = JsonSerializer.Deserialize<List<FileVersion>>(json) ?? new List<FileVersion>();
            }

            int newVersionNumber = versions.Count + 1;
            //string fileNameOnly = Path.GetFileName(targetFile);
            string savedFileName = $"{newVersionNumber}-{fileNameOnly}";
            string destinationPath = Path.Combine(VersionsDir, savedFileName);

            // Copy file to versions directory
            File.Copy(targetFile, destinationPath, true);

            // Create new version entry
            FileVersion newVersion = new FileVersion
            {
                VersionNumber = newVersionNumber,
                OriginalFilename = fileNameOnly,
                SavedFileName = savedFileName,
                CommitMessage = commitMessage,
                Timestamp = DateTime.UtcNow,
                Author = author,
                Email = email
            };

            versions.Add(newVersion);

            // Write updated version history
            string updatedJson = JsonSerializer.Serialize(versions, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(DataFile, updatedJson);

            Console.WriteLine($"Committed version {newVersionNumber}: {fileNameOnly}");
        }
    }
}
