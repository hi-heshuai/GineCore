

using GineCore.Common;
using GineCore.Entity.Entities;
using GineCore.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GineCore.Service.PlanService
{
    public interface IPlanService:IBaseService<Plan>
    {
        Task<JqGridModel<PlanModel>> GetList(PlanParam param);
    }
}
