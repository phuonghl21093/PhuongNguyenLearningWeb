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
    public class ArticleGroupDABERepository : BaseBE<tblGroupArticle>, IArticleGroupDABERepository
    {
        public int[] GetSameArticleGroupByLanguage(int[] rootId, int langId)
        {
            var query = from c in web365db.tblGroupArticle
                        where c.IsDeleted == false && c.IsShow == true && c.RootId.HasValue && rootId.Contains(c.RootId.Value) && c.LanguageId == langId
                        select c.ID;

            return query.ToArray();
        }

        /// <summary>
        /// function get all data tblGroupArticle
        /// </summary>
        /// <returns></returns>
        public List<ArticleGroupItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            var query = from p in web365db.tblGroupArticle
                        where p.Name.ToLower().Contains(name) && p.IsDeleted == isDelete && p.LanguageId == (int) StaticEnum.LanguageId.Vietnamese
                        select p;

            total = query.Count();

            query = descending ? QueryableHelper.OrderByDescending(query, propertyNameSort) : QueryableHelper.OrderBy(query, propertyNameSort);

            var result = query.Select(p => new ArticleGroupItem()
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
                IsShow = p.IsShow,
                LanguageId = p.LanguageId,
                LanguageName = p.tblLanguage.Name,
                RootId = p.RootId
            }).Skip(currentRecord).Take(numberRecord).ToList();

            foreach (var group in result)
            {
                group.ListOtherLanguage = web365db
                    .tblGroupArticle
                    .Where(c => c.IsDeleted == false && c.RootId == group.ID)
                    .Select(c => new ArticleGroupItem()
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
                        IsShow = c.IsShow,
                        LanguageId = c.LanguageId,
                        LanguageName = c.tblLanguage.Name,
                        RootId = c.RootId
                    }).ToList();
            }

            return result;
        }

        public T GetListForTree<T>(int languageId = (int) StaticEnum.LanguageId.Vietnamese, bool isShow = true, bool isDelete = false)
        {
            var query = from p in web365db.tblGroupArticle
                        where p.IsShow == isShow && p.IsDeleted == isDelete && p.LanguageId == languageId
                        orderby p.ID descending
                        select new ArticleGroupItem()
                        {
                            ID = p.ID,
                            Name = p.Name,
                            NameAscii = p.NameAscii,
                            LanguageId = p.LanguageId,
                            LanguageName = p.tblLanguage.Name,
                            RootId = p.RootId
                        };

            return (T)(object)query.ToList();
        }

        public T GetItemById<T>(int id)
        {
            var query = from p in web365db.tblGroupArticle
                        where p.ID == id
                        orderby p.ID descending
                        select new ArticleGroupItem()
                        {
                            ID = p.ID,
                            Name = p.Name,
                            NameAscii = p.NameAscii,
                            SEOTitle = p.SEOTitle,
                            SEODescription = p.SEODescription,
                            SEOKeyword = p.SEOKeyword,
                            DateCreated = p.DateCreated,
                            DateUpdated = p.DateUpdated,
                            Number = p.Number,
                            PictureID = p.PictureID,
                            Summary = p.Summary,
                            Detail = p.Detail,
                            IsShow = p.IsShow,
                            LanguageId = p.LanguageId,
                            LanguageName = p.tblLanguage.Name,
                            RootId = p.RootId
                        };
            return (T)(object)query.FirstOrDefault();
        }


        public void Show(int id)
        {
            var groupArticle = web365db.tblGroupArticle.SingleOrDefault(p => p.ID == id);
            groupArticle.IsShow = true;
            web365db.Entry(groupArticle).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        public void Hide(int id)
        {
            var groupArticle = web365db.tblGroupArticle.SingleOrDefault(p => p.ID == id);
            groupArticle.IsShow = false;
            web365db.Entry(groupArticle).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        #region Check
        public bool NameExist(int id, string name, int languageId = (int) StaticEnum.LanguageId.Vietnamese)
        {
            var query = web365db.tblGroupArticle.Count(c => c.Name.ToLower() == name.ToLower() && c.ID != id && c.LanguageId == languageId);

            return query > 0;
        }
        #endregion
    }
}
