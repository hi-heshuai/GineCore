using GineCore.Entity.Entities;
using GineCore.Repository.BaseRepository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GineCore.Repository
{
    public interface IUserRoleRepository : IBaseRepository<UserRole>
    {

    }

    public class UserRoleRepository: BaseRepository<UserRole>, IUserRoleRepository
    {

    }
}
