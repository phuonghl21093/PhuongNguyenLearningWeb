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
    public class VideoRepositoryBE : BaseBE<tblVideo>, IVideoRepositoryBE
    {

        private readonly IVideoDABERepository videoDABERepository;

        public VideoRepositoryBE(IVideoDABERepository _videoDABERepository)
            : base(_videoDABERepository)
        {
            videoDABERepository = _videoDABERepository;
        }

        /// <summary>
        /// function get all data list
        /// </summary>
        /// <returns></returns>
        public List<VideoItem> GetList(out int total, string name, int[] typeId, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return videoDABERepository.GetList(out total, name, typeId, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }        

        public List<VideoItem> GetListByArrayID(int[] arrId, bool isDelete = false)
        {
            return videoDABERepository.GetListByArrayID(arrId, isDelete);
        }        
    }
}
