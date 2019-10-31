using GineCore.Common.Appsetting;
using GineCore.Entity.Entities;
using GineCore.Service.PlanService;
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
    public class PlanController : BaseController<Plan>
    {
        private readonly IPlanService planService = Ioc.GetService<IPlanService>();

        public PlanController(IOptions<AppsettingModel> setting)
            : base(setting)
        {
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetPager(PlanParam param)
        {
            var result = await planService.GetList(param);
            return Json(result);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(PlanCreateModel model)
        {
            model.Status = PlanEnum.Plan.ToString();
            return await base.Create(model);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(PlanEditModel model)
        {
            return await Edit(model, "Title", "Description");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public new async Task<IActionResult> Delete(int id)
        {
            return await base.Delete(id);
        }
    }
}
