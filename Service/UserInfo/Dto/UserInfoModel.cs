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

        //�Ƿ����û���ɫ��ϵ��ѯ
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
    /// ����
    /// </summary>
    public class UserInfoCreateModel : UserInfo
    {
        [Required(ErrorMessage = "�������û���")]
        [MaxLength(length: 20, ErrorMessage = "�û���С��20���ַ�")]
        [MinLength(length: 5, ErrorMessage = "�û�������5���ַ�")]
        public new string UserName { get; set; }

        [Required(ErrorMessage = "��������ʵ����")]
        public new string RealName { get; set; }

        [Required(ErrorMessage = "���ձ���")]
        public new DateTime? Birthday { get; set; }

        [RegularExpression(@"^(13[0-9]|14[579]|15[0-3,5-9]|16[6]|17[0135678]|18[0-9]|19[89])\d{8}", ErrorMessage = "�ֻ������ʽ����")]
        public new string MobilePhone { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z0-9]{2,6}", ErrorMessage = "�����ʽ����")]
        public new string Email { get; set; }
    }

    /// <summary>
    /// �޸�
    /// </summary>
    public class UserInfoEditModel : UserInfo
    {
        [Required(ErrorMessage = "�������û���")]
        [MaxLength(length: 20, ErrorMessage = "�û���С��20���ַ�")]
        [MinLength(length: 5, ErrorMessage = "�û�������5���ַ�")]
        public new string UserName { get; set; }

        [Required(ErrorMessage = "��������ʵ����")]
        public new string RealName { get; set; }

        [Required(ErrorMessage = "���ձ���")]
        public new DateTime? Birthday { get; set; }

        [RegularExpression(@"^(13[0-9]|14[579]|15[0-3,5-9]|16[6]|17[0135678]|18[0-9]|19[89])\d{8}", ErrorMessage = "�ֻ������ʽ����")]
        public new string MobilePhone { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z0-9]{2,6}", ErrorMessage = "�����ʽ����")]
        public new string Email { get; set; }
    }


}
