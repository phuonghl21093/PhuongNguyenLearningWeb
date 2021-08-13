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
    public class AdvertiesRepositoryBE : BaseBE<tblAdvertise>, IAdvertiesRepositoryBE
    {
        private readonly IAdvertiesDABERepository advertiesDABERepository;

        public AdvertiesRepositoryBE(IAdvertiesDABERepository _advertiesDABERepository)
            : base(_advertiesDABERepository)
        {
            advertiesDABERepository = _advertiesDABERepository;
        }

        /// <summary>
        /// function get all data tblAdvertise
        /// </summary>
        /// <returns></returns>
        public List<AdvertiesItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return advertiesDABERepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }
    }
}
