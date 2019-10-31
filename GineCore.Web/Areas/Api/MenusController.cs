using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GineCore.Common;
using GineCore.Common.Appsetting;
using GineCore.Entity.Entities;
using GineCore.Service.MenusService;
using GineCore.Web.Controllers.Base;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GineCore.Web.Areas.Api
{
    [EnableCors("any")]
    [Area("Api")]
    public class MenusController : BaseController<Menus>
    {
        private readonly IMenusService menusService;

        public MenusController(
            IMenusService _menusService,
            IOptions<AppsettingModel> setting)
            : base(setting)
        {
            menusService = _menusService;
        }

        #region 数据
        [HttpPost]
        public async Task<IActionResult> Create(CreateMenusModel model)
        {
            if (model.menuType == 0)
            {
                model.Type = null;
            }

            return await base.Create(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditMenusModel model)
        {
            if (model.menuType == 0)
            {
                model.Type = null;
            }

            return await Edit(model, "Icon", "Type", "Name", "Sort", "EnableMarked", "Key");
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await entityService.Get<MenusModel>(id);

            return Success(entity);
        }

        [HttpPost]
        public new async Task<IActionResult> Delete(int id)
        {
            var entities = await entityService.GetEntities<MenusModel>(string.Format(" ( parentid={0} or id={0})", id));
            var ids = entities.Select(m => m.Id).ToList();
            return await Delete(ids);
        }

        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetMenus()
        {
            var menusList = await menusService.GetMenus();
            return Json(menusList);
        }

        /// <summary>
        /// 获取角色菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetLoginUserMenusTree()
        {
            var user = await GetTokenUser();
            var result = await menusService.GetMenusTreeByRoleId(user.RoleId);

            return Success(result);
        }

        /// <summary>
        /// 获取角色菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetLoginUserMenus()
        {
            var user = await GetTokenUser();
            var result = await menusService.GetMenusByRoleId(user.RoleId);

            return Success(result);
        }

        /// <summary>
        /// 获取菜单按钮类型
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetMenuBtnTypes()
        {
            return await Task.Run(() =>
            {
                var result = EnumsHelper.GetEnumDictionary<MenusTypeEnum.MenuBtnTypeEnum>();
                return Success(result);
            });
        }
        #endregion
    }
}