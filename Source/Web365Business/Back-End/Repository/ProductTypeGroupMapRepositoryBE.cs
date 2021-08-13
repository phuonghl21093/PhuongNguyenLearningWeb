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
    public class ProductTypeGroupMapRepositoryBE : BaseBE<tblProductType_Group_Map>, IProductTypeGroupMapRepositoryBE
    {

        private readonly IProductTypeGroupMapDABERepository productTypeGroupMapDABERepository;

        public ProductTypeGroupMapRepositoryBE(IProductTypeGroupMapDABERepository _productTypeGroupMapDABERepository)
        {
            productTypeGroupMapDABERepository = _productTypeGroupMapDABERepository;
        }

        public void ResetGroupOfType(int TypeId, int[] groupId)
        {
            productTypeGroupMapDABERepository.ResetGroupOfType(TypeId, groupId);
        }

        public void ResetTypeOfGroup(int groupId, int[] TypeId)
        {
            productTypeGroupMapDABERepository.ResetTypeOfGroup(groupId, TypeId);
        }
    }
}
