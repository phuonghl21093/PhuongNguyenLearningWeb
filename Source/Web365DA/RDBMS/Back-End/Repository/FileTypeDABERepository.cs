using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Web365Base;
using Web365DA.RDBMS.Back_End.IRepository;
using Web365Domain;
using Web365Utility;


namespace Web365DA.RDBMS.Back_End.Repository
{
    public class FileTypeDABERepository : BaseBE<tblTypeFile>, IFileTypeDABERepository
    {

        /// <summary>
        /// function get all data tblTypeFile
        /// </summary>
        /// <returns></returns>
        public List<FileTypeItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            var query = from p in web365db.tblTypeFile
                        where p.Name.ToLower().Contains(name) && p.IsDeleted == isDelete
                        select p;

            query = query.Where(p => p.LanguageId == (int) StaticEnum.LanguageId.Vietnamese);

            total = query.Count();

            query = descending ? QueryableHelper.OrderByDescending(query, propertyNameSort) : QueryableHelper.OrderBy(query, propertyNameSort);

            var result = query.Select(p => new FileTypeItem()
            {
                ID = p.ID,
                Name = p.Name,
                NameAscii = p.NameAscii,
                SEOTitle = p.SEOTitle,
                SEODescription = p.SEODescription,
                SEOKeyword = p.SEOKeyword,
                Number = p.Number,
                Summary = p.Summary,
                Detail = p.Detail,
                Parent = p.Parent,
                IsShow = p.IsShow,
                LanguageId = p.LanguageId,
                LanguageName = p.tblLanguage.Name,
                RootId = p.RootId
            }).Skip(currentRecord).Take(numberRecord).ToList();

            foreach (var item in result)
            {
                item.ListOtherLanguage = web365db
                    .tblTypeFile
                    .Where(c => c.IsDeleted == false && c.RootId == item.ID)
                    .Select(c => new FileTypeItem()
                    {
                        ID = c.ID,
                        Name = c.Name,
                        NameAscii = c.NameAscii,
                        SEOTitle = c.SEOTitle,
                        SEODescription = c.SEODescription,
                        SEOKeyword = c.SEOKeyword,
                        Number = c.Number,
                        Summary = c.Summary,
                        Detail = c.Detail,
                        Parent = c.Parent,
                        IsShow = c.IsShow,
                        LanguageId = c.LanguageId,
                        LanguageName = c.tblLanguage.Name,
                        RootId = c.RootId
                    }).ToList();
            }

            return result;
        }

        public T GetListForTree<T>(int languageId = (int) StaticEnum.LanguageId.Vietnamese, bool isShow = true, bool isDelete = false)
        {
            var query = from p in web365db.tblTypeFile
                        where p.IsShow == isShow && p.IsDeleted == isDelete && languageId == p.LanguageId
                        select p;

            query = query.Where(p => p.LanguageId == languageId);

            return (T)(object)query.OrderByDescending(p => p.ID).Select(p => new FileTypeItem()
            {
                ID = p.ID,
                Name = p.Name,
                NameAscii = p.NameAscii,
                Parent = p.Parent,
                LanguageId = p.LanguageId,
                LanguageName = p.tblLanguage.Name,
                RootId = p.RootId
            }).ToList();
        }

        public T GetItemById<T>(int id)
        {
            var query = from p in web365db.tblTypeFile
                        where p.ID == id
                        orderby p.ID descending
                        select new FileTypeItem()
                        {
                            ID = p.ID,
                            Name = p.Name,
                            NameAscii = p.NameAscii,
                            SEOTitle = p.SEOTitle,
                            SEODescription = p.SEODescription,
                            SEOKeyword = p.SEOKeyword,
                            DateCreated = p.DateCreated,
                            DateUpdated = p.DateUpdated,
                            Number = p.Number,
                            PictureID = p.PictureID,
                            Summary = p.Summary,
                            Detail = p.Detail,
                            Parent = p.Parent,
                            IsShow = p.IsShow,
                            LanguageId = p.LanguageId,
                            LanguageName = p.tblLanguage.Name,
                            RootId = p.RootId
                        };
            return (T)(object)query.FirstOrDefault();
        }

        public FileTypeItem GetSameTypeByLanguage(int rootId, int langId)
        {
            var query = from c in web365db.tblTypeFile
                        where c.IsDeleted == false && c.IsShow == true && langId == c.LanguageId && c.RootId == rootId
                        select new FileTypeItem()
                        {
                            ID = c.ID,
                            Name = c.Name,
                            NameAscii = c.NameAscii,
                            SEOTitle = c.SEOTitle,
                            SEODescription = c.SEODescription,
                            SEOKeyword = c.SEOKeyword,
                            DateCreated = c.DateCreated,
                            DateUpdated = c.DateUpdated,
                            Number = c.Number,
                            PictureID = c.PictureID,
                            Summary = c.Summary,
                            Detail = c.Detail,
                            Parent = c.Parent,
                            IsShow = c.IsShow,
                            LanguageId = c.LanguageId,
                            LanguageName = c.tblLanguage.Name,
                            RootId = c.RootId
                        };

            return query.FirstOrDefault();
        }

        public void Show(int id)
        {
            var typeFile = web365db.tblTypeFile.SingleOrDefault(p => p.ID == id);
            typeFile.IsShow = true;
            web365db.Entry(typeFile).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        public void Hide(int id)
        {
            var typeFile = web365db.tblTypeFile.SingleOrDefault(p => p.ID == id);
            typeFile.IsShow = false;
            web365db.Entry(typeFile).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        #region Check
        public bool NameExist(int id, string name, int languageId = (int) StaticEnum.LanguageId.Vietnamese)
        {
            var query = web365db.tblTypeFile.Count(c => c.Name.ToLower() == name.ToLower() && c.LanguageId == languageId && c.ID != id);

            return query > 0;
        }
        #endregion
    }
}
