using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web365Business.Front_End.IRepository;
using Web365Domain;
using Web365Utility;

namespace Web365.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILayoutContentRepositoryFE _layoutContent;
        private readonly IArticleRepositoryFE _article;
        private readonly IArticleTypeRepositoryFE _articleType;
        private readonly ISettingRepositoryFE _settingDafeRepository;
        public HomeController(ILayoutContentRepositoryFE layoutContent, IArticleRepositoryFE article, IArticleTypeRepositoryFE articleType, ISettingRepositoryFE settingDafeRepository)
        {
            _layoutContent = layoutContent;
            _article = article;
            _articleType = articleType;
            _settingDafeRepository = settingDafeRepository;
        }
        [HttpGet]
        public ActionResult CheckIsHaveLanding()
        {
            var objIsHaveLanding = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.IsHaveLanding, LanguageId);
            if(objIsHaveLanding != null && objIsHaveLanding.IsShow.HasValue)
            {
                return Json(new
                {
                    error = false,
                    message = objIsHaveLanding
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    error = false,
                    message = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ImagesSlider()
        {
            try
            {
                ViewData.Model = _article.GetListByType(GetArticleTypeIdByLanguage((int)StaticEnum.ArticleType.Banner), string.Empty, 0, 10);

            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View();
        }
      
        public ActionResult About()
        {
            try
            {
                ViewData.Model = _layoutContent.GetListByGroupId(GetLayoutGroupIdByLanguage((int)StaticEnum.LayoutContent.OverviewContent));
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View();
        }

        public ActionResult MessageAndPartner()
        {
            try
            {
                ViewData.Model = _layoutContent.GetListByGroupId(GetLayoutGroupIdByLanguage((int)StaticEnum.LayoutContent.Partner));
                ViewData["Message"] = _layoutContent.GetListByGroupId(GetLayoutGroupIdByLanguage((int)StaticEnum.LayoutContent.Message));

            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View();
        }


        public ActionResult Project()
        {
            try
            { 
                ////1.Lấy danh sách các loại dự án
                //int[] parent = new int[] { (int)StaticEnum.ArticleType.Project };
                //ViewBag.ltsArticleProjectType = _articleType.GetListByParent(parent);
                ////2.Lấy danh sách dự án
                //var listArticleModel =
                //    _article.GetListByType((int)StaticEnum.ArticleType.Project, string.Empty, 0, int.MaxValue); 
                //ViewBag.ltsArticle = listArticleModel.List;

                int groupId = GetArticleGroupIdByLanguage((int)StaticEnum.ArticleGroup.FeaturedProject);
                ViewData.Model = _article.GetListByGroup(groupId, 0, ConfigWeb.FeaturedProject);
                ViewBag.ArticleGroup = _article.GetGroupById(groupId);
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View();
        }
         

        public ActionResult GetListServiceOfType(int typeId)
        {
            try
            {
                ViewData.Model = _article.GetListByType(GetArticleTypeIdByLanguage(typeId), string.Empty, 0, ConfigWeb.FeaturedService);
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View();
        }

        public ActionResult Statistical()
        {
            try
            {
                ViewData.Model = _layoutContent.GetListByGroupId(GetLayoutGroupIdByLanguage((int)StaticEnum.LayoutContent.Statistical));
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View();
        }

        public ActionResult Feedback()
        {
            try
            {
                ViewData.Model = _layoutContent.GetListByGroupId(GetLayoutGroupIdByLanguage((int)StaticEnum.LayoutContent.Feedback));
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View();
        }

        public ActionResult MessageAndPartnerContainer()
        {
            return View();
        }

        public ActionResult Message()
        {
            try
            {
                var group =
                    _layoutContent.GetListByGroupId(GetLayoutGroupIdByLanguage((int)StaticEnum.LayoutContent.Message));
                ViewData.Model = group.LayoutContents.FirstOrDefault();
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View();
        }
        public ActionResult ServiceQuality()
        {
            try
            {
                //ViewData.Model = _layoutContent.GetListByGroupId(GetLayoutGroupIdByLanguage((int)StaticEnum.LayoutContent.Partner));
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View();
        }
        //Ban lãnh đạo
        public ActionResult Leadership()
        {
            try
            {
                var listArticleModel =
                    _article.GetListByType(ConfigWeb.ArticleTypeLeadership, string.Empty, 0, int.MaxValue);
                ViewBag.ltsArticle = listArticleModel.List;
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View();
        }
        //Những thiết kế nổi bật
        public ActionResult OutstandingDesign()
        {
            try
            {
                var listArticleModel =
                    _article.GetListByType(ConfigWeb.ArticleTypeOutstandingDesigns, string.Empty, 0, 2);
                ViewBag.ltsArticle = listArticleModel.List;
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View();
        }
        //Dịch vụ
        public ActionResult Service()
        {
            try
            {
                var listArticleModel =
                    _article.GetListByType(ConfigWeb.OurService, string.Empty, 0, int.MaxValue);
                ViewBag.ltsArticle = listArticleModel.List;
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View();
        }       
        // tinh năng vượt trội
        public ActionResult tinhnangdinhvivaphanloaivuottroi()
        {
            var tinhnangdinhvivaphanloaivuottroi = new ArticleItem();
            try
            {
                //dong nay lay ra ID 63 trong bang tblArtile.
                tinhnangdinhvivaphanloaivuottroi = _article.GetItemById(ConfigWeb.tinhnangdinhvivaphanloaivuottroi);
                //ViewBag.tinhnangdinhvivaphanloaivuottroi = tinhnangdinhvivaphanloaivuottroi;
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View(tinhnangdinhvivaphanloaivuottroi);
        }
        
        // tải app
        public ActionResult taiungdung()
        {
            var taiungdung = new ArticleItem();
            try
            {
                //dong nay lay ra ID 67 trong bang tblArtile.
                taiungdung = _article.GetItemById(ConfigWeb.taiungdung);
                //ViewBag.taiungdung = taiungdung;
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View(taiungdung);
        }
        //Manhinhapp
        public ActionResult Manhinhapp()
        {
            var Manhinhapp = new ArticleItem();
            try
            {
                //dong nay lay ra ID 67 trong bang tblArtile.
                Manhinhapp = _article.GetItemById(ConfigWeb.Manhinhapp);
                //ViewBag.taiungdung = taiungdung;
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View(Manhinhapp);
        }
        // tải ve chung toi
        public ActionResult vechungtoi()
        {
            var vechungtoi = new ArticleItem();
            try
            {
                //dong nay lay ra ID 75 trong bang tblArtile.
                vechungtoi = _article.GetItemById(ConfigWeb.vechungtoi);
                //ViewBag.taiungdung = taiungdung;
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View(vechungtoi);
        }
        // Video Giới thiệu app
        public ActionResult Gioithieuapp()
        {
            var Gioithieuapp = new ArticleItem();
            try
            {
                //dong nay lay ra ID 75 trong bang tblArtile.
                Gioithieuapp = _article.GetItemById(ConfigWeb.Gioithieuapp);
                //ViewBag.taiungdung = taiungdung;
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View(Gioithieuapp);
        }

        //Đối tác
        public ActionResult Partner()
        {
            try
            {
                ViewData.Model = _layoutContent.GetListByGroupId(GetLayoutGroupIdByLanguage((int)StaticEnum.LayoutContent.Partner));
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View();
        }
        public ActionResult Capacity()
        {
            try
            {
                var listArticleModel =
                    _article.GetListByType(ConfigWeb.Capacity, string.Empty, 0, 4);
                ViewBag.ltsArticle = listArticleModel.List;
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View();
        }
        public ActionResult OpenLetter()
        {
            try
            {
                var listArticleModel =
                    _article.GetListByType(ConfigWeb.OpenLetter, string.Empty, 0, 1);
                ViewBag.ltsArticle = listArticleModel.List;
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View();
        }

        public ActionResult News()
        {
            try
            {
                List<ArticleItem> ArticleItems = new List<ArticleItem>();
                var newList = _article.Get;
                var topHighlightNews = _article.GetListByGroup(GetArticleGroupIdByLanguage(ConfigWeb.AchievementHighlight), 0, ConfigWeb.TopHighlightInNewsEvents);
                var topOtherNews = _article.GetTopByType(GetArticleTypeIdByLanguage(ConfigWeb.OtherNewsRightBarInCategory), 0, ConfigWeb.TopOtherInNewsEvents);
                if (topHighlightNews != null && topHighlightNews.List != null && topHighlightNews.List.Count > 0)
                {
                    ArticleItems.AddRange(topHighlightNews.List);
                }
                if (topOtherNews != null && topOtherNews.Count > 0)
                {
                    ArticleItems.AddRange(topOtherNews);
                }
                Web365Models.ListArticleModel list = new Web365Models.ListArticleModel();
                list.List = ArticleItems;
                list.total = ArticleItems.Count;
                //ViewData.Model = _article.GetListByType(GetArticleTypeIdByLanguage((int)StaticEnum.ArticleType.News), string.Empty, 0, ConfigWeb.OtherNewsHomepageInCategory);
                ViewData.Model = list;
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View();
        }

        public ActionResult OtherNewsHomepage()
        {
            try
            {
                ViewData.Model = _article.GetTopByType(GetArticleTypeIdByLanguage(ConfigWeb.OtherNewsRightBarInCategory), 0, ConfigWeb.OtherNewsRightBarInCategoryQuantity);
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View();
        }

        public ActionResult AchievementHighlight()
        {
            try
            {
                ViewData.Model = _article.GetListByGroup(GetArticleGroupIdByLanguage(ConfigWeb.AchievementHighlight), 0, ConfigWeb.AchievementHighlightQuantity);
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Facebook = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.Facebook, LanguageId);
            ViewBag.Twitter = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.Twitter, LanguageId);
            ViewBag.Youtube = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.Youtube, LanguageId);
            ViewBag.Instagram = _settingDafeRepository.GetById((int)StaticEnum.SettingInfo.Instagram, LanguageId);

            var contactArticle =
                           _article.GetListByType(GetArticleTypeIdByLanguage((int)StaticEnum.ArticleType.Contact),
                               string.Empty, 0, 1).List.FirstOrDefault();
            ViewData.Model = contactArticle;

            return View();
        }

        public int GetArticleGroupIdByLanguage(int groupId)
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
        

    }
}