using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Domain;

namespace Web365DA.RDBMS.Back_End.IRepository
{
    public interface ICommentDABERepository : IBaseDABERepository
    {
        List<CommentItem> GetList(out int total, string url, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false);
        void UpdateFieldWithName(int id, string name, object value); 
    }
}
