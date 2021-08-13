using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web365Utility;
using Web365Base;
using Web365Business.Back_End.IRepository;
using Web365Domain;
using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
//using WebMatrix.WebData;
namespace Web365Admin.Controllers
{
    public class UserPageController : BaseController
    {

        private IUserPageRepositoryBE userPageRepository;

        // GET: /Admin/ProductType/

        public UserPageController(IUserPageRepositoryBE _userPageRepository)
        {
            this.userPageRepository = _userPageRepository;
            this.baseRepository = _userPageRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetList(string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending)
        {
            var total = 0;
            var list = userPageRepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending);

            return Json(new
            {
                total = total,
                list = list
            },
            JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPageOfUser(string userId)
        {

            userId = !string.IsNullOrEmpty(userId) ? userId : User.Identity.GetUserId();

            var list = userPageRepository.GetPageOfUser(userId);

            return Json(new
            {
                list = list
            },
            JsonRequestBehavior.AllowGet);
        }        

        [HttpGet]
        public ActionResult EditForm(int? id)
        {
            var obj = new PageItem();

            var listTree= userPageRepository.GetListForTree<object>();

            if (id.HasValue)
                obj = userPageRepository.GetItemById<PageItem>(id.Value);

            return Json(new
            {
                data = obj,
                listTree = listTree
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Action(tblPage objSubmit)
        {
            
            if (objSubmit.ID == 0)
            {
                objSubmit.DateCreated = DateTime.Now;
                objSubmit.DateUpdated = DateTime.Now;
                objSubmit.IsDeleted = false;
                objSubmit.IsShow = true;
                userPageRepository.Add(objSubmit);
            }
            else
            {
                var obj = userPageRepository.GetById<tblPage>(objSubmit.ID);
                
                UpdateModel(obj);

                obj.DateUpdated = DateTime.Now;
                obj.Parent = objSubmit.Parent == 0 ? null : objSubmit.Parent;

                userPageRepository.Update(obj);
            }

            return Json(new
            {
                Error = false
            }, JsonRequestBehavior.AllowGet);
        }
        
    }
}
