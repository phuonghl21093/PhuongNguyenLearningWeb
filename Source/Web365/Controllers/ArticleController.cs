using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web365Business.Front_End.IRepository;
using Web365Models;
using Web365Utility;

namespace Web365.Controllers
{
    public class ArticleController : BaseController
    {
        private readonly IArticleTypeRepositoryFE _articleType;
        private readonly IArticleRepositoryFE _article;
        private readonly ILayoutContentRepositoryFE _layoutContent;
        private readonly ILanguageRepositoryFE _languageRepository;
        private readonly ISettingRepositoryFE _settingDafeRepository;
        public ArticleController(IArticleTypeRepositoryFE articleTypeRepositoryFe, IArticleRepositoryFE articleRepositoryFe, ILanguageRepositoryFE languageRepository, ISettingRepositoryFE settingDafeRepository)
        {
            _articleType = articleTypeRepositoryFe;
            _article = articleRepositoryFe;
            _languageRepository = languageRepository;
            _settingDafeRepository = settingDafeRepository;
        }

        // GET: Article
        public ActionResult Index(string category, int? page)
        {
            try
            {
                var articleType = _articleType.GetItemByNameAscii(category);
                if (articleType != null && articleType.LanguageId != LanguageId)
                {
                    var language =
                        _languageRepository.GetItemById(articleType.LanguageId ?? (int)StaticEnum.LanguageId.Vietnamese);
                    return Redirect("/" + language.Code + "/" + category);
                }

                ViewBag.Page = page ?? 1;

                int defaultLangTypeId = !articleType.RootId.HasValue
                    ? articleType.ID
                    : _articleType.GetItemById(articleType.RootId.Value).ID;

                switch (defaultLangTypeId)
                {
                    case (int)StaticEnum.ArticleType.Introduce:
                        var firstArticle = _article.GetFirstArticleItem(articleType.ID);

                        var otherCates = _articleType.GetListByParent(new[] { articleType.ID });

                        ViewBag.OtherCategories = otherCates;
                        ViewBag.OtherArticles = _article.GetOtherArticle(articleType.ID, 0, 0, 10);

                        return View("IntroduceIndex", firstArticle);
                    case (int)StaticEnum.ArticleType.News:
                        ViewData.Model =
                           _article.GetListByType(GetArticleTypeIdByLanguage(defaultLangTypeId), string.Empty,
                               ((page ?? 1) - 1) * ConfigWeb.ListNewsInCate, ConfigWeb.ListNewsInCate);
                        ViewBag.TypeArticle = _articleType.GetItemById(GetArticleTypeIdByLanguage(defaultLangTypeId));

                        return View("NewsIndex", ViewData.Model);
                    case (int)StaticEnum.AbilityChildCate.Certificate:
                    case (int)StaticEnum.AbilityChildCate.Equipment:
                    case (int)StaticEnum.AbilityChildCate.Human:
                        if (articleType.Parent.HasValue)
                        {
                            ViewBag.OtherCategories = _articleType.GetListByParent(new[] { articleType.Parent.Value }, 0,
                                100);
                        }

                        var defaultArticle = GetArticleTypeIdByLanguage((int)StaticEnum.ArticleType.Introduce);

                        ViewBag.ParentName = articleType.Name;
                        var articlesModel = _article.GetListByArrType(new[] { defaultArticle }, 0, 100);
                        ViewBag.OtherArticles = articlesModel != null ? articlesModel.List : null;
                        return View("AbilityIndex", articleType);
                    case (int)StaticEnum.ArticleType.Project:
                    case (int)StaticEnum.ProjectChildCate.OnGoing:


                    case (int)StaticEnum.ProjectChildCate.Completed:
                        //1.Lấy danh sách các loại dự án
                        int[] parent = new int[] { (int)StaticEnum.ArticleType.Project };
                        ViewBag.ltsArticleProjectType = _articleType.GetListByParent(parent);
                        //2.Lấy danh sách dự án
                        var listArticleModel =
                            _article.GetListByType(GetArticleTypeIdByLanguage(defaultLangTypeId), string.Empty, 0, int.MaxValue);
                        articleType.Articles = listArticleModel.List;
                        ViewBag.TotalRecord = listArticleModel.total;
                        return View("ProjectIndex", articleType);
                    case (int)StaticEnum.ArticleType.Service:
                        articleType.Articles =
                            _article.GetListByType(GetArticleTypeIdByLanguage((int)StaticEnum.ArticleType.Service),
                                string.Empty, 0, 100).List;
                        return View("ServiceIndex", articleType);
                    case (int)StaticEnum.ArticleType.Document:
                        return View("DocumentIndex");
                    case (int)StaticEnum.ArticleType.Contact:
                        ViewBag.Facebook = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.Facebook, LanguageId);
                        ViewBag.Twitter = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.Twitter, LanguageId);
                        ViewBag.Youtube = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.Youtube, LanguageId);
                        ViewBag.Instagram = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.Instagram, LanguageId);
                        var contactArticle =
                            _article.GetListByType(GetArticleTypeIdByLanguage((int)StaticEnum.ArticleType.Contact),
                                string.Empty, 0, 1).List.FirstOrDefault();
                        return View("ContactIndex", contactArticle);
                    default:
                        var childCategory =
                            _article.GetListByType(articleType.ID, string.Empty,
                                ((page ?? 1) - 1) * ConfigWeb.ListNewsInCategory,
                                ConfigWeb.ListNewsInCategory);
                        childCategory.Type = articleType;
                        return View("ChildCategoryIndex", childCategory);
                }
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                return Redirect("/not-found");
            }
        }

        public ActionResult Featured()
        {
            var group =
                _article.GetGroupById(GetGroupIdByLanguage((int)StaticEnum.ArticleGroup.FeaturedNews));
            ViewBag.ArticleGroup = group;
            ViewData.Model = _article.GetListByGroup(group.ID, 0, ConfigWeb.FeaturedNews);
            return View();
        }

        //TuNT    15/04/2017  fix group Introduction
        public ActionResult Introduction()
        {
            ViewData.Model = _article.GetItemById(ConfigWeb.Introduction);
            return View();
        }
        //TuNT    15/04/2017  fix group Introduction      
        public ActionResult IntroductionEn()
        {
            ViewData.Model = _article.GetItemById(ConfigWeb.Introduction);
            return View();
        }

        public ActionResult NewsCategories()
        {
            var cates = _articleType.GetListByParent(new[] { GetArticleTypeIdByLanguage((int)StaticEnum.ArticleType.News) });

            foreach (var cate in cates)
            {
                var cateModel = _article.GetListByType(cate.ID, string.Empty, 0, ConfigWeb.ListNewsInRootCate);
                cate.Articles = cateModel.List;
            }

            ViewData.Model = cates;
            return View();
        }

        public ActionResult CommonDetail(string category, string article)
        {
            try
            {
                var articleItem = _article.GetItemByNameAscii(article);

                if (articleItem.LanguageId != LanguageId)
                {
                    var language = _languageRepository.GetItemById(articleItem.LanguageId.Value);
                    return Redirect("/" + language.Code + "/" + category + "/" + article);
                }

                var articleType = _articleType.GetItemByNameAscii(category);

                ViewData.Model = articleItem;
                ViewBag.ArticleType = articleType;
                switch (
                    !articleType.RootId.HasValue
                        ? articleType.ID
                        : _articleType.GetItemById(articleType.RootId.Value).ID)
                {
                    case (int)StaticEnum.ArticleType.Introduce:

                        var otherCates = _articleType.GetListByParent(new[] { articleType.ID });

                        ViewBag.OtherCategories = otherCates;
                        ViewBag.OtherArticles = _article.GetOtherArticle(articleType.ID, 0, 0, 10);
                        return View("IntroduceIndex");
                    //case (int)StaticEnum.ArticleType.Service:
                    //    return View("ServiceDetail");
                    case (int)StaticEnum.ArticleType.Project:
                    case (int)StaticEnum.ProjectChildCate.OnGoing:
                    case (int)StaticEnum.ProjectChildCate.Completed:
                        ViewBag.OtherProjects = _article.GetOtherArticle(articleType.ID, articleItem.ID, 0,
                            ConfigWeb.ListOtherProjects);
                        return View("ProjectDetail");
                    default:
                        ViewBag.OtherProjects = _article.GetOtherArticle(articleType.ID, articleItem.ID, 0,
                            ConfigWeb.ListOtherProjects);
                        return View("NewsDetail");
                }
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                return Redirect("/not-found");
            }
        }

        public ActionResult AbilityDetail(int type, int? page)
        {
            try
            {
                var typeItem = _articleType.GetItemById(type);
                int defaultLangTypeId = !typeItem.RootId.HasValue
                    ? typeItem.ID
                    : _article.GetItemById(typeItem.RootId.Value).ID;
                ViewBag.Page = page ?? 1;
                ViewBag.Type = typeItem;

                switch (defaultLangTypeId)
                {
                    case (int)StaticEnum.AbilityChildCate.Equipment:
                        ViewData.Model = _article.GetListByType(type, string.Empty,
                            ((page ?? 1) - 1) * ConfigWeb.ListNewsInCate, ConfigWeb.ListNewsInCate);
                        return View("AbilityEquipmentDetail");
                    case (int)StaticEnum.AbilityChildCate.Certificate:
                        ViewData.Model = _article.GetListByType(type, string.Empty,
                            ((page ?? 1) - 1) * ConfigWeb.ListCertificate, ConfigWeb.ListCertificate);
                        return View("AbilityCertificatesDetail");
                    default:
                        var result = new List<ListArticleModel>
                        {
                            _article.GetListByGroup(GetGroupIdByLanguage((int) StaticEnum.ArticleGroup.Director), 0, 10),
                            _article.GetListByGroup(GetGroupIdByLanguage((int) StaticEnum.ArticleGroup.Administrator), 0,
                                10),
                            _article.GetListByGroup(GetGroupIdByLanguage((int) StaticEnum.ArticleGroup.Contruction), 0,
                                10)
                        };

                        ViewData.Model = result;
                        return View();
                }

            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                return Redirect("/not-found");
            }
        }

        public ActionResult OtherArticlesInCate(int currentCate, int currentArticleId, string parentName)
        {
            try
            {
                ViewData.Model = _article.GetOtherArticleService(GetArticleTypeIdByLanguage(currentCate), currentArticleId, 0,
                    10);
                ViewBag.ParentName = parentName;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        public ActionResult FeaturedEvents()
        {
            try
            {
                var featuredEvent =
                   _article.GetGroupById(GetGroupIdByLanguage((int)StaticEnum.ArticleGroup.FeaturedNews));

                featuredEvent.ListArticle = _article.GetListByGroup(GetGroupIdByLanguage((int)StaticEnum.ArticleGroup.FeaturedNews), 0,
                    ConfigWeb.FeaturedEventInLeft).List;
                ViewData.Model = featuredEvent;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        public ActionResult TypicalFaces()
        {
            try
            {
                var typicalFace =
                     _article.GetGroupById(GetGroupIdByLanguage((int)StaticEnum.ArticleGroup.TypicalFace));

                typicalFace.ListArticle = _article.GetListByGroup(GetGroupIdByLanguage((int)StaticEnum.ArticleGroup.TypicalFace), 0,
                    ConfigWeb.TypicalFaceInLeft).List;

                ViewData.Model = typicalFace;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        public ActionResult OtherCategoriesInCate(int currentCate, string parentName)
        {
            try
            {
                var category = _articleType.GetItemById(GetArticleTypeIdByLanguage(currentCate));
                var otherCates = _articleType.GetListByParent(new[] { category.Parent ?? 0 });

                ViewBag.ParentName = parentName;
                ViewData.Model = otherCates.Where(c => c.ID != category.ID).OrderBy(c => c.Number);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        public ActionResult RightBarInNews(int typeId)
        {
            try
            {
                var currentType = _articleType.GetItemById(GetArticleTypeIdByLanguage(typeId));
                ViewData.Model = _article.GetTopByType(typeId, 0, ConfigWeb.OtherNewsHomepageInCategory);
                ViewBag.ListOtherCategory = _articleType.GetListByParent(new[] { currentType.Parent ?? currentType.ID });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        public ActionResult RightBarOtherNews(int typeId)
        {
            try
            {
                var currentType = _articleType.GetItemById(GetArticleTypeIdByLanguage(typeId));
                ViewData.Model = _article.GetTopByType(typeId, 0, ConfigWeb.OtherNewsHomepageInCategory);
                ViewBag.ListOtherCategory = _articleType.GetListByParent(new[] { currentType.Parent ?? currentType.ID });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        public ActionResult FooterNews(int typeId)
        {
            try
            {
                var featuredEvent =
                   _article.GetGroupById(GetGroupIdByLanguage((int)StaticEnum.ArticleGroup.FeaturedNews));

                featuredEvent.ListArticle = _article.GetListByGroup(GetGroupIdByLanguage((int)StaticEnum.ArticleGroup.FeaturedNews), 0,
                    ConfigWeb.FeaturedEventInLeft).List;
                ViewData.Model = featuredEvent;
                ViewBag.ListOtherCategory = _articleType.GetListByParent(new[] { typeId });
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        public ActionResult Search(string s, int? page)
        {
            try
            {
                ViewBag.Keywords = s;
                ViewBag.Page = page ?? 1;
                ViewData.Model = _article.ArticleSeach(new[] { s }, new[] { string.Empty }, ((page ?? 1) - 1) * ConfigWeb.ListNewsInCategory, ConfigWeb.ListNewsInCategory, true, false, LanguageId);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        public int GetGroupIdByLanguage(int groupId)
        {
            try
            {
                return LanguageId == (int)StaticEnum.LanguageId.Vietnamese
                    ? groupId
                    : _article.GetGroupInOtherLang(groupId, LanguageId).ID;
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }


        public int GetArticleTypeIdByLanguage(int groupId)
        {
            try
            {
                return LanguageId == (int)StaticEnum.LanguageId.Vietnamese
                    ? groupId
                    : _articleType.GetTypeInOtherLang(groupId, LanguageId).ID;
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }

        public int GetLayoutGroupIdByLanguage(int groupId)
        {
            try
            {
                return LanguageId == (int)StaticEnum.LanguageId.Vietnamese
                    ? groupId
                    : _layoutContent.GetGroupInOtherLang(groupId, LanguageId).ID;
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }
        public ActionResult Vechungtoi()
        {
            //var Article = _article.GetItemByNameAscii ("ve-chung-toi");
            var Article = _article.GetItemByNameAscii("ve-chung-toi");
            return View(Article);
        }
        public ActionResult Ungdung()
        {
            //var Article = _article.GetItemByNameAscii ("ve-chung-toi");
            var Article = _article.GetItemByNameAscii("ung-dung");
            return View(Article);
        }


    }
}