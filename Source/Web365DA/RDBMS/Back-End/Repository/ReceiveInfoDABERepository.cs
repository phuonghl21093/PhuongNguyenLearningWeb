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
    public class ReceiveInfoDABERepository : BaseBE<tblReceiveInfo>, IReceiveInfoDABERepository
    {
        /// <summary>
        /// function get all data tblTypeProduct
        /// </summary>
        /// <returns></returns>
        public List<ReceiveInfoItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            var query = from p in web365db.tblReceiveInfo
                        where p.Name.ToLower().Contains(name) && p.IsDeleted == isDelete
                        orderby p.ID descending
                        select new ReceiveInfoItem()
                        {
                            ID = p.ID,
                            Name = p.Name,
                            IsShow = p.IsShow
                        };

            total = query.Count();

            query = descending ? QueryableHelper.OrderByDescending(query, propertyNameSort) : QueryableHelper.OrderBy(query, propertyNameSort);

            return query.Skip(currentRecord).Take(numberRecord).ToList();
        }

        public T GetListForTree<T>(int languageId = (int) StaticEnum.LanguageId.Vietnamese, bool isShow = true, bool isDelete = false)
        {
            var query = from p in web365db.tblReceiveInfo
                        where p.IsShow == isShow && p.IsDeleted == isDelete
                        orderby p.ID descending
                        select new ReceiveInfoItem()
                        {
                            ID = p.ID,
                            Name = p.Name,
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
            var result = GetById<tblReceiveInfo>(id);

            return (T)(object)new ReceiveInfoItem()
                        {
                            ID = result.ID,
                            Name = result.Name,
                            Email = result.Email,
                            Phone = result.Phone,
                            DateCreated = result.DateCreated,
                            IsShow = result.IsShow
                        };
        }        

        #region Insert, Edit, Delete, Save

        public void Show(int id)
        {
            var ReceiveInfo = web365db.tblReceiveInfo.SingleOrDefault(p => p.ID == id);
            ReceiveInfo.IsShow = true;
            web365db.Entry(ReceiveInfo).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        public void Hide(int id)
        {
            var ReceiveInfo = web365db.tblReceiveInfo.SingleOrDefault(p => p.ID == id);
            ReceiveInfo.IsShow = false;
            web365db.Entry(ReceiveInfo).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        #endregion

        #region Check
        public bool NameExist(int id, string name, int languageId = (int) StaticEnum.LanguageId.Vietnamese)
        {
            var query = web365db.tblReceiveInfo.Count(c => c.Name.ToLower() == name.ToLower() && c.ID != id);

            return query > 0;
        }
        #endregion
    }
}
