using GineCore.Common;
using GineCore.Entity.Entities;
using GineCore.EntityFrameworkCore.DbExtensions;
using GineCore.Repository;
using GineCore.Repository.BaseRepository.Base;
using GineCore.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GineCore.Service.PlanService
{
    public class PlanService 
        : BaseService<Plan>, IPlanService
    {
        private readonly IPlanRepository planRepository;
        public PlanService(
            IPlanRepository _planRepository,
        IBaseRepository<Plan> baseRepository)
            : base(baseRepository)
        {
            planRepository = _planRepository;
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<JqGridModel<PlanModel>> GetList(PlanParam param)
        {
            string sql = "select Plan.* {0} from Plan {1} where 1=1 {2} {3}";
            string orderBy = " ORDER BY Id ";
            string where = string.Empty;
            string join = "";
            string select = "";

            //条件
            if (!string.IsNullOrEmpty(param.SearchKey))
            {
                where += string.Format(" and( Title like '%{0}%')", param.SearchKey);
            }
            //排序
            if (!string.IsNullOrEmpty(param.sidx) && !string.IsNullOrEmpty(param.sord))
            {
                orderBy = string.Format(" order by {0} {1} ", param.sidx, param.sord);
            }

            sql = string.Format(sql, select, join, where, orderBy);

            var list = await planRepository.FindListPager<PlanModel>(sql, param);
            return list;
        }
    }
}
