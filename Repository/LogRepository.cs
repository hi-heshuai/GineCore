using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GineCore.Entity.Entities;
using GineCore.Repository.BaseRepository.Base;

namespace GineCore.Repository
{
    public interface ILogRepository : IBaseRepository<Log>
    {

    }

    public class LogRepository: BaseRepository<Log>, ILogRepository
    {

    }
}
