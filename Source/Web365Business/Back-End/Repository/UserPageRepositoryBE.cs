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
    public class UserPageRepositoryBE : BaseBE<tblPage>, IUserPageRepositoryBE
    {
        private readonly IUserPageDABERepository userPageDABERepository;

        public UserPageRepositoryBE(IUserPageDABERepository _userPageDABERepository)
            : base(_userPageDABERepository)
        {
            userPageDABERepository = _userPageDABERepository;
        }

        /// <summary>
        /// function get all data tblPage
        /// </summary>
        /// <returns></returns>
        public List<PageItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return userPageDABERepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }

        public List<PageItem> GetPageOfUser(string userId)
        {
            return userPageDABERepository.GetPageOfUser(userId);
        }
    }
}
