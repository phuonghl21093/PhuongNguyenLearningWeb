using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web365Utility;
using Web365Base;
using Web365Business.Back_End.IRepository;
using Web365Domain;

namespace Web365Admin.Controllers
{
    public class ProductStatusController : BaseController
    {
        private readonly IProductStatusRepositoryBE productStatusRepository;
        //
        // GET: /Admin/ProductManufacturer/
        public ProductStatusController(IProductStatusRepositoryBE _productStatusRepository)
        {
            this.baseRepository = _productStatusRepository;
            this.productStatusRepository = _productStatusRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetList(string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending)
        {
            var total = 0;
            var list = productStatusRepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending);

            return Json(new
            {
                total = total,
                list = list
            },
            JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult EditForm(int? id)
        {
            var obj = new ProductStatusItem();

            var listProductType = productStatusRepository.GetListForTree<object>();

            if (id.HasValue)
                obj = productStatusRepository.GetItemById<ProductStatusItem>(id.Value);

            return Json(new
            {
                data = obj,
                listType = listProductType
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Action(tblProductStatus objSubmit)
        {

            if (objSubmit.ID == 0)
            {
                objSubmit.DateCreated = DateTime.Now;
                objSubmit.DateUpdated = DateTime.Now;
                objSubmit.IsDeleted = false;
                objSubmit.IsShow = true;
                productStatusRepository.Add(objSubmit);
            }
            else
            {
                var obj = productStatusRepository.GetById<tblProductStatus>(objSubmit.ID);

                UpdateModel(obj);

                objSubmit.DateUpdated = DateTime.Now;

                productStatusRepository.Update(obj);
            }

            return Json(new
            {
                Error = false
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
