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
    public class UserDABERepository : BaseBE<AspNetUsers>, IUserDABERepository
    {
        /// <summary>
        /// function get all data UserProfile
        /// </summary>
        /// <returns></returns>
        public List<AspNetUserItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            var query = from p in Web365DB.AspNetUsers
                        where p.UserName.ToLower().Contains(name)
                        select p;

            total = query.Count();

            query = descending ? QueryableHelper.OrderByDescending(query, propertyNameSort) : QueryableHelper.OrderBy(query, propertyNameSort);

            return query.Select(p => new AspNetUserItem()
            {
                Id = p.Id,
                UserName = p.UserName,
                Email = p.Email,
                PhoneNumber = p.PhoneNumber

            }).Skip(currentRecord).Take(numberRecord).ToList();
        }

        public T GetListForTree<T>(bool isActive = true, bool isDelete = false)
        {
            var query = from p in Web365DB.AspNetUsers
                        //where p.IsActive == isActive && p.IsDeleted == p.IsDeleted
                        orderby p.UserName ascending
                        select new AspNetUserItem()
                        {
                            Id = p.Id,
                            UserName = p.UserName
                        };

            return (T)(object)query.ToList();
        }

        public T GetItemById<T>(string id)
        {
            var result = GetById<AspNetUsers>(id);

            return (T)(object)new AspNetUserItem()
            {
                Id = result.Id,
                UserName = result.UserName,
                Email = result.Email,
                PhoneNumber = result.PhoneNumber,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Gender = result.Gender.HasValue && result.Gender.Value,
                Address = result.Address,
                Note = result.Note,
                ListRoleId = result.AspNetRoles.Select(r => r.Id).ToArray()
            };
        }

        public T GetListForTree<T>(int languageId = 1, bool isShow = true, bool isDelete = false)
        {
            throw new NotImplementedException();
        }

        public T GetItemById<T>(int id)
        {
            var result = GetById<AspNetUsers>(id);

            return (T)(object)new AspNetUserItem()
            {
                Id = result.Id,
                UserName = result.UserName,
                Email = result.Email,
                PhoneNumber = result.PhoneNumber,
                ListRoleId = result.AspNetRoles.Select(r => r.Id).ToArray()
            };
        }

        public bool NameExist(int id, string name, int languageId = 1)
        {
            throw new NotImplementedException();
        }

        public T GetByUserName<T>(string userName)
        {
            var result = Web365DB.AspNetUsers.FirstOrDefault(u => u.UserName == userName);

            return (T)(object)new AspNetUserItem()
            {
                Id = result.Id,
                UserName = result.UserName,
                Email = result.Email,
                PhoneNumber = result.PhoneNumber,
                ListRoleId = result.AspNetRoles.Select(r => r.Id).ToArray()
            };
        }

        public void RoleForUser(string userId, string[] roleId)
        {
            var user = GetById<AspNetUsers>(userId);

            user.AspNetRoles.Clear();

            Web365DB.SaveChanges();

            foreach (var item in roleId)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    var query = Web365DB.Database.SqlQuery<object>("EXEC PRC_InsertRoleForUser {0}, {1}", userId, item);

                    query.FirstOrDefault();
                }
            }
        }

        public bool NameExist(int id, string name)
        {
            return false;
        }

        public bool NameExist(string id, string name)
        {
            var query = Web365DB.AspNetUsers.Count(c => c.UserName.ToLower() == name.ToLower() && c.Id != id);

            return query > 0;
        }

        public void Delete(string[] ids)
        {
            if(ids != null && ids.Length > 0)
            {
                var users = Web365DB.AspNetUsers.Where(u => ids.Contains(u.Id)).ToList();
                if(users != null && users.Count > 0)
                {
                    Web365DB.AspNetUsers.RemoveRange(users);
                    Web365DB.SaveChanges();
                }
            }
        }
    }
}
