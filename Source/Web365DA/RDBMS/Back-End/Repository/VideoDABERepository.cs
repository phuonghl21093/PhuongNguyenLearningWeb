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
    public class VideoDABERepository : BaseBE<tblVideo>, IVideoDABERepository
    {

        /// <summary>
        /// function get all data tblVideo
        /// </summary>
        /// <returns></returns>
        public List<VideoItem> GetList(out int total, string name, int[] typeId, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            var query = from p in web365db.tblVideo
                        where p.Name.ToLower().Contains(name) && p.IsDeleted == isDelete
                        select p;

            if (typeId != null && typeId.Count() > 0)
            {
                query = query.Where(p => typeId.Contains(p.TypeID.Value));
            }

            query = query.Where(p => p.LanguageId == (int)StaticEnum.LanguageId.Vietnamese);

            total = query.Count();

            query = descending ? QueryableHelper.OrderByDescending(query, propertyNameSort) : QueryableHelper.OrderBy(query, propertyNameSort);

            var result = query.Select(p => new VideoItem()
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
                IsShow = p.IsShow,
                LanguageId = p.LanguageId,
                LanguageName = p.tblLanguage.Name,
                RootId = p.RootId
            }).Skip(currentRecord).Take(numberRecord).ToList();

            foreach (var videoItem in result)
            {
                videoItem.ListOtherLanguage = web365db
                    .tblVideo
                    .Where(p => p.IsDeleted == false && p.RootId == videoItem.ID)
                    .Select(p => new VideoItem()
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
                        IsShow = p.IsShow,
                        LanguageId = p.LanguageId,
                        LanguageName = p.tblLanguage.Name,
                        RootId = p.RootId
                    }).ToList();
            }

            return result;
        }

        public List<VideoItem> GetListByArrayID(int[] arrId, bool isDelete = false)
        {
            var result = new List<VideoItem>();

            var query = from p in web365db.tblVideo
                        where arrId.Contains(p.ID) && p.IsDeleted == isDelete
                        orderby p.ID descending
                        select new VideoItem()
                        {
                            ID = p.ID,
                            Name = p.Name,
                            NameAscii = p.NameAscii,
                            TypeID = p.TypeID,
                            Summary = p.Summary,
                            DateCreated = p.DateCreated,
                            DateUpdated = p.DateUpdated,
                            CreatedBy = p.CreatedBy,
                            UpdatedBy = p.UpdatedBy,
                            IsShow = p.IsShow,
                            IsDeleted = p.IsDeleted,
                            LanguageId = p.LanguageId,
                            LanguageName = p.tblLanguage.Name,
                            RootId = p.RootId
                        };

            result = query.ToList();

            result = arrId.Select(item => result.FirstOrDefault(p => p.ID == item)).Where(product => product != null).ToList();

            return result;
        }

        public T GetItemById<T>(int id)
        {
            var query = from p in web365db.tblVideo
                        where p.ID == id
                        orderby p.ID descending
                        select new VideoItem()
                        {
                            ID = p.ID,
                            Name = p.Name,
                            NameAscii = p.NameAscii,
                            SEOTitle = p.SEOTitle,
                            SEODescription = p.SEODescription,
                            SEOKeyword = p.SEOKeyword,
                            Link = p.Link,
                            TypeID = p.TypeID,
                            DateCreated = p.DateCreated,
                            DateUpdated = p.DateUpdated,
                            Number = p.Number,
                            PictureID = p.PictureID,
                            FileID = p.FileID,
                            Summary = p.Summary,
                            Detail = p.Detail,
                            IsShow = p.IsShow,
                            LanguageId = p.LanguageId,
                            LanguageName = p.tblLanguage.Name,
                            RootId = p.RootId
                        };
            return (T)(object)query.FirstOrDefault();
        }

        public void Show(int id)
        {
            var Video = web365db.tblVideo.SingleOrDefault(p => p.ID == id);
            Video.IsShow = true;
            web365db.Entry(Video).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        public void Hide(int id)
        {
            var Video = web365db.tblVideo.SingleOrDefault(p => p.ID == id);
            Video.IsShow = false;
            web365db.Entry(Video).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        #region Check
        public bool NameExist(int id, string name, int languageId = (int) StaticEnum.LanguageId.Vietnamese)
        {
            var query = web365db.tblVideo.Count(c => c.Name.ToLower() == name.ToLower() && c.ID != id);

            return query > 0;
        }
        #endregion

        public T GetListForTree<T>(int languageId = (int) StaticEnum.LanguageId.Vietnamese, bool isShow = true, bool isDelete = false)
        {
            throw new NotImplementedException();
        }
    }
}
