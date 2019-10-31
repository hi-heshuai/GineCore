using GineCore.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GineCore.Service.RoleMenuService
{
    public interface IRoleMenuService:IBaseService<RoleMenu>
    {
        /// <summary>
        /// �����û���Ȩ��
        /// </summary>
        Task SaveMenusByRoles(int roleId, List<int> menuIdList);
    }
}
