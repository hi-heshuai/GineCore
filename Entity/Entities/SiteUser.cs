using System;
using System.Collections.Generic;
using System.Text;

namespace GineCore.Entity.Entities
{
    public class SiteUser: BaseEntity
    {
        public int Id { get; set; }
        public int? SiteId { get; set; }
        public int? UserId { get; set; }
    }
}
