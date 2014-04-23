using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Framework
{
    public class dbConnection 
    {
        public static string connString = string.Empty;
        public static int connTimeout = 500000;

        #region Constructor
        /// <method>
        /// Get default connection string from AppSetting.config
        /// </method>
        public dbConnection()
        {
            if (ConfigurationManager.ConnectionStrings["shopDB"] != null)
            {
                connString = ConfigurationManager.ConnectionStrings["shopDB"].ToString();
            }
            if (String.IsNullOrEmpty(connString))
            {
                throw new Exception("Please pass in connection string to constructor.");
            }   
        }
        /// <method>
        /// Get connection string from pass in paratemer
        /// </method>
        public dbConnection(string strConn)
        {
            connString = strConn;
        }
        #endregion

        #region Public method
        /// <method>
        /// Select Query - return Integer
        /// </method>
        public int executeQueryInt(String _query, SqlParameter[] sqlParameter = null, string connectionString = null)
        {
            if (string.IsNullOrEmpty(connectionString)) connectionString = connString;
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(_query, conn))
                    {
                        cmd.CommandTimeout = connTimeout;
                        if (sqlParameter != null) cmd.Parameters.AddRange(sqlParameter);
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <method>
        /// Select Query - return String
        /// </method>
        public string executeQueryString(String _query, SqlParameter[] sqlParameter = null, string connectionString = null)
        {
            if (string.IsNullOrEmpty(connectionString)) connectionString = connString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(_query, conn))
                    {
                        cmd.CommandTimeout = connTimeout;
                        if (sqlParameter != null) cmd.Parameters.AddRange(sqlParameter);
                        return cmd.ExecuteScalar().ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <method>
        /// Select Query - return DataTable
        /// </method>
        public DataTable executeQueryDataTable(String _query, SqlParameter[] sqlParameter = null, string connectionString = null)
        {
            DataTable dt = new DataTable();
            if (string.IsNullOrEmpty(connectionString)) connectionString = connString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(_query, conn))
                    {
                        cmd.CommandTimeout = connTimeout;
                        if(sqlParameter != null) cmd.Parameters.AddRange(sqlParameter);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        /// <method>
        /// Insert Query
        /// </method>
        public bool executeInsertQuery(String _query, SqlParameter[] sqlParameter, string connectionString = null)
        {
            if (string.IsNullOrEmpty(connectionString)) connectionString = connString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(_query, conn))
                    {
                        cmd.CommandTimeout = connTimeout;
                        if (sqlParameter != null) cmd.Parameters.AddRange(sqlParameter);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return true;
        }

        /// <method>
        /// Insert Query with ID return
        /// </method>
        public int executeInsertQueryReturnID(String _query, SqlParameter[] sqlParameter, string connectionString = null)
        {
            int id = 0;

            _query += "SELECT SCOPE_IDENTITY()";

            if (string.IsNullOrEmpty(connectionString)) connectionString = connString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(_query, conn))
                    {
                        cmd.CommandTimeout = connTimeout;
                        if (sqlParameter != null) cmd.Parameters.AddRange(sqlParameter);
                        //cmd.ExecuteNonQuery();
                        //cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                        //cmd.Parameters.Clear();
                        id = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return id;
        }


        /// <method>
        /// Update Query
        /// </method>
        public bool executeUpdateQuery(String _query, SqlParameter[] sqlParameter, string connectionString = null)
        {
            if (string.IsNullOrEmpty(connectionString)) connectionString = connString;

            try
            {
                foreach (SqlParameter parameter in sqlParameter)
                {
                    if (parameter.Value == null)
                    {
                        parameter.Value = DBNull.Value;
                    }
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(_query, conn))
                    {
                        cmd.CommandTimeout = connTimeout;
                        if (sqlParameter != null) cmd.Parameters.AddRange(sqlParameter);
                        Convert.ToBoolean(cmd.ExecuteNonQuery());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        /// <summary>
        /// execute select query and assign to class when property name and column name match
        /// sample function call <![CDATA[ List<Developer> developers = executeSelectQuery<Developer>("xx"); ]]> 
        /// </summary>        
        public List<T> executeSelectQuery<T>(String _query, SqlParameter[] sqlParameter = null, string connectionString = null) where T : new()
        { 
            if (string.IsNullOrEmpty(connectionString)) connectionString = connString;
            var properties = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly);

            List<T> result = new List<T>();
            List<string> dbColumns = new List<string>();
            string propertyType = string.Empty;
            string columnName = string.Empty;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(_query, conn))
                {
                    cmd.CommandTimeout = connTimeout;
                    if (sqlParameter != null) cmd.Parameters.AddRange(sqlParameter);
                    SqlDataReader dr = cmd.ExecuteReader();

                    //get list of db field name to map with class property name
                    for (int i=0; i < dr.FieldCount; i++)
                    {
                        dbColumns.Add(dr.GetName(i).ToLower());                        
                    }

                    //loop each row and assign to property when column name and property name match
                    while (dr.HasRows && dr.Read())
                    {
                        T obj = new T();
                        
                        foreach (var property in properties)
                        {
                            //database column name format is "column_name" but property name is "ColumnName"
                            if (!dbColumns.Contains(property.Name, StringComparer.OrdinalIgnoreCase))
                            {
                                columnName = string.Concat(property.Name.Select(x => Char.IsUpper(x) ? "_" + x : x.ToString())).TrimStart('_').ToLower();
                            }
                            else
                            {
                                columnName = property.Name;
                            }

                            if (property.CanWrite && dbColumns.Contains(columnName, StringComparer.OrdinalIgnoreCase) && dr[columnName] != DBNull.Value)
                            {
                                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                                {
                                    propertyType = property.PropertyType.GetGenericArguments()[0].Name;
                                }
                                else
                                {
                                    propertyType = property.PropertyType.Name;
                                }
                                switch (propertyType.ToLower())
                                { 
                                    case "string":
                                        //All data type can be cast to string.
                                        property.SetValue(obj, dr[columnName].ToString(), null);
                                        break;
                                    case "bool":
                                    case "boolean":
                                        //Purposely handle boolean data type because it does not exists in mysql and will throw cast exception
                                        property.SetValue(obj, Convert.ToBoolean(dr[columnName]), null);
                                        break;
                                    default:
                                        property.SetValue(obj, dr[columnName], null);
                                        break;
                                }
                            }
                        }
                        result.Add(obj);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Select 2 column only and assign to Dictionary as string - use default connection string
        /// sample function call <![CDATA[ Dictionary<string, string> developers = executeSelectQuery("ref_States", "key", "value"); ]]> 
        /// </summary>        
        public List<KeyValuePair<string, string>> executeSelectQuery(String _query, string keyName, string valueName, SqlParameter[] sqlParameter = null)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            List<string> dbColumns = new List<string>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(_query, conn))
                {
                    cmd.CommandTimeout = connTimeout;
                    if (sqlParameter != null) cmd.Parameters.AddRange(sqlParameter);
                    SqlDataReader dr = cmd.ExecuteReader();

                    //loop each row and assign to property when column name and property name match
                    while (dr.HasRows && dr.Read())
                    {
                        result.Add(new KeyValuePair<string, string>(dr[keyName].ToString(), dr[valueName].ToString()));
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Select 2 column only and assign to Dictionary as string
        /// sample function call <![CDATA[ Dictionary<string, string> developers = executeSelectQuery("ref_States", "key", "value"); ]]> 
        /// </summary>        
        public List<KeyValuePair<string, string>> executeSelectQuery(String _query, string keyName, string valueName, string parentName, SqlParameter[] sqlParameter = null)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            List<string> dbColumns = new List<string>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(_query, conn))
                {
                    cmd.CommandTimeout = connTimeout;
                    if (sqlParameter != null) cmd.Parameters.AddRange(sqlParameter);
                    SqlDataReader dr = cmd.ExecuteReader();

                    //loop each row and assign to property when column name and property name match
                    while (dr.HasRows && dr.Read())
                    {
                        result.Add(new KeyValuePair<string, string>(dr[keyName].ToString(), dr[valueName].ToString()));
                    }
                }
            }
            return result;
        }
     
        #endregion        
    }    
}