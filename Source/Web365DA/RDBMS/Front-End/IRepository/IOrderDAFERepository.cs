using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Domain;

namespace Web365DA.RDBMS.Front_End.IRepository
{
    public interface IOrderDAFERepository
    {
        bool AddOrder(tblOrder order, List<tblOrderDetail> orderDetail, tblOrder_Shipping orderShipping);
        List<OrderItem> GetListOrderByCustomer(int customerId);
    }
}
