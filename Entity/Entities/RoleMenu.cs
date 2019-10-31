using System;
using System.Collections.Generic;

namespace GineCore.Entity.Entities
{
    public partial class RoleMenu : BaseEntity
    {
        public int Id { get; set; }
        public int RolesId { get; set; }
        public int MenusId { get; set; }
    }
}
