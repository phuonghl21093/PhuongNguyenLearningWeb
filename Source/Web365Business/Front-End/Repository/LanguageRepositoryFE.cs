using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Business.Front_End.IRepository;
using Web365DA.RDBMS.Front_End.IRepository;
using Web365Domain;
using Web365Domain.Language;
using Web365Domain.Other;
using Web365Utility;

namespace Web365Business.Front_End.Repository
{
    public class LanguageRepositoryFE : BaseFE, ILanguageRepositoryFE
    {
        private readonly ILanguageDAFERepository languageDAFERepository;

        public LanguageRepositoryFE(ILanguageDAFERepository languageDafeRepository)
        {
            languageDAFERepository = languageDafeRepository;
        }

        public LanguageItem GetItemByCode(string code)
        {
            var key = string.Format("LanguageRepositoryFE{0}{1}", "GetItemByCode", code);

            LanguageItem item;

            var isDataCache = TryGetCache<LanguageItem>(out item, key);

            if (!isDataCache)
            {
                item = languageDAFERepository.GetItemByCode(code);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public List<LanguageItem> GetAll()
        {
            var key = string.Format("LanguageRepositoryFE{0}", "GetAll");

            List<LanguageItem> item;

            var isDataCache = TryGetCache<List<LanguageItem>>(out item, key);

            if (!isDataCache)
            {
                item = languageDAFERepository.GetAll();
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public LanguageItem GetItemById(int id)
        {
            var key = string.Format("LanguageRepositoryFE{0}{1}", "GetItemById", id);

            LanguageItem item;

            var isDataCache = TryGetCache<LanguageItem>(out item, key);

            if (!isDataCache)
            {
                item = languageDAFERepository.GetItemById(id);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }
    }
}
