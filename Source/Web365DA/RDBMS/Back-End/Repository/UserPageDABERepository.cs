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
    public class UserPageDABERepository : BaseBE<tblPage>, IUserPageDABERepository
    {
        /// <summary>
        /// function get all data tblPage
        /// </summary>
        /// <returns></returns>
        public List<PageItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            var query = from p in web365db.tblPage
                        where p.IsDeleted == isDelete
                        where p.Name.ToLower().Contains(name)
                        select p;

            total = query.Count();

            query = descending ? QueryableHelper.OrderByDescending(query, propertyNameSort) : QueryableHelper.OrderBy(query, propertyNameSort);

            return query.Select(p => new PageItem()
            {
                ID = p.ID,
                Name = p.Name,
                Link = p.Link,
                ClassAtrtibute = p.ClassAtrtibute,
                Number = p.Number.HasValue ? p.Number.Value : 0,
                Parent = p.Parent.HasValue ? p.Parent.Value : 0,
                HasChild = p.HasChild.HasValue && p.HasChild.Value,
                IsShow = p.IsShow.HasValue && p.IsShow.Value
            }).Skip(currentRecord).Take(numberRecord).ToList();
        }

        public T GetListForTree<T>(int languageId = (int) StaticEnum.LanguageId.Vietnamese, bool isShow = true, bool isDelete = false)
        {
            var query = from p in web365db.tblPage
                        where p.IsShow == isShow && p.IsDeleted == isDelete
                        orderby p.ID ascending
                        select new PageItem()
                        {
                            ID = p.ID,
                            Name = p.Name,
                            Parent = p.Parent.HasValue ? p.Parent.Value : 0
                        };

            return (T)(object)query.ToList();
        }

        public List<PageItem> GetPageOfUser(string userId)
        {
            var query = web365db.Database.SqlQuery<PageItem>("EXEC PRC_GetMenu {0}", userId);

            return query.ToList();
        }

        public T GetItemById<T>(int id)
        {
            var result = GetById<tblPage>(id);

            return (T)(object)new PageItem()
            {
                ID = result.ID,
                Name = result.Name,
                Link = result.Link,
                ClassAtrtibute = result.ClassAtrtibute,
                Parent = result.Parent.HasValue ? result.Parent.Value : 0,
                HasChild = result.HasChild.HasValue && result.HasChild.Value,
                IsShow = result.IsShow.HasValue && result.IsShow.Value,
                Number = result.Number.HasValue ? result.Number.Value : 0,
                Detail = result.Detail,
                CreatedBy = result.CreateBy,
                UpdatedBy = result.UpdateBy,
                DateCreated = result.DateCreated.HasValue ? result.DateCreated.Value : DateTime.Now,
                DateUpdated = result.DateUpdated.HasValue ? result.DateUpdated.Value : DateTime.Now
            };
        }

        #region Check
        public bool NameExist(int id, string name, int languageId = (int) StaticEnum.LanguageId.Vietnamese)
        {
            var query = web365db.tblPage.Count(c => c.Name.ToLower() == name.ToLower() && c.ID != id);

            return query > 0;
        }
        #endregion
    }
}
