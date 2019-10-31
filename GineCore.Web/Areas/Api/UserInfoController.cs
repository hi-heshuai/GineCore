using GineCore.Common;
using GineCore.Common.Appsetting;
using GineCore.Entity.Entities;
using GineCore.Service.MenusService;
using GineCore.Service.RolesService;
using GineCore.Service.UserInfoService;
using GineCore.Web.Areas.Base.Filter;
using GineCore.Web.Controllers.Base;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GineCore.Web.Areas.Api.Controllers
{
    [EnableCors("any")]
    [Area("Api")]
    public class UserInfoController : BaseController<UserInfo>
    {
        private readonly IUserInfoService userInfoService = Ioc.GetService<IUserInfoService>();

        public UserInfoController(IOptions<AppsettingModel> setting)
            :base(setting)
        {
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetPager(UserInfoParam param)
        {
            var result = await userInfoService.GetList(param);
            return Json(result);
        }

        /// <summary>
        /// 添加人员
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(UserInfoCreateModel model)
        {
            var userByName = await entityService.Get<UserInfo>(string.Format("userName='{0}'", model.UserName));
            if(userByName != null)
            {
                return Fail("该用户名已存在");
            }

            var user = await GetTokenUser();
            model.EnableMarked = true;
            model.Password = DESEncrypt.Encrypt(appsetting.DefaultPassword);
            if (string.IsNullOrEmpty(model.Avatar))
            {
                model.Avatar = appsetting.IpAndPoint + appsetting.DefaultHeadPic;
            }

            return await base.Create(model);
        }

        /// <summary>
        /// 编辑信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(UserInfoEditModel model)
        {
            return await Edit(model, "UserName", "RealName", "Birthday", "MobilePhone", "Email");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return Fail("参数错误");
            }

            return await base.Delete((int)id);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [NoLogin]
        public async Task<IActionResult> Login(string userName, string password)
        {
            password = DESEncrypt.Encrypt(password);

            var result = await userInfoService.CheckLogin(userName, password);

            if (result.result)
            {
                result.data.LoginToken = Guid.NewGuid().ToString();
                result.data.LoginTime = DateTime.Now;
                await entityService.Update(result.data, "LoginToken", "LoginTime");
            }
            
            return Json(result);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetLoginUserInfo(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return Fail("用户非法使用");
            }

            var model = await GetTokenUser();

            return Success(model);
        }
    }
}
