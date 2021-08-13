using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Domain;

namespace Web365DA.RDBMS.Back_End.IRepository
{
    public interface IProductVariantDABERepository : IBaseDABERepository
    {
        List<ProductVariantItem> GetList(out int total,
            string name,
            string code,
            int? productId,
            int currentRecord,
            int numberRecord,
            string propertyNameSort,
            bool descending,
            bool isDelete = false);
    }
}
