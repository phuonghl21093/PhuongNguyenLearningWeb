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
    public class ArticleRepositoryFE : BaseFE, IArticleRepositoryFE
    {
        private readonly IArticleDAFERepository articleDAFERepository;

        public object Get => throw new NotImplementedException();

        public ArticleRepositoryFE(IArticleDAFERepository _articleDAFERepository)
        {
            articleDAFERepository = _articleDAFERepository;
        }

        public ArticleItem GetItemById(int id)
        {
            var key = string.Format("ArticleRepositoryFE{0}{1}", "GetItemById", id);

            var item = new ArticleItem();

            var isDataCache = TryGetCache<ArticleItem>(out item, key);

            if (!isDataCache)
            {
                item = articleDAFERepository.GetItemById(id);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public ArticleItem GetItemByNameAscii(string nameAscii, bool isShow = true, bool isDeleted = false)
        {
            var key = string.Format("ArticleRepositoryFE{0}{1}{2}{3}", "GetItemByNameAscii", nameAscii, isShow, isDeleted);

            var item = new ArticleItem();

            var isDataCache = TryGetCache<ArticleItem>(out item, key);

            if (!isDataCache)
            {
                item = articleDAFERepository.GetItemByNameAscii(nameAscii);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public ListArticleModel GetListByType(int typeID, string nameAscii, int currentRecord, int pageSize, bool isShow = true, bool isDeleted = false)
        {
            var key = string.Format("ArticleRepositoryFE{0}{1}{2}{3}{4}{5}{6}", "GetListByType", typeID, nameAscii, currentRecord, pageSize, isShow, isDeleted);

            var item = new ListArticleModel();

            var isDataCache = TryGetCache<ListArticleModel>(out item, key);

            if (!isDataCache)
            {
                item = articleDAFERepository.GetListByType(typeID, nameAscii, currentRecord, pageSize, isShow, isDeleted);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public ListArticleModel GetListByArrType(int[] arrType, int currentRecord, int pageSize, bool isShow = true, bool isDeleted = false)
        {
            var key = string.Format("ArticleRepositoryFE{0}{1}{2}{3}{4}{5}", "GetListByArrType", string.Join("_", arrType), currentRecord, pageSize, isShow, isDeleted);

            var item = new ListArticleModel();

            var isDataCache = TryGetCache<ListArticleModel>(out item, key);

            if (!isDataCache)
            {
                item = articleDAFERepository.GetListByArrType(arrType, currentRecord, pageSize, isShow, isDeleted);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public List<ArticleItem> GetTopByType(int type, int skip, int top, bool isShow = true, bool isDeleted = false)
        {
            var key = string.Format("ArticleRepositoryFE{0}{1}{2}{3}{4}{5}", "GetTopByType", type, skip, top, isShow, isDeleted);

            var item = new List<ArticleItem>();

            var isDataCache = TryGetCache<List<ArticleItem>>(out item, key);

            if (!isDataCache)
            {
                item = articleDAFERepository.GetTopByType(type, skip, top, isShow, isDeleted);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public List<ArticleItem> GetOtherArticle(int type, int articleId, int skip, int top, bool isShow = true, bool isDeleted = false)
        {
            var key = string.Format("ArticleRepositoryFE{0}{1}{2}{3}{4}{5}{6}", "GetOtherArticle", type, articleId, skip, top, isShow, isDeleted);

            var item = new List<ArticleItem>();

            var isDataCache = TryGetCache<List<ArticleItem>>(out item, key);

            if (!isDataCache)
            {
                item = articleDAFERepository.GetOtherArticle(type, articleId, skip, top, isShow, isDeleted);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }
        public List<ArticleItem> GetOtherArticleService(int type, int articleId, int skip, int top, bool isShow = true, bool isDeleted = false)
        {
            var key = string.Format("ArticleRepositoryFE{0}{1}{2}{3}{4}{5}{6}", "GetOtherArticleService", type, articleId, skip, top, isShow, isDeleted);

            var item = new List<ArticleItem>();

            var isDataCache = TryGetCache<List<ArticleItem>>(out item, key);

            if (!isDataCache)
            {
                item = articleDAFERepository.GetOtherArticleService(type, articleId, skip, top, isShow, isDeleted);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public ArticleGroupItem GetGroupById(int groupId)
        {
            var key = string.Format("ArticleRepositoryFE{0}{1}", "GetGroupById", groupId);

            var item = new ArticleGroupItem();

            var isDataCache = TryGetCache<ArticleGroupItem>(out item, key);

            if (!isDataCache)
            {
                item = articleDAFERepository.GetGroupById(groupId);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public ArticleGroupItem GetGroupInOtherLang(int groupId, int languageId)
        {
            var key = string.Format("ArticleRepositoryFE{0}{1}{2}", "GetGroupInOtherLang", groupId, languageId);

            var item = new ArticleGroupItem();

            var isDataCache = TryGetCache<ArticleGroupItem>(out item, key);

            if (!isDataCache)
            {
                item = articleDAFERepository.GetGroupInOtherLang(groupId, languageId);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public ArticleItem GetFirstArticleItem(int type)
        {
            var key = string.Format("ArticleRepositoryFE{0}{1}", "GetFirstArticleItem", type);

            var item = new ArticleItem();

            var isDataCache = TryGetCache<ArticleItem>(out item, key);

            if (!isDataCache)
            {
                item = articleDAFERepository.GetFirstArticleItem(type);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public ArticleItem GetSameInOtherLangByNameascii(string nameascii, int languageId)
        {
            var key = string.Format("ArticleRepositoryFE{0}{1}{2}", "GetSameInOtherLangByNameascii", nameascii, languageId);

            var item = new ArticleItem();

            var isDataCache = TryGetCache<ArticleItem>(out item, key);

            if (!isDataCache)
            {
                item = articleDAFERepository.GetSameInOtherLangByNameascii(nameascii, languageId);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public ListArticleModel ArticleSeach(string[] keyword, string[] keywordAscii, int currentRecord, int top, bool isShow = true, bool isDeleted = false, int languageId = (int)StaticEnum.LanguageId.Vietnamese)
        {
            var key = string.Format("ArticleRepositoryFE{0}{1}{2}{3}{4}{5}{6}{7}", "ArticleSeach", string.Join("_", keyword), string.Join("_", keywordAscii), currentRecord, top, isShow, isDeleted, languageId);

            var item = new ListArticleModel();

            var isDataCache = TryGetCache<ListArticleModel>(out item, key);

            if (!isDataCache)
            {
                item = articleDAFERepository.ArticleSeach(keyword, keywordAscii, currentRecord, top, isShow, isDeleted, languageId);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public ListArticleModel GetListByGroup(int groupId, int skip, int top, bool isShow = true, bool isDeleted = false)
        {
            var key = string.Format("ArticleRepositoryFE{0}{1}{2}{3}{4}{5}", "ArticleSeach", groupId, skip, top, isShow, isDeleted);

            var item = new ListArticleModel();

            var isDataCache = TryGetCache<ListArticleModel>(out item, key);

            if (!isDataCache)
            {
                item = articleDAFERepository.GetListByGroup(groupId, skip, top, isShow, isDeleted);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }

        public ListArticleModel GetListByTypeAndDetail(int typeId, int skip, int top, bool isShow = true, bool isDeleted = false)
        {
            var key = string.Format("ArticleRepositoryFE{0}{1}{2}{3}{4}{5}", "GetListByTypeAndDetail", typeId, skip, top, isShow, isDeleted);

            var item = new ListArticleModel();

            var isDataCache = TryGetCache<ListArticleModel>(out item, key);

            if (!isDataCache)
            {
                item = articleDAFERepository.GetListByTypeAndDetail(typeId, skip, top, isShow, isDeleted);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }
    }
}
