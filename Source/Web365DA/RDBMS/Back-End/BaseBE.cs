using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using Web365Base;
using Web365Domain.Language;
using Web365Utility;

namespace Web365DA.RDBMS.Back_End
{
    public class BaseBE<T> : IDisposable where T : class
    {
        protected Entities365 web365db = new Entities365();
        private bool disposed = false;
        internal DbSet<T> dbSet;

        public BaseBE()
        {
            this.dbSet = web365db.Set<T>();
        }

        public E GetById<E>(int id)
        {
            return (E)(object)dbSet.Find(id);
        }

        public E GetById<E>(string id)
        {
            return (E)(object)dbSet.Find(id);
        }

        public void Add(object entity)
        {
            dynamic obj = entity;
            var prop = entity.GetType().GetProperty("LanguageId", BindingFlags.Public | BindingFlags.Instance);
            if (prop != null
                && prop.CanWrite
                && (prop.GetValue(entity, null) == null || (int) prop.GetValue(entity, null) == 0))
            {
                obj.LanguageId = (int)StaticEnum.LanguageId.Vietnamese;
            }

            dbSet.Add(obj);
            web365db.SaveChanges();
        }

        public void Update(object entity)
        {
            web365db.Entry(entity).State = EntityState.Modified;
            web365db.SaveChanges();
        }

        public void Show(int id)
        {
            dynamic obj = GetById<T>(id);
            if (obj != null)
            {
                obj.IsShow = true;
                web365db.Entry(obj).State = EntityState.Modified;
            }
            web365db.SaveChanges();
        }

        public void Hide(int id)
        {
            dynamic obj = GetById<T>(id);
            if (obj != null)
            {
                obj.IsShow = false;
                web365db.Entry(obj).State = EntityState.Modified;
            }
            web365db.SaveChanges();
        }

        public void Delete(int id)
        {
            dynamic obj = GetById<T>(id);
            if (obj != null)
            {

                obj.IsDeleted = true;
                web365db.Entry(obj).State = EntityState.Modified;

                //if (obj.GetType().GetProperty("IsDeleted") != null)
                //{
                //    obj.IsDeleted = true;
                //    web365db.Entry(obj).State = EntityState.Modified;
                //}
                //else
                //{
                //    web365db.Entry(obj).State = EntityState.Deleted;
                //}    

            }
            web365db.SaveChanges();
        }

        public Entities365 Web365DB
        {
            get
            {
                return web365db;
            }
        }

        //public int LanguageId
        //{
        //    get
        //    {
        //        return System.Web.HttpContext.Current.Request.Cookies[".ASPL"] != null ? Convert.ToInt32(System.Web.HttpContext.Current.Request.Cookies[".ASPL"].Value) : 1;
        //    }
        //} 

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    web365db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
