using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CharityWebUI.DataAccess.IMainRepository;
using CharityWebUI.Models.DbModels;
using CharityWebUI.Models.ViewModels;

namespace CharityWebUI.Areas.GeneralAdmin.Controllers
{
    [Area("GeneralAdmin")]
    public class CharityController : Controller
    {
        #region Variables
        private readonly IUnitOfWork _uow;

        #endregion

        #region CTOR
        public CharityController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        #endregion

        #region ACTIONS
        public IActionResult Index()
        {
            var model = new CharityViewModel
            {
                Charities = _uow.Charity.GetAll(),
                ApplicationUsers = _uow.ApplicationUser.GetAll()
            };
            return View(model);
    
        } 
        #endregion

        #region API CALLS
        //public IActionResult GetAll()
        //{
            




        //    var allObj = _uow.Charity.GetAll();
        //    return Json(new { data = allObj });
        //}

        public IActionResult Delete(int id)
        {
            var deleteData = _uow.Charity.Get(id);
            if (deleteData==null)
            {
                return Json(new {success = false, message = "Data Not Found!"});
            }
            _uow.Charity.Remove(deleteData);
            _uow.Save();
            return Json(new {success = true, message = "Delete Operations Successfully!"});
        }
        #endregion

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Charity charity=new Charity();
            if (id==null)
            {
                return View(charity);
            }

            charity = _uow.Charity.Get((int)id);
            if (charity != null)
            {
                return View(charity);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult Upsert(Charity charity)
        {
            if (ModelState.IsValid)
            {
                if (charity.Id==0)
                {
                    _uow.Charity.Add(charity);
                }
                else
                {
                    _uow.Charity.Update(charity);
                }

                _uow.Save();
                return RedirectToAction("Index");
            }

            return View(charity);
        }
    }
}
