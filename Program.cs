using System;
using System.Reflection;
using Basic_Version_Control.Commands;

namespace VCS
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("VCS CLI -  Available commands: init, commit, log, restore, diff");
                return;
            }

            if (args.Length == 1 && (args[0] == "--version" || args[0] == "-v"))
            {
                var versionNumber = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "unknown";

                Console.WriteLine($"basic-vcs CLI v{versionNumber}");
                return;
            }

            var command = args[0].ToLower();

            switch (command)
            {
                case "init":
                    InitCommand.Execute();
                    break;
                case "commit":
                    CommitCommand.Execute(args);
                    break;
                case "log":
                    LogCommand.Execute();
                    break;
                case "restore":
                    RestoreCommand.Execute(args);
                    break;
                case "diff":
                    DiffCommand.Execute(args);
                    break;
                default:
                    Console.WriteLine($"Unknown command: {command}");
                    break;
            }
        }
    }
}