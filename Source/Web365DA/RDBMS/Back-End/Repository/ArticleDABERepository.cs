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
    public class ArticleDABERepository : BaseBE<tblArticle>, IArticleDABERepository
    {
        /// <summary>
        /// function get all data tblArticle
        /// </summary>
        /// <returns></returns>
        public List<ArticleItem> GetList(out int total, string name, int? typeId, int? groupId, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            var query = from p in web365db.tblArticle
                        where p.Title.ToLower().Contains(name) && p.IsDeleted == isDelete
                        select p;

            query = query.Where(p => p.LanguageId == (int) StaticEnum.LanguageId.Vietnamese);

            if (typeId.HasValue)
            {
                query = query.Where(a => a.TypeID == typeId);
            }
            else
            {
                query = query.Where(a => a.TypeID != (int) StaticEnum.ArticleType.Banner);
            }

            if (groupId.HasValue)
            {
                query = query.Where(a => a.tblGroup_Article_Map.Any(g => g.GroupID == groupId));
            }

            total = query.Count();

            query = descending ? QueryableHelper.OrderByDescending(query, propertyNameSort) : QueryableHelper.OrderBy(query, propertyNameSort);
            var result = query.Select(p => new ArticleItem()
            {
                ID = p.ID,
                Title = p.Title,
                TitleAscii = p.TitleAscii,
                SEOTitle = p.SEOTitle,
                SEODescription = p.SEODescription,
                SEOKeyword = p.SEOKeyword,
                Number = p.Number,
                Summary = p.Summary,
                Detail = p.Detail,
                IsShow = p.IsShow,
                LanguageId = p.LanguageId,
                RootId = 0,
                LanguageName = p.tblLanguage.Name
            }).Skip(currentRecord).Take(numberRecord).ToList();

            foreach (var articleItem in result)
            {
                articleItem.ListOtherLanguage =
                    web365db.tblArticle.Where(c => c.IsDeleted == isDelete && c.RootId == articleItem.ID && c.LanguageId != (int) StaticEnum.LanguageId.Vietnamese)
                        .Select(c => new ArticleItem()
                        {
                            ID = c.ID,
                            Title = c.Title,
                            TitleAscii = c.TitleAscii,
                            SEOTitle = c.SEOTitle,
                            SEODescription = c.SEODescription,
                            SEOKeyword = c.SEOKeyword,
                            Number = c.Number,
                            Summary = c.Summary,
                            Detail = c.Detail,
                            IsShow = c.IsShow,
                            LanguageId = c.LanguageId,
                            RootId = c.RootId,
                            LanguageName = c.tblLanguage.Name
                        }).ToList();
            }

            return result;
        }

        public List<ArticleItem> GetListOtherLanguage(int id)
        {
            var target = GetById<tblArticle>(id);
            var result = new List<ArticleItem>();
            if (target.RootId > 0)
            {
                var parent = from c in web365db.tblArticle
                             where c.IsDeleted == false && c.IsShow == true && c.ID == target.RootId
                             select new ArticleItem()
                             {
                                 ID = c.ID,
                                 Title = c.Title,
                                 TitleAscii = c.TitleAscii,
                                 SEOTitle = c.SEOTitle,
                                 SEODescription = c.SEODescription,
                                 SEOKeyword = c.SEOKeyword,
                                 Number = c.Number,
                                 Summary = c.Summary,
                                 Detail = c.Detail,
                                 IsShow = c.IsShow,
                                 LanguageId = c.LanguageId,
                                 RootId = c.RootId,
                                 LanguageName = c.tblLanguage.Name
                             };

                result.Add(parent.FirstOrDefault());
            }

            var query = from c in web365db.tblArticle
                        where c.IsDeleted == false && c.IsShow == true && ((!target.RootId.HasValue && c.RootId == id) || c.RootId == target.RootId)
                        select new ArticleItem()
                        {
                            ID = c.ID,
                            Title = c.Title,
                            TitleAscii = c.TitleAscii,
                            SEOTitle = c.SEOTitle,
                            SEODescription = c.SEODescription,
                            SEOKeyword = c.SEOKeyword,
                            Number = c.Number,
                            Summary = c.Summary,
                            Detail = c.Detail,
                            IsShow = c.IsShow,
                            LanguageId = c.LanguageId,
                            RootId = c.RootId,
                            LanguageName = c.tblLanguage.Name
                        };

            result.AddRange(query.ToList());

            return result;
        }

        public T GetListForTree<T>(int languageId = 1, bool isShow = true, bool isDelete = false)
        {
            throw new NotImplementedException();
        }

        public T GetItemById<T>(int id)
        {
            var result = GetById<tblArticle>(id);

            return (T)(object)new ArticleItem()
            {
                ID = result.ID,
                TypeID = result.TypeID,
                Title = result.Title,
                TitleAscii = result.TitleAscii,
                SEOTitle = result.SEOTitle,
                SEODescription = result.SEODescription,
                SEOKeyword = result.SEOKeyword,
                DateCreated = result.DateCreated,
                DateUpdated = result.DateUpdated,
                Number = result.Number,
                PictureID = result.PictureID,
                Summary = result.Summary,
                Detail = result.Detail,
                IsShow = result.IsShow,
                Viewed = result.Viewed,
                ListGroupID = result.tblGroup_Article_Map.Select(g => g.GroupID.Value).ToArray(),
                ListPictureID = result.tblPicture1.Select(p => p.ID).ToArray(),
                RootId = result.RootId,
                LanguageId = result.LanguageId,
                IconCss = result.IconCss
            };
        }

        public void Show(int id)
        {
            var article = web365db.tblArticle.SingleOrDefault(p => p.ID == id);
            article.IsShow = true;
            web365db.Entry(article).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        public void Hide(int id)
        {
            var article = web365db.tblArticle.SingleOrDefault(p => p.ID == id);
            article.IsShow = false;
            web365db.Entry(article).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        public void ResetListPicture(int id, string listPictureId)
        {
            web365db.Database.SqlQuery<object>("EXEC [dbo].[PRC_ResetPictureArticle] {0}, {1}", id, listPictureId).FirstOrDefault();
        }

        #region Check
        public bool NameExist(int id, string name, int languageId = (int)StaticEnum.LanguageId.Vietnamese )
        {
            var query = web365db.tblArticle.Count(c => c.Title.ToLower() == name.ToLower() && c.tblTypeArticle.LanguageId == languageId && c.ID != id);

            return query > 0;
        }
        #endregion
    }
}
