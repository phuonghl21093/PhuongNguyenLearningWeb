using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Web365Base;
using Web365DA.RDBMS.Back_End.IRepository;
using Web365Domain;
using Web365Domain.SettingInfo;
using Web365Utility;

namespace Web365DA.RDBMS.Back_End.Repository
{
    public class SettingDABERepository : BaseBE<tblSetting>, ISettingDABERepository
    {
        /// <summary>
        /// function get all data tblSupport
        /// </summary>
        /// <returns></returns>
        public List<SettingInfoItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            var query = from p in web365db.tblSetting
                        where p.Name.ToLower().Contains(name) && p.IsDeleted == isDelete
                        select p;

            query = query.Where(p => p.LanguageId == (int)StaticEnum.LanguageId.Vietnamese);

            total = query.Count();

            query = descending ? QueryableHelper.OrderByDescending(query, propertyNameSort) : QueryableHelper.OrderBy(query, propertyNameSort);

            var result = query.Select(p => new SettingInfoItem()
            {
                ID = p.ID,
                Content = p.Content,
                Name = p.Name,
                IsShow = p.IsShow,
                Description = p.Description,
                LanguageId = p.LanguageId,
                LanguageName = p.tblLanguage.Name,
                RootId = p.RootId
            }).Skip(currentRecord).Take(numberRecord).ToList();

            foreach (var item in result)
            {
                item.ListOtherLanguage = web365db.tblSetting
                    .Where(c => c.IsDeleted == false && c.RootId == item.ID)
                    .Select(c => new SettingInfoItem()
                    {
                        ID = c.ID,
                        Content = c.Content,
                        Name = c.Name,
                        IsShow = c.IsShow,
                        Description = c.Description,
                        LanguageId = c.LanguageId,
                        LanguageName = c.tblLanguage.Name,
                        RootId = c.RootId
                    }).ToList();
            }
            return result;
        }

        public T GetListForTree<T>(int languageId = (int) StaticEnum.LanguageId.Vietnamese, bool isShow = true, bool isDelete = false)
        {
            var query = from p in web365db.tblSetting
                        where p.IsShow == isShow && p.IsDeleted == isDelete
                        select p;

            query = query.Where(p => p.LanguageId == languageId);

            return (T)(object)query.OrderByDescending(p => p.ID).Select(p => new SettingInfoItem()
            {
                ID = p.ID,
                Content = p.Content,
                LanguageName = p.tblLanguage.Name,
                Name = p.Name,
                Description = p.Description,
                IsShow = p.IsShow,
                RootId = p.RootId,
                LanguageId = p.LanguageId
            }).ToList();
        }

        public T GetItemById<T>(int id)
        {
            var result = GetById<tblSetting>(id);

            return (T)(object)new SettingInfoItem()
            {
                ID = result.ID,
                Description = result.Description,
                LanguageId = result.LanguageId,
                RootId = result.RootId,
                Name = result.Name,
                Content = result.Content,
                IsShow = result.IsShow
            };
        }

        public void Show(int id)
        {
            var adv = web365db.tblSetting.SingleOrDefault(p => p.ID == id);
            adv.IsShow = true;
            web365db.Entry(adv).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        public void Hide(int id)
        {
            var adv = web365db.tblSetting.SingleOrDefault(p => p.ID == id);
            adv.IsShow = false;
            web365db.Entry(adv).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        public bool NameExist(int id, string name, int languageId = (int) StaticEnum.LanguageId.Vietnamese)
        {
            throw new NotImplementedException();
        }
    }
}
