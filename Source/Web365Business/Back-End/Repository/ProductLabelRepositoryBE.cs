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
    public class ProductLabelRepositoryBE : BaseBE<tblProductLabel>, IProductLabelRepositoryBE
    {
        private readonly IProductLabelDABERepository productLabelDABERepository;

        public ProductLabelRepositoryBE(IProductLabelDABERepository _productLabelDABERepository)
            : base(_productLabelDABERepository)
        {
            productLabelDABERepository = _productLabelDABERepository;
        }

        /// <summary>
        /// function get all data list
        /// </summary>
        /// <returns></returns>
        public List<ProductLabelItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return productLabelDABERepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }   
    }
}
