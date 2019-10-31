using GineCore.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GineCore.Service.LogService
{
    public interface ILogService
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="entity"></param>
        Task WriteLog(Log entity);
    }
}
