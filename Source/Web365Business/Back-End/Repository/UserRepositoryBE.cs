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
    public class UserRepositoryBE : BaseBE<AspNetUsers>, IUserRepositoryBE
    {
        private readonly IUserDABERepository userDABERepository;

        public UserRepositoryBE(IUserDABERepository _userDABERepository)
            : base(_userDABERepository)
        {
            userDABERepository = _userDABERepository;
        }

        public List<AspNetUserItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return userDABERepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }

        public void RoleForUser(string userId, string[] roleId)
        {
            userDABERepository.RoleForUser(userId, roleId);
        }

        public T GetItemById<T>(string id)
        {
            return userDABERepository.GetItemById<T>(id);
        }

        public T GetByUserName<T>(string userName)
        {
            return userDABERepository.GetByUserName<T>(userName);
        }

        public bool NameExist(string id, string name)
        {
            return userDABERepository.NameExist(id, name);
        }

        public void Delete(string[] ids)
        {
            userDABERepository.Delete(ids);
        }
    }
}
