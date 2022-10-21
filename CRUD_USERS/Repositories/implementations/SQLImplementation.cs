using CRUD_USERS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace CRUD_USERS.Repositories.implementations
{
    public class SQLImplementation : IUserRepository
    {

        string sqlConnectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;

        public void add(UserModel userModel)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            using (var cmd = new SqlCommand())
            {
                connection.Open();
                cmd.Connection = connection;
                cmd.CommandText = "insert into Users values (@name, @lastname, @address, @city, @state)";
                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = userModel.Name;
                cmd.Parameters.Add("@lastname", SqlDbType.NVarChar).Value = userModel.LastName;
                cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = userModel.Address;
                cmd.Parameters.Add("@city", SqlDbType.NVarChar).Value = userModel.City;
                cmd.Parameters.Add("@state", SqlDbType.NVarChar).Value = userModel.State;
                cmd.ExecuteNonQuery();

            }
        }

        public void delete(int id)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            using (var cmd = new SqlCommand())
            {
                connection.Open();
                cmd.Connection = connection;
                cmd.CommandText = "delete from Users where User_Id=@id";
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.ExecuteNonQuery();

            }
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            var userList = new List<UserModel>();
            using (var connection = new SqlConnection(sqlConnectionString))
            using (var cmd = new SqlCommand())
            {
                connection.Open();
                cmd.Connection = connection;
                cmd.CommandText = "Select * from Users order by User_Id desc";
                using (var reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        var userModel = new UserModel();
                        userModel.Id = (int)reader[0];
                        userModel.Name = (string)reader[1];
                        userModel.LastName = (string)reader[2];
                        userModel.Address = (string)reader[3];
                        userModel.City = (string)reader[4];
                        userModel.State = (string)reader[5];
                        userList.Add(userModel);
                    }
                }
            }
            return userList;
        }

        public IEnumerable<UserModel> getUser(string value)
        {
            var userList = new List<UserModel>();
            int userId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            string userName = value;
            using (var connection = new SqlConnection(sqlConnectionString))
            using (var cmd = new SqlCommand())
            {
                connection.Open();
                cmd.Connection = connection;
                cmd.CommandText = @"Select * from Users where User_Id=@id or User_Name like @name+'%' order by User_Id desc";
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = userId;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = userName;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var userModel = new UserModel();
                        userModel.Id = (int)reader[0];
                        userModel.Name = (string)reader[1];
                        userModel.LastName = (string)reader[2];
                        userModel.Address = (string)reader[3];
                        userModel.City = (string)reader[4];
                        userModel.State = (string)reader[5];
                        userList.Add(userModel);
                    }
                }
            }
            return userList;
        }

        public void update(UserModel userModel)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            using (var cmd = new SqlCommand())
            {
                connection.Open();
                cmd.Connection = connection;
                cmd.CommandText = @"update Users set User_Name=@name, User_LastName=@lastname, User_Address=@address, User_City=@city, User_State=@state where User_Id=@id";
                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = userModel.Name;
                cmd.Parameters.Add("@lastname", SqlDbType.NVarChar).Value = userModel.LastName;
                cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = userModel.Address;
                cmd.Parameters.Add("@city", SqlDbType.NVarChar).Value = userModel.City;
                cmd.Parameters.Add("@state", SqlDbType.NVarChar).Value = userModel.State;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = userModel.Id;
                cmd.ExecuteNonQuery();

            }
        }
    }
}
