using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MVCApplication.Models
{
    public class DataAccessLayer
    {
        //1. return DataTable
        public static DataTable GetDataTable(string conStr, SqlCommand cmd)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                DataTable dt = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Connection = con;
                    con.Open();
                    //da.SelectCommand = cmd;
                    try
                    {
                        da.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        cmd.Parameters.Clear();
                        cmd.Dispose();
                    }
                    return dt;
                }
            }
        }

        //2. ExecuteNonQuery: INSERT, UPDATE, DELETE
        public static int ExecuteCommand(string conStr, SqlCommand cmd)
        {
            int cnt = 0;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.Transaction = con.BeginTransaction();
                    cnt = cmd.ExecuteNonQuery();        //The number of rows affected
                }
                catch (Exception)
                {
                    cmd.Transaction.Rollback();
                    //throw (ex);
                }
                finally
                {
                    cmd.Transaction.Commit();
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    con.Close();
                }
                return cnt;
            }
        }
    }
}