using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarsTerraform.Models
{
    public class VaultVM
    {
        public int Id { get; set; }
        public int Money { get; set; }
        public int Steel { get; set; }
        public int Titan { get; set; }
        public int Flora { get; set; }
        public int Energy { get; set; }
        public int Heat { get; set; }
    }
}