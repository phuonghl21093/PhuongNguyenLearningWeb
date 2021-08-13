using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Business.Back_End.IRepository;
using Web365DA.RDBMS.Back_End.IRepository;
using Web365Domain;
using Web365Utility;

namespace Web365Business.Back_End.Repository
{
    public class UserRoleRepositoryBE : BaseBE<AspNetRoles>, IUserRoleRepositoryBE
    {
        private readonly IUserRoleDABERepository userRoleDABERepository;

        public UserRoleRepositoryBE(IUserRoleDABERepository _userRoleDABERepository)
            : base(_userRoleDABERepository)
        {
            userRoleDABERepository = _userRoleDABERepository;
        }

        public List<AspNetRoleItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return userRoleDABERepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }

        public T GetItemById<T>(string id)
        {
            return userRoleDABERepository.GetItemById<T>(id);
        }

        public void PageForRole(string roleId, int[] pageId)
        {
            userRoleDABERepository.PageForRole(roleId, pageId);
        }

        public bool NameExist(string id, string name)
        {
            return userRoleDABERepository.NameExist(id, name);
        }
    }
}
