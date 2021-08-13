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
using Web365Domain.Other;
using Web365Utility;

namespace Web365Business.Back_End.Repository
{
    /// <summary>
    /// create by BienLV 05-01-2013
    /// </summary>
    public class GroupLayoutContentRepositoryBE : BaseBE<tblLayoutGroup>, IGroupLayoutContentRepositoryBE
    {
        private readonly IGroupLayoutContentDABERepository groupLayoutDABERepository;

        public GroupLayoutContentRepositoryBE(IGroupLayoutContentDABERepository groupLayoutDabeRepository)
            : base(groupLayoutDabeRepository)
        {
            groupLayoutDABERepository = groupLayoutDabeRepository;
        }

        /// <summary>
        /// function get all data tblTypeProduct
        /// </summary>
        /// <returns></returns>
        public List<LayoutGroupItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return groupLayoutDABERepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }

        public void ResetListPicture(int id, string listPictureId)
        {
            groupLayoutDABERepository.ResetListPicture(id, listPictureId);
        }

        public LayoutGroupItem GetSameGroupInOtherLang(int rootId, int language)
        {
            return groupLayoutDABERepository.GetSameGroupInOtherLang(rootId, language);
        }
    }
}
