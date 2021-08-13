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
    public class ArticleTypeRepositoryBE : BaseBE<tblTypeArticle>, IArticleTypeRepositoryBE
    {

        private readonly IArticleTypeDABERepository articleTypeDABERepository;

        public ArticleTypeRepositoryBE(IArticleTypeDABERepository _articleTypeDABERepository)
            : base(_articleTypeDABERepository)
        {
            articleTypeDABERepository = _articleTypeDABERepository;
        }

        /// <summary>
        /// function get all data tblTypeArticle
        /// </summary>
        /// <returns></returns>
        public List<ArticleTypeItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return articleTypeDABERepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending, isDelete);  
        }

        public List<ArticleTypeItem> GetListOfGroup(int groupId, bool isShow = true, bool isDelete = false)
        {
            return articleTypeDABERepository.GetListOfGroup(groupId, isShow, isDelete);
        }

        public ArticleTypeItem GetSameArticleTypeByLanguage(int rootId, int langId)
        {
            return articleTypeDABERepository.GetSameArticleTypeByLanguage(rootId, langId);
        }
    }
}
