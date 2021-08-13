using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Business.Front_End.IRepository;
using Web365DA.RDBMS.Front_End.IRepository;
using Web365Domain;
using Web365Domain.SettingInfo;
using Web365Utility;

namespace Web365Business.Front_End.Repository
{
    public class SettingRepositoryFE : BaseFE, ISettingRepositoryFE
    {
        private readonly ISettingDAFERepository settingDAFERepository;

        public SettingRepositoryFE(ISettingDAFERepository settingDafeRepository)
        {
            settingDAFERepository = settingDafeRepository;
        }

        public SettingInfoItem GetById(int id, int languageId)
        {
            return settingDAFERepository.GetById(id, languageId);
        }

        public List<SettingInfoItem> GetSettingInfos(int languageId)
        {
            return settingDAFERepository.GetSettingInfos(languageId);
        }
    }
}
