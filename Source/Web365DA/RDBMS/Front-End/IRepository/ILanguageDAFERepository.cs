using System.Collections.Generic;
using Web365Domain;
using Web365Domain.Language;
using Web365Domain.Other;

namespace Web365DA.RDBMS.Front_End.IRepository
{
    public interface ILanguageDAFERepository
    {
        LanguageItem GetItemById(int id);
        LanguageItem GetItemByCode(string code);
        List<LanguageItem> GetAll();
    }
}
