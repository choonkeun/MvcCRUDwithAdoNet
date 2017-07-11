using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MVCApplication.Models
{
    public class UserRepository
    {
        public string conStr = String.Empty;
        //class constructor
        public UserRepository()
        {
            //<add name="ConnString" connectionString="data source=(LocalDB)\v11.0;Integrated Security=True;AttachDbFilename=|DataDirectory|\App_Data\SampleDatabase.mdf;" providerName="System.Data.SqlClient" />
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            AppDomain.CurrentDomain.SetData("DataDirectory", baseDir);
            conStr = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
        }

        public List<User> GetAllUsers()
        {
            List<User> UserList = new List<User>();

            //Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 50;
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from [dbo].[User]; ";
            DataTable dt = DataAccessLayer.GetDataTable(conStr, cmd);

            foreach (DataRow dr in dt.Rows)
            {
                UserList.Add(
                    new User
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        UserId = Convert.ToString(dr["UserId"] == System.DBNull.Value ? String.Empty : dr["UserId"]),
                        Password = Convert.ToString(dr["Password"] == System.DBNull.Value ? String.Empty : dr["Password"]),
                        FirstName = Convert.ToString(dr["FirstName"] == System.DBNull.Value ? String.Empty : dr["FirstName"]),
                        LastName = Convert.ToString(dr["LastName"] == System.DBNull.Value ? String.Empty : dr["LastName"]),
                        PhoneNumber = Convert.ToString(dr["PhoneNumber"] == System.DBNull.Value ? String.Empty : dr["PhoneNumber"]),
                        EmailAddress = Convert.ToString(dr["EmailAddress"] == System.DBNull.Value ? String.Empty : dr["EmailAddress"]),
                        Gender = Convert.ToInt16(dr["Gender"] == System.DBNull.Value ? 0 : dr["Gender"]),
                        TermCondition = Convert.ToBoolean(dr["TermCondition"] == System.DBNull.Value ? false : dr["TermCondition"]),
                        CreationDate = Convert.ToDateTime(dr["CreationDate"] == System.DBNull.Value ? DateTime.MinValue : dr["CreationDate"])
                    }
                );
            }
            return UserList;
        }

        public User GetUser(int? Id)
        {
            //Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 50;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from [dbo].[User] WHERE Id = @Id; ";
            cmd.Parameters.AddWithValue("@Id", Id);
            DataTable dt = DataAccessLayer.GetDataTable(conStr, cmd);

            foreach (DataRow dr in dt.Rows)
            {
                User user = new User
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    UserId = Convert.ToString(dr["UserId"] == System.DBNull.Value ? String.Empty : dr["UserId"]),
                    Password = Convert.ToString(dr["Password"] == System.DBNull.Value ? String.Empty : dr["Password"]),
                    FirstName = Convert.ToString(dr["FirstName"] == System.DBNull.Value ? String.Empty : dr["FirstName"]),
                    LastName = Convert.ToString(dr["LastName"] == System.DBNull.Value ? String.Empty : dr["LastName"]),
                    PhoneNumber = Convert.ToString(dr["PhoneNumber"] == System.DBNull.Value ? String.Empty : dr["PhoneNumber"]),
                    EmailAddress = Convert.ToString(dr["EmailAddress"] == System.DBNull.Value ? String.Empty : dr["EmailAddress"]),
                    Gender = Convert.ToInt16(dr["Gender"] == System.DBNull.Value ? 0 : dr["Gender"]),
                    TermCondition = Convert.ToBoolean(dr["TermCondition"] == System.DBNull.Value ? false : dr["TermCondition"]),
                    CreationDate = Convert.ToDateTime(dr["CreationDate"] == System.DBNull.Value ? DateTime.MinValue : dr["CreationDate"])
                };
                return user;
            }
            return null;
        }

        //To Add User details    
        public bool AddUser(User obj)
        {
            string sql = "INSERT INTO [dbo].[User] ";
            sql += "(UserId,Password,FirstName,LastName,PhoneNumber,EmailAddress,Gender,TermCondition,CreationDate) VALUES ";
            sql += "(@UserId,@Password,@FirstName,@LastName,@PhoneNumber,@EmailAddress,@Gender,@TermCondition,@CreationDate); ";

            //Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 50;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue("@Id", obj.Id);
            cmd.Parameters.AddWithValue("@UserId", obj.UserId);
            cmd.Parameters.AddWithValue("@Password", obj.Password);
            cmd.Parameters.AddWithValue("@FirstName", obj.FirstName);
            cmd.Parameters.AddWithValue("@LastName", obj.LastName);
            cmd.Parameters.AddWithValue("@PhoneNumber", obj.PhoneNumber);
            cmd.Parameters.AddWithValue("@EmailAddress", obj.EmailAddress);
            cmd.Parameters.AddWithValue("@Gender", obj.Gender);
            cmd.Parameters.AddWithValue("@TermCondition", obj.TermCondition);
            cmd.Parameters.AddWithValue("@CreationDate", obj.CreationDate);

            int i = DataAccessLayer.ExecuteCommand(conStr, cmd);
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //To Update User details    
        public bool UpdateUser(User obj)
        {
            string sql = "UPDATE [dbo].[User] SET ";
            sql += " UserId = @UserId, Password = @Password, FirstName = @FirstName, LastName = @LastName, ";
            sql += " PhoneNumber = @PhoneNumber, EmailAddress = @EmailAddress, Gender = @Gender, ";
            sql += " TermCondition = @TermCondition, CreationDate = @CreationDate ";
            sql += " WHERE id = @id";

            //Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 50;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            cmd.Parameters.AddWithValue("@Id", obj.Id);
            cmd.Parameters.AddWithValue("@UserId", obj.UserId);
            cmd.Parameters.AddWithValue("@Password", obj.Password);
            cmd.Parameters.AddWithValue("@FirstName", obj.FirstName);
            cmd.Parameters.AddWithValue("@LastName", obj.LastName);
            cmd.Parameters.AddWithValue("@PhoneNumber", obj.PhoneNumber);
            cmd.Parameters.AddWithValue("@EmailAddress", obj.EmailAddress);
            cmd.Parameters.AddWithValue("@Gender", obj.Gender);
            cmd.Parameters.AddWithValue("@TermCondition", obj.TermCondition);
            cmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);

            int i = DataAccessLayer.ExecuteCommand(conStr, cmd);
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //To delete User details    
        public bool DeleteUser(int Id)
        {
            //Create Command
            SqlCommand cmd = new SqlCommand("DELETE [dbo].[User] WHERE Id = @Id;");
            cmd.CommandTimeout = 50;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Id", Id);
            int i = DataAccessLayer.ExecuteCommand(conStr, cmd);
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }    

    }
}