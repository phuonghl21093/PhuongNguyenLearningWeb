using System;
using System.Collections.Generic;
using System.Data;
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
    public class PictureTypeRepositoryBE : BaseBE<tblTypePicture>, IPictureTypeRepositoryBE
    {

        private readonly IPictureTypeDABERepository pictureTypeDABERepository;

        public PictureTypeRepositoryBE(IPictureTypeDABERepository _pictureTypeDABERepository)
            : base(_pictureTypeDABERepository)
        {
            pictureTypeDABERepository = _pictureTypeDABERepository;
        }

        public PictureTypeItem GetSameTypeByLanguage(int id, int languageId)
        {
            return pictureTypeDABERepository.GetSameTypeByLanguage(id, languageId);
        }

        /// <summary>
        /// function get all data list
        /// </summary>
        /// <returns></returns>
        public List<PictureTypeItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return pictureTypeDABERepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }        
    }
}
