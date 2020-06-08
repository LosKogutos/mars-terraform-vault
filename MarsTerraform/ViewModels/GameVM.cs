using System;

namespace MarsTerraform.ViewModels
{
    public class GameVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Closed { get; set; }
        public bool IsActive { get; set; }
        public int Players { get; set; }
    }
}