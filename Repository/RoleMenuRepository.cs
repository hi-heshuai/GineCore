using GineCore.Entity.Entities;
using GineCore.EntityFrameworkCore.DbExtensions;
using GineCore.Repository.BaseRepository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GineCore.Repository
{
    public interface IRoleMenuRepository : IBaseRepository<RoleMenu>
    {
        Task<List<T>> GetRoleMenu<T>(int? roleId) where T : class, new();

        Task SaveMenusByRoles(int roleId, List<int> menuIdList);
    }

    public class RoleMenuRepository
        : BaseRepository<RoleMenu>, IRoleMenuRepository
    {
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<List<T>> GetRoleMenu<T>(int? roleId) where T : class, new()
        {
            return await Task.Run(() =>
            {
                string sql = string.Format(@"select distinct menus.* from
                            RoleMenu left join Menus on RoleMenu.menusid=menus.id 
                            where menus.EnableMarked='1' and (RoleMenu.rolesid={0} or Menus.ParentId is null or Menus.ParentId=0)", roleId);
                return FindListBySql<T>(sql);
            });

        }

        /// <summary>
        /// 保存用户组权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuIdList"></param>
        public async Task SaveMenusByRoles(int roleId, List<int> menuIdList)
        {
            List<string> sqls = new List<string>();

            //加入父级id
            //string sql = string.Format("select * from Menus where id in({0})", string.Join(",", menuIdList));
            //var result = FindListBySql<Menus>(sql);
            //string parentIds = string.Empty;
            //if(result != null && result.Count > 0)
            //{
            //    menuIdList.AddRange(result.Where(u => u.ParentId != null).Select(u => (int)u.ParentId).ToList());
            //}

            //删除原来的关系
            sqls.Add("delete from RoleMenu where rolesid=" + roleId);

            //添加新关系
            menuIdList.Distinct().ToList().ForEach(menuId =>
            {
                RoleMenu roleMenu = new RoleMenu()
                {
                    MenusId = menuId,
                    RolesId = roleId
                };

                sqls.Add(EntityToSql.AddSql(roleMenu));
            });

            await TransQuery(sqls);
        }
    }
}
