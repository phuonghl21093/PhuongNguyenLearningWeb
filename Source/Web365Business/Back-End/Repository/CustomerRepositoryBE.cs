using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Business.Back_End.IRepository;
using Web365DA.RDBMS.Back_End.IRepository;
using Web365Domain;
using Web365Utility;


namespace Web365Business.Back_End.Repository
{
    public class CustomerRepositoryBE : BaseBE<tblCustomer>, ICustomerRepositoryBE
    {

        private readonly ICustomerDABERepository customerDABERepository;

        public CustomerRepositoryBE(ICustomerDABERepository _customerDABERepository)
            : base(_customerDABERepository)
        {
            customerDABERepository = _customerDABERepository;
        }

        /// <summary>
        /// function get all data tblCustomer
        /// </summary>
        /// <returns></returns>
        public List<CustomerItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return customerDABERepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }        
    }
}
