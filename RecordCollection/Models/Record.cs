using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordLibrary.Models
{
    public class Record
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public string ReleaseName { get; set; }
        public string ReleaseYear { get; set; }
    }
}
