using GineCore.Common;
using GineCore.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GineCore.Service.MenusService
{
    public class MenusModel : Menus
    {

    }

    public class CreateMenusModel : Menus
    {
        [Required(ErrorMessage = "������˵�����")]
        [MaxLength(length: 10, ErrorMessage = "�˵�����С��10���ַ�")]
        [MinLength(length: 1, ErrorMessage = "�˵����ƴ���1���ַ�")]
        public new string Name { get; set; }


        [Required(ErrorMessage = "�����������")]
        public new int? Sort { get; set; }

        /// <summary>
        /// ��ť��˵�
        /// </summary>
        public int? menuType { get; set; }
    }

    public class EditMenusModel : Menus
    {
        [Required(ErrorMessage = "������˵�����")]
        [MaxLength(length: 10, ErrorMessage = "�˵�����С��10���ַ�")]
        [MinLength(length: 1, ErrorMessage = "�˵����ƴ���1���ַ�")]
        public new string Name { get; set; }


        [Required(ErrorMessage = "�����������")]
        public new int? Sort { get; set; }

        /// <summary>
        /// ��ť��˵�
        /// </summary>
        public int? menuType { get; set; }
    }

    /// <summary>
    /// Ȩ�����α��ģ��
    /// </summary>
    public class MenusAuthority : Menus
    {
        public string TypeName
        {
            get
            {
                if (string.IsNullOrEmpty(Type))
                {
                    return "�˵�";
                }
                return EnumsHelper.GetDescription(typeof(MenusTypeEnum.MenuBtnTypeEnum), Type) + " ��ť";
            }
        }

        public string ParentName { get; set; }

        public List<MenusAuthority> Children { get; set; }
    }

    /// <summary>
    /// Ȩ�� ��ģ��
    /// </summary>
    public class MenusAuthorityTree
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// �Ƿ�չ��
        /// </summary>
        public bool Expand { get; set; } = true;

        /// <summary>
        /// �Ƿ����
        /// </summary>
        public bool Disabled { get; set; } = false;

        /// <summary>
        /// ���ӽڵ�
        /// </summary>
        public List<MenusAuthorityTree> Children { get; set; }

        /// <summary>
        /// �Ƿ�ѡ��
        /// </summary>
        public bool @Checked { get; set; }
    }
}
