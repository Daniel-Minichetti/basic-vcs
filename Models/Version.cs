using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Version_Control.Models
{
    internal class FileVersion
    {
        public int VersionNumber { get; set; }
        public string? OriginalFilename { get; set; } 
        public string? SavedFileName { get; set; }
        public string? CommitMessage { get; set; } 
        public DateTime Timestamp { get; set; }
        public string? Author { get; set; }
        public string? Email { get; set; }
    }
}
