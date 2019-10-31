using GineCore.Common;
using GineCore.Entity.Entities;
using GineCore.EntityFrameworkCore.DbExtensions;
using GineCore.Repository;
using GineCore.Repository.BaseRepository.Base;
using GineCore.Service.MenusService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GineCore.Service.RolesService
{
    public class RolesService
        : BaseService<Roles>, IRolesService
    {

        private readonly IRolesRepository rolesRepository;
        private readonly IMenusRepository menusRepository;
        private readonly IRoleMenuRepository roleMenuRepository;

        public RolesService(IRolesRepository _rolesRepository,
            IMenusRepository _menusRepository,
            IRoleMenuRepository _roleMenuRepository,
        IBaseRepository<Roles> baseRepository)
            : base(baseRepository)
        {
            rolesRepository = _rolesRepository;
            menusRepository = _menusRepository;
            roleMenuRepository = _roleMenuRepository;
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<JqGridModel<RolesModel>> GetList(RolesParam param)
        {
            string sql = "select Roles.* {0} from Roles {1} where 1=1 {2} {3}";
            string orderBy = " ORDER BY Id ";
            string where = string.Empty;
            string join = "";
            string select = "";

            //条件
            if (!string.IsNullOrEmpty(param.SearchKey))
            {
                where += string.Format(" and( RoleName like '%{0}%')", param.SearchKey);
            }
            //排序
            if (!string.IsNullOrEmpty(param.sidx) && !string.IsNullOrEmpty(param.sord))
            {
                orderBy = string.Format(" order by {0} {1} ", param.sidx, param.sord);
            }

            sql = string.Format(sql, select, join, where, orderBy);

            var list = await rolesRepository.FindListPager<RolesModel>(sql, param);
            return list;
        }

        /// <summary>
        /// 获取角色菜单权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<List<MenusAuthorityTree>> GetMenusByRoleId(int roleId)
        {
            return await Task.Run(async () =>
            {
                var menuAllList = await menusRepository.FindList<MenusModel>(string.Format(" EnableMarked='{0}'", 1));
                var roleMenuList = await roleMenuRepository.GetRoleMenu<MenusModel>(roleId);

                var result = menuAllList.Where(u => (u.ParentId == 0 || u.ParentId == null)).OrderBy(u => u.Sort)
                    .Select(i => new MenusAuthorityTree()
                    {
                        Id = i.Id,
                        @Checked = roleMenuList.Where(m => m.Id == i.Id).Count() > 0 ? true : false,
                        Disabled = false,
                        Expand = true,
                        Title = i.Name,
                        Children = GetTreeData(menuAllList, roleMenuList, i.Id),
                    }).ToList();

                return result;
            });
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        public async Task TransDelete(int roleId)
        {
            List<string> sqls = new List<string>();
            sqls.Add(EntityToSql.Delete("roles", "Id", roleId.ToString()));
            sqls.Add(string.Format("delete from userrole where rolesid={0}", roleId));
            sqls.Add(string.Format("delete from rolemenu where rolesid={0}", roleId));

            await rolesRepository.TransQuery(sqls);
        }
        #region
        private List<MenusAuthorityTree> GetTreeData(List<MenusModel> menuAllList, List<MenusModel> roleMenuList, int id)
        {
            var result = menuAllList.Where(u => u.ParentId == id).OrderBy(u => u.Sort).Select(i => new MenusAuthorityTree()
            {
                Id = i.Id,
                @Checked = roleMenuList.Where(m => m.Id == i.Id).Count() > 0 ? true : false,
                Disabled = false,
                Expand = true,
                Title = i.Name,
                Children = GetTreeData(menuAllList, roleMenuList, i.Id),
            }).ToList();

            return result;
        }
        #endregion
    }
}
