using System;
using System.Collections.Generic;
using System.Text;

namespace GineCore.Entity.Entities
{
    public class Plan : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CompletionTime { get; set; }
        public string Recollections { get; set; }
        public string Status { get; set; }
    }
}
