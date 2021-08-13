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
    public class ProductAttributeRepositoryBE : BaseBE<tblProductAttribute>, IProductAttributeRepositoryBE
    {
        private readonly IProductAttributeDABERepository productAttributeDABERepository;

        public ProductAttributeRepositoryBE(IProductAttributeDABERepository _productAttributeDABERepository)
            : base(_productAttributeDABERepository)
        {
            productAttributeDABERepository = _productAttributeDABERepository;
        }

        /// <summary>
        /// function get all data tblTypeProduct
        /// </summary>
        /// <returns></returns>
        public List<ProductAttributeItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return productAttributeDABERepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }

        public List<ProductAttributeItem> GetListByProductType(int typeId, int productId, bool isShow = true, bool isDelete = false)
        {
            return productAttributeDABERepository.GetListByProductType(typeId, productId, isShow, isDelete);
        }

        public void AddProductAttribute(int productId, int attributeId, string value)
        {
            productAttributeDABERepository.AddProductAttribute(productId, attributeId, value);
        }
    }
}
