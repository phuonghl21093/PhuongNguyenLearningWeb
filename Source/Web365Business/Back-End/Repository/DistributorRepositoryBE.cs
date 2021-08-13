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
    public class DistributorRepositoryBE : BaseBE<tblDistributor>, IDistributorRepositoryBE
    {
        private readonly IDistributorDABERepository distributorDABERepository;

        public DistributorRepositoryBE(IDistributorDABERepository _distributorDABERepository)
            : base(_distributorDABERepository)
        {
            distributorDABERepository = _distributorDABERepository;
        }

        /// <summary>
        /// function get all data tblDistributor
        /// </summary>
        /// <returns></returns>
        public List<ProductDistributorItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return distributorDABERepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }
        
    }
}
