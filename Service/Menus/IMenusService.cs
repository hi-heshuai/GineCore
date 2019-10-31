using GineCore.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GineCore.Service.MenusService
{
    public interface IMenusService : IBaseService<Menus>
    {
        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        Task<List<MenusAuthority>> GetMenus();

        /// <summary>
        /// 获取角色菜单列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<List<MenusModel>> GetMenusByRoleId(int? roleId);

        /// <summary>
        /// 获取角色获取菜单树
        /// </summary>
        /// <returns></returns>
        Task<List<MenusAuthority>> GetMenusTreeByRoleId(int? roleId);
    }
}
