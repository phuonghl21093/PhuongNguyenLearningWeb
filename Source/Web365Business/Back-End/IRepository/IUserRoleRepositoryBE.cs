using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Domain;

namespace Web365Business.Back_End.IRepository
{
    public interface IUserRoleRepositoryBE : IBaseRepositoryBE
    {
        List<AspNetRoleItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false);
        T GetItemById<T>(string id);
        void PageForRole(string roleId, int[] pageId);
        bool NameExist(string id, string name);
    }
}
