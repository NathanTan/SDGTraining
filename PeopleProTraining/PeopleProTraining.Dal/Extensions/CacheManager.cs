using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using System.Linq;

namespace PeopleProTraining.Dal.Extensions
{
    /// <summary>
    /// Helper class for caching serializers and reflection information to improve performance. Cache is stored in memory as static private members.  Thread safe.
    /// </summary>
    public static class CacheManager
    {
        private static object m_locker = new object();
        private static Dictionary<Type, XmlSerializer> m_serializerCache = new Dictionary<Type, XmlSerializer>();
        private static Dictionary<Type, PropertyInfo[]> m_reflectionCache = new Dictionary<Type, PropertyInfo[]>();
        private static Dictionary<Guid, CacheItemBase> m_Cache = new Dictionary<Guid, CacheItemBase>();

        public delegate T GetCacheResult<T>();

        #region Methods

        public static T GetGenericCacheResult<T>(Guid key)
        {
            T result = default(T);
            CacheItem<T> temp = m_Cache.Values.SingleOrDefault(t => t.Key == key) as CacheItem<T>;

            if (temp != null)
            {
                result = temp.GetItem();
            }

            return result;
        }

        /// <summary>
        /// Gets the cached serializer for the type specified, or creates one if it is not yet cached.
        /// </summary>
        /// <param name="typeToSerialize">The type to serialize.</param>
        /// <returns>An XmlSerializer instance for the type specified.</returns>
        public static XmlSerializer GetSerializerForType(Type typeToSerialize)
        {
            XmlSerializer result = null;

            lock (m_locker)
            {
                if (m_serializerCache.ContainsKey(typeToSerialize))
                {
                    result = m_serializerCache[typeToSerialize];
                }
                else
                {
                    result = new XmlSerializer(typeToSerialize);
                    m_serializerCache.Add(typeToSerialize, result);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the type reflected properties for the specified type, including all private/public properties.
        /// </summary>
        /// <param name="typeToReflect">The type for which to get properties.</param>
        /// <returns>An array of PropertyInfo instances representing the properties of the specified type.</returns>
        public static PropertyInfo[] GetPropertyInfoForType(Type typeToReflect)
        {
            PropertyInfo[] result = null;

            lock (m_locker)
            {
                if (m_reflectionCache.ContainsKey(typeToReflect))
                {
                    result = m_reflectionCache[typeToReflect];
                }
                else
                {
                    result = typeToReflect.GetProperties();
                    m_reflectionCache.Add(typeToReflect, result);
                }
            }

            return result;
        }

        #endregion


    }
}
