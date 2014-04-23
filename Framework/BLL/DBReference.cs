using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using Framework.Model;
using Framework.BLL;

namespace Framework.BLL
{
    public class DBReference
    {
        private const string referenceTypePrefix = "RefType";

        /// <summary>
        /// Get active reference list base on table, include selected value regardless or the activate.
        /// </summary>
        /// <param name="type">ReferenceType enum, will define which type id to retrieve from ref_value table</param>
        /// <returns>return List of key valur pair to preseve the ordering because some ordering is done in db level (store in table)</returns>
        public static List<KeyValuePair<string, string>> GetListOld(ReferenceType type)
        {
            return GetActiveReference(type, null, null)
                .ToDictionary(x => x.KeyString, x => x.ValueString).ToList();
        }

        public static List<objReference> GetList(ReferenceType type)
        {
            return GetActiveReference(type, null, null).ToList();
        }

        /// <summary>
        /// Get reference list but filter by parent value and activated flag.
        /// </summary>
        /// <param name="type">ReferenceType enum, will define which type id to retrieve from ref_value table</param>
        /// <param name="selectedValue">selected value will be included even if it is deactivated.</param>
        /// <param name="parentValue">Parent Value</param>
        /// <returns>IEnumerable of objReference</returns>
        private static IEnumerable<objReference> GetActiveReference(ReferenceType type, string parentValue, string selectedValue)
        {
            return (from x in GetReference(type)
                    where (parentValue == null || x.ParentValue == parentValue) && (x.ActivateFlag == true || x.ActivateFlag == null) && (selectedValue == null || x.CodeString == selectedValue)
                    select x);
        }

        /// <summary>
        /// Get reference list base on culture code, will fallback to parent culture and then default culture (en) when the culture list does not exists in db.
        /// </summary>    
        private static List<objReference> GetReference(ReferenceType type)
        {
            string dictionaryName = referenceTypePrefix + "_" + type.ToString();

            if (HttpContext.Current.Cache.Get(dictionaryName) == null)
            {
                ReloadReferenceCache(type, dictionaryName);
            }

            return (List<objReference>)HttpContext.Current.Cache.Get(dictionaryName);
        }

        /// <summary>
        /// Reload reference list from database to cache, each country will have their own cache and can exists together. Can call this function to force reload.
        /// </summary>
        /// <param name="tab">ReferenceTable enum, will define which table to retrieve data from</param>
        /// <param name="dictionaryName">Internal name to represent reference list in cache. Prefic with DBRef</param>
        private static void ReloadReferenceCache(ReferenceType type, string dictionaryName)
        {
            List<objReference> referenceList = new List<objReference>();

            ReferenceBL _referenceBL = new ReferenceBL();

            referenceList = _referenceBL.GetReferenceList(type);

            if (HttpContext.Current.Cache.Get(dictionaryName) != null)
            {
                HttpContext.Current.Cache.Remove(dictionaryName);
            }

            HttpContext.Current.Cache.Insert(dictionaryName, referenceList, null);
        }
    }
}
