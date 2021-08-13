using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Business.Front_End.IRepository;
using Web365DA.RDBMS.Front_End.IRepository;
using Web365Domain;
using Web365Utility;

namespace Web365Business.Front_End.Repository
{
    public class OrderRepositoryFE : BaseFE, IOrderRepositoryFE
    {
        private readonly IOrderDAFERepository orderDAFERepository;

        public OrderRepositoryFE(IOrderDAFERepository _orderDAFERepository)
        {
            orderDAFERepository = _orderDAFERepository;
        }

        public bool AddOrder(tblOrder order, List<tblOrderDetail> orderDetail, tblOrder_Shipping orderShipping)
        {
            return orderDAFERepository.AddOrder(order, orderDetail, orderShipping); 
        }

        public List<OrderItem> GetListOrderByCustomer(int customerId)
        {
            var key = string.Format("OrderRepositoryFE{0}{1}}", "GetListOrderByCustomer", customerId);

            var item = new List<OrderItem>();

            var isDataCache = TryGetCache<List<OrderItem>>(out item, key);

            if (!isDataCache)
            {
                item = orderDAFERepository.GetListOrderByCustomer(customerId);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }
    }
}
