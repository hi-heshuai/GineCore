using System;
using System.Collections.Generic;

namespace GineCore.Entity.Entities
{
    public partial class UserInfo : BaseEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string RealName { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string LoginIpAddress { get; set; }
        public string LoginToken { get; set; }
        public DateTime? LoginTime { get; set; }
        public string Avatar { get; set; }
        public DateTime? Birthday { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string WeChat { get; set; }
        public bool IsRoot { get; set; }
        public bool EnableMarked { get; set; }
    }
}
