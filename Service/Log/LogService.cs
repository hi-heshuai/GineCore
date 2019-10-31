using GineCore.Repository;
using GineCore.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GineCore.Service.LogService
{
    public class LogService:ILogService
    {
        private readonly ILogRepository _repository;

        public LogService(ILogRepository repository)
        {
            this._repository = repository;
        }

        public async Task WriteLog(Log entity)
        {
            await _repository.Insert(entity);
        }
    }
}
