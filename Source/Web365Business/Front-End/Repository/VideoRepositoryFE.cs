using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Business.Front_End.IRepository;
using Web365DA.RDBMS.Front_End.IRepository;
using Web365Domain;
using Web365Models;
using Web365Utility;

namespace Web365Business.Front_End.Repository
{
    public class VideoRepositoryFE : BaseFE, IVideoRepositoryFE
    {
        private readonly IVideoDAFERepository videoDAFERepository;

        public VideoRepositoryFE(IVideoDAFERepository _videoDAFERepository)
        {
            videoDAFERepository = _videoDAFERepository;
        }

        public VideoTypeItem GetSameTypeFromDefault(int id, int languageId)
        {
            var key = string.Format("VideoRepositoryFE{0}{1}{2}", "GetSameTypeFromDefault", id, languageId);

            var item = new VideoTypeItem();

            var isDataCache = TryGetCache<VideoTypeItem>(out item, key);

            if (!isDataCache)
            {
                item = videoDAFERepository.GetSameTypeFromDefault(id, languageId);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public List<VideoTypeItem> GetListTypeByParent(int id)
        {
            var key = string.Format("VideoRepositoryFE{0}{1}", "GetListTypeByParent", id);

            var item = new List<VideoTypeItem>();

            var isDataCache = TryGetCache<List<VideoTypeItem>>(out item, key);

            if (!isDataCache)
            {
                item = videoDAFERepository.GetListTypeByParent(id);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public VideoModel GetListByType(int id, string ascii, int skip, int top)
        {
            var key = string.Format("VideoRepositoryFE{0}{1}{2}{3}", "GetListByType", id, ascii, skip, top);

            var item = new VideoModel();

            var isDataCache = TryGetCache<VideoModel>(out item, key);

            if (!isDataCache)
            {
                item = videoDAFERepository.GetListByType(id, ascii, skip, top);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }
    }
}
