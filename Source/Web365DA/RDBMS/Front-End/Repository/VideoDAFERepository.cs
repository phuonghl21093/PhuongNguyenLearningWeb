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
    public class VideoDAFERepository : BaseFE, IVideoDAFERepository
    {
        public VideoModel GetListByType(int id, string ascii, int skip, int top)
        {
            var Video = new VideoModel();

            var paramTotal = new SqlParameter
            {
                ParameterName = "Total",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            var query = web365db.Database.SqlQuery<VideoMapItem>("exec [dbo].[PRC_Video_GetVideoByType] @TypeID, @TypeAscii, @Skip, @Top, @Total OUTPUT",
                           new SqlParameter("TypeID", id),
                           new SqlParameter("TypeAscii", ascii),
                           new SqlParameter("Skip", skip),
                           new SqlParameter("Top", top),
                           paramTotal);

            Video.List = query.Select(f => new VideoItem()
            {
                ID = f.ID,
                Name = f.Name,
                NameAscii = f.NameAscii,
                Link = f.Link,
                Picture = new PictureItem()
                {
                    FileName = f.URLPicture
                }
            }).ToList();

            Video.Total = Convert.ToInt32(paramTotal.Value);

            return Video;
        }

        public List<VideoTypeItem> GetListTypeByParent(int id)
        {
            var query = from c in web365db.tblTypeVideo
                        where c.IsDeleted == false && c.IsShow == true && c.Parent == id
                        select new VideoTypeItem()
                        {
                            ID = c.ID,
                            LanguageId = c.LanguageId,
                            RootId = c.RootId,
                            Name = c.Name,
                            Videos =
                                c.tblVideo.Where(p => p.IsShow == true && p.IsDeleted == false).Select(p => new VideoItem()
                                {
                                    ID = p.ID,
                                    Name = p.Name,
                                    NameAscii = p.NameAscii,
                                    Link = p.Link,
                                    Picture = new PictureItem()
                                    {
                                        FileName = p.tblPicture != null ? p.tblPicture.FileName : string.Empty
                                    }
                                }).ToList()
                        };

            return query.ToList();
        }

        public VideoTypeItem GetSameTypeFromDefault(int id, int languageId)
        {
            var query = from c in web365db.tblTypeVideo
                        where c.IsDeleted == false && c.IsShow == true && c.RootId == id && languageId == c.LanguageId
                        select new VideoTypeItem()
                        {
                            ID = c.ID,
                            LanguageId = c.LanguageId,
                            RootId = c.RootId,
                            Name = c.Name,
                            NameAscii = c.NameAscii
                        };

            return query.FirstOrDefault();
        } 
    }
}
