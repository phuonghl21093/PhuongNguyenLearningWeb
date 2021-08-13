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
    public class ArticleController : BaseController
    {

        private IArticleRepositoryBE articleRepository;
        private IArticleGroupRepositoryBE articleGroupRepository;
        private IArticleTypeRepositoryBE articleTypeRepository;
        private IArticleGroupMapRepositoryBE articleGroupMapRepository;
        private ILanguageRepositoryBE languageRepositoryBe;

        // GET: /Admin/ProductType/

        public ArticleController(IArticleRepositoryBE _articleRepository,
            IArticleGroupRepositoryBE _articleGroupRepository,
            IArticleTypeRepositoryBE _articleTypeRepository,
            ILanguageRepositoryBE _languageRepositoryBe,
            IArticleGroupMapRepositoryBE _articleGroupMapRepository)
        {
            this.baseRepository = _articleRepository;
            this.articleRepository = _articleRepository;
            this.articleGroupRepository = _articleGroupRepository;
            this.articleTypeRepository = _articleTypeRepository;
            this.articleGroupMapRepository = _articleGroupMapRepository;
            this.languageRepositoryBe = _languageRepositoryBe;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetList(string name, int? typeId, int? groupId, int currentRecord, int numberRecord, string propertyNameSort, bool descending)
        {
            var total = 0;
            var list = articleRepository.GetList(out total, name, typeId, groupId, currentRecord, numberRecord, propertyNameSort, descending);

            var jsonResult = Json(new
            {
                total = total,
                list = list
            },
            JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpGet]
        public ActionResult GetPropertyFilter()
        {
            var listArticleType = articleTypeRepository.GetListForTree<object>();

            var listArticleGroup = articleGroupRepository.GetListForTree<object>();

            return Json(new
            {
                listType = listArticleType,
                listGroup = listArticleGroup
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditForm(int? id, int? language)
        {
            var obj = new ArticleItem
            {
                LanguageId = language ?? (int) StaticEnum.LanguageId.Vietnamese,
                LanguageName = languageRepositoryBe.GetItemById<LanguageItem>(language ?? (int)StaticEnum.LanguageId.Vietnamese).Name
            };

            //var listLanguage = languageRepositoryBe.GetListForTree<object>();

            if (id.HasValue)
            {
                if (!language.HasValue || language.Value == 0)
                {
                    obj = articleRepository.GetItemById<ArticleItem>(id.Value);
                }
                else
                {
                    var defaultLanguage = articleRepository.GetItemById<ArticleItem>(id.Value);
                    var typeInThisLanguage = articleTypeRepository.GetSameArticleTypeByLanguage(defaultLanguage.TypeID ?? 0, language.Value);
                    var groupInThisLanguage = articleGroupRepository.GetSameArticleGroupByLanguage(defaultLanguage.ListGroupID ?? new int[]{}, language.Value);

                    if (typeInThisLanguage != null)
                    {
                        obj.TypeID = typeInThisLanguage.ID;
                    }

                    if (groupInThisLanguage != null)
                    {
                        obj.ListGroupID = groupInThisLanguage;
                    }

                    obj.RootId = id;
                    obj.Number = defaultLanguage.Number;
                    obj.ListPictureID = defaultLanguage.ListPictureID;
                    obj.PictureID = defaultLanguage.PictureID;
                }
            }

            var listArticleType = articleTypeRepository.GetListForTree<object>(obj.LanguageId.Value);

            var listArticleGroup = articleGroupRepository.GetListForTree<object>(obj.LanguageId.Value);

            var jsonResult = Json(new
            {
                data = obj,
                listType = listArticleType,
                listGroup = listArticleGroup,
                //listLanguage
            },
            JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Action(tblArticle objSubmit)
        {

            if (objSubmit.ID == 0)
            {
                objSubmit.DateCreated = DateTime.Now;
                objSubmit.DateUpdated = DateTime.Now;
                objSubmit.IsDeleted = false;
                objSubmit.IsShow = true;
                articleRepository.Add(objSubmit);

            }
            else
            {
                var obj = articleRepository.GetById<tblArticle>(objSubmit.ID);

                UpdateModel(obj);

                objSubmit.DateUpdated = DateTime.Now;

                articleRepository.Update(obj);
            }

            articleGroupMapRepository.ResetGroupOfNews(objSubmit.ID, Web365Utility.Web365Utility.StringToArrayInt(Request["groupID"]));

            articleRepository.ResetListPicture(objSubmit.ID, Request["listPictureId"]);

            return Json(new
            {
                Error = false

            }, JsonRequestBehavior.AllowGet);
        }
    }
}
