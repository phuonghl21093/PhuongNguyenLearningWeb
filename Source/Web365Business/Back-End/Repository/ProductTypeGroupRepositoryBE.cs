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
    public class ProductTypeGroupRepositoryBE : BaseBE<tblProductTypeGroup>, IProductTypeGroupRepositoryBE
    {
        private readonly IProductTypeGroupDABERepository productTypeGroupDABERepository;

        public ProductTypeGroupRepositoryBE(IProductTypeGroupDABERepository _productTypeGroupDABERepository)
            : base(_productTypeGroupDABERepository)
        {
            productTypeGroupDABERepository = _productTypeGroupDABERepository;
        }

        /// <summary>
        /// function get all data list
        /// </summary>
        /// <returns></returns>
        public List<ProductTypeGroupItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return productTypeGroupDABERepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }     
    }
}
