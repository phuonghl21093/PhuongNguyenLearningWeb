using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Domain;
using Web365Domain.SettingInfo;

namespace Web365Business.Front_End.IRepository
{
    public interface ISettingRepositoryFE
    {
        SettingInfoItem GetById(int id, int languageId);
        List<SettingInfoItem> GetSettingInfos(int languageId);
    }
}
