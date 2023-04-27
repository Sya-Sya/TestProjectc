using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace Luza_Project
{
    public class RepDao
    {
       public static SqlConnection GetConnection()
        {
            SqlConnection conn = null;
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBcon"].ConnectionString);
            if (conn.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return conn;
        }

        public static DataTable ExecSql(string sql)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql,conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandTimeout = 900;
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public static string FilterString(string str)
        {
            if (str == "" || str == null)
            {
                return "NULL";
            }
            str = str.Replace(";", "");
            str = str.Replace("'", "''");
            str = str.Replace("--", "");
            str = str.Replace("<script>", "");
            str = str.Replace("</script>", "");
            return "'" + str + "'";
        }
    }
    
}