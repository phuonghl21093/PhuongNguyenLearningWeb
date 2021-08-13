using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Domain;
using Web365Models;
using Web365Utility;

namespace Web365Business.Front_End.IRepository
{
    public interface IMenuRepositoryFE
    {
        MenuItem GetByNameAscii(string name);
        MenuItem GetMenuItemByLanguage(int id, int languageId);
        List<MenuItem> GetListByParent(string parentId, bool isShow = true, bool isDeleted = false, int languageId = (int)StaticEnum.LanguageId.Vietnamese);
    }
}
