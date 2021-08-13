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
    public class FileDAFERepository : BaseFE, IFileDAFERepository
    {
        public FileModel GetListByType(int id, string ascii, int skip, int top)
        {
            var file = new FileModel();

            var paramTotal = new SqlParameter
            {
                ParameterName = "Total",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            var query = web365db.Database.SqlQuery<FileMapItem>("exec [dbo].[PRC_File_GetFileByType] @TypeID, @TypeAscii, @Skip, @Top, @Total OUTPUT",
                           new SqlParameter("TypeID", id),
                           new SqlParameter("TypeAscii", ascii),
                           new SqlParameter("Skip", skip),
                           new SqlParameter("Top", top),
                           paramTotal);

            file.List = query.Select(f => new FileItem()
            {
                ID = f.ID,
                Name = f.Name,
                NameAscii = f.NameAscii,
                FileName = f.FileName,
                Detail = f.Detail
            }).ToList();

            file.Total = Convert.ToInt32(paramTotal.Value);


            return file;
        }

        public List<FileTypeItem> GetListTypeByParent(int id)
        {
            var query = from c in web365db.tblTypeFile
                        where c.IsDeleted == false && c.IsShow == true && c.Parent == id
                        select new FileTypeItem()
                        {
                            ID = c.ID,
                            LanguageId = c.LanguageId,
                            RootId = c.RootId,
                            Name = c.Name,
                            Files =
                                c.tblFile.Where(p => p.IsShow == true && p.IsDeleted == false).Select(p => new FileItem()
                                {
                                    ID = p.ID,
                                    Name = p.Name,
                                    NameAscii = p.NameAscii,
                                    FileName = p.FileName,
                                    Detail = p.Detail
                                }).ToList()
                        };

            return query.ToList();
        }

        public FileTypeItem GetSameTypeFromDefault(int id, int languageId)
        {
            var query = from c in web365db.tblTypeFile
                        where c.IsDeleted == false && c.IsShow == true && c.RootId == id && languageId == c.LanguageId
                        select new FileTypeItem()
                        {
                            ID = c.ID,
                            LanguageId = c.LanguageId,
                            RootId = c.RootId,
                            Name = c.Name,
                            Detail = c.Detail,
                            NameAscii = c.NameAscii
                        };

            return query.FirstOrDefault();
        } 
    }
}
