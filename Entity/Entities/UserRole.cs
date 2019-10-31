using System;
using System.Collections.Generic;

namespace GineCore.Entity.Entities
{
    public partial class UserRole : BaseEntity
    {
        public int Id { get; set; }
        public int RolesId { get; set; }
        public int UserInfoId { get; set; }
    }
}
