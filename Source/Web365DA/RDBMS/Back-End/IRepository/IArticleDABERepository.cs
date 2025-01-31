﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Domain;

namespace Web365DA.RDBMS.Back_End.IRepository
{
    public interface IArticleDABERepository : IBaseDABERepository
    {
        List<ArticleItem> GetList(out int total, string name, int? typeId, int? groupId, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false);
        List<ArticleItem> GetListOtherLanguage(int id);
        void ResetListPicture(int id, string listPictureId);
    }
}
