using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Web365Base;
using Web365DA.RDBMS.Front_End.IRepository;
using Web365Domain;
using Web365Domain.SettingInfo;
using Web365Utility;

namespace Web365DA.RDBMS.Front_End.Repository
{
    public class SettingDAFERepository : BaseFE, ISettingDAFERepository
    {
        public List<SettingInfoItem> GetSettingInfos(int languageId)
        {
            var entity = from c in web365db.tblSetting
                         where c.IsDeleted != true && c.IsShow == true && c.LanguageId == languageId
                         select new SettingInfoItem()
                         {
                             ID = c.ID,
                             Content = c.Content,
                             Description = c.Description,
                             Name = c.Name
                         };

            return entity.ToList();
        }

        public SettingInfoItem GetById(int id, int languageId)
        {
            var entity = (from c in web365db.tblSetting
                          where c.ID == id && c.IsDeleted != true && c.IsShow == true
                          select new SettingInfoItem()
                          {
                              ID = c.ID,
                              Content = c.Content,
                              Description = c.Description,
                              Name = c.Name,
                              IsShow = c.IsShow,
                              IsDeleted = c.IsDeleted
                          }).FirstOrDefault();

            if (entity == null)
                return null;

            if (languageId == (int)StaticEnum.LanguageId.Vietnamese)
            {
                return entity;
            }
            else
            {
                return (from c in web365db.tblSetting
                        where
                            c.RootId == id && c.IsDeleted == false && c.IsShow == true &&
                            languageId == c.LanguageId
                        select new SettingInfoItem()
                        {
                            ID = c.ID,
                            Content = c.Content,
                            Description = c.Description,
                            Name = c.Name,
                            IsShow = c.IsShow,
                            IsDeleted = c.IsDeleted
                        }).FirstOrDefault();
            }
        }
    }
}
