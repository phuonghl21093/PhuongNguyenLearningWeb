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
    public class ProductTypeRepositoryBE : BaseBE<tblTypeProduct>, IProductTypeRepositoryBE
    {
        private readonly IProductTypeDABERepository productTypeDABERepository;

        public ProductTypeRepositoryBE(IProductTypeDABERepository _productTypeDABERepository)
            : base(_productTypeDABERepository)
        {
            productTypeDABERepository = _productTypeDABERepository;
        }

        /// <summary>
        /// function get all data list
        /// </summary>
        /// <returns></returns>
        public List<ProductTypeItem> GetList(out int total, string name, int? parentId, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return productTypeDABERepository.GetList(out total, name, parentId, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }

        public List<ProductTypeItem> GetListOfGroup(int groupId, bool isShow = true, bool isDelete = false)
        {
            return productTypeDABERepository.GetListOfGroup(groupId, isShow, isDelete);
        }

        public void ResetListPicture(int id, string listPictureId)
        {
            productTypeDABERepository.ResetListPicture(id, listPictureId);
        }
        
    }
}
