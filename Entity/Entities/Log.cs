using System;
using System.Collections.Generic;

namespace GineCore.Entity.Entities
{
    public partial class Log : BaseEntity
    {
        public int Id { get; set; }
        public string OperateName { get; set; }
        public string Description { get; set; }
        public bool? Result { get; set; }
        public int? OperateUserId { get; set; }
        public string OperateUserName { get; set; }
        public DateTime? OperateTime { get; set; }
    }
}
