using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using Web365Base;
using Web365Cached;
using Web365DA.RDBMS.Back_End.IRepository;
using Web365Domain.Language;
using Web365Utility;

namespace Web365Business.Back_End
{
    public class BaseBE<T> : CacheController
    {
        private readonly IBaseDABERepository baseDABERepository;

        public BaseBE()
        {

        }

        public BaseBE(IBaseDABERepository _baseDABERepository)
        {
            baseDABERepository = _baseDABERepository;
        }

        public BaseBE(Web365Utility.StaticEnum.TypeCache typeCahe)
            : base(typeCahe)
        {

        }

        public T GetListForTree<T>(int languageId = (int) StaticEnum.LanguageId.Vietnamese, bool isShow = true, bool isDelete = false)
        {
            return baseDABERepository.GetListForTree<T>(languageId, isShow, isDelete);
        }

        public T GetById<T>(int id)
        {
            return baseDABERepository.GetById<T>(id);
        }

        public T GetById<T>(string id)
        {
            return baseDABERepository.GetById<T>(id);
        }

        public T GetItemById<T>(int id)
        {
            return baseDABERepository.GetItemById<T>(id);
        }

        public void Add(object obj)
        {
            baseDABERepository.Add(obj);
        }

        public void Show(int id)
        {
            baseDABERepository.Show(id);
        }

        public void Hide(int id)
        {
            baseDABERepository.Hide(id);
        }

        public void Update(object obj)
        {
            baseDABERepository.Update(obj);
        }

        public void Delete(int id)
        {
            baseDABERepository.Delete(id);
        }

        #region Check
        public bool NameExist(int id, string name)
        {
            return baseDABERepository.NameExist(id, name);
        }
        #endregion
    }
}
