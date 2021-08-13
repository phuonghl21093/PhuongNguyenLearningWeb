using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Business.Front_End.IRepository;
using Web365DA.RDBMS.Front_End.IRepository;
using Web365Domain;
using Web365Models;
using Web365Utility;

namespace Web365Business.Front_End.Repository
{
    public class FileRepositoryFE : BaseFE, IFileRepositoryFE
    {
        private readonly IFileDAFERepository fileDAFERepository;

        public FileRepositoryFE(IFileDAFERepository _fileDAFERepository)
        {
            fileDAFERepository = _fileDAFERepository;
        }

        public FileTypeItem GetSameTypeFromDefault(int id, int languageId)
        {
            var key = string.Format("FileRepositoryFE{0}{1}{2}", "GetListByType", id, languageId);

            var item = new FileTypeItem();

            var isDataCache = TryGetCache<FileTypeItem>(out item, key);

            if (!isDataCache)
            {
                item = fileDAFERepository.GetSameTypeFromDefault(id, languageId);
            }

            SetCache(key, item, isDataCache, ConfigCache.Cache10Minute);

            return item;
        }

        public List<FileTypeItem> GetListTypeByParent(int id)
        {
            var key = string.Format("FileRepositoryFE{0}{1}", "GetListTypeByParent", id);

            var item = new List<FileTypeItem>();

            var isDataCache = TryGetCache<List<FileTypeItem>>(out item, key);

            if (!isDataCache)
            {
                item = fileDAFERepository.GetListTypeByParent(id);
            }

            SetCache(key, item, isDataCache, ConfigCache.Cache10Minute);

            return item;
        }

        public FileModel GetListByType(int id, string ascii, int skip, int top)
        {
            var key = string.Format("FileRepositoryFE{0}{1}{2}{3}{4}", "GetListByType", id, ascii, skip, top);

            var item = new FileModel();

            var isDataCache = TryGetCache<FileModel>(out item, key);

            if (!isDataCache)
            {
                item = fileDAFERepository.GetListByType(id, ascii, skip, top);
            }

            SetCache(key, item, isDataCache, Web365Utility.ConfigCache.Cache10Minute);

            return item;
        }
    }
}
