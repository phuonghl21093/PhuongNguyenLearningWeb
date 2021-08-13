using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web365Cached
{
    public class CacheController
    {
        ICachingReposity cache;

        public CacheController()
        {
            cache = new InitCaching();
        }

        public CacheController(Web365Utility.StaticEnum.TypeCache typeCahe)
        {

            switch (typeCahe)
            {
                case Web365Utility.StaticEnum.TypeCache.Caching:
                    cache = new InitCaching();
                    break;
                case Web365Utility.StaticEnum.TypeCache.Redis:
                    cache = new InitRedisCache();
                    break;
                case Web365Utility.StaticEnum.TypeCache.Memcached:
                    cache = new InitMemcached();
                    break;
            }
            
        }

        public bool TryGetCache<T>(out T obj, string key) where T : new()
        {
            try
            {
                if (Web365Utility.ConfigWeb.UseCache && cache.Exists(key))
                {
                    obj = cache.Get<T>(key);
                    return true;
                }                
            }
            catch (Exception)
            {
            }

            obj = new T();

            return false;
        }

        public void SetCache(string key, object obj, bool isDataCache, int minutes)
        {
            try
            {
                if (Web365Utility.ConfigWeb.UseCache && !isDataCache && obj != null)
                {
                    cache.Set(key, obj, minutes);                    
                }  
            }
            catch (Exception)
            {
            }
                      
        }

        public void DeleteCache(string key)
        {
            try
            {
                if (cache.Exists(key))
                {
                    cache.Delete(key);
                }
            }
            catch (Exception)
            {
            }          
        }
        
    }
}
