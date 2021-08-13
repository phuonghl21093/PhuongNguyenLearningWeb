using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Web365Base;
using Web365DA.RDBMS.Front_End.IRepository;
using Web365Domain;
using Web365Domain.Other;
using Web365Utility;

namespace Web365DA.RDBMS.Front_End.Repository
{
    public class LayoutContentDAFERepository : BaseFE, ILayoutContentDAFERepository
    {
        public tblLayoutContent GetById(int id)
        {
            var query = from p in web365db.tblLayoutContent
                        where p.ID == id && p.IsDeleted == false && p.IsShow == true
                        orderby p.ID descending
                        select p;
            return query.FirstOrDefault();
        }

        public LayoutContentItem GetItemById(int id)
        {
            var result = GetById(id);

            var content = new LayoutContentItem()
            {
                ID = result.ID,
                Name = result.Name,
                NameAscii = result.NameAscii,
                SEOTitle = result.SEOTitle,
                SEODescription = result.SEODescription,
                SEOKeyword = result.SEOKeyword,
                DateCreated = result.DateCreated,
                DateUpdated = result.DateUpdated,
                PictureID = result.PictureID,
                Summary = result.Summary,
                Detail = result.Detail,
                IsShow = result.IsShow,
                LayoutGroupId = result.LayoutGroupId,
                IconClassCss = result.IconClassCss,
                UrlPicture = result.tblPicture != null ? result.tblPicture.FileName : string.Empty,
                ListPicture = result.tblLayoutContent_Picture_Map.Select(p => new PictureItem()
                {
                    ID = p.PictureID ?? 0,
                    FileName = p.PictureID.HasValue ? p.tblPicture.FileName : string.Empty
                }).ToList(),
                GroupNumber = result.GroupNumber
            };

            return content;
        }

        public LayoutGroupItem GetListByGroupId(int id)
        {
            var entiy = from c in web365db.tblLayoutGroup
                        where c.ID == id && c.IsDeleted == false && c.IsShow == true
                        select new LayoutGroupItem()
                        {
                            ID = c.ID,
                            Name = c.Name,
                            Description = c.Description,
                            LayoutContents = c.tblLayoutContent.Where(p => p.IsDeleted == false && p.IsShow == true).OrderBy(p => p.Number).Select(p => new LayoutContentItem()
                            {
                                ID = p.ID,
                                Name = p.Name,
                                NameAscii = p.NameAscii,
                                SEOTitle = p.SEOTitle,
                                SEODescription = p.SEODescription,
                                SEOKeyword = p.SEOKeyword,
                                DateCreated = p.DateCreated,
                                DateUpdated = p.DateUpdated,
                                PictureID = p.PictureID,
                                Summary = p.Summary,
                                Detail = p.Detail,
                                IsShow = p.IsShow,
                                LayoutGroupId = p.LayoutGroupId,
                                IconClassCss = p.IconClassCss,
                                UrlPicture = p.tblPicture != null ? p.tblPicture.FileName : string.Empty,
                                ListPicture = p.tblLayoutContent_Picture_Map.Select(g => new PictureItem()
                                {
                                    ID = p.PictureID ?? 0,
                                    FileName = p.PictureID.HasValue ? p.tblPicture.FileName : string.Empty
                                }).ToList(),
                                GroupNumber = p.GroupNumber
                            }).ToList()
                        };
            return entiy.FirstOrDefault();
        }

        public LayoutGroupItem GetGroupById(int id)
        {
            var entiy = from c in web365db.tblLayoutGroup
                        where c.ID == id && c.IsDeleted == false && c.IsShow == true
                        select new LayoutGroupItem()
                        {
                            ID = c.ID,
                            Name = c.Name,
                            Description = c.Description
                        };
            return entiy.FirstOrDefault();
        }

        public LayoutGroupItem GetGroupInOtherLang(int groupId, int languageId)
        {
            var _default = GetGroupById(groupId);
            if (_default.LanguageId == languageId)
            {
                return _default;
            }

            int rootId = _default.RootId ?? _default.ID;

            var entity = from c in web365db.tblLayoutGroup
                         where (languageId != (int)StaticEnum.LanguageId.Vietnamese ? c.RootId == rootId : c.ID == rootId)
                         && c.IsDeleted != true
                         && c.IsShow == true
                         && languageId == c.LanguageId
                         select new LayoutGroupItem()
                         {
                             ID = c.ID,
                             Name = c.Name,
                             Description = c.Description
                         };
            return entity.FirstOrDefault();
        }
    }
}
