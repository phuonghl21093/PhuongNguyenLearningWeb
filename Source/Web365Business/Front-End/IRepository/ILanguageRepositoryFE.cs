using System.Collections.Generic;
using Web365Domain.Language;

namespace Web365Business.Front_End.IRepository
{
    public interface ILanguageRepositoryFE
    {
        LanguageItem GetItemById(int id);
        LanguageItem GetItemByCode(string code);
        List<LanguageItem> GetAll();
    }
}
