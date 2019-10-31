using GineCore.Entity.Entities;
using GineCore.Repository.BaseRepository.Base;

namespace GineCore.Repository
{
    public interface IPlanRepository : IBaseRepository<Plan>
    {
        
    }

    public class PlanRepository: BaseRepository<Plan>, IPlanRepository
    {

    }
}
