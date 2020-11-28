using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CharityWebUI.Data;
using CharityWebUI.DataAccess.IMainRepository;
using CharityWebUI.Models.DbModels;

namespace CharityWebUI.DataAccess.MainRepository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db; 

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    } 
}
