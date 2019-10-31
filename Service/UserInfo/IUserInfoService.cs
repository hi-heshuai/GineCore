using GineCore.Common;
using GineCore.Entity.Entities;
using System.Threading.Tasks;

namespace GineCore.Service.UserInfoService
{
    public interface IUserInfoService: IBaseService<UserInfo>
    {
        Task<ResponseModel<UserInfoModel>> CheckLogin(string userName, string password);

        Task<JqGridModel<UserInfoModel>> GetList(UserInfoParam param);
    }
}
