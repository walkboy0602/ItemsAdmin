using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Model;

namespace Framework
{
    public class CustomControl
    {
        public static string GenerateDropDown(List<objReference> L)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in L)
            {
                sb.Append("<option value='" + item.KeyString + "'>" + item.ValueString + "</option>");
            }
            return sb.ToString();
        }
    }
}
