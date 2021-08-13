using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using Web365Base;
using Web365DA.RDBMS.Back_End.IRepository;
using Web365Domain;
using Web365Utility;

namespace Web365DA.RDBMS.Back_End.Repository
{
    /// <summary>
    /// create by BienLV 05-01-2013
    /// </summary>
    public class LayoutContentDABERepository : BaseBE<tblLayoutContent>, ILayoutContentDABERepository
    {
        /// <summary>
        /// function get all data tblTypeProduct
        /// </summary>
        /// <returns></returns>
        public List<LayoutContentItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            var query = from p in web365db.tblLayoutContent
                        where p.Name.ToLower().Contains(name) && p.IsDeleted == isDelete
                        select p;


            query = query.Where(p => p.LanguageId == (int)StaticEnum.LanguageId.Vietnamese);

            total = query.Count();

            query = descending ? QueryableHelper.OrderByDescending(query, propertyNameSort) : QueryableHelper.OrderBy(query, propertyNameSort);

            var result = query.Select(p => new LayoutContentItem()
            {
                ID = p.ID,
                Name = p.Name,
                NameAscii = p.NameAscii,
                DateCreated = p.DateCreated,
                SEOTitle = p.SEOTitle,
                SEODescription = p.SEODescription,
                LayoutGroupId = p.LayoutGroupId,
                SEOKeyword = p.SEOKeyword,
                IsShow = p.IsShow,
                IconClassCss = p.IconClassCss,
                LanguageId = p.LanguageId,
                LanguageName = p.tblLanguage.Name,
                RootId = p.RootId
            }).Skip(currentRecord).Take(numberRecord).ToList();

            foreach (var item in result)
            {
                item.ListOtherLanguage = web365db.tblLayoutContent
                    .Where(c => c.IsDeleted == false && c.RootId == item.ID)
                    .Select(c => new LayoutContentItem()
                    {
                        ID = c.ID,
                        Name = c.Name,
                        NameAscii = c.NameAscii,
                        DateCreated = c.DateCreated,
                        SEOTitle = c.SEOTitle,
                        SEODescription = c.SEODescription,
                        LayoutGroupId = c.LayoutGroupId,
                        SEOKeyword = c.SEOKeyword,
                        IsShow = c.IsShow,
                        IconClassCss = c.IconClassCss,
                        LanguageId = c.LanguageId,
                        LanguageName = c.tblLanguage.Name,
                        RootId = c.RootId,
                        Number = c.Number
                    }).ToList();
            }

            return result;
        }

        public T GetListForTree<T>(int languageId = (int) StaticEnum.LanguageId.Vietnamese, bool isShow = true, bool isDelete = false)
        {
            var query = from p in web365db.tblLayoutContent
                        where p.IsShow == isShow && p.IsDeleted == isDelete
                        select p;

            query = query.Where(p => p.LanguageId == languageId);

            return (T)(object)query.OrderByDescending(p => p.ID).Select(p => new LayoutContentItem()
            {
                ID = p.ID,
                Name = p.Name,
                NameAscii = p.NameAscii,
                LayoutGroupId = p.LayoutGroupId,
                IconClassCss = p.IconClassCss,
                LanguageId = p.LanguageId,
                LanguageName = p.tblLanguage.Name,
                RootId = p.RootId,
                Number = p.Number
            }).ToList();
        }

        /// <summary>
        /// get product type item
        /// </summary>
        /// <param name="id">id of product type</param>
        /// <returns></returns>
        public T GetItemById<T>(int id)
        {
            var result = GetById<tblLayoutContent>(id);

            return (T)(object)new LayoutContentItem()
                        {
                            ID = result.ID,
                            LayoutGroupId = result.LayoutGroupId,
                            Name = result.Name,
                            NameAscii = result.NameAscii,
                            SEOTitle = result.SEOTitle,
                            SEODescription = result.SEODescription,
                            SEOKeyword = result.SEOKeyword,
                            DateCreated = result.DateCreated,
                            DateUpdated = result.DateUpdated,
                            PictureID = result.PictureID,
                            Detail = result.Detail,
                            IsShow = result.IsShow,
                            IconClassCss = result.IconClassCss,
                            ListPictureID = result.tblLayoutContent_Picture_Map.Select(p => p.PictureID.Value).ToArray(),
                            LanguageId = result.LanguageId,
                            LanguageName = result.tblLanguage.Name,
                            RootId = result.RootId,
                            Number = result.Number,
                            GroupNumber = result.GroupNumber
                        };
        }

        #region Insert, Edit, Delete, Save
        public void Show(int id)
        {
            var layoutContent = web365db.tblLayoutContent.SingleOrDefault(p => p.ID == id);
            layoutContent.IsShow = true;
            web365db.Entry(layoutContent).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        public void Hide(int id)
        {
            var layoutContent = web365db.tblLayoutContent.SingleOrDefault(p => p.ID == id);
            layoutContent.IsShow = false;
            web365db.Entry(layoutContent).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        public void ResetListPicture(int id, string listPictureId)
        {
            web365db.Database.SqlQuery<object>("EXEC [dbo].[PRC_ResetPictureLayoutContent] {0}, {1}", id, listPictureId).FirstOrDefault();
        }

        #endregion

        #region Check
        public bool NameExist(int id, string name, int languageId = (int) StaticEnum.LanguageId.Vietnamese)
        {
            var query = web365db.tblLayoutContent.Count(c => c.Name.ToLower() == name.ToLower() && c.LanguageId == languageId && c.ID != id);

            return query > 0;
        }
        #endregion
    }
}
