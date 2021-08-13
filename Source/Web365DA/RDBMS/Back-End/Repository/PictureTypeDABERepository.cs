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
    public class PictureTypeDABERepository : BaseBE<tblTypePicture>, IPictureTypeDABERepository
    {
        /// <summary>
        /// function get all data tblTypeProduct
        /// </summary>
        /// <returns></returns>
        public List<PictureTypeItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            var query = from p in web365db.tblTypePicture
                        where p.Name.ToLower().Contains(name) && p.IsDeleted == isDelete
                        orderby p.ID descending
                        select new PictureTypeItem()
                        {
                            ID = p.ID,
                            Name = p.Name,
                            NameAscii = p.NameAscii,
                            SEOTitle = p.SEOTitle,
                            SEODescription = p.SEODescription,
                            SEOKeyword = p.SEOKeyword,
                            Number = p.Number,
                            Detail = p.Detail,
                            Parent = p.Parent,
                            IsShow = p.IsShow,
                            LanguageId = p.LanguageId,
                            LanguageName = p.tblLanguage.Name,
                            RootId = p.RootId
                        };

            query = query.Where(p => p.LanguageId == (int) StaticEnum.LanguageId.Vietnamese);

            total = query.Count();

            query = descending ? QueryableHelper.OrderByDescending(query, propertyNameSort) : QueryableHelper.OrderBy(query, propertyNameSort);

            var result = query.Skip(currentRecord).Take(numberRecord).ToList();

            foreach (var item in result)
            {
                item.ListOtherLanguage = web365db
                    .tblTypePicture
                    .Where(p => p.IsDeleted == false && p.RootId == item.ID)
                    .Select(p => new PictureTypeItem()
                    {
                        ID = p.ID,
                        Name = p.Name,
                        NameAscii = p.NameAscii,
                        SEOTitle = p.SEOTitle,
                        SEODescription = p.SEODescription,
                        SEOKeyword = p.SEOKeyword,
                        Number = p.Number,
                        Detail = p.Detail,
                        Parent = p.Parent,
                        IsShow = p.IsShow,
                        LanguageId = p.LanguageId,
                        LanguageName = p.tblLanguage.Name,
                        RootId = p.RootId
                    }).ToList();
            }

            return result;
        }

        public T GetListForTree<T>(int languageId = (int) StaticEnum.LanguageId.Vietnamese, bool isShow = true, bool isDelete = false)
        {
            var query = from p in web365db.tblTypePicture
                        where p.IsShow == isShow && p.IsDeleted == isDelete && p.LanguageId == languageId
                        orderby p.ID descending
                        select new PictureTypeItem()
                        {
                            ID = p.ID,
                            Name = p.Name,
                            NameAscii = p.NameAscii,
                            Parent = p.Parent,
                            LanguageId = p.LanguageId,
                            LanguageName = p.tblLanguage.Name,
                            RootId = p.RootId
                        };

            return (T)(object)query.ToList();
        }

        /// <summary>
        /// get product type item
        /// </summary>
        /// <param name="id">id of product type</param>
        /// <returns></returns>
        public T GetItemById<T>(int id)
        {
            var query = from p in web365db.tblTypePicture
                        where p.ID == id
                        orderby p.ID descending
                        select new PictureTypeItem()
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
                            Detail = p.Detail,
                            Parent = p.Parent,
                            IsShow = p.IsShow,
                            LanguageId = p.LanguageId,
                            LanguageName = p.tblLanguage.Name,
                            RootId = p.RootId
                        };

            return (T)(object)query.FirstOrDefault();
        }

        public PictureTypeItem GetSameTypeByLanguage(int id, int languageId)
        {
            var query = from p in web365db.tblTypePicture
                        where p.RootId == id && p.IsDeleted == false && p.LanguageId == languageId
                        orderby p.ID descending
                        select new PictureTypeItem()
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
                            Detail = p.Detail,
                            Parent = p.Parent,
                            IsShow = p.IsShow,
                            LanguageId = p.LanguageId,
                            LanguageName = p.tblLanguage.Name,
                            RootId = p.RootId
                        };

            return query.FirstOrDefault();
        }

        #region Insert, Edit, Delete, Save

        public void Show(int id)
        {
            var typePicture = web365db.tblTypePicture.SingleOrDefault(p => p.ID == id);
            typePicture.IsShow = true;
            web365db.Entry(typePicture).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        public void Hide(int id)
        {
            var typePicture = web365db.tblTypePicture.SingleOrDefault(p => p.ID == id);
            typePicture.IsShow = false;
            web365db.Entry(typePicture).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        #endregion

        #region Check
        public bool NameExist(int id, string name, int languageId = (int) StaticEnum.LanguageId.Vietnamese)
        {
            var query = web365db.tblTypePicture.Count(c => c.Name.ToLower() == name.ToLower() && c.ID != id);

            return query > 0;
        }
        #endregion
    }
}
