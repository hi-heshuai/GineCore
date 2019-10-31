using GineCore.Common;
using GineCore.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GineCore.Service.RolesService
{
    public class RolesParam : JqgridParam
    {
        public string SearchKey { get; set; }
    }

    public class RolesModel : Roles
    {
        public string CreateTimeStr
        {
            get
            {
                if(CreatedAt != null)
                {
                    return ((DateTime)CreatedAt).ToString("yyyy-MM-dd hh:mm:ss");
                }
                return string.Empty;
            }
        }
    }

    /// <summary>
    /// ´´½¨
    /// </summary>
    public class RolesCreateModel : Roles
    {
        
    }

    /// <summary>
    /// ÐÞ¸Ä
    /// </summary>
    public class RolesEditModel : Roles
    {

    }

}
