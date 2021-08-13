using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Business.Back_End.IRepository;
using Web365DA.RDBMS.Back_End.IRepository;
using Web365Domain;
using Web365Utility;


namespace Web365Business.Back_End.Repository
{
    public class VideoTypeRepositoryBE : BaseBE<tblTypeVideo>, IVideoTypeRepositoryBE
    {

        private readonly IVideoTypeDABERepository videoTypeDABERepository;

        public VideoTypeRepositoryBE(IVideoTypeDABERepository _videoTypeDABERepository)
            : base(_videoTypeDABERepository)
        {
            videoTypeDABERepository = _videoTypeDABERepository;
        }

        /// <summary>
        /// function get all data list
        /// </summary>
        /// <returns></returns>
        public List<VideoTypeItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return videoTypeDABERepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }

        public VideoTypeItem GetSameTypeByLanguage(int rootId, int langId)
        {
            return videoTypeDABERepository.GetSameTypeByLanguage(rootId, langId);
        }
    }
}
