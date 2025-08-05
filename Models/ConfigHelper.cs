using System;
using System.Collections.Generic;
using System.IO;

namespace Basic_Version_Control.Utils
{
    internal static class ConfigHelper
    {
        public static Dictionary<string, string> LoadConfig(string rootPath)
        {
            string path = Path.Combine(rootPath, ".vcsconfig");
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (!File.Exists(path)) return dict;

            foreach (var line in File.ReadAllLines(path))
            {
                var parts = line.Split('=', 2);
                if (parts.Length == 2)
                    dict[parts[0].Trim()] = parts[1].Trim();
            }
            return dict;
        }
    }
}
