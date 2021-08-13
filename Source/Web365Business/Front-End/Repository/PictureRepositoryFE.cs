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
    public class PictureRepositoryFE : BaseFE, IPictureRepositoryFE
    {
        private readonly IPictureDAFERepository pictureDAFERepository;

        public PictureRepositoryFE(IPictureDAFERepository _pictureDAFERepository)
        {
            pictureDAFERepository = _pictureDAFERepository;
        }

        public PictureTypeItem GetSameTypeFromDefault(int id, int languageId)
        {
            var key = string.Format("PictureRepositoryFE{0}{1}{2}", "GetSameTypeFromDefault", id, languageId);

            var item = new PictureTypeItem();

            var isDataCache = TryGetCache<PictureTypeItem>(out item, key);

            if (!isDataCache)
            {
                item = pictureDAFERepository.GetSameTypeFromDefault(id, languageId);
            }

            SetCache(key, item, isDataCache, ConfigCache.Cache10Minute);

            return item;
            
        }

        public List<PictureTypeItem> GetListTypeByParent(int id)
        {
            var key = string.Format("PictureRepositoryFE{0}{1}", "GetListTypeByParent", id);

            var item = new List<PictureTypeItem>();

            var isDataCache = TryGetCache<List<PictureTypeItem>>(out item, key);

            if (!isDataCache)
            {
                item = pictureDAFERepository.GetListTypeByParent(id);
            }

            SetCache(key, item, isDataCache, ConfigCache.Cache10Minute);

            return item;
        }

        public PictureModel GetListByType(int id, string ascii, int skip, int top)
        {
            var key = string.Format("PictureRepositoryFE{0}{1}", "GetListByType", id, ascii, skip, top);

            var item = new PictureModel();

            var isDataCache = TryGetCache<PictureModel>(out item, key);

            if (!isDataCache)
            {
                item = pictureDAFERepository.GetListByType(id, ascii, skip, top);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }
    }
}
