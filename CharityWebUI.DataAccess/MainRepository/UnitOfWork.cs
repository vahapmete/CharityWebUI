using System;
using System.Collections.Generic;
using System.Text;
using CharityWebUI.Data;
using CharityWebUI.DataAccess.IMainRepository;

namespace CharityWebUI.DataAccess.MainRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Charity=new CharityRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
        }
        public ICharityRepository Charity { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public void Dispose()
        {
            _db.Dispose();
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
