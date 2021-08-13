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
    public class PictureRepositoryBE : BaseBE<tblPicture>, IPictureRepositoryBE
    {
        private readonly IPictureDABERepository pictureDABERepository;

        public PictureRepositoryBE(IPictureDABERepository _pictureDABERepository)
            : base(_pictureDABERepository)
        {
            pictureDABERepository = _pictureDABERepository;
        }

        /// <summary>
        /// function get all data tblTypeProduct
        /// </summary>
        /// <returns></returns>
        public List<PictureItem> GetList(out int total, 
            string name, 
            string nameFile, 
            int[] typeId,
            int currentRecord, 
            int numberRecord, 
            string propertyNameSort,
            bool descending, 
            bool? isShow,
            bool isDelete = false)
        {
            return pictureDABERepository.GetList(out total, name, nameFile, typeId, currentRecord, numberRecord, propertyNameSort, descending, isShow, isDelete);
        }

        public List<PictureItem> GetListByArrayID(int[] arrId, bool isDelete = false)
        {
            return pictureDABERepository.GetListByArrayID(arrId, isDelete);
        }        
    }
}
