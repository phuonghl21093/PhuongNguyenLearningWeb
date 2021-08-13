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
    public class MenuDABERepository : BaseBE<tblMenu>, IMenuDABERepository
    {

        /// <summary>
        /// function get all data tblFile
        /// </summary>
        /// <returns></returns>
        public List<MenuItem> GetList(out int total, string name, int? parentId, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            var query = from p in web365db.tblMenu
                        where p.Name.ToLower().Contains(name) && p.IsDeleted == isDelete
                        select p;

            query = query.Where(p => p.LanguageId == (int)StaticEnum.LanguageId.Vietnamese);

            if (parentId.HasValue)
            {
                query = query.Where(p => p.Parent == parentId);
            }

            total = query.Count();

            query = descending ? QueryableHelper.OrderByDescending(query, propertyNameSort) : QueryableHelper.OrderBy(query, propertyNameSort);

            var result = query.Select(p => new MenuItem()
            {
                ID = p.ID,
                Name = p.Name,
                NameAscii = p.NameAscii,
                DateCreated = p.DateCreated,
                DisplayOrder = p.DisplayOrder,
                IsShow = p.IsShow,
                LanguageId = p.LanguageId,
                LanguageName = p.tblLanguage.Name,
                RootId = p.RootId
            }).Skip(currentRecord).Take(numberRecord).ToList();

            foreach (var menuItem in result)
            {
                menuItem.ListOtherLanguage =
                    web365db.tblMenu.Where(c => c.IsDeleted == false && c.RootId == menuItem.ID)
                        .Select(c => new MenuItem()
                        {
                            ID = c.ID,
                            Name = c.Name,
                            NameAscii = c.NameAscii,
                            DateCreated = c.DateCreated,
                            DisplayOrder = c.DisplayOrder,
                            IsShow = c.IsShow,
                            LanguageId = c.LanguageId,
                            LanguageName = c.tblLanguage.Name,
                            RootId = c.RootId
                        }).ToList();
            }

            return result;
        }

        public MenuItem GetSameMenuInOtherLanguage(int rootId, int languageId)
        {
            var query = from c in web365db.tblMenu
                        where c.IsDeleted == false && c.IsShow == true && c.RootId == rootId && c.LanguageId == languageId
                        select new MenuItem()
                        {
                            ID = c.ID,
                            Name = c.Name,
                            NameAscii = c.NameAscii,
                            DateCreated = c.DateCreated,
                            DisplayOrder = c.DisplayOrder,
                            IsShow = c.IsShow,
                            LanguageId = c.LanguageId,
                            LanguageName = c.tblLanguage.Name,
                            RootId = c.RootId
                        };

            return query.FirstOrDefault();
        }

        public T GetItemById<T>(int id)
        {
            var query = from p in web365db.tblMenu
                        where p.ID == id
                        orderby p.ID descending
                        select new MenuItem()
                        {
                            ID = p.ID,
                            Name = p.Name,
                            NameAscii = p.NameAscii,
                            Link = p.Link,
                            Target = p.Target,
                            CssClass = p.CssClass,
                            Parent = p.Parent,
                            DisplayOrder = p.DisplayOrder,
                            DateCreated = p.DateCreated,
                            DateUpdated = p.DateUpdated,
                            CreatedBy = p.CreatedBy,
                            UpdatedBy = p.UpdatedBy,
                            IsShow = p.IsShow,
                            LanguageId = p.LanguageId,
                            LanguageName = p.tblLanguage.Name,
                            RootId = p.RootId
                        };

            return (T)(object)query.FirstOrDefault();
        }

        public void Show(int id)
        {
            var obj = web365db.tblMenu.SingleOrDefault(p => p.ID == id);
            obj.IsShow = true;
            web365db.Entry(obj).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        public void Hide(int id)
        {
            var obj = web365db.tblMenu.SingleOrDefault(p => p.ID == id);
            obj.IsShow = false;
            web365db.Entry(obj).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        #region Check
        public bool NameExist(int id, string name, int languageId = (int) StaticEnum.LanguageId.Vietnamese)
        {
            var query = web365db.tblMenu.Count(c => c.Name.ToLower() == name.ToLower() && c.LanguageId == languageId && c.ID != id);

            return query > 0;
        }
        #endregion

        public T GetListForTree<T>(int languageId = (int) StaticEnum.LanguageId.Vietnamese, bool isShow = true, bool isDelete = false)
        {
            var query = from p in web365db.tblMenu
                        where p.IsShow == isShow && p.IsDeleted == isDelete
                        select p;

            query = query.Where(p => p.LanguageId == languageId);

            return (T)(object)query.OrderByDescending(p => p.ID).Select(p => new MenuItem()
            {
                ID = p.ID,
                Name = p.Name,
                NameAscii = p.NameAscii,
                Parent = p.Parent,
                DisplayOrder = p.DisplayOrder,
                LanguageId = p.LanguageId,
                LanguageName = p.tblLanguage.Name,
                RootId = p.RootId
            }).ToList();
        }
    }
}
