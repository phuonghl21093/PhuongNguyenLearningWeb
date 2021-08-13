using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Domain;
using Web365Models;
using Web365Utility;

namespace Web365DA.RDBMS.Front_End.IRepository
{
    public interface IArticleDAFERepository
    {
        ArticleItem GetItemByNameAscii(string nameAscii, bool isShow = true, bool isDeleted = false);
        ArticleItem GetItemById(int id);
        List<ArticleItem> GetTopByType(int type, int skip, int top, bool isShow = true, bool isDeleted = false);
        ListArticleModel GetListByType(int typeID, string nameAscii, int currentRecord, int pageSize, bool isShow = true, bool isDeleted = false);
        ListArticleModel GetListByArrType(int[] arrType, int currentRecord, int pageSize, bool isShow = true, bool isDeleted = false);
        ListArticleModel ArticleSeach(string[] keyword, string[] keywordAscii, int currentRecord, int top, bool isShow = true, bool isDeleted = false, int languageId = (int)StaticEnum.LanguageId.Vietnamese);
        ListArticleModel GetListByGroup(int groupId, int skip, int top, bool isShow = true, bool isDeleted = false);
        ListArticleModel GetListByTypeAndDetail(int typeId, int skip, int top, bool isShow = true, bool isDeleted = false);
        List<ArticleItem> GetOtherArticle(int type, int articleId, int skip, int top, bool isShow = true, bool isDeleted = false);
        List<ArticleItem> GetOtherArticleService(int type, int articleId, int skip, int top, bool isShow = true, bool isDeleted = false);
        ArticleGroupItem GetGroupById(int groupId);
        ArticleGroupItem GetGroupInOtherLang(int groupId, int languageId);
        ArticleItem GetFirstArticleItem(int type);
        ArticleItem GetSameInOtherLangByNameascii(string nameascii, int languageId);
    }
}
