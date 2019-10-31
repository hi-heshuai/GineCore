using GineCore.Common.Appsetting;
using GineCore.Entity.Entities;
using GineCore.Service.MenusService;
using GineCore.Service.RoleMenuService;
using GineCore.Service.RolesService;
using GineCore.Service.UserRoleService;
using GineCore.Web.Controllers.Base;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GineCore.Web.Areas.Api
{
    [EnableCors("any")]
    [Area("Api")]
    public class RolesController
        : BaseController<Roles>
    {
        private readonly IRolesService rolesService = Ioc.GetService<IRolesService>();
        private readonly IRoleMenuService roleMenuService = Ioc.GetService<IRoleMenuService>();
        private readonly IUserRoleService userRoleService = Ioc.GetService<IUserRoleService>();
        public RolesController(
            IOptions<AppsettingModel> setting)
            : base(setting)
        {
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetPager(RolesParam param)
        {
            var result = await rolesService.GetList(param);
            return Json(result);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(RolesCreateModel model)
        {
            var userByName = await entityService.Get<Roles>(string.Format("RoleName='{0}'", model.RoleName));
            if (userByName != null)
            {
                return Fail("该用户组名已存在");
            }

            return await base.Create(model);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(RolesEditModel model)
        {
            return await Edit(model, "RoleName", "Describe");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await rolesService.TransDelete(id);
            return Success("删除成功");
        }

        /// <summary>
        /// 获取角色菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetMenusByRoles(int roleId)
        {
            var result = await rolesService.GetMenusByRoleId(roleId);

            return Success(result);
        }

        /// <summary>
        /// 保存角色菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveMenusByRoles(int roleId, string menuIdsStr)
        {
            var menuIdList = menuIdsStr.Trim(',').Split(',')
                .Select(str => int.Parse(str)).ToList();

            await roleMenuService.SaveMenusByRoles(roleId, menuIdList);

            return Success();
        }

        /// <summary>
        /// 角色绑定用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> BindUser(int roleId, string userIds)
        {
            var userIdList = userIds.Trim(',').Split(',')
                .Select(str => int.Parse(str)).ToList();

            await userRoleService.BindUser(roleId, userIdList);

            return Success();
        }

        /// <summary>
        /// 角色解除绑定用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UnBindUser(int roleId, string userIds)
        {
            userIds = userIds.Trim(',');

            await userRoleService.UnBindUser(roleId, userIds);

            return Success();
        }
    }
}
