﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Domain;
using Web365Models;

namespace Web365Business.Front_End.IRepository
{
    public interface IVideoRepositoryFE
    {
        VideoTypeItem GetSameTypeFromDefault(int id, int languageId);
        List<VideoTypeItem> GetListTypeByParent(int id);
        VideoModel GetListByType(int id, string ascii, int skip, int top);
    }
}
