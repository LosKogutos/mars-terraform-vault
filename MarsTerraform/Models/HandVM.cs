using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarsTerraform.Models
{
    public class HandVM
    {
        public List<string> PlayersList { get; set; }
        public ProductionVM Production { get; set; }
        public VaultVM Vault { get; set; }
        public string Owner { get; set; }
        public int GameId { get; set; }
    }
}