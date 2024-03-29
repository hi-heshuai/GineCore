using GineCore.Entity.Entities;
using GineCore.Repository.BaseRepository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GineCore.Repository
{
    public interface IUserInfoRepository : IBaseRepository<UserInfo>
    {
        
    }

    public class UserInfoRepository: BaseRepository<UserInfo>, IUserInfoRepository
    {

    }
}
