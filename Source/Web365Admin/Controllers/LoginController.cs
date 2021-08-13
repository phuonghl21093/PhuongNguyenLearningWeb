using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Web365Admin.Models;

namespace Web365Admin.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Admin/Login/

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public LoginController()
        {
        }

        public LoginController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
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

        [HttpPost]
        public async Task<ActionResult> Login(string userName, string password, bool psesistCookie)
        { 
            //var user = new ApplicationUser() { UserName = "sonnichkhunno1@gmail.com", Email = "sonnichkhunno1@gmail.com" };
            //IdentityResult result1 = await UserManager.CreateAsync(user, "123@123aA@");
            var result = await SignInManager.PasswordSignInAsync(userName, password, psesistCookie, shouldLockout: false);

            var message = string.Empty;

            switch (result)
            {
                case SignInStatus.Success:
                    message = "Đăng nhập thành công";
                    break;
                case SignInStatus.LockedOut:
                    message = "Tài khoản của bạn đã bị khóa";
                    break;
                case SignInStatus.RequiresVerification:
                    message = "Sai tên đăng nhập hoặc mật khẩu";
                    break;
                case SignInStatus.Failure:
                    message = "Sai tên đăng nhập hoặc mật khẩu";
                    break;
                default:
                    message = "Đăng nhập không thành công, bạn vui lòng liên hệ quản trị viên";
                    break;
            }

            return Json(new
            {
                result = true,//result == SignInStatus.Success,
                message
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Logout()
        {

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return Json(new
            {
                result = true,
                message = string.Empty
            },
            JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChangePassword(string currentPass, string newPass)
        {
            var result = UserManager.ChangePasswordAsync(User.Identity.GetUserId(), currentPass, newPass);

            return Json(new
            {
                result = !result.Result.Succeeded,
                message = string.Join(", ", result.Result.Errors)
            },
            JsonRequestBehavior.AllowGet);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}
