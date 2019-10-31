using GineCore.Common;
using GineCore.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GineCore.Service.UserInfoService
{
    public class UserInfoParam : JqgridParam
    {
        public string Key { get; set; }

        public int? RoleId { get; set; }

        //是否是用户角色关系查询
        public bool IsUserRole { get; set; } = false;
    }

    public class UserInfoModel : UserInfo
    {
        public int ParentId { get; set; } = 0;
        public int? RoleId { get; set; }

        public string BithdayStr
        {
            get
            {
                if (Birthday != null)
                {
                    return ((DateTime)Birthday).ToString("yyyy-MM-dd");
                }
                return string.Empty;
            }
        }
    }

    /// <summary>
    /// 创建
    /// </summary>
    public class UserInfoCreateModel : UserInfo
    {
        [Required(ErrorMessage = "请输入用户名")]
        [MaxLength(length: 20, ErrorMessage = "用户名小于20个字符")]
        [MinLength(length: 5, ErrorMessage = "用户名大于5个字符")]
        public new string UserName { get; set; }

        [Required(ErrorMessage = "请输入真实名称")]
        public new string RealName { get; set; }

        [Required(ErrorMessage = "生日必填")]
        public new DateTime? Birthday { get; set; }

        [RegularExpression(@"^(13[0-9]|14[579]|15[0-3,5-9]|16[6]|17[0135678]|18[0-9]|19[89])\d{8}", ErrorMessage = "手机号码格式错误")]
        public new string MobilePhone { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z0-9]{2,6}", ErrorMessage = "邮箱格式错误")]
        public new string Email { get; set; }
    }

    /// <summary>
    /// 修改
    /// </summary>
    public class UserInfoEditModel : UserInfo
    {
        [Required(ErrorMessage = "请输入用户名")]
        [MaxLength(length: 20, ErrorMessage = "用户名小于20个字符")]
        [MinLength(length: 5, ErrorMessage = "用户名大于5个字符")]
        public new string UserName { get; set; }

        [Required(ErrorMessage = "请输入真实名称")]
        public new string RealName { get; set; }

        [Required(ErrorMessage = "生日必填")]
        public new DateTime? Birthday { get; set; }

        [RegularExpression(@"^(13[0-9]|14[579]|15[0-3,5-9]|16[6]|17[0135678]|18[0-9]|19[89])\d{8}", ErrorMessage = "手机号码格式错误")]
        public new string MobilePhone { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z0-9]{2,6}", ErrorMessage = "邮箱格式错误")]
        public new string Email { get; set; }
    }


}
