using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Domain;
using Web365Domain.SettingInfo;

namespace Web365DA.RDBMS.Front_End.IRepository
{
    public interface ISettingDAFERepository
    {
        SettingInfoItem GetById(int id, int languageId);
        List<SettingInfoItem> GetSettingInfos(int languageId);
    }
}
