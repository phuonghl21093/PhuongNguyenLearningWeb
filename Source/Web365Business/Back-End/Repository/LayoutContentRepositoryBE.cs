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
    public class LayoutContentRepositoryBE : BaseBE<tblLayoutContent>, ILayoutContentRepositoryBE
    {
        private readonly ILayoutContentDABERepository layoutContentDABERepository;

        public LayoutContentRepositoryBE(ILayoutContentDABERepository _layoutContentDABERepository)
            : base(_layoutContentDABERepository)
        {
            layoutContentDABERepository = _layoutContentDABERepository;
        }

        /// <summary>
        /// function get all data tblTypeProduct
        /// </summary>
        /// <returns></returns>
        public List<LayoutContentItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return layoutContentDABERepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }

        public void ResetListPicture(int id, string listPictureId)
        {
            layoutContentDABERepository.ResetListPicture(id, listPictureId);
        }
    }
}
