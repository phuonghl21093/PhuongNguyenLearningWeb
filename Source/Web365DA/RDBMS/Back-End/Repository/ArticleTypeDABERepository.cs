using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Web365Base;
using Web365DA.RDBMS.Back_End.IRepository;
using Web365Domain;
using Web365Utility;


namespace Web365DA.RDBMS.Back_End.Repository
{
    public class ArticleTypeDABERepository : BaseBE<tblTypeArticle>, IArticleTypeDABERepository
    {

        /// <summary>
        /// function get all data tblTypeArticle
        /// </summary>
        /// <returns></returns>
        public List<ArticleTypeItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            var query = from p in web365db.tblTypeArticle
                        where p.Name.ToLower().Contains(name) && p.IsDeleted == isDelete && p.ID != (int) StaticEnum.ArticleType.Banner
                select p;

            query = query.Where(p => p.LanguageId == (int)StaticEnum.LanguageId.Vietnamese);

            total = query.Count();

            query = descending ? QueryableHelper.OrderByDescending(query, propertyNameSort) : QueryableHelper.OrderBy(query, propertyNameSort);

            var result = query.Select(p => new ArticleTypeItem()
            {
                ID = p.ID,
                Name = p.Name,
                NameAscii = p.NameAscii,
                SEOTitle = p.SEOTitle,
                SEODescription = p.SEODescription,
                SEOKeyword = p.SEOKeyword,
                Number = p.Number,
                Summary = p.Summary,
                Detail = p.Detail,
                Parent = p.Parent,
                IsShow = p.IsShow,
                LanguageId = p.LanguageId,
                RootId = p.RootId,
                LanguageName = p.tblLanguage.Name
            }).Skip(currentRecord).Take(numberRecord).ToList();

            foreach (var type in result)
            {
                type.ListOtherLanguage = web365db
                    .tblTypeArticle
                    .Where(c => c.IsDeleted == false && c.RootId == type.ID)
                    .Select(c => new ArticleTypeItem()
                    {
                        ID = c.ID,
                        Name = c.Name,
                        NameAscii = c.NameAscii,
                        SEOTitle = c.SEOTitle,
                        SEODescription = c.SEODescription,
                        SEOKeyword = c.SEOKeyword,
                        Number = c.Number,
                        Summary = c.Summary,
                        Detail = c.Detail,
                        Parent = c.Parent,
                        IsShow = c.IsShow,
                        LanguageId = c.LanguageId,
                        RootId = c.RootId,
                        LanguageName = c.tblLanguage.Name
                    }).ToList();
            }

            return result;
        }

        public ArticleTypeItem GetSameArticleTypeByLanguage(int rootId, int langId)
        {
            var query = from c in web365db.tblTypeArticle
                        where (rootId == (int) StaticEnum.ArticleType.Banner ||  (c.IsDeleted == false && c.IsShow == true)) && langId == c.LanguageId && c.RootId == rootId
                        select new ArticleTypeItem()
                        {
                            ID = c.ID,
                            Name = c.Name,
                            NameAscii = c.NameAscii,
                            SEOTitle = c.SEOTitle,
                            SEODescription = c.SEODescription,
                            SEOKeyword = c.SEOKeyword,
                            Number = c.Number,
                            Summary = c.Summary,
                            Detail = c.Detail,
                            Parent = c.Parent,
                            IsShow = c.IsShow,
                            LanguageId = c.LanguageId,
                            RootId = c.RootId,
                            LanguageName = c.tblLanguage.Name
                        };

            return query.FirstOrDefault();
        }

        public List<ArticleTypeItem> GetListOfGroup(int groupId, bool isShow = true, bool isDelete = false)
        {
            var query = from p in web365db.tblGroup_TypeArticle_Map
                        where p.GroupTypeID == groupId && p.tblTypeArticle.IsShow == isShow && p.tblTypeArticle.IsDeleted == isDelete
                        select p;

            query = query.Where(p => p.tblTypeArticle.LanguageId == (int)StaticEnum.LanguageId.Vietnamese);

            return query.OrderByDescending(p => p.DisplayOrder).Select(p => new ArticleTypeItem()
            {
                ID = p.tblTypeArticle.ID,
                Name = p.tblTypeArticle.Name,
                NameAscii = p.tblTypeArticle.NameAscii,
                RootId = p.tblTypeArticle.RootId,
                IsShow = p.tblTypeArticle.IsShow
            }).ToList();
        }

        public T GetListForTree<T>(int languageId = (int) StaticEnum.LanguageId.Vietnamese, bool isShow = true, bool isDelete = false)
        {
            var query = from p in web365db.tblTypeArticle
                        where p.IsShow == isShow && p.IsDeleted == isDelete && p.ID != (int)StaticEnum.ArticleType.Banner
                        select p;

            query = query.Where(p => p.LanguageId == languageId);

            return (T)(object)query.OrderByDescending(p => p.ID).Select(p => new ArticleTypeItem()
                        {
                            ID = p.ID,
                            Name = p.Name,
                            NameAscii = p.NameAscii,
                            Parent = p.Parent,
                            LanguageId = p.LanguageId,
                            RootId = p.RootId,
                            LanguageName = p.tblLanguage.Name
                        }).ToList();
        }

        public T GetItemById<T>(int id)
        {

            var result = GetById<tblTypeArticle>(id);

            return (T)(object)new ArticleTypeItem()
            {
                ID = result.ID,
                Name = result.Name,
                NameAscii = result.NameAscii,
                SEOTitle = result.SEOTitle,
                SEODescription = result.SEODescription,
                SEOKeyword = result.SEOKeyword,
                DateCreated = result.DateCreated,
                DateUpdated = result.DateUpdated,
                Number = result.Number,
                PictureID = result.PictureID,
                Summary = result.Summary,
                Detail = result.Detail,
                Parent = result.Parent,
                IconCss = result.IconCss,
                IsShow = result.IsShow,
                ListGroupID = result.tblGroup_TypeArticle_Map.Select(g => g.GroupTypeID.Value).ToArray(),
                LanguageName = result.tblLanguage.Name,
                RootId = result.RootId,
                LanguageId = result.LanguageId
            };
        }

        public void Show(int id)
        {
            var typeArticle = web365db.tblTypeArticle.SingleOrDefault(p => p.ID == id);
            typeArticle.IsShow = true;
            web365db.Entry(typeArticle).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        public void Hide(int id)
        {
            var typeArticle = web365db.tblTypeArticle.SingleOrDefault(p => p.ID == id);
            typeArticle.IsShow = false;
            web365db.Entry(typeArticle).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        #region Check
        public bool NameExist(int id, string name, int languageId = (int) StaticEnum.LanguageId.Vietnamese)
        {
            var query = web365db.tblTypeArticle.Count(c => c.Name.ToLower() == name.ToLower() && c.LanguageId == languageId && c.ID != id);

            return query > 0;
        }
        #endregion
    }
}
