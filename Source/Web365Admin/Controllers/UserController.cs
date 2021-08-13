using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web365Base;
using Web365Domain;
using Web365Business.Back_End.IRepository;
using Web365Admin.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;

namespace Web365Admin.Controllers
{

    public class UserController : BaseController
    {

        private IUserRepositoryBE userRepository;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public UserController(IUserRepositoryBE _userRepository,
            ApplicationUserManager userManager,
            ApplicationSignInManager signInManager)
        {
            this.baseRepository = _userRepository;
            this.userRepository = _userRepository;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetMenu()
        {
            var result = true;

            return Json(new
            {
                result = result,
                message = string.Empty
            },
            JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetList(string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending)
        {
            var total = 0;
            var list = userRepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending);

            return Json(new
            {
                total = total,
                list = list
            },
            JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditForm(string id)
        {
            var obj = new AspNetUserItem();

            //var list = userRepository.GetListForTree<object>();

            if (!string.IsNullOrEmpty(id))
                obj = userRepository.GetItemById<AspNetUserItem>(id);

            return Json(new
            {
                data = obj
                //list = list
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Action(AspNetUsers objSubmit)
        {
            bool Error = false;
            string Message = "";
            if (string.IsNullOrEmpty(objSubmit.Id))
            {
                var user = new ApplicationUser
                {
                    UserName = objSubmit.UserName,
                    Email = objSubmit.Email,
                    //FirstName = objSubmit.FirstName,
                    //LastName = objSubmit.LastName,
                    //Gender = objSubmit.Gender.HasValue && objSubmit.Gender.Value,
                    //Address = objSubmit.Address,
                    //Note = objSubmit.Note
                };
                var result = await UserManager.CreateAsync(user, objSubmit.PasswordHash);
                if (!result.Succeeded)
                {
                    Message = result.Errors.FirstOrDefault();
                    Error = true;
                }
            }
            else
            {
                var obj = userRepository.GetById<AspNetUsers>(objSubmit.Id);

                UpdateModel(obj);

                //objSubmit.DateUpdated = DateTime.Now;

                userRepository.Update(obj);
                Error = false;
            }

            return Json(new
            {
                Error = Error,
                Message = Message
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RoleForUser(string userId, string roleId)
        {
            userRepository.RoleForUser(userId, Web365Utility.Web365Utility.StringToArrayString(roleId));

            return Json(new
            {
                Error = false
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCurrentUser()
        {
            var obj = userRepository.GetByUserName<object>(User.Identity.Name);

            return Json(new
            {
                data = obj
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteUser(string listId)
        {
            userRepository.Delete(listId.Split(','));
            return Json(new
            {
                Error = false
            }, JsonRequestBehavior.AllowGet);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        if (_userManager != null)
        //        {
        //            _userManager.Dispose();
        //            _userManager = null;
        //        }

        //        if (_signInManager != null)
        //        {
        //            _signInManager.Dispose();
        //            _signInManager = null;
        //        }
        //    }

        //    base.Dispose(disposing);
        //}

    }
}
