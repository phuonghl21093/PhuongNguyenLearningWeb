using Web365Domain;
using Web365Domain.Other;

namespace Web365DA.RDBMS.Front_End.IRepository
{
    public interface ILayoutContentDAFERepository
    {
        LayoutContentItem GetItemById(int id);
        LayoutGroupItem GetListByGroupId(int id);
        LayoutGroupItem GetGroupInOtherLang(int groupId, int languageId);
    }
}
