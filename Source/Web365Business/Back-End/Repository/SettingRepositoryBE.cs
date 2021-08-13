using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Business.Back_End.IRepository;
using Web365DA.RDBMS.Back_End.IRepository;
using Web365Domain;
using Web365Domain.SettingInfo;
using Web365Utility;

namespace Web365Business.Back_End.Repository
{
    public class SettingRepositoryBE : BaseBE<tblSetting>, ISettingRepositoryBE
    {
        private readonly ISettingDABERepository settingDABERepository;

        public SettingRepositoryBE(ISettingDABERepository settingDabeRepository)
            : base(settingDabeRepository)
        {
            this.settingDABERepository = settingDabeRepository;
        }

        /// <summary>
        /// function get all data list
        /// </summary>
        /// <returns></returns>
        public List<SettingInfoItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return settingDABERepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }
    }
}
