using GineCore.Common;
using GineCore.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GineCore.Service.PlanService
{
    public class PlanParam : JqgridParam
    {
        public string SearchKey { get; set; }
    }

    public class PlanModel : Plan
    {
        public string CreateTimeStr
        {
            get
            {
                if (CreatedAt != null)
                {
                    return ((DateTime)CreatedAt).ToString("yyyy-MM-dd hh:mm:ss");
                }
                return string.Empty;
            }
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    public class PlanCreateModel : Plan
    {

    }

    /// <summary>
    /// �޸�
    /// </summary>
    public class PlanEditModel : Plan
    {

    }
}
