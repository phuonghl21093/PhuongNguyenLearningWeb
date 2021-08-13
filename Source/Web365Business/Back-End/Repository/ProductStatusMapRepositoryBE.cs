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
    public class ProductStatusMapRepositoryBE : BaseBE<tblProduct_Status_Map>, IProductStatusMapRepositoryBE
    {
        private readonly IProductStatusMapDABERepository productStatusMapDABERepository;

        public ProductStatusMapRepositoryBE(IProductStatusMapDABERepository _productStatusMapDABERepository)
        {
            productStatusMapDABERepository = _productStatusMapDABERepository;
        }

        public void ResetStatusOfProduct(int productId, int[] statusId)
        {
            productStatusMapDABERepository.ResetStatusOfProduct(productId, statusId);
        }

        public void ResetProductOfProduct(int statusId, int[] productId)
        {
            productStatusMapDABERepository.ResetProductOfProduct(statusId, productId);
        }

    }
}
