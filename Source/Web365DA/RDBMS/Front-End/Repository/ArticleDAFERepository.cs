using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Web365Base;
using Web365DA.RDBMS.Front_End.IRepository;
using Web365Domain;
using Web365Models;
using Web365Utility;

namespace Web365DA.RDBMS.Front_End.Repository
{
    public class ArticleDAFERepository : BaseFE, IArticleDAFERepository
    {
        public ArticleItem GetItemById(int id)
        {
            var result = from p in web365db.tblArticle
                         where p.ID == id
                         orderby p.ID descending
                         select new ArticleItem()
                         {
                             ID = p.ID,
                             TypeID = p.TypeID,
                             Title = p.Title,
                             TitleAscii = p.TitleAscii,
                             SEOTitle = p.SEOTitle,
                             SEODescription = p.SEODescription,
                             SEOKeyword = p.SEOKeyword,
                             DateCreated = p.DateCreated,
                             DateUpdated = p.DateUpdated,
                             Number = p.Number,
                             PictureID = p.PictureID,
                             Summary = p.Summary,
                             Detail = p.Detail,
                             IsShow = p.IsShow
                         };

            return result.FirstOrDefault();
        }

        public ArticleItem GetItemByNameAscii(string nameAscii, bool isShow = true, bool isDeleted = false)
        {  
            var result = from c in web365db.tblArticle
                         where c.TitleAscii == nameAscii && c.IsShow == isShow && c.IsDeleted == isDeleted
                         orderby c.ID descending
                         select new ArticleItem()
                         {
                             ID = c.ID,
                             TypeID = c.TypeID,
                             Title = c.Title,
                             TitleAscii = c.TitleAscii,
                             SEOTitle = c.SEOTitle,
                             SEODescription = c.SEODescription,
                             SEOKeyword = c.SEOKeyword,
                             DateCreated = c.DateCreated,
                             DateUpdated = c.DateUpdated,
                             Number = c.Number,
                             PictureID = c.PictureID,
                             Summary = c.Summary,
                             Detail = c.Detail,
                             IsShow = c.IsShow,
                             ArticleType = new ArticleTypeItem()
                             {
                                 ID = c.tblTypeArticle.ID,
                                 Name = c.tblTypeArticle.Name,
                                 NameAscii = c.tblTypeArticle.NameAscii
                             },
                             //ListPicture = c.tblPicture1.Select(p => new PictureItem()
                             //{
                             //    ID = p.ID,
                             //    FileName = p.FileName
                             //}).ToList(),
                             Picture = c.tblPicture != null ? new PictureItem
                             {
                                 ID = c.tblPicture.ID,
                                 Name = c.tblPicture.Name,
                                 FileName = c.tblPicture.FileName,
                                 Summary = c.tblPicture.Summary,
                                 Alt = c.tblPicture.Alt,
                                 Link = c.tblPicture.Link,
                             } : null,
                             LanguageId = c.LanguageId,
                             RootId = c.RootId
                         };

            return result.FirstOrDefault();
        }

        public ListArticleModel GetListByType(int typeID, string nameAscii, int currentRecord, int pageSize, bool isShow = true, bool isDeleted = false)
        {
            var result = new ListArticleModel();

            var paramTotal = new SqlParameter
            {
                ParameterName = "Total",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            var query = web365db.Database.SqlQuery<ArticleMapItem>("exec [dbo].[PRC_GetListNewsByType] @TypeID, @TypeAscii, @CurrentRecord, @PageSize, @Total OUTPUT",
                new SqlParameter("TypeID", typeID),
                new SqlParameter("TypeAscii", nameAscii),
                new SqlParameter("CurrentRecord", currentRecord),
                new SqlParameter("PageSize", pageSize),
                paramTotal);

            result.List = query.Select(p => new ArticleItem()
            {
                ID = p.ID,
                Title = p.Title,
                TypeID = p.TypeID,
                TitleAscii = p.TitleAscii,
                Summary = p.Summary,
                Detail = p.Detail,
                Number = p.Number,
                DateCreated = p.DateCreated,
                IconCss = p.IconCss,
                Picture = new PictureItem()
                {
                    FileName = p.PictureURL
                },
                PictureFirst = new PictureItem()
                {
                    FileName = p.PictureFirstURL
                },
                ArticleType = new ArticleTypeItem()
                {
                    Name = p.TypeName,
                    NameAscii = p.TypeNameAscii,
                    ParentName = p.TypeParentName,
                    ParentAscii = p.TypeParentNameAscii,
                    LanguageId = p.LanguageId,
                    RootId = p.RootId
                }
            }).OrderBy(p => p.Number).ToList();

            result.total = Convert.ToInt32(paramTotal.Value);

            return result;
        }

        public ListArticleModel GetListByArrType(int[] arrType, int currentRecord, int pageSize, bool isShow = true, bool isDeleted = false)
        {
            var result = new ListArticleModel();

            var query = from p in web365db.tblArticle
                        where arrType.Contains(p.TypeID.Value) && p.IsShow == isShow && p.IsDeleted == isDeleted
                        orderby p.Number
                        select new ArticleItem()
                        {
                            ID = p.ID,
                            TypeID = p.TypeID,
                            Title = p.Title,
                            TitleAscii = p.TitleAscii,
                            SEOTitle = p.SEOTitle,
                            Summary = p.Summary,
                            Picture = new PictureItem()
                            {
                                ID = p.PictureID ?? 0,
                                FileName = p.PictureID.HasValue ? p.tblPicture.FileName : string.Empty
                            },
                            ArticleType = new ArticleTypeItem()
                            {
                                ID = p.tblTypeArticle.ID,
                                Name = p.tblTypeArticle.Name,
                                NameAscii = p.tblTypeArticle.NameAscii,
                                SEOTitle = p.tblTypeArticle.SEOTitle,
                                SEODescription = p.tblTypeArticle.SEODescription,
                                SEOKeyword = p.tblTypeArticle.SEOKeyword,
                                LanguageId = p.tblTypeArticle.LanguageId,
                                RootId = p.tblTypeArticle.RootId
                            }
                            ,
                            LanguageId = p.LanguageId,
                            RootId = p.RootId
                        };

            result.total = query.Count();

            result.List = query.Skip(currentRecord).Take(pageSize).ToList();

            return result;
        }

        public List<ArticleItem> GetTopByType(int type, int skip, int top, bool isShow = true, bool isDeleted = false)
        {
            var list = from p in web365db.tblArticle
                       where p.TypeID == type && p.IsShow == isShow && p.IsDeleted == isDeleted
                       orderby p.ID descending
                       select new ArticleItem()
                       {
                           ID = p.ID,
                           TypeID = p.TypeID,
                           Title = p.Title,
                           TitleAscii = p.TitleAscii,
                           SEOTitle = p.SEOTitle,
                           DateCreated = p.DateCreated,
                           Picture = new PictureItem()
                           {
                               FileName = p.PictureID.HasValue ? p.tblPicture.FileName : string.Empty
                           },
                           ArticleType = new ArticleTypeItem()
                           {
                               ID = p.tblTypeArticle.ID,
                               Name = p.tblTypeArticle.Name,
                               NameAscii = p.tblTypeArticle.NameAscii,
                               SEOTitle = p.tblTypeArticle.SEOTitle,
                               SEODescription = p.tblTypeArticle.SEODescription,
                               SEOKeyword = p.tblTypeArticle.SEOKeyword,
                               LanguageId = p.tblTypeArticle.LanguageId,
                               RootId = p.tblTypeArticle.RootId
                           },
                           LanguageId = p.LanguageId,
                           RootId = p.RootId
                       };

            return list.Skip(skip).Take(top).ToList();
        }

        public ArticleItem GetFirstArticleItem(int type)
        {
            var query = from p in web365db.tblArticle
                        where p.TypeID == type && p.IsShow == true && p.IsDeleted == false
                        orderby p.Number
                        select new ArticleItem()
                        {
                            ID = p.ID,
                            TypeID = p.TypeID,
                            Title = p.Title,
                            TitleAscii = p.TitleAscii,
                            SEOTitle = p.SEOTitle,
                            Detail = p.Detail,
                            Summary = p.Summary,
                            DateCreated = p.DateCreated,
                            Picture = new PictureItem()
                            {
                                ID = p.PictureID ?? 0,
                                FileName = p.PictureID.HasValue ? p.tblPicture.FileName : string.Empty
                            },
                            ArticleType = new ArticleTypeItem()
                            {
                                ID = p.tblTypeArticle.ID,
                                Name = p.tblTypeArticle.Name,
                                NameAscii = p.tblTypeArticle.NameAscii,
                                SEOTitle = p.tblTypeArticle.SEOTitle,
                                SEODescription = p.tblTypeArticle.SEODescription,
                                SEOKeyword = p.tblTypeArticle.SEOKeyword,
                                LanguageId = p.tblTypeArticle.LanguageId,
                                RootId = p.tblTypeArticle.RootId
                            },
                            LanguageId = p.LanguageId,
                            RootId = p.RootId
                        };

            return query.FirstOrDefault();
        }

        public ArticleItem GetSameInOtherLangByNameascii(string nameascii, int languageId)
        {
            var _default = GetItemByNameAscii(nameascii);
            if (_default.LanguageId == languageId)
            {
                return _default;
            }

            int rootId = _default.RootId ?? _default.ID;

            var query = from p in web365db.tblArticle
                        where (languageId != (int)StaticEnum.LanguageId.Vietnamese ? p.RootId == rootId : p.ID == rootId)
                        && p.IsShow == true && p.IsDeleted == false
                        orderby p.Number
                        select new ArticleItem()
                        {
                            ID = p.ID,
                            TypeID = p.TypeID,
                            Title = p.Title,
                            TitleAscii = p.TitleAscii,
                            DateCreated = p.DateCreated,
                            LanguageId = p.LanguageId,
                            RootId = p.RootId
                        };

            return query.FirstOrDefault();
        }

        public List<ArticleItem> GetOtherArticle(int type, int articleId, int skip, int top, bool isShow = true, bool isDeleted = false)
        {
            var list = from p in web365db.tblArticle
                       where p.TypeID == type && p.ID != articleId && p.IsShow == isShow && p.IsDeleted == isDeleted
                       orderby p.Number ascending
                       select new ArticleItem()
                       {
                           ID = p.ID,
                           TypeID = p.TypeID,
                           Title = p.Title,
                           TitleAscii = p.TitleAscii,
                           SEOTitle = p.SEOTitle,
                           Picture = new PictureItem()
                           {
                               ID = p.PictureID ?? 0,
                               FileName = p.PictureID.HasValue ? p.tblPicture.FileName : string.Empty
                           },
                           ArticleType = new ArticleTypeItem()
                           {
                               ID = p.tblTypeArticle.ID,
                               Name = p.tblTypeArticle.Name,
                               NameAscii = p.tblTypeArticle.NameAscii,
                               SEOTitle = p.tblTypeArticle.SEOTitle,
                               SEODescription = p.tblTypeArticle.SEODescription,
                               SEOKeyword = p.tblTypeArticle.SEOKeyword,
                               LanguageId = p.tblTypeArticle.LanguageId,
                               RootId = p.tblTypeArticle.RootId
                           },
                           LanguageId = p.LanguageId,
                           RootId = p.RootId
                       };

            return list.Skip(skip).Take(top).ToList();
        }
        public List<ArticleItem> GetOtherArticleService(int type, int articleId, int skip, int top, bool isShow = true, bool isDeleted = false)
        {
            var list = from p in web365db.tblArticle
                       where p.TypeID == type && p.IsShow == isShow && p.IsDeleted == isDeleted
                       orderby p.Number ascending
                       select new ArticleItem()
                       {
                           ID = p.ID,
                           TypeID = p.TypeID,
                           Title = p.Title,
                           TitleAscii = p.TitleAscii,
                           SEOTitle = p.SEOTitle,
                           Picture = new PictureItem()
                           {
                               ID = p.PictureID ?? 0,
                               FileName = p.PictureID.HasValue ? p.tblPicture.FileName : string.Empty
                           },
                           ArticleType = new ArticleTypeItem()
                           {
                               ID = p.tblTypeArticle.ID,
                               Name = p.tblTypeArticle.Name,
                               NameAscii = p.tblTypeArticle.NameAscii,
                               SEOTitle = p.tblTypeArticle.SEOTitle,
                               SEODescription = p.tblTypeArticle.SEODescription,
                               SEOKeyword = p.tblTypeArticle.SEOKeyword,
                               LanguageId = p.tblTypeArticle.LanguageId,
                               RootId = p.tblTypeArticle.RootId
                           },
                           LanguageId = p.LanguageId,
                           RootId = p.RootId
                       };

            return list.Skip(skip).Take(top).ToList();
        }

        public ListArticleModel ArticleSeach(string[] keyword, string[] keywordAscii, int currentRecord, int top, bool isShow = true, bool isDeleted = false, int languageId = (int)StaticEnum.LanguageId.Vietnamese)
        {
            var list = new ListArticleModel();
            //var listNewsType = from c in web365db.tblTypeArticle
            //                   where c.IsDeleted == false && c.IsShow == true && c.Parent.HasValue
            //                   select c.ID;

            var query = (from p in web365db.tblArticle
                         where (keyword.All(k => p.Title.ToLower().Contains(k)) || keyword.All(k => p.TitleAscii.Contains(k)))
                         && p.TypeID > 1 && p.LanguageId == languageId && p.IsShow == isShow && p.IsDeleted == isDeleted 
                         //&& listNewsType.Contains(p.TypeID.Value)
                         orderby p.ID descending
                         select new ArticleItem()
                         {
                             ID = p.ID,
                             Title = p.Title,
                             TitleAscii = p.TitleAscii,
                             SEOTitle = p.SEOTitle,
                             DateCreated = p.DateCreated,
                             Summary = p.Summary,
                             Picture = new PictureItem()
                             {
                                 Name = p.tblPicture != null ? p.tblPicture.Name : string.Empty,
                                 FileName = p.tblPicture != null ? p.tblPicture.FileName : string.Empty
                             },
                             ArticleType = new ArticleTypeItem()
                             {
                                 ID = p.tblTypeArticle.ID,
                                 Name = p.tblTypeArticle.Name,
                                 NameAscii = p.tblTypeArticle.NameAscii,
                                 ParentAscii = p.tblTypeArticle.tblTypeArticle2.NameAscii,
                                 LanguageId = p.tblTypeArticle.LanguageId,
                                 RootId = p.tblTypeArticle.RootId
                             }
                            ,
                             LanguageId = p.LanguageId,
                             RootId = p.RootId
                         });

            list.total = query.Count();

            list.List = query.Skip(currentRecord).Take(top).ToList();

            return list;
        }

        public ListArticleModel GetListByGroup(int groupId, int skip, int top, bool isShow = true, bool isDeleted = false)
        {
            var list = new ListArticleModel { Group = GetGroupById(groupId) };

            var query = (from p in web365db.tblGroup_Article_Map
                         where p.GroupID == groupId && p.tblArticle.IsShow == isShow && p.tblArticle.IsDeleted == isDeleted
                         orderby p.tblArticle.Number
                         select new ArticleItem()
                         {
                             ID = p.tblArticle.ID,
                             TypeID = p.tblArticle.TypeID,
                             Title = p.tblArticle.Title,
                             TitleAscii = p.tblArticle.TitleAscii,
                             SEOTitle = p.tblArticle.SEOTitle,
                             Summary = p.tblArticle.Summary,
                             DateCreated = p.tblArticle.DateCreated,
                             Picture = new PictureItem()
                             {
                                 ID = p.tblArticle.PictureID ?? 0,
                                 FileName = p.tblArticle.PictureID.HasValue ? p.tblArticle.tblPicture.FileName : string.Empty
                             },
                             ArticleType = p.tblArticle.TypeID.HasValue ? new ArticleTypeItem()
                             {
                                 ID = p.tblArticle.tblTypeArticle.ID,
                                 Name = p.tblArticle.tblTypeArticle.Name,
                                 NameAscii = p.tblArticle.tblTypeArticle.NameAscii,
                                 ParentName = p.tblArticle.tblTypeArticle.tblTypeArticle2.Name,
                                 ParentAscii = p.tblArticle.tblTypeArticle.tblTypeArticle2.NameAscii,
                                 LanguageId = p.tblArticle.LanguageId,
                                 RootId = p.tblArticle.RootId
                             } : null,
                             LanguageId = p.tblArticle.LanguageId,
                             RootId = p.tblArticle.RootId
                         });

            list.total = query.Count();

            list.List = query.Skip(skip).Take(top).ToList();

            return list;
        }

        public ListArticleModel GetListByTypeAndDetail(int typeId, int skip, int top, bool isShow = true, bool isDeleted = false)
        {
            var list = new ListArticleModel();

            var query = (from p in web365db.tblArticle
                         where p.TypeID == typeId && p.IsShow == isShow && p.IsDeleted == isDeleted
                         orderby p.Number
                         select new ArticleItem()
                         {
                             ID = p.ID,
                             TypeID = p.TypeID,
                             Title = p.Title,
                             TitleAscii = p.TitleAscii,
                             SEOTitle = p.SEOTitle,
                             Summary = p.Summary,
                             Detail = p.Detail,
                             Picture = new PictureItem()
                             {
                                 ID = p.PictureID ?? 0,
                                 FileName = p.PictureID.HasValue ? p.tblPicture.FileName : string.Empty
                             },
                             ArticleType = new ArticleTypeItem()
                             {
                                 ID = p.tblTypeArticle.ID,
                                 Name = p.tblTypeArticle.Name,
                                 NameAscii = p.tblTypeArticle.NameAscii,
                                 ParentAscii = p.tblTypeArticle.tblTypeArticle2.NameAscii,
                                 ParentName = p.tblTypeArticle.tblTypeArticle2.Name,
                                 LanguageId = p.tblTypeArticle.LanguageId,
                                 RootId = p.tblTypeArticle.RootId
                             },
                             LanguageId = p.LanguageId,
                             RootId = p.RootId
                         });

            list.total = query.Count();

            list.List = query.Skip(skip).Take(top).ToList();

            return list;
        }

        public ArticleGroupItem GetGroupById(int groupId)
        {
            var entity = from c in web365db.tblGroupArticle
                         where c.ID == groupId && c.IsDeleted != true && c.IsShow == true
                         select new ArticleGroupItem()
                         {
                             ID = c.ID,
                             Name = c.Name,
                             Detail = c.Detail,
                             Summary = c.Summary,
                             LanguageId = c.LanguageId,
                             LanguageName = c.tblLanguage.Name,
                             RootId = c.RootId
                         };
            return entity.FirstOrDefault();
        }

        public ArticleGroupItem GetGroupInOtherLang(int groupId, int languageId)
        {
            var _default = GetGroupById(groupId);
            if (_default.LanguageId == languageId)
            {
                return _default;
            }

            int rootId = _default.RootId ?? _default.ID;

            var entity = from c in web365db.tblGroupArticle
                         where (languageId != (int)StaticEnum.LanguageId.Vietnamese ? c.RootId == rootId : c.ID == rootId)
                         && c.IsDeleted != true
                         && c.IsShow == true
                         && languageId == c.LanguageId
                         select new ArticleGroupItem()
                         {
                             ID = c.ID,
                             Name = c.Name,
                             Detail = c.Detail,
                             Summary = c.Summary,
                             LanguageId = c.LanguageId,
                             RootId = c.RootId
                         };
            return entity.FirstOrDefault();
        }
    }
}
