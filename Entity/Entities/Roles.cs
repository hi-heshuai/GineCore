using System;
using System.Collections.Generic;

namespace GineCore.Entity.Entities
{
    public partial class Roles : BaseEntity
    {

        public int Id { get; set; }
        public string RoleName { get; set; }
        public string Describe { get; set; }

    }
}
