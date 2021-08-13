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
    public class CommentDABERepository : BaseBE<tblComment>, ICommentDABERepository
    {
        /// <summary>
        /// function get all data tblComment
        /// </summary>
        /// <returns></returns>
        public List<CommentItem> GetList(out int total, string url, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            var query = from p in web365db.tblComment
                        where p.Url.ToLower().Contains(url) && p.IsDeleted == isDelete
                        select p;

            total = query.Count();

            query = descending ? QueryableHelper.OrderByDescending(query, propertyNameSort) : QueryableHelper.OrderBy(query, propertyNameSort);

            return query.Select(p => new CommentItem()
            {
                ID = p.ID,
                Detail = p.Detail,
                DateCreated = p.DateCreated.HasValue ? p.DateCreated.Value : DateTime.Now,
                IsShow = p.IsShow.HasValue && p.IsShow.Value,
                IsApprove = p.IsApprove.HasValue && p.IsApprove.Value,
                IsReslove = p.IsReslove.HasValue && p.IsReslove.Value,
                Customer = new CustomerItem()
                {
                    LastName = p.tblCustomer.LastName
                }
            }).Skip(currentRecord).Take(numberRecord).ToList();
        }

        public T GetListForTree<T>(int languageId = (int) StaticEnum.LanguageId.Vietnamese, bool isShow = true, bool isDelete = false)
        {
            return default(T);
        }

        public T GetItemById<T>(int id)
        {
            var query = from p in web365db.tblComment
                        where p.ID == id
                        orderby p.ID descending
                        select new CommentItem()
                        {
                            ID = p.ID,
                            Detail = p.Detail,
                            Url = p.Url,
                            DateCreated = p.DateCreated.HasValue ? p.DateCreated.Value : DateTime.Now,
                            IsShow = p.IsShow.HasValue && p.IsShow.Value,
                            IsApprove = p.IsApprove.HasValue && p.IsApprove.Value
                        };
            return (T)(object)query.FirstOrDefault();
        }

        public void Show(int id)
        {
            var Customer = web365db.tblCustomer.SingleOrDefault(p => p.ID == id);
            Customer.IsActive = true;
            web365db.Entry(Customer).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        public void Hide(int id)
        {
            var Customer = web365db.tblCustomer.SingleOrDefault(p => p.ID == id);
            Customer.IsActive = false;
            web365db.Entry(Customer).State = EntityState.Modified;
            web365db.SaveChanges();
        }


        public bool NameExist(int id, string name, int languageId = (int) StaticEnum.LanguageId.Vietnamese)
        {
            return false;
        }

        public void UpdateFieldWithName(int id, string name, object value)
        {
            web365db.Database.ExecuteSqlCommand("UPDATE tblComment SET " + name + " = {0} WHERE ID = {1}", value, id);
        }
    }
}
