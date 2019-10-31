using GineCore.Entity.Entities;
using GineCore.EntityFrameworkCore.DbExtensions;
using GineCore.Repository;
using GineCore.Repository.BaseRepository.Base;
using GineCore.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GineCore.Service.UserRoleService
{
    public class UserRoleService 
        : BaseService<UserRole>, IUserRoleService
    {
        private readonly IUserRoleRepository userRoleRepository;
        public UserRoleService(
            IUserRoleRepository _userRoleRepository,
        IBaseRepository<UserRole> baseRepository)
            : base(baseRepository)
        {
            userRoleRepository = _userRoleRepository;
        }

        /// <summary>
        /// 角色绑定用户
        /// </summary>
        public async Task BindUser(int roleId, List<int> userIdList)
        {
            List<string> sqls = new List<string>();
            userIdList.ForEach(userId =>
            {
                UserRole userRole = new UserRole()
                {
                    RolesId = roleId,
                    UserInfoId = userId
                };

                sqls.Add(EntityToSql.AddSql(userRole));
            });

            await TransQuery(sqls);
        }

        /// <summary>
        /// 角色绑定用户
        /// </summary>
        public async Task UnBindUser(int roleId, string userIds)
        {
            var relations = await userRoleRepository.FindList(string.Format("RolesId='{0}' and UserInfoId in({1})", roleId, userIds));
            List<string> sqls = new List<string>();
            relations.ForEach(relation =>
            {
                sqls.Add(EntityToSql.DeleteTemplate(relation));
            });

            await TransQuery(sqls);
        }
    }
}
