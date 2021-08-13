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
using Web365Domain.Language;
using Web365Utility;

namespace Web365Business.Back_End.Repository
{
    /// <summary>
    /// create by BienLV 05-01-2013
    /// </summary>
    public class LanguageRepositoryBE : BaseBE<tblLanguage>, ILanguageRepositoryBE
    {
        private readonly ILanguageDABERepository languageDABERepository;

        public LanguageRepositoryBE(ILanguageDABERepository languageDabeRepository)
            : base(languageDabeRepository)
        {
            languageDABERepository = languageDabeRepository;
        }

        /// <summary>
        /// function get all data tblTypeProduct
        /// </summary>
        /// <returns></returns>
        public List<LanguageItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return languageDABERepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }

        public void ResetListPicture(int id, string listPictureId)
        {
            languageDABERepository.ResetListPicture(id, listPictureId);
        }

        public List<LanguageItem> GetListExpectDefault()
        {
            return languageDABERepository.GetListExpectDefault();

        }

        public LanguageItem GetItemByCode(string code)
        {
            return languageDABERepository.GetItemByCode(code);
        }
    }
}
