using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSM.Models
{
    public class Variant
    {
        public string Name { get; set; }
        public List<string[]> Versions = new();
    }
}
