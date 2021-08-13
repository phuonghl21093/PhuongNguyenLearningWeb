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
    public class MenuRepositoryFE : BaseFE, IMenuRepositoryFE
    {
        private readonly IMenuDAFERepository menuDAFERepository;

        public MenuRepositoryFE(IMenuDAFERepository _menuDAFERepository)
        {
            menuDAFERepository = _menuDAFERepository;
        }

        public MenuItem GetByNameAscii(string name)
        {
            var key = string.Format("MenuRepositoryFE{0}{1}", "GetByNameAscii", name);

            MenuItem item;

            var isDataCache = TryGetCache<MenuItem>(out item, key);

            if (!isDataCache)
            {
                item = menuDAFERepository.GetByNameAscii(name);
            }

            SetCache(key, item, isDataCache, ConfigCache.Cache10Minute);

            return item;
        }

        public MenuItem GetMenuItemByLanguage(int id, int languageId)
        {
            var key = string.Format("MenuRepositoryFE{0}{1}{2}", "GetMenuItemByLanguage", id, languageId);

            var item = new MenuItem();

            var isDataCache = TryGetCache<MenuItem>(out item, key);

            if (!isDataCache)
            {
                item = menuDAFERepository.GetMenuItemByLanguage(id, languageId);
            }

            SetCache(key, item, isDataCache, ConfigCache.Cache10Minute);

            return item;
        }

        public List<MenuItem> GetListByParent(string parentId, bool isShow = true, bool isDeleted = false, int languageId = (int)StaticEnum.LanguageId.Vietnamese)
        {
            var key = string.Format("MenuRepositoryFE{0}{1}{2}{3}{4}", "GetListByParent", parentId, isShow, isDeleted, languageId);

            var item = new List<MenuItem>();

            var isDataCache = TryGetCache<List<MenuItem>>(out item, key);

            if (!isDataCache)
            {
                item = menuDAFERepository.GetListByParent(parentId, isShow, isDeleted, languageId);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }
    }
}
