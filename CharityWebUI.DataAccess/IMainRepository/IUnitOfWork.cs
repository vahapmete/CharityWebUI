using System;
using System.Collections.Generic;
using System.Text;

namespace CharityWebUI.DataAccess.IMainRepository
{
    public interface IUnitOfWork:IDisposable
    {
        ICharityRepository Charity { get; }
        IApplicationUserRepository ApplicationUser { get; }
        void Save();
    }
}
