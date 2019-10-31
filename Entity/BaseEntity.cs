using System;
using System.Collections.Generic;
using System.Text;

namespace GineCore.Entity
{
    public class BaseEntity
    {
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
