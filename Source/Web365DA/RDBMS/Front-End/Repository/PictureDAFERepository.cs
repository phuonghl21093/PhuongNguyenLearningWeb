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
    public class PictureDAFERepository : BaseFE, IPictureDAFERepository
    {
        public PictureModel GetListByType(int id, string ascii, int skip, int top)
        {
            var picture = new PictureModel();

            var paramTotal = new SqlParameter
            {
                ParameterName = "Total",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            var query = web365db.Database.SqlQuery<PictureItem>("exec [dbo].[PRC_Picture_GetPictureByType] @TypeID, @TypeAscii, @Skip, @Top, @Total OUTPUT",
                           new SqlParameter("TypeID", id),
                           new SqlParameter("TypeAscii", ascii),
                           new SqlParameter("Skip", skip),
                           new SqlParameter("Top", top),
                           paramTotal);

            picture.List = query.ToList();

            picture.Total = Convert.ToInt32(paramTotal.Value);

            return picture;
        }

        public List<PictureTypeItem> GetListTypeByParent(int id)
        {
            var query = (from c in web365db.tblTypePicture
                where c.IsDeleted == false && c.IsShow == true && c.Parent == id
                select new PictureTypeItem()
                {
                    ID = c.ID,
                    LanguageId = c.LanguageId,
                    RootId = c.RootId,
                    Name = c.Name,
                    Pictures =
                        c.tblPicture.Where(p => p.IsShow == true && p.IsDeleted == false).Select(p => new PictureItem()
                        {
                            FileName = p.FileName,
                            Alt = p.Alt
                        }).ToList()
                }).ToList();

            if (query.Any() && query.First().RootId.HasValue)
            {
                foreach (var item in query)
                {
                    item.Pictures = web365db
                        .tblPicture
                        .Where(p => p.IsDeleted == false && p.IsShow == true && p.TypeID == item.RootId)
                        .Select(p => new PictureItem()
                        {
                            FileName = p.FileName,
                            Alt = p.Alt
                        }).ToList();
                }
            }

            return query;
        } 

        public PictureTypeItem GetSameTypeFromDefault(int id, int languageId)
        {
            var query = from c in web365db.tblTypePicture
                where c.IsDeleted == false && c.IsShow == true && c.RootId == id && languageId == c.LanguageId
                select new PictureTypeItem()
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
