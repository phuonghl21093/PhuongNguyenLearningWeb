using System;
using System.Collections.Generic;
using Web365Domain;
using Web365Utility;

namespace Web365DA.RDBMS.Front_End.IRepository
{
    public interface IArticleTypeDAFERepository
    {
        ArticleTypeItem GetItemById(int id);
        ArticleTypeItem GetItemByNameAscii(string nameAscii, bool isShow = true, bool isDeleted = false);
        List<ArticleTypeItem> GetListByGroup(int groupId, bool isShow = true, bool isDeleted = false);
        List<ArticleTypeItem> GetListByParent(int[] parent, bool isShow = true, bool isDeleted = false);
        List<ArticleTypeItem> GetListByParent(int[] parent, int skip, int top, bool isShow = true, bool isDeleted = false);
        List<ArticleTypeItem> GetListByParentAscii(string parentAscii, bool isShow = true, bool isDeleted = false);
        List<ArticleTypeItem> GetAllChildOfTypeWithGroup(int groupId);
        List<ArticleTypeItem> GetAllChildOfTypeAscii(string[] parent, bool isShow = true, bool isDeleted = false);
        List<ArticleTypeItem> GetAllChildOfType(int[] parent, bool isShow = true, bool isDeleted = false);
        ArticleTypeItem GetTypeInOtherLang(int typeId, int languageId);
        ArticleTypeItem GetTypeInOtherLangByNameascii(string type, int languageId);
    }
}
