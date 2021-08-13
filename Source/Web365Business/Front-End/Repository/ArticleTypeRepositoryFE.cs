using System;
using System.Collections.Generic;
using System.Data;
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
    public class ArticleTypeRepositoryFE : BaseFE, IArticleTypeRepositoryFE
    {
        private readonly IArticleTypeDAFERepository articleTypeDAFERepository;

        public ArticleTypeRepositoryFE(IArticleTypeDAFERepository _articleTypeDAFERepository)
        {
            articleTypeDAFERepository = _articleTypeDAFERepository;
        }

        public ArticleTypeItem GetItemById(int id)
        {
            var key = string.Format("ArticleTypeRepositoryFE{0}{1}", "GetItemById", id);

            var item = new ArticleTypeItem();

            var isDataCache = TryGetCache<ArticleTypeItem>(out item, key);

            if (!isDataCache)
            {
                item = articleTypeDAFERepository.GetItemById(id);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public ArticleTypeItem GetItemByNameAscii(string nameAscii, bool isShow = true, bool isDeleted = false)
        {
            var key = string.Format("ArticleTypeRepositoryFE{0}{1}{2}{3}", "GetItemByNameAscii", nameAscii, isShow, isDeleted);

            var item = new ArticleTypeItem();

            var isDataCache = TryGetCache<ArticleTypeItem>(out item, key);

            if (!isDataCache)
            {
                item = articleTypeDAFERepository.GetItemByNameAscii(nameAscii, isShow, isDeleted);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public List<ArticleTypeItem> GetListByGroup(int groupId, bool isShow = true, bool isDeleted = false)
        {
            var key = string.Format("ArticleTypeRepositoryFE{0}{1}{2}{3}", "GetListByGroup", groupId, isShow, isDeleted);

            var item = new List<ArticleTypeItem>();

            var isDataCache = TryGetCache<List<ArticleTypeItem>>(out item, key);

            if (!isDataCache)
            {
                item = articleTypeDAFERepository.GetListByGroup(groupId, isShow, isDeleted);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public List<ArticleTypeItem> GetListByParent(int[] parent, bool isShow = true, bool isDeleted = false)
        {
            var key = string.Format("ArticleTypeRepositoryFE{0}{1}{2}{3}", "GetListByParent", string.Join("_", parent), isShow, isDeleted);

            var item = new List<ArticleTypeItem>();

            var isDataCache = TryGetCache<List<ArticleTypeItem>>(out item, key);

            if (!isDataCache)
            {
                item = articleTypeDAFERepository.GetListByParent(parent, isShow, isDeleted);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public List<ArticleTypeItem> GetListByParent(int[] parent, int skip, int top, bool isShow = true, bool isDeleted = false)
        {
            var key = string.Format("ArticleTypeRepositoryFE{0}{1}{2}{3}{4}{5}", "GetListByParent", string.Join("_", parent), skip, top, isShow, isDeleted);

            var item = new List<ArticleTypeItem>();

            var isDataCache = TryGetCache<List<ArticleTypeItem>>(out item, key);

            if (!isDataCache)
            {
                item = articleTypeDAFERepository.GetListByParent(parent, skip, top, isShow, isDeleted);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public List<ArticleTypeItem> GetListByParentAscii(string parentAscii, bool isShow = true, bool isDeleted = false)
        {
            var key = string.Format("ArticleTypeRepositoryFE{0}{1}{2}{3}", "GetListByParent", parentAscii, isShow, isDeleted);

            var item = new List<ArticleTypeItem>();

            var isDataCache = TryGetCache<List<ArticleTypeItem>>(out item, key);

            if (!isDataCache)
            {
                item = articleTypeDAFERepository.GetListByParentAscii(parentAscii, isShow, isDeleted);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public List<ArticleTypeItem> GetAllChildOfType(int[] parent, bool isShow = true, bool isDeleted = false)
        {
            var key = string.Format("ArticleTypeRepositoryFE{0}{1}{2}{3}", "GetAllChildOfType", string.Join("_", parent), isShow, isDeleted);

            var item = new List<ArticleTypeItem>();

            var isDataCache = TryGetCache<List<ArticleTypeItem>>(out item, key);

            if (!isDataCache)
            {
                item = articleTypeDAFERepository.GetAllChildOfType(parent, isShow, isDeleted);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public ArticleTypeItem GetTypeInOtherLang(int typeId, int languageId)
        {
            var key = string.Format("ArticleTypeRepositoryFE{0}{1}{2}", "GetTypeInOtherLang", typeId, languageId);

            var item = new ArticleTypeItem();

            var isDataCache = TryGetCache<ArticleTypeItem>(out item, key);

            if (!isDataCache)
            {
                item = articleTypeDAFERepository.GetTypeInOtherLang(typeId, languageId);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public ArticleTypeItem GetTypeInOtherLangByNameascii(string type, int languageId)
        {
            var key = string.Format("ArticleTypeRepositoryFE{0}{1}{2}", "GetTypeInOtherLangByNameascii", type, languageId);

            var item = new ArticleTypeItem();

            var isDataCache = TryGetCache<ArticleTypeItem>(out item, key);

            if (!isDataCache)
            {
                item = articleTypeDAFERepository.GetTypeInOtherLangByNameascii(type, languageId);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public List<ArticleTypeItem> GetAllChildOfTypeWithGroup(int groupId)
        {
            var key = string.Format("ArticleTypeRepositoryFE{0}{1}", "GetAllChildOfTypeWithGroup", groupId);

            var item = new List<ArticleTypeItem>();

            var isDataCache = TryGetCache<List<ArticleTypeItem>>(out item, key);

            if (!isDataCache)
            {
                item = articleTypeDAFERepository.GetAllChildOfTypeWithGroup(groupId);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public List<ArticleTypeItem> GetAllChildOfTypeAscii(string[] parent, bool isShow = true, bool isDeleted = false)
        {
            var key = string.Format("ArticleTypeRepositoryFE{0}{1}{2}{3}", "GetAllChildOfTypeAscii", string.Join("_", parent), isShow, isDeleted);

            var item = new List<ArticleTypeItem>();

            var isDataCache = TryGetCache<List<ArticleTypeItem>>(out item, key);

            if (!isDataCache)
            {
                item = articleTypeDAFERepository.GetAllChildOfTypeAscii(parent, isShow, isDeleted);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }
    }
}
