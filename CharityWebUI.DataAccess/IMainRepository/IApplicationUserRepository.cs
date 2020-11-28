using System;
using System.Collections.Generic;
using System.Text;
using CharityWebUI.Models.DbModels;

namespace CharityWebUI.DataAccess.IMainRepository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        //void Update(ApplicationUser applicationUser);
    }
}
