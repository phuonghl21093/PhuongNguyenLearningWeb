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
    public class ArticleGroupTypeMapRepositoryBE : BaseBE<tblGroup_TypeArticle_Map>, IArticleGroupTypeMapRepositoryBE
    {
        private readonly IArticleGroupTypeMapDABERepository articleGroupTypeMapDABERepository;

        public ArticleGroupTypeMapRepositoryBE(IArticleGroupTypeMapDABERepository _articleGroupTypeMapDABERepository)
        {
            this.articleGroupTypeMapDABERepository = _articleGroupTypeMapDABERepository;
        }

        public void ResetGroupOfType(int TypeId, int[] groupId)
        {
            articleGroupTypeMapDABERepository.ResetGroupOfType(TypeId, groupId);
        }

        public void ResetTypeOfGroup(int groupId, int[] TypeId)
        {
            articleGroupTypeMapDABERepository.ResetTypeOfGroup(groupId, TypeId);
        }
        
    }
}
