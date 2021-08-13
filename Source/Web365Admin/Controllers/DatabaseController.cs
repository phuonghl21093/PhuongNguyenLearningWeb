using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web365Utility;
using Web365Base;
using Web365Business.Back_End.IRepository;
using Web365Domain;
using System;
namespace Web365Admin.Controllers
{
    public class DatabaseController : Controller
    {

        private IDatabaseRepositoryBE databaseRepositoryBE;

        // GET: /Admin/ProductType/

        public DatabaseController(IDatabaseRepositoryBE _databaseRepositoryBEs)
        {
            this.databaseRepositoryBE = _databaseRepositoryBEs;
        }

        [HttpPost]
        public ActionResult Run(string str)
        {
            databaseRepositoryBE.ExecQuery(str);

            return Json(new
            {
                error = false
            },
            JsonRequestBehavior.AllowGet);
        }
    }
}
