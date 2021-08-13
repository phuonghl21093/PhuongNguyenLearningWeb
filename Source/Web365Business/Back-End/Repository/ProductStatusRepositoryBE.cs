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
    public class ProductStatusRepositoryBE : BaseBE<tblProductStatus>, IProductStatusRepositoryBE
    {

        private readonly IProductStatusDABERepository productStatusDABERepository;

        public ProductStatusRepositoryBE(IProductStatusDABERepository _productStatusDABERepository)
            : base(_productStatusDABERepository)
        {
            productStatusDABERepository = _productStatusDABERepository;
        }

        /// <summary>
        /// function get all data tblProductStatus
        /// </summary>
        /// <returns></returns>
        public List<ProductStatusItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return productStatusDABERepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }        
    }
}
