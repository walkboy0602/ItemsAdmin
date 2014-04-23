using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Model
{
    public class objReference
    {
        #region property

        public string KeyString
        {
            set;
            get;
        }

        public string CodeString
        {
            get;
            set;
        }

        public string ValueString
        {
            get;
            set;
        }

        public string ParentID
        {
            get;
            set;
        }

        public string ParentValue
        {
            get;
            set;
        }

        public string CssClass
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public bool? ActivateFlag
        {
            get;
            set;
        }

        public int OrderBy
        {
            get;
            set;
        }

        #endregion
    }
}
