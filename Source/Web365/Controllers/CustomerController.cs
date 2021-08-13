using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Web365Base;
using Web365Business.Front_End.IRepository;
using Web365Models.Customer;

namespace Web365.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IOtherRepositoryFE _otherRepository;

        public CustomerController(IOtherRepositoryFE otherRepository)
        {
            this._otherRepository = otherRepository;
        }

        // GET: Customer
        [HttpPost]
        public ActionResult SendContactInfo(ContactInfoItem dto)
        {

            try
            {
                var contactEntity = new tblContact()
                {
                    DateCreated = DateTime.Now,
                    Title = dto.Title,
                    IsDeleted = false,
                    Name = dto.FullName,
                    Email = dto.Email,
                    Message = dto.Message
                };
                _otherRepository.AddContact(contactEntity);
                return Json(new
                {
                    error = false,
                    message = "Gửi thông tin thành công"
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new
                {
                    error = true,
                    message = "Gửi thông tin thất bại"
                }, JsonRequestBehavior.AllowGet);
            }     
        }
    }
}