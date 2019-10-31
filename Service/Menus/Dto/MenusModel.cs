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
        [Required(ErrorMessage = "请输入菜单名称")]
        [MaxLength(length: 10, ErrorMessage = "菜单名称小于10个字符")]
        [MinLength(length: 1, ErrorMessage = "菜单名称大于1个字符")]
        public new string Name { get; set; }


        [Required(ErrorMessage = "请输入排序号")]
        public new int? Sort { get; set; }

        /// <summary>
        /// 按钮或菜单
        /// </summary>
        public int? menuType { get; set; }
    }

    public class EditMenusModel : Menus
    {
        [Required(ErrorMessage = "请输入菜单名称")]
        [MaxLength(length: 10, ErrorMessage = "菜单名称小于10个字符")]
        [MinLength(length: 1, ErrorMessage = "菜单名称大于1个字符")]
        public new string Name { get; set; }


        [Required(ErrorMessage = "请输入排序号")]
        public new int? Sort { get; set; }

        /// <summary>
        /// 按钮或菜单
        /// </summary>
        public int? menuType { get; set; }
    }

    /// <summary>
    /// 权限树形表格模型
    /// </summary>
    public class MenusAuthority : Menus
    {
        public string TypeName
        {
            get
            {
                if (string.IsNullOrEmpty(Type))
                {
                    return "菜单";
                }
                return EnumsHelper.GetDescription(typeof(MenusTypeEnum.MenuBtnTypeEnum), Type) + " 按钮";
            }
        }

        public string ParentName { get; set; }

        public List<MenusAuthority> Children { get; set; }
    }

    /// <summary>
    /// 权限 树模型
    /// </summary>
    public class MenusAuthorityTree
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 是否展开
        /// </summary>
        public bool Expand { get; set; } = true;

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool Disabled { get; set; } = false;

        /// <summary>
        /// 孩子节点
        /// </summary>
        public List<MenusAuthorityTree> Children { get; set; }

        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool @Checked { get; set; }
    }
}
