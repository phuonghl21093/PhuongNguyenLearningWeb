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
    public class ArticleGroupMapRepositoryBE : BaseBE<object>, IArticleGroupMapRepositoryBE
    {
        private readonly IArticleGroupMapDABERepository articleGroupMapDABERepository;

        public ArticleGroupMapRepositoryBE(IArticleGroupMapDABERepository _articleGroupMapDABERepository)
        {
            this.articleGroupMapDABERepository = _articleGroupMapDABERepository;
        }

        public void ResetGroupOfNews(int newsId, int[] groupId)
        {
            articleGroupMapDABERepository.ResetGroupOfNews(newsId, groupId);
        }

        public void ResetNewsOfGroup(int groupId, int[] newsId)
        {
            articleGroupMapDABERepository.ResetNewsOfGroup(groupId, newsId);
        }       

    }
}
