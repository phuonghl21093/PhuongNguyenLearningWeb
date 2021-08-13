using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Domain;
using Web365Models;

namespace Web365Business.Front_End.IRepository
{
    public interface IPictureRepositoryFE
    {
        PictureTypeItem GetSameTypeFromDefault(int id, int languageId);
        List<PictureTypeItem> GetListTypeByParent(int id);
        PictureModel GetListByType(int id, string ascii, int skip, int top);
    }
}
