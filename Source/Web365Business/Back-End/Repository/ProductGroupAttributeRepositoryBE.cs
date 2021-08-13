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
    public class ProductGroupAttributeRepositoryBE : BaseBE<tblProductGroupAttribute>, IProductGroupAttributeRepositoryBE
    {
        private readonly IProductGroupAttributeDABERepository productGroupAttributeDABERepository;

        public ProductGroupAttributeRepositoryBE(IProductGroupAttributeDABERepository _productGroupAttributeDABERepository)
            : base(_productGroupAttributeDABERepository)
        {
            productGroupAttributeDABERepository = _productGroupAttributeDABERepository;
        }

        /// <summary>
        /// function get all data tblTypeProduct
        /// </summary>
        /// <returns></returns>
        public List<ProductGroupAttributeItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return productGroupAttributeDABERepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }

        public List<ProductGroupAttributeItem> GetListByProductType(int typeId, bool isShow = true, bool isDelete = false)
        {
            return productGroupAttributeDABERepository.GetListByProductType(typeId, isShow, isDelete);
        }
    }
}
