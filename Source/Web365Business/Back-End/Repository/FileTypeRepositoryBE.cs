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
    public class FileTypeRepositoryBE : BaseBE<tblTypeFile>, IFileTypeRepositoryBE
    {

        private readonly IFileTypeDABERepository fileTypeDABERepository;

        public FileTypeRepositoryBE(IFileTypeDABERepository _fileTypeDABERepository)
            : base(_fileTypeDABERepository)
        {
            fileTypeDABERepository = _fileTypeDABERepository;
        }

        public FileTypeItem GetSameTypeByLanguage(int rootId, int langId)
        {
            return fileTypeDABERepository.GetSameTypeByLanguage(rootId, langId);
        }

        /// <summary>
        /// function get all data tblTypeFile
        /// </summary>
        /// <returns></returns>
        public List<FileTypeItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return fileTypeDABERepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }        
    }
}
