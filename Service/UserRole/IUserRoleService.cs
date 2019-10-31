

using GineCore.Entity.Entities;
using GineCore.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GineCore.Service.UserRoleService
{
    public interface IUserRoleService:IBaseService<UserRole>
    {
        /// <summary>
        /// 角色绑定用户
        /// </summary>
        Task BindUser(int roleId, List<int> userIdList);

        /// <summary>
        /// 解绑用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userIds"></param>
        Task UnBindUser(int roleId, string userIds);
    }
}
