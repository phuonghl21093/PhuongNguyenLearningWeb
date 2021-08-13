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
    public class FileRepositoryBE : BaseBE<tblFile>, IFileRepositoryBE
    {

        private readonly IFileDABERepository fileDABERepository;

        public FileRepositoryBE(IFileDABERepository _fileDABERepository)
            : base(_fileDABERepository)
        {
            fileDABERepository = _fileDABERepository;
        }

        /// <summary>
        /// function get all data tblFile
        /// </summary>
        /// <returns></returns>
        public List<FileItem> GetList(out int total, string name, int[] typeId, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return fileDABERepository.GetList(out total, name, typeId, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }

        public List<FileItem> GetListByArrayID(int[] arrId, bool isDelete = false)
        {
            return fileDABERepository.GetListByArrayID(arrId, isDelete);
        }
        
    }
}
