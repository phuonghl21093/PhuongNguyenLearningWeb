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
    public class ArticleRepositoryBE : BaseBE<tblArticle>, IArticleRepositoryBE
    {
        private readonly IArticleDABERepository articleDABERepository;

        public ArticleRepositoryBE(IArticleDABERepository _articleDABERepository)
            : base(_articleDABERepository)
        {
            articleDABERepository = _articleDABERepository;
        }

        /// <summary>
        /// function get all data tblArticle
        /// </summary>
        /// <returns></returns>
        public List<ArticleItem> GetList(out int total, string name, int? typeId, int? groupId, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return articleDABERepository.GetList(out total, name, typeId, groupId, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }

        public List<ArticleItem> GetListOtherLanguage(int id)
        {
            return articleDABERepository.GetListOtherLanguage(id);
        }

        public void ResetListPicture(int id, string listPictureId)
        {
            articleDABERepository.ResetListPicture(id, listPictureId);
        }
    }
}
