using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using Web365Base;
using Web365Business.Back_End.IRepository;
using Web365DA.RDBMS.Back_End.IRepository;
using Web365Domain;
using Web365Utility;

namespace Web365Business.Back_End.Repository
{
    /// <summary>
    /// create by BienLV 05-01-2013
    /// </summary>
    public class ContactRepositoryBE : BaseBE<tblContact>, IContactRepositoryBE
    {

        private readonly IContactDABERepository contactDABERepository;

        public ContactRepositoryBE(IContactDABERepository _contactDABERepository)
            :base(_contactDABERepository)
        {
            contactDABERepository = _contactDABERepository;
        }

        /// <summary>
        /// function get all data tblTypeProduct
        /// </summary>
        /// <returns></returns>
        public List<ContactItem> GetList(out int total, string name, string title, DateTime? fromDate, DateTime? toDate, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return contactDABERepository.GetList(out total, name, title, fromDate, toDate, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }        
    }
}
