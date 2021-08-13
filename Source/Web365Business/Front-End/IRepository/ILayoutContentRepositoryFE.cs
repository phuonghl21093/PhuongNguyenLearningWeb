using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Domain;
using Web365Domain.Other;

namespace Web365Business.Front_End.IRepository
{
    public interface ILayoutContentRepositoryFE
    {
        LayoutContentItem GetItemById(int id);
        LayoutGroupItem GetListByGroupId(int id);
        LayoutGroupItem GetGroupInOtherLang(int groupId, int languageId);
    }
}
