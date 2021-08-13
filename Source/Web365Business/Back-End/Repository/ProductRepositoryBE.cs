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
    public class ProductRepositoryBE : BaseBE<tblProduct>, IProductRepositoryBE
    {
        private readonly IProductDABERepository productDABERepository;

        public ProductRepositoryBE(IProductDABERepository _productDABERepository)
            : base(_productDABERepository)
        {
            productDABERepository = _productDABERepository;
        }

        /// <summary>
        /// function get all data tblProduct
        /// </summary>
        /// <returns></returns>
        public List<ProductItem> GetList(out int total, 
            string name, 
            string serial, 
            int? typeId, 
            int? manuId, 
            int? distributorId, 
            int? statusId, 
            int? labelId, 
            int currentRecord, 
            int numberRecord, 
            string propertyNameSort, 
            bool descending, 
            bool isDelete = false)
        {
            return productDABERepository.GetList(out total, name, serial, typeId, manuId, distributorId, statusId, labelId, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }

        public ProductItem GetEditFormFilter(int productId)
        {
            return productDABERepository.GetEditFormFilter(productId);
        }        

        public void LabelForProduct(int productId, int[] labelId)
        {
            productDABERepository.LabelForProduct(productId, labelId);
        }

        public void FilterForProduct(int productId, string filterId)
        {
            productDABERepository.FilterForProduct(productId, filterId);
        }

        public void ResetListPicture(int id, string listPictureId)
        {
            productDABERepository.ResetListPicture(id, listPictureId);
        }

        public void ResetListFile(int id, string listFileId)
        {
            productDABERepository.ResetListFile(id, listFileId);
        }        
    }
}
