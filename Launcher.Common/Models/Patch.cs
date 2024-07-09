using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher.Common.Models
{
    public class Patch
    {
        [Key]
        public int Id { get; set; }

        public string Version { get; set; }

        public string DownloadUrl { get; set; }
    }
}
