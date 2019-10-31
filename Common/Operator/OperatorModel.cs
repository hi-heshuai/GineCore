using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GineCore.Common
{
    /// <summary>
    /// 用户model
    /// </summary>
    public class OperatorModel
    {
        public int UserId { get; set; }

        public string UserKey { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int? RoleId { get; set; }

        public string LoginIpAddress { get; set; }

        public string LoginIpAddressName { get; set; }

        public string LoginToken { get; set; }

        public DateTime? LoginTime { get; set; }

        public bool? IsSys { get; set; }
    }
}
