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
    public class UserRoleDABERepository : BaseBE<AspNetRoles>, IUserRoleDABERepository
    {
        /// <summary>
        /// function get all data webpages_Roles
        /// </summary>
        /// <returns></returns>
        public List<AspNetRoleItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            var query = from p in web365db.AspNetRoles
                        where p.Name.ToLower().Contains(name)
                        select p;

            total = query.Count();

            query = descending ? QueryableHelper.OrderByDescending(query, propertyNameSort) : QueryableHelper.OrderBy(query, propertyNameSort);

            return query.Select(p => new AspNetRoleItem()
            {
                Id = p.Id,
                Name = p.Name
            }).Skip(currentRecord).Take(numberRecord).ToList();
        }

        public T GetListForTree<T>(int languageId = (int) StaticEnum.LanguageId.Vietnamese, bool isShow = true, bool isDelete = false)
        {
            var query = from p in web365db.AspNetRoles
                        orderby p.Id ascending
                        select new AspNetRoleItem()
                        {
                            Id = p.Id,
                            Name = p.Name
                        };

            return (T)(object)query.ToList();
        }

        public T GetItemById<T>(int id)
        {
            return default(T);
        }

        public bool NameExist(int id, string name, int languageId = 1)
        {
            throw new NotImplementedException();
        }

        public T GetItemById<T>(string id)
        {
            var result = GetById<AspNetRoles>(id);

            return (T)(object)new AspNetRoleItem()
            {
                Id = result.Id,
                Name = result.Name,
                ListPageId = result.tblPage.Select(p => p.ID).ToArray()
            };
        }

        public void PageForRole(string roleId, int[] pageId)
        {
            var role = GetById<AspNetRoles>(roleId);

            role.tblPage.Clear();

            web365db.SaveChanges();

            foreach (var item in pageId)
            {
                if (item > 0)
                {
                    var query = web365db.Database.SqlQuery<object>("EXEC PRC_InsertPageForRole {0}, {1}", roleId, item);

                    query.FirstOrDefault();
                }
            }
        }

        public bool NameExist(string id, string name)
        {
            throw new NotImplementedException();
        }

        public bool NameExist(int id, string name)
        {
            return false;
        }

        public bool NameExist(string id, string name, int languageId = (int) StaticEnum.LanguageId.Vietnamese)
        {
            var query = web365db.AspNetRoles.Count(c => c.Name.ToLower() == name.ToLower() && c.Id != id);

            return query > 0;
        }
    }
}
