using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web365Utility;
using Web365Base;
using Web365Business.Back_End.IRepository;
using Web365Domain;
using System;
using Web365Domain.Language;

namespace Web365Admin.Controllers
{
    public class ArticleTypeController : BaseController
    {

        private IArticleTypeRepositoryBE articleTypeRepository;
        private IArticleGroupTypeRepositoryBE articleGroupTypeRepository;
        private IArticleGroupTypeMapRepositoryBE articleGroupTypeMapRepository;
        private ILanguageRepositoryBE languageRepository;

        // GET: /Admin/ProductType/

        public ArticleTypeController(IArticleTypeRepositoryBE _articleTypeRepository,
            IArticleGroupTypeRepositoryBE _articleGroupTypeRepository,
            IArticleGroupTypeMapRepositoryBE _articleGroupTypeMapRepository,
            ILanguageRepositoryBE _languageRepository)
        {
            this.baseRepository = _articleTypeRepository;
            this.articleTypeRepository = _articleTypeRepository;
            this.articleGroupTypeRepository = _articleGroupTypeRepository;
            this.articleGroupTypeMapRepository = _articleGroupTypeMapRepository;
            this.languageRepository = _languageRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetList(string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending)
        {
            var total = 0;
            var list = articleTypeRepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending);

            return Json(new
            {
                total = total,
                list = list
            },
            JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetListOfGroup(int groupId)
        {
            var list = articleTypeRepository.GetListOfGroup(groupId);

            return Json(new
            {
                list = list
            },
            JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditForm(int? id, int? language)
        {
            var obj = new ArticleTypeItem()
            {
                LanguageId = language ?? (int)StaticEnum.LanguageId.Vietnamese,
                LanguageName = languageRepository.GetItemById<LanguageItem>(language ?? (int)StaticEnum.LanguageId.Vietnamese).Name
            };


            if (id.HasValue)
            {
                if (language.HasValue && language.Value != 0)
                {
                    var defaultLanguage = articleTypeRepository.GetItemById<ArticleTypeItem>(id.Value);
                    var typeInThisLanguage = articleTypeRepository.GetSameArticleTypeByLanguage(defaultLanguage.Parent ?? 0, language.Value);
                    if (typeInThisLanguage != null)
                    {
                        obj.Parent = typeInThisLanguage.ID;
                    }

                    obj.Number = defaultLanguage.Number;
                    obj.PictureID = defaultLanguage.PictureID;
                    obj.RootId = id;
                }
                else
                {
                    obj = articleTypeRepository.GetItemById<ArticleTypeItem>(id.Value);
                }
            }


            var listType = articleTypeRepository.GetListForTree<object>(obj.LanguageId.Value);
            //var listGroup = articleGroupTypeRepository.GetListForTree<object>(obj.LanguageId.Value);

            return Json(new
            {
                data = obj,
                listType = listType,
                //listGroup = listGroup
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Action(tblTypeArticle objSubmit)
        {

            if (objSubmit.ID == 0)
            {
                objSubmit.DateCreated = DateTime.Now;
                objSubmit.DateUpdated = DateTime.Now;
                objSubmit.IsDeleted = false;
                objSubmit.IsShow = true;
                articleTypeRepository.Add(objSubmit);
            }
            else
            {
                var obj = articleTypeRepository.GetById<tblTypeArticle>(objSubmit.ID);

                UpdateModel(obj);

                objSubmit.DateUpdated = DateTime.Now;

                articleTypeRepository.Update(obj);
            }

            articleGroupTypeMapRepository.ResetGroupOfType(objSubmit.ID, Web365Utility.Web365Utility.StringToArrayInt(Request["groupID"]));

            return Json(new
            {
                Error = false,

            }, JsonRequestBehavior.AllowGet);
        }
    }
}
