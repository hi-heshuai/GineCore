using GineCore.Common;
using GineCore.Entity.Entities;
using GineCore.Repository;
using GineCore.Repository.BaseRepository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GineCore.Service.MenusService
{
    public class MenusService : BaseService<Menus>, IMenusService
    {
        private readonly IMenusRepository menusRepository;
        private readonly IRoleMenuRepository roleMenuRepository;

        public MenusService(IMenusRepository _menusRepository,
            IRoleMenuRepository _roleMenuRepository,
            IBaseRepository<Menus> baseRepository)
            : base(baseRepository)
        {
            menusRepository = _menusRepository;
            roleMenuRepository = _roleMenuRepository;
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        public async Task<List<MenusAuthority>> GetMenusTreeByRoleId(int? roleId)
        {
            return await Task.Run(async () =>
            {
                List<MenusModel> list;
                if (roleId != -1)
                {
                    list = await roleMenuRepository.GetRoleMenu<MenusModel>(roleId);
                }
                else
                {
                    list = await menusRepository.FindList<MenusModel>();
                }

                List<MenusAuthority> result = list
                    .Where(u => (u.ParentId == 0 || u.ParentId == null)).OrderBy(u => u.Sort)
                    .Select(u => new MenusAuthority()
                    {
                        Id = u.Id,
                        Icon = u.Icon,
                        LinkUrl = u.LinkUrl,
                        Name = u.Name,
                        Key = u.Key,
                        Children = GetTreeData(list, u.Id, u.Name)
                    }).ToList();

                result = result.Where(u => u.Children != null && u.Children.Count > 0).ToList();

                return result;
            });

        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        public async Task<List<MenusModel>> GetMenusByRoleId(int? roleId)
        {
            return await Task.Run(() =>
            {
                var list = roleMenuRepository.GetRoleMenu<MenusModel>(roleId);

                return list;
            });

        }

        /// <summary>
        /// 获取权限树
        /// </summary>
        /// <returns></returns>
        public async Task<List<MenusAuthority>> GetMenus()
        {
            return await Task.Run(async () =>
            {
                var list = (await menusRepository.FindList<MenusModel>()).OrderBy(m => m.Sort).ToList();

                List<MenusAuthority> result = list.Where(u => (u.ParentId == 0 || u.ParentId == null)).Select(u => new MenusAuthority()
                {
                    Id = u.Id,
                    Icon = u.Icon,
                    LinkUrl = u.LinkUrl,
                    Name = u.Name,
                    EnableMarked = u.EnableMarked,
                    Sort = u.Sort,
                    Type = u.Type,
                    Key = u.Key,
                    ParentId = u.ParentId,
                    Children = GetTreeData(list, u.Id, u.Name)
                }).ToList();

                return result;
            });
        }

        #region
        private List<MenusAuthority> GetTreeData(List<MenusModel> list, int id, string parentName)
        {
            var result = list.Where(u => u.ParentId == id).OrderBy(u => u.Sort).Select(i => new MenusAuthority()
            {
                Id = i.Id,
                Icon = i.Icon,
                LinkUrl = i.LinkUrl,
                Name = i.Name,
                EnableMarked = i.EnableMarked,
                Sort = i.Sort,
                Key = i.Key,
                Type = i.Type,
                ParentId = i.ParentId,
                ParentName = parentName,
                Children = GetTreeData(list, i.Id, i.Name),
            }).ToList();

            return result;
        }
        #endregion
    }
}
