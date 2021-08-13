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
    public class SupportRepositoryBE : BaseBE<tblSupport>, ISupportRepositoryBE
    {
        private readonly ISupportDABERepository supportDABERepository;

        public SupportRepositoryBE(ISupportDABERepository _supportDABERepository)
            : base(_supportDABERepository)
        {
            supportDABERepository = _supportDABERepository;
        }

        /// <summary>
        /// function get all data list
        /// </summary>
        /// <returns></returns>
        public List<SupportItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return supportDABERepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }
    }
}
