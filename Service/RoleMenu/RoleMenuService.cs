using GineCore.Entity.Entities;
using GineCore.EntityFrameworkCore.DbExtensions;
using GineCore.Repository;
using GineCore.Repository.BaseRepository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GineCore.Service.RoleMenuService
{
    public class RoleMenuService : BaseService<RoleMenu>, IRoleMenuService
    {
        private readonly IRoleMenuRepository roleMenuRepository;

        public RoleMenuService(
            IRoleMenuRepository _roleMenuRepository,
        IBaseRepository<RoleMenu> baseRepository)
            : base(baseRepository)
        {
            roleMenuRepository = _roleMenuRepository;
        }

        /// <summary>
        /// 保存用户组权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuIdList"></param>
        public async Task SaveMenusByRoles(int roleId, List<int> menuIdList)
        {
            await roleMenuRepository.SaveMenusByRoles(roleId, menuIdList);
        }
    }
}
