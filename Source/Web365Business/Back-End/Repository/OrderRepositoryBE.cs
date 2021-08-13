using System;
using System.Collections.Generic;
using System.Data;
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
    public class OrderRepositoryBE : BaseBE<tblOrder>, IOrderRepositoryBE
    {
        private readonly IOrderDABERepository orderDABERepository;

        public OrderRepositoryBE(IOrderDABERepository _orderDABERepository)
            : base(_orderDABERepository)
        {
            orderDABERepository = _orderDABERepository;
        }

        /// <summary>
        /// function get all data tblTypeProduct
        /// </summary>
        /// <returns></returns>
        public List<OrderItem> GetList(out int total,
            int? id,
            string customerName, 
            string email,
            string phone,
            int? statusId,
            DateTime? fromDate,
            DateTime? toDate,
            int currentRecord, 
            int numberRecord, 
            string propertyNameSort,
            bool descending,
            bool? isViewed,
            bool isDelete = false)
        {
            return orderDABERepository.GetList(out total, id, customerName, email, phone, statusId, fromDate, toDate, currentRecord, numberRecord, propertyNameSort, descending, isViewed, isDelete);
        }
        
    }
}
