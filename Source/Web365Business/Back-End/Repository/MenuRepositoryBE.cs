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
    public class MenuRepositoryBE : BaseBE<tblMenu>, IMenuRepositoryBE
    {
        private readonly IMenuDABERepository menuDABERepository;

        public MenuRepositoryBE(IMenuDABERepository _menuDABERepository)
            : base(_menuDABERepository)
        {
            menuDABERepository = _menuDABERepository;
        }

        public MenuItem GetSameMenuInOtherLanguage(int rootId, int languageId)
        {
            return menuDABERepository.GetSameMenuInOtherLanguage(rootId, languageId);
        }

        /// <summary>
        /// function get all data tblFile
        /// </summary>
        /// <returns></returns>
        public List<MenuItem> GetList(out int total, string name, int? parentId, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return menuDABERepository.GetList(out total, name, parentId, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }
    }
}
