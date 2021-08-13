using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web365Business.Front_End.IRepository;
using Web365Utility;

namespace Web365.Controllers
{
    public class LibraryController : BaseController
    {
        private IFileRepositoryFE _fileRepository;
        private IVideoRepositoryFE _videoRepository;
        private IPictureRepositoryFE _pictureRepository;

        public LibraryController(IFileRepositoryFE fileRepository, IVideoRepositoryFE videoRepository, IPictureRepositoryFE pictureRepository)
        {
            _fileRepository = fileRepository;
            _videoRepository = videoRepository;
            _pictureRepository = pictureRepository;
        }

        // GET: Library
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FeaturedImage()
        {
            try
            {
                ViewData.Model = _pictureRepository.GetListByType(
                    (int)StaticEnum.FeatureLibrary.Image,
                    string.Empty,
                    0,
                    ConfigWeb.FeatureImages);
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View();
        }

        public ActionResult FeaturedVideo()
        {
            try
            {
                ViewData.Model = _videoRepository.GetListByType(
                    GetVideoTypeId((int)StaticEnum.FeatureLibrary.Video),
                    string.Empty,
                    0,
                    ConfigWeb.FeatureVideos);
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
            return View();
        }

        public ActionResult FeaturedDocument()
        {
            ViewData.Model = _fileRepository.GetListByType(GetFileTypeId((int)StaticEnum.FeatureLibrary.Document), string.Empty, 0, ConfigWeb.FeatureFiles);
            return View();
        }

        public ActionResult LibraryCategory(string libraryCate)
        {
            try
            {
                switch (libraryCate)
                {
                    case "hinh-anh":
                    case "image":
                        ViewData.Model = _pictureRepository.GetListTypeByParent(GetPictureTypeId((int)StaticEnum.Library.Image));
                        return View("PictureLib");
                    case "video":
                    case "video-en":
                        ViewData.Model = _videoRepository.GetListTypeByParent(GetVideoTypeId((int)StaticEnum.Library.Video));
                        return View("VideoLib");
                    default:
                        ViewData.Model = _fileRepository.GetListTypeByParent(GetFileTypeId((int)StaticEnum.Library.Document));
                        return View("DocumentLib");
                }
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                return Redirect("/not-found");
            }
        }

        public ActionResult PictureLib()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                return Redirect("/not-found");
            }
        }

        public ActionResult VideoLib()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                return Redirect("/not-found");
            }
        }

        public ActionResult DocumentLib()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                return Redirect("/not-found");
            }
        }

        public int GetPictureTypeId(int defaultId)
        {
            if (LanguageId == (int)StaticEnum.LanguageId.Vietnamese)
            {
                return defaultId;
            }
            var type = _pictureRepository.GetSameTypeFromDefault(defaultId, LanguageId);
            return type != null ? type.ID : 0;
        }

        public int GetVideoTypeId(int defaultId)
        {
            if (LanguageId == (int)StaticEnum.LanguageId.Vietnamese)
            {
                return defaultId;
            }
            var type = _videoRepository.GetSameTypeFromDefault(defaultId, LanguageId);
            return type != null ? type.ID : 0;
        }

        public int GetFileTypeId(int defaultId)
        {
            if (LanguageId == (int)StaticEnum.LanguageId.Vietnamese)
            {
                return defaultId;
            }
            var type = _fileRepository.GetSameTypeFromDefault(defaultId, LanguageId);
            return type != null ? type.ID : 0;
        }
    }
}