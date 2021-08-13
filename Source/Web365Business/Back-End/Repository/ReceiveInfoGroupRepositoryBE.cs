using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    public class ReceiveInfoGroupRepositoryBE : BaseBE<tblReceiveInfoGroup>, IReceiveInfoGroupRepositoryBE
    {
        private readonly IReceiveInfoGroupDABERepository receiveInfoGroupRepositoryBE;

        public ReceiveInfoGroupRepositoryBE(IReceiveInfoGroupDABERepository _receiveInfoGroupRepositoryBE)
            : base(_receiveInfoGroupRepositoryBE)
        {
            receiveInfoGroupRepositoryBE = _receiveInfoGroupRepositoryBE;
        }

        /// <summary>
        /// function get all data list
        /// </summary>
        /// <returns></returns>
        public List<ReceiveInfoGroupItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return receiveInfoGroupRepositoryBE.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }
    }
}
