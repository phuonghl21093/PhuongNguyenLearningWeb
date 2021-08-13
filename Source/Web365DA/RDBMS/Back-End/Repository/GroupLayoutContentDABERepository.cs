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
using Web365Domain.Other;
using Web365Utility;

namespace Web365DA.RDBMS.Back_End.Repository
{
    /// <summary>
    /// create by BienLV 05-01-2013
    /// </summary>
    public class GroupLayoutContentDABERepository : BaseBE<tblLayoutGroup>, IGroupLayoutContentDABERepository
    {
        /// <summary>
        /// function get all data tblTypeProduct
        /// </summary>
        /// <returns></returns>
        public List<LayoutGroupItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            var query = from p in web365db.tblLayoutGroup
                        where p.Name.ToLower().Contains(name) && p.IsDeleted == isDelete
                        select p;


            query = query.Where(p => p.LanguageId == (int)StaticEnum.LanguageId.Vietnamese);

            total = query.Count();

            query = descending ? QueryableHelper.OrderByDescending(query, propertyNameSort) : QueryableHelper.OrderBy(query, propertyNameSort);

            var result = query.Select(p => new LayoutGroupItem()
            {
                ID = p.ID,
                Name = p.Name,
                Description = p.Description,
                IsShow = p.IsShow,
                LanguageId = p.LanguageId,
                LanguageName = p.tblLanguage.Name,
                RootId = p.RootId
            }).Skip(currentRecord).Take(numberRecord).ToList();

            foreach (var group in result)
            {
                group.ListOtherLanguage =
                    web365db.tblLayoutGroup
                        .Where(c => c.IsDeleted == false && c.RootId == group.ID)
                        .Select(c => new LayoutGroupItem()
                        {
                            ID = c.ID,
                            Name = c.Name,
                            Description = c.Description,
                            IsShow = c.IsShow,
                            LanguageId = c.LanguageId,
                            LanguageName = c.tblLanguage.Name,
                            RootId = c.RootId
                        }).ToList();
            }

            return result;
        }

        public LayoutGroupItem GetSameGroupInOtherLang(int rootId, int language)
        {
            var query = from p in web365db.tblLayoutGroup
                        where p.IsShow == true && p.IsDeleted == false && p.LanguageId == language && p.RootId == rootId
                        select p;

            return query.OrderByDescending(p => p.ID).Select(p => new LayoutGroupItem()
            {
                ID = p.ID,
                Name = p.Name,
                Description = p.Description,
                IsShow = p.IsShow,
                LanguageId = p.LanguageId,
                LanguageName = p.tblLanguage.Name,
                RootId = p.RootId
            }).FirstOrDefault();
        }

        public void ResetListPicture(int id, string listPictureId)
        {
            throw new NotImplementedException();
        }

        public T GetListForTree<T>(int languageId = (int) StaticEnum.LanguageId.Vietnamese, bool isShow = true, bool isDelete = false)
        {
            var query = from p in web365db.tblLayoutGroup
                        where p.IsShow == isShow && p.IsDeleted == isDelete
                        select p;

            query = query.Where(p => p.LanguageId == languageId);

            return (T)(object)query.OrderByDescending(p => p.ID).Select(p => new LayoutGroupItem()
            {
                ID = p.ID,
                Name = p.Name,
                Description = p.Description,
                IsShow = p.IsShow,
                LanguageId = p.LanguageId,
                LanguageName = p.tblLanguage.Name,
                RootId = p.RootId
            }).ToList();
        }

        /// <summary>
        /// get product type item
        /// </summary>
        /// <param name="id">id of product type</param>
        /// <returns></returns>
        public T GetItemById<T>(int id)
        {
            var result = GetById<tblLayoutGroup>(id);

            return (T)(object)new LayoutGroupItem()
                        {
                            ID = result.ID,
                            Name = result.Name,
                            Description = result.Description,
                            IsShow = result.IsShow,
                            LanguageId = result.LanguageId,
                            LanguageName = result.tblLanguage.Name,
                            RootId = result.RootId
                        };
        }

        #region Insert, Edit, Delete, Save
        public void Show(int id)
        {
            var layoutGroup = web365db.tblLayoutGroup.SingleOrDefault(p => p.ID == id);
            layoutGroup.IsShow = true;
            web365db.Entry(layoutGroup).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        public void Hide(int id)
        {
            var layoutGroup = web365db.tblLayoutGroup.SingleOrDefault(p => p.ID == id);
            layoutGroup.IsShow = false;
            web365db.Entry(layoutGroup).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        //public void ResetListPicture(int id, string listPictureId)
        //{
        //    web365db.Database.SqlQuery<object>("EXEC [dbo].[PRC_ResetPictureLayoutGroup] {0}, {1}", id, listPictureId).FirstOrDefault();
        //}

        #endregion

        #region Check
        public bool NameExist(int id, string name, int languageId = (int) StaticEnum.LanguageId.Vietnamese)
        {
            var query = web365db.tblLayoutGroup.Count(c => c.Name.ToLower() == name.ToLower() && c.LanguageId == languageId && c.ID != id);

            return query > 0;
        }
        #endregion
    }
}
