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
    public class SupportTypeRepositoryBE : BaseBE<tblTypeSupport>, ISupportTypeRepositoryBE
    {
        private readonly ISupportTypeDABERepository supportTypeDABERepository;

        public SupportTypeRepositoryBE(ISupportTypeDABERepository _supportTypeDABERepository)
            : base(_supportTypeDABERepository)
        {
            supportTypeDABERepository = _supportTypeDABERepository;
        }

        /// <summary>
        /// function get all data list
        /// </summary>
        /// <returns></returns>
        public List<SupportTypeItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return supportTypeDABERepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }
    }
}
