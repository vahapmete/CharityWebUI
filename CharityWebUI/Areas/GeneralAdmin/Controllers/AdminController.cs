using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CharityWebUI.Data;
using CharityWebUI.DataAccess.IMainRepository;
using CharityWebUI.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace CharityWebUI.Areas.GeneralAdmin.Controllers
{
    [Area("GeneralAdmin")]
    public class UserController : Controller
    {
        //private readonly IUnitOfWork _uow;
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        #region API CALLS

        public IActionResult GetAll()
        {
            var userList = _db.ApplicationUser.Include(c => c.Charity).ToList();
            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            foreach (var user in userList)
            {
                var roleId = userRole.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;

                if (user.Charity == null)
                {
                    user.Charity = new Charity()
                    {
                        Name = string.Empty
                    };
                }
            }

            return Json((new { data = userList }));
        }
        #endregion
    }
}
