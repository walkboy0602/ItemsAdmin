using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Model;
using System.Data.SqlClient;

namespace Framework.DAL
{
    class ReferenceDL
    {
        private dbConnection conn;

        //Constructor
        public ReferenceDL()
        {
            conn = new dbConnection();
        }

        public List<objReference> GetReferenceList(ReferenceType type)
        {
            List<objReference> result = new List<objReference>();

            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT rv.ID AS KeyString, rv.ValueCode AS CodeString, rv.TypeID as Type, rv.ParentID, rv2.Description AS ParentValue, rv.Description as ValueString, ");
            sb.Append("rv.[Order] as OrderBy, rv.IsActive as ActivateFlag, rv.CssClass ");
            sb.Append("FROM RefValue rv ");
            sb.Append("LEFT JOIN RefValue rv2 on rv2.ID = rv.ParentID ");

            if (type != ReferenceType.None) { sb.Append("WHERE rv.TypeID = @TypeID "); }

            SqlParameter[] param = 
            {
                new SqlParameter("@TypeID", (int)type)
            };

            result = conn.executeSelectQuery<objReference>(sb.ToString(), param);

            return result;
        }

    }
}
