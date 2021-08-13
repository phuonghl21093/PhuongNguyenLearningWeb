using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Domain;

namespace Web365DA.RDBMS.Back_End.IRepository
{
    public interface IProductAttributeDABERepository : IBaseDABERepository
    {
        List<ProductAttributeItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false);
        List<ProductAttributeItem> GetListByProductType(int typeId, int productId, bool isShow = true, bool isDelete = false);
        void AddProductAttribute(int productId, int attributeId, string value);
    }
}
