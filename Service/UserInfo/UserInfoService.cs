using GineCore.Repository;
using GineCore.Common;
using GineCore.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using GineCore.Repository.BaseRepository.Base;
using System.Threading.Tasks;

namespace GineCore.Service.UserInfoService
{
    public class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        private readonly IUserInfoRepository userInfoRepository;

        public UserInfoService(IUserInfoRepository _userInfoRepository,
            IBaseRepository<UserInfo> baseRepository)
            : base(baseRepository)
        {
            userInfoRepository = _userInfoRepository;
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<ResponseModel<UserInfoModel>> CheckLogin(string userName, string password)
        {
            var entity = await userInfoRepository.Find<UserInfoModel>(
                                string.Format(@" UserName='{0}' and Password='{1}'", userName, password));

            if (entity != null)
            {
                if (entity.EnableMarked)
                {
                    return Success(entity);
                }
                else
                {
                    return Fail<UserInfoModel>("账户被系统锁定,请联系管理员");
                }
            }
            else
            {
                return Fail<UserInfoModel>("账号或密码错误，请重新输入");
            }
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<JqGridModel<UserInfoModel>> GetList(UserInfoParam param)
        {
            return await Task.Run(() =>
            {
                string sql = "select UserInfo.* {0} from UserInfo {1} where 1=1 {2} {3}";
                string orderBy = " ORDER BY Id ";
                string where = string.Format(" and (IsRoot='false' or isroot is null)");
                string join = "";
                string select = "";

                //条件
                if (!string.IsNullOrEmpty(param.Key))
                {
                    where += string.Format(" and( UserInfo.UserName like '%{0}%')", param.Key);
                }

                if (param.IsUserRole)
                {
                    join += string.Format(" left join UserRole on UserRole.UserInfoId=UserInfo.Id");

                    if (param.RoleId != null && param.RoleId != 0)
                    {
                        where += string.Format(" and(UserRole.RolesId='{0}')", param.RoleId);
                    }
                    else
                    {
                        where += string.Format(" and(UserRole.RolesId is null)");
                    }
                }

                //排序
                if (!string.IsNullOrEmpty(param.sidx) && !string.IsNullOrEmpty(param.sord))
                {
                    orderBy = string.Format(" order by {0} {1} ", param.sidx, param.sord);
                }

                sql = string.Format(sql, select, join, where, orderBy);

                var list = userInfoRepository.FindListPager<UserInfoModel>(sql, param);

                return list;
            });
        }
    }
}
