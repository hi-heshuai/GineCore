

using GineCore.Entity.Entities;
using GineCore.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GineCore.Service.UserRoleService
{
    public interface IUserRoleService:IBaseService<UserRole>
    {
        /// <summary>
        /// ��ɫ���û�
        /// </summary>
        Task BindUser(int roleId, List<int> userIdList);

        /// <summary>
        /// ����û�
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userIds"></param>
        Task UnBindUser(int roleId, string userIds);
    }
}
