using GineCore.Common;
using GineCore.Entity.Entities;
using GineCore.Service.MenusService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GineCore.Service.RolesService
{
    public interface IRolesService:IBaseService<Roles>
    {
        Task<JqGridModel<RolesModel>> GetList(RolesParam param);

        Task<List<MenusAuthorityTree>> GetMenusByRoleId(int roleId);

        /// <summary>
        /// ɾ����ɫ
        /// </summary>
        Task TransDelete(int roleId);
    }
}
