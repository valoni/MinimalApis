using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace WebApplication4
{
    public static class DataAccess
    {
        /// <summary>
        /// All infos from datatables
        /// </summary>
        /// <param name="fromtable"></param>
        /// <returns></returns>

        public static string ConStr="";


        public static DataTable ExecuteQuery(string query)
        {
            using (SqlConnection conn = new SqlConnection(ConStr))
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds.Tables[0];
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static int DeleteFromTable(string tableName, string deletefield, string deleteid)
        {
            int rowsAffected = 0;

            SqlConnection conn = new SqlConnection(ConStr);
            conn.Open();

            // Construct the WHERE clause with the provided OrderId parameter
            string whereClause = ""+deletefield+" = '"+deletefield+"'";

            string query = $"DELETE FROM {tableName} WHERE {whereClause}";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                rowsAffected = cmd.ExecuteNonQuery();
            }

            conn.Close();
            return rowsAffected;
        }


        /// <returns></returns>
        public static int InsertDynamicTable(string tableName, JObject tableData)
        {
            int rowAffected = 0;

            SqlConnection conn = new SqlConnection(ConStr);
            conn.Open();

            string query = $"INSERT INTO {tableName} ({string.Join(", ", tableData.Properties().Select(p => p.Name))}) VALUES ({string.Join(", ", tableData.Properties().Select(p => "@" + p.Name))})";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                foreach (JProperty property in tableData.Properties())
                {
                    cmd.Parameters.AddWithValue("@" + property.Name, property.Value?.ToString());
                }

                rowAffected = cmd.ExecuteNonQuery();
            }

            conn.Close();
            return rowAffected;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromtable"></param>
        /// <returns></returns>
        internal static DataTable dtTable(string fromtable)
        {
            string SqlSelect = "Select * from " + fromtable;
            SqlConnection sqlCon = new SqlConnection(ConStr);
            sqlCon.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(SqlSelect, sqlCon);
            da.Fill(ds);
            sqlCon.Close();

            return ds.Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromtable"></param>
        /// <param name="whereclausole"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static DataTable dtTable2(string fromtable, string whereclausole, string value)
        {
            string SqlSelect = "Select * from " + fromtable + " WHERE " + whereclausole + "='" + value + "'";
            SqlConnection sqlCon = new SqlConnection(ConStr);
            sqlCon.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(SqlSelect, sqlCon);
            da.Fill(ds);
            sqlCon.Close();

            return ds.Tables[0];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromtable"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static DataTable dtTable3(string fromtable, string list)
        {
            string SqlSelect = "Select " + list + " from " + fromtable + ";";
            SqlConnection sqlCon = new SqlConnection(ConStr);
            sqlCon.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(SqlSelect, sqlCon);
            da.Fill(ds);
            sqlCon.Close();

            return ds.Tables[0];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromtable"></param>
        /// <param name="fields"></param>
        /// <param name="whereclausole"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static DataTable dtTable3(string fromtable, string fields, string whereclausole, string value)
        {
            string SqlSelect = "Select " + fields + " from " + fromtable + " WHERE " + whereclausole + "='" + value + "'";
            SqlConnection sqlCon = new SqlConnection(ConStr);
            sqlCon.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(SqlSelect, sqlCon);
            da.Fill(ds);
            sqlCon.Close();

            return ds.Tables[0];
        }

        internal static DataTable dtTable4(string fromtable, string fields, string whereclausole, string value)
        {
            string SqlSelect = "Select " + fields + " from " + fromtable + " WHERE 1=1 ";

            string[] allwheres = whereclausole.Split(',');
            string[] allvalues = value.Split(',');

            for (int i = 0; i < allwheres.Length; i++)
            {
                SqlSelect += " AND " + allwheres[i] + "='" + allvalues[i] + "'";
            }

            SqlConnection sqlCon = new SqlConnection(ConStr);
            sqlCon.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(SqlSelect, sqlCon);
            da.Fill(ds);
            sqlCon.Close();

            return ds.Tables[0];
        }

        
        /* used for select procdures */
        internal static DataTable dtTable5(string spName, string spparameters)
        {
            string spExceute = "EXEC "+spName+" "+spparameters;

     
            SqlConnection sqlCon = new SqlConnection(ConStr);
            sqlCon.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(spExceute, sqlCon);
            da.Fill(ds);
            sqlCon.Close();

            return ds.Tables[0];
        }


        /* use for insert procedures */
        internal static int retVal(string spName, string spparameters)
        {
            string spExceute = "EXEC " + spName + " " + spparameters;

            int rowAffected = 0;

            SqlConnection sqlCon = new SqlConnection(ConStr);
            sqlCon.Open();
            SqlCommand cmd=new SqlCommand(spExceute,sqlCon);
            rowAffected = cmd.ExecuteNonQuery();
            sqlCon.Close();

            return rowAffected;
        }


        /* insert into tables */
        internal static int retVal(string tables, string fields, string paramters)
        {
            string SqlInsert = "Insert Into " + tables + "(";

            string[] field = fields.Split(',');

            for (int i = 0; i < field.Length; i++)
            {
                if (i == field.Length - 1)
                {
                    SqlInsert += field[i] + ") Values (";
                }
                else
                {
                    SqlInsert += field[i];
                }
                SqlInsert += paramters + ");";
            }

            int rowAffected = 0;

            SqlConnection sqlCon = new SqlConnection(ConStr);
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand(SqlInsert, sqlCon);
            rowAffected = cmd.ExecuteNonQuery();
            sqlCon.Close();

            return rowAffected;
        }


        /* update table with single values */
        internal static int updateTable(string tables, string fields, string values , string wh , string cond)
        {
            string SqlUpdate = "Update " + tables + " SET ";

            string[] field = fields.Split(',');
            string[] vals = values.Split(',');

            for (int i = 0; i < field.Length; i++)
            {
                if (i == field.Length - 1)
                {
                    SqlUpdate += field[i] + "='" + vals[i] + "'"; 
                }
                else
                {
                    SqlUpdate += field[i] + "='" + vals[i] + "',";
                }
                
            }

            SqlUpdate += " WHERE " + wh + "='" + cond + "'";

            int rowAffected = 0;

            SqlConnection sqlCon = new SqlConnection(ConStr);
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand(SqlUpdate, sqlCon);
            rowAffected = cmd.ExecuteNonQuery();
            sqlCon.Close();

            return rowAffected;
        }

        /* update table with single values */
        internal static int updateTable2(string tables, string fields, string values, string wh, string cond)
        {
            string SqlUpdate = "Update " + tables + " SET ";

            string[] field = fields.Split(',');
            string[] vals = values.Split(',');
            string[] where = wh.Split(",");
            string[] conditions = cond.Split(',');

            /* set values */
            for (int i = 0; i < field.Length; i++)
            {
                if (i == field.Length - 1)
                {
                    SqlUpdate += field[i] + "='" + vals[i] + "'";
                }
                else
                {
                    SqlUpdate += field[i] + "='" + vals[i] + "',";
                }

            }
        
            
            /* where condition */
            for (int j = 0; j < where.Length; j++)
            {

                if(j==0) SqlUpdate+= " WHERE "+ where[j] + "='" + conditions[j] + "'";
               
                if (j != 0)
                {
            
                        SqlUpdate +="AND" + where[j] + "='" + conditions[j] + "'";
                }
            }

          

            int rowAffected = 0;

            SqlConnection sqlCon = new SqlConnection(ConStr);
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand(SqlUpdate, sqlCon);
            rowAffected = cmd.ExecuteNonQuery();
            sqlCon.Close();

            return rowAffected;
        }


        internal static int delFromTable(string tables, string fieldsid, string value)
        {
            string SqlInsert = "DELETE FROM " + tables + " WHERE " + fieldsid + "='"+ value + "'";

            int rowAffected = 0;

            SqlConnection sqlCon = new SqlConnection(ConStr);
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand(SqlInsert, sqlCon);
            rowAffected = cmd.ExecuteNonQuery();
            sqlCon.Close();

            return rowAffected;
        }


        internal static bool CheckForIt(string uname, string upass)
        {
            int cnt = 0;
            bool checekd = false;

            string SqlSelect = "SELECT count(*) Cnts FROM [Users] WHERE [Username]='" + uname + "' and [Password]='" + upass + "'";

            SqlConnection sqlCon = new SqlConnection(ConStr);
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand(SqlSelect, sqlCon);
            cnt = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            sqlCon.Close();

            if (cnt > 0)
            {
                checekd = true;
            }
            else
            {
                checekd = false;
            }

            return checekd;
        }

    }
}
