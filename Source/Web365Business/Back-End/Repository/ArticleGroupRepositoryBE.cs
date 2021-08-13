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
    public class ArticleGroupRepositoryBE : BaseBE<tblGroupArticle>, IArticleGroupRepositoryBE
    {
        private readonly IArticleGroupDABERepository articleGroupDABERepository;

        public ArticleGroupRepositoryBE(IArticleGroupDABERepository _articleGroupDABERepository)
            : base(_articleGroupDABERepository)
        {
            articleGroupDABERepository = _articleGroupDABERepository;
        }

        /// <summary>
        /// function get all data tblGroupArticle
        /// </summary>
        /// <returns></returns>
        public List<ArticleGroupItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return articleGroupDABERepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }

        public int[] GetSameArticleGroupByLanguage(int[] rootId, int langId)
        {
            return articleGroupDABERepository.GetSameArticleGroupByLanguage(rootId, langId);
        }
    }
}
