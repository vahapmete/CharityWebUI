using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CharityWebUI.Data;
using CharityWebUI.DataAccess.IMainRepository;
using CharityWebUI.Models.DbModels;

namespace CharityWebUI.DataAccess.MainRepository
{
    public class CharityRepository : Repository<Charity>, ICharityRepository
    {
        private readonly ApplicationDbContext _db;

        public CharityRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public void Update(Charity charity)
        {
            var data = _db.Charities.FirstOrDefault(x => x.Id == charity.Id);
            if (data != null)
            {
                data.Name = charity.Name;
                data.About = charity.About;
                data.Address = charity.Address;
                data.AdminId = charity.AdminId;
                data.City = charity.City;
                data.Email = charity.Email;
            }

            _db.SaveChanges();
        }
    }
}
