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
    public class ProductFilterRepositoryBE : BaseBE<tblProductFilter>, IProductFilterRepositoryBE
    {
        private readonly IProductFilterDABERepository productFilterDABERepository;

        public ProductFilterRepositoryBE(IProductFilterDABERepository _productFilterDABERepository)
            : base(_productFilterDABERepository)
        {
            productFilterDABERepository = _productFilterDABERepository;
        }

        /// <summary>
        /// function get all data tblTypeProduct
        /// </summary>
        /// <returns></returns>
        public List<ProductFilterItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return productFilterDABERepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }   
    }
}
