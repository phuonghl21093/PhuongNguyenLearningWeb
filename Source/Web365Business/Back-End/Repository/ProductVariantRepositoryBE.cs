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
    public class ProductVariantRepositoryBE : BaseBE<tblProduct_Variant>, IProductVariantRepositoryBE
    {
        private readonly IProductVariantDABERepository productVariantDABERepository;

        public ProductVariantRepositoryBE(IProductVariantDABERepository _productVariantDABERepository)
            : base(_productVariantDABERepository)
        {
            productVariantDABERepository = _productVariantDABERepository;
        }

        /// <summary>
        /// function get all data list
        /// </summary>
        /// <returns></returns>
        public List<ProductVariantItem> GetList(out int total, string name, string code, int? productId, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return productVariantDABERepository.GetList(out total, name, code, productId, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }
    }
}
