using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Web365Base;
using Web365DA.RDBMS.Back_End.IRepository;
using Web365Domain.Language;
using Web365Utility;

namespace Web365DA.RDBMS.Back_End.Repository
{
    /// <summary>
    /// create by BienLV 05-01-2013
    /// </summary>
    public class LanguageDABERepository : BaseBE<tblLanguage>, ILanguageDABERepository
    {
        /// <summary>
        /// function get all data tblTypeProduct
        /// </summary>
        /// <returns></returns>
        public List<LanguageItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            var query = from p in web365db.tblLanguage
                        where p.Name.ToLower().Contains(name) && p.IsDeleted == isDelete && p.ID != (int)StaticEnum.LanguageId.Vietnamese
                        select p;

            total = query.Count();

            query = descending ? QueryableHelper.OrderByDescending(query, propertyNameSort) : QueryableHelper.OrderBy(query, propertyNameSort);

            return query.Select(p => new LanguageItem()
            {
                ID = p.ID,
                Name = p.Name,
                Code = p.Code,
                IsShow = p.IsShow,
                DateCreated = p.DateCreated
            }).Skip(currentRecord).Take(numberRecord).ToList();
        }

        public LanguageItem GetItemByCode(string code)
        {
            var query = from p in web365db.tblLanguage
                        where p.IsDeleted == false && p.IsShow == true && p.Code == code
                        select p;

            return query.Select(p => new LanguageItem()
            {
                ID = p.ID,
                Name = p.Name,
                Code = p.Code,
                IsShow = p.IsShow,
                DateCreated = p.DateCreated
            }).FirstOrDefault();
        }

        public List<LanguageItem> GetListExpectDefault()
        {
            var query = from p in web365db.tblLanguage
                        where p.IsDeleted == false && p.IsShow == true && p.ID != (int)StaticEnum.LanguageId.Vietnamese
                        select p;

            return query.Select(p => new LanguageItem()
            {
                ID = p.ID,
                Name = p.Name,
                Code = p.Code,
                IsShow = p.IsShow,
                DateCreated = p.DateCreated
            }).ToList();
        }

        public T GetListForTree<T>(int languageId = (int) StaticEnum.LanguageId.Vietnamese, bool isShow = true, bool isDelete = false)
        {
            var query = from p in web365db.tblLanguage
                        where p.IsShow == isShow && p.IsDeleted == isDelete
                        select p;

            return (T)(object)query.OrderByDescending(p => p.ID).Select(p => new LanguageItem()
            {
                ID = p.ID,
                Name = p.Name,
                Code = p.Code
            }).ToList();
        }

        public T GetItemById<T>(int id)
        {
            var result = GetById<tblLanguage>(id);

            return (T) (object) new LanguageItem()
            {
                ID = result.ID,
                Name = result.Name,
                Code = result.Code,
                IsShow = result.IsShow,
                DateCreated = result.DateCreated
            };
        }

        #region Insert, Edit, Delete, Save
        public void Show(int id)
        {
            var tblLanguage = web365db.tblLanguage.SingleOrDefault(p => p.ID == id);
            tblLanguage.IsShow = true;
            web365db.Entry(tblLanguage).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        public void Hide(int id)
        {
            var tblLanguage = web365db.tblLanguage.SingleOrDefault(p => p.ID == id);
            tblLanguage.IsShow = false;
            web365db.Entry(tblLanguage).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        public void ResetListPicture(int id, string listPictureId)
        {
            //web365db.Database.SqlQuery<object>("EXEC [dbo].[PRC_ResetPictureLayoutContent] {0}, {1}", id, listPictureId).FirstOrDefault();
        }

        #endregion

        #region Check
        public bool NameExist(int id, string name, int languageId = (int) StaticEnum.LanguageId.Vietnamese)
        {
            return web365db.tblLanguage.Any(c => c.Name.ToLower() == name.ToLower() && c.ID != id && c.IsDeleted == false);
        }
        #endregion
    }
}
