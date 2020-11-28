using System;
using System.Collections.Generic;
using System.Text;
using CharityWebUI.Models.DbModels;

namespace CharityWebUI.DataAccess.IMainRepository
{
    public interface ICharityRepository:IRepository<Charity>
    {
        void Update (Charity charity);
    }
}
