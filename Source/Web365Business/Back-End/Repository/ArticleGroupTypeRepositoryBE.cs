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
    public class ArticleGroupTypeRepositoryBE : BaseBE<tblGroupTypeArticle>, IArticleGroupTypeRepositoryBE
    {
        private readonly IArticleGroupTypeDABERepository articleGroupTypeDABERepository;

        public ArticleGroupTypeRepositoryBE(IArticleGroupTypeDABERepository _articleGroupTypeDABERepository)
            : base(_articleGroupTypeDABERepository)
        {
            articleGroupTypeDABERepository = _articleGroupTypeDABERepository;
        }

        /// <summary>
        /// function get all data tblGroupTypeArticle
        /// </summary>
        /// <returns></returns>
        public List<ArticleGroupTypeItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return articleGroupTypeDABERepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }
    }
}
