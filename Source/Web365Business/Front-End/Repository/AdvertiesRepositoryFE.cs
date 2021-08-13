using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Business.Front_End.IRepository;
using Web365DA.RDBMS.Front_End.IRepository;
using Web365Domain;
using Web365Utility;

namespace Web365Business.Front_End.Repository
{
    public class AdvertiesRepositoryFE : BaseFE, IAdvertiesRepositoryFE
    {
        private readonly IAdvertiesDAFERepository advertiesDAFERepository;

        public AdvertiesRepositoryFE(IAdvertiesDAFERepository _advertiesDAFERepository)
        {
            advertiesDAFERepository = _advertiesDAFERepository;
        }

        public AdvertiesItem GetItemById(int id)
        {
            var key = string.Format("AdvertiesRepositoryFE{0}{1}", "GetItemById", id);

            var item = new AdvertiesItem();

            var isDataCache = TryGetCache<AdvertiesItem>(out item, key);

            if (!isDataCache)
            {
                item = advertiesDAFERepository.GetItemById(id);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public AdvertiesItem GetItemByLink(string link)
        {
            var key = string.Format("AdvertiesRepositoryFE{0}{1}", "GetItemByLink", link);

            var item = new AdvertiesItem();

            var isDataCache = TryGetCache<AdvertiesItem>(out item, key);

            if (!isDataCache)
            {
                item = advertiesDAFERepository.GetItemByLink(link);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }
    }
}
