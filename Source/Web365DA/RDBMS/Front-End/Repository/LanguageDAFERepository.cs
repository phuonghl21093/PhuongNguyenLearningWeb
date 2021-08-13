using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Web365Base;
using Web365DA.RDBMS.Front_End.IRepository;
using Web365Domain;
using Web365Domain.Language;
using Web365Domain.Other;
using Web365Utility;

namespace Web365DA.RDBMS.Front_End.Repository
{
    public class LanguageDAFERepository : BaseFE, ILanguageDAFERepository
    {
        public LanguageItem GetItemById(int id)
        {
            var query = from c in web365db.tblLanguage
                        where c.IsDeleted == false && c.IsShow == true && c.ID == id
                        select new LanguageItem()
                        {
                            Code = c.Code,
                            ID = c.ID,
                            Name = c.Name
                        };
            return query.FirstOrDefault();
        }

        public LanguageItem GetItemByCode(string code)
        {
            var query = from c in web365db.tblLanguage
                        where c.IsShow == true && c.IsDeleted == false && c.Code == code
                        select new LanguageItem()
                        {
                            Code = c.Code,
                            ID = c.ID,
                            Name = c.Name
                        };
            return query.FirstOrDefault();
        }

        public List<LanguageItem> GetAll()
        {
            var query = from c in web365db.tblLanguage
                        where c.IsShow == true && c.IsDeleted == false
                        select new LanguageItem()
                        {
                            Code = c.Code,
                            ID = c.ID,
                            Name = c.Name
                        };
            return query.ToList();
        }
    }
}
