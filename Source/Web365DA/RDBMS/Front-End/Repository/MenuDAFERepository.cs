using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Web365Base;
using Web365DA.RDBMS.Front_End.IRepository;
using Web365Domain;
using Web365Models;
using Web365Utility;

namespace Web365DA.RDBMS.Front_End.Repository
{
    public class MenuDAFERepository : BaseFE, IMenuDAFERepository
    {
        public List<MenuItem> GetListByParent(string parentId, bool isShow = true, bool isDeleted = false, int languageId = (int)StaticEnum.LanguageId.Vietnamese)
        {
            var list = new List<MenuItem>();

            var query = web365db.Database.SqlQuery<MenuItem>("EXEC [dbo].[PRC_MenuByParentId] {0}, {1}", string.Join(",", parentId), languageId);

            list = query.Select(p => new MenuItem()
            {
                ID = p.ID,
                Parent = p.Parent,
                Name = p.Name,
                NameAscii = p.NameAscii,
                CssClass = p.CssClass,
                Link = p.Link,
                IsShow = p.IsShow
            }).ToList();

            return list;
        }

        public MenuItem GetByNameAscii(string name)
        {
            var query = from c in web365db.tblMenu
                        where c.IsDeleted == false && c.NameAscii == name
                        select new MenuItem()
                        {
                            ID = c.ID,
                            Parent = c.Parent,
                            Name = c.Name,
                            NameAscii = c.NameAscii,
                            CssClass = c.CssClass,
                            Link = c.Link,
                            IsShow = c.IsShow,
                            LanguageId = c.LanguageId,
                            LanguageName = c.tblLanguage.Name,
                            RootId = c.RootId
                        };
            return query.FirstOrDefault();
        }

        public MenuItem GetMenuItemByLanguage(int id, int languageId)
        {
            MenuItem result;

            var query = web365db
                .tblMenu
                .Where(p => p.IsDeleted == false && p.ID == id)
                .Select(p => new MenuItem()
                {
                    ID = p.ID,
                    Parent = p.Parent,
                    Name = p.Name,
                    NameAscii = p.NameAscii,
                    CssClass = p.CssClass,
                    Link = p.Link,
                    IsShow = p.IsShow,
                    LanguageId = p.LanguageId,
                    LanguageName = p.tblLanguage.Name,
                    RootId = p.RootId
                }).FirstOrDefault();

            if (languageId == (int)StaticEnum.LanguageId.Vietnamese)
            {
                result = web365db.tblMenu
                    .Where(
                        p =>
                            p.IsDeleted == false &&
                            p.ID == query.RootId &&
                            p.LanguageId == languageId)
                    .Select(p => new MenuItem()
                    {
                        ID = p.ID,
                        Parent = p.Parent,
                        Name = p.Name,
                        NameAscii = p.NameAscii,
                        CssClass = p.CssClass,
                        Link = p.Link,
                        IsShow = p.IsShow,
                        LanguageId = p.LanguageId,
                        LanguageName = p.tblLanguage.Name,
                        RootId = p.RootId
                    }).FirstOrDefault();
            }
            else
            {
                result = web365db.tblMenu
                    .Where(
                        p =>
                            p.IsDeleted == false &&
                            (query.RootId.HasValue ? p.RootId == query.RootId : p.RootId == query.ID) &&
                            p.LanguageId == languageId)
                    .Select(p => new MenuItem()
                    {
                        ID = p.ID,
                        Parent = p.Parent,
                        Name = p.Name,
                        NameAscii = p.NameAscii,
                        CssClass = p.CssClass,
                        Link = p.Link,
                        IsShow = p.IsShow,
                        LanguageId = p.LanguageId,
                        LanguageName = p.tblLanguage.Name,
                        RootId = p.RootId
                    }).FirstOrDefault();
            }
            return result;
        }
    }
}
