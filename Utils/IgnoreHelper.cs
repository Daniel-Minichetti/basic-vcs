using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Basic_Version_Control.Utils
{
    internal static class IgnoreHelper
    {
        public static HashSet<string> LoadIgnoredFiles(string rootPath)
        {
            string ignoreFile = Path.Combine(rootPath, ".vcsignore");

            if (!File.Exists(ignoreFile))
                return new HashSet<string>();

            return new HashSet<string>(
                File.ReadAllLines(ignoreFile)
                    .Select(line => line.Trim())
                    .Where(line => !string.IsNullOrWhiteSpace(line) && !line.StartsWith("#")),
                StringComparer.OrdinalIgnoreCase // Case-insensitive compare
            );

        }
    }
}
