using System.Collections.Generic;
using Web365Domain.Language;

namespace Web365DA.RDBMS.Back_End.IRepository
{
    public interface ILanguageDABERepository : IBaseDABERepository
    {
        List<LanguageItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false);
        void ResetListPicture(int id, string listPictureId);
        List<LanguageItem> GetListExpectDefault();
        LanguageItem GetItemByCode(string code);
    }
}
