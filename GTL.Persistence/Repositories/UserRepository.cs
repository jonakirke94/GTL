using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTL.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
  
        public IEnumerable<User> GetUsers()
        {
            try
            {
                List<User> users = new List<User>();

                using (SqlConnection connection = new DbConnection().GetConnection())
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM Users";
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User user = BuildUserObject(reader);
                                users.Add(user);
                            }
                        }
                    }
                }
                return users.AsEnumerable();
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong while retriving all users! Try again");
            }
        }

        public void Update(User user)
        {
            try
            {
                    using (SqlConnection connection = new DbConnection().GetConnection())
                    {
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.CommandText =
                                "UPDATE USERS Set name = @param2, city = @param3, zipcode = @param4 WHERE id = @param1";

                            SqlParameter p1 = new SqlParameter(@"param1", SqlDbType.Int);
                            SqlParameter p2 = new SqlParameter(@"param2", SqlDbType.VarChar);
                            SqlParameter p3 = new SqlParameter(@"param3", SqlDbType.VarChar);
                            SqlParameter p4 = new SqlParameter(@"param4", SqlDbType.VarChar);


                            p1.Value = user.Id;
                            p2.Value = user.Name;
                            p3.Value = user.City;
                            p4.Value = user.ZipCode;

                            command.Parameters.Add(p1);
                            command.Parameters.Add(p2);
                            command.Parameters.Add(p3);
                            command.Parameters.Add(p4);

                            command.ExecuteNonQuery();

                        }
                    }                
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong while updating an user! Try again");
            }
        }

        public User GetUser(int id)
        {
            User user = new User();
            using (SqlConnection connection = new DbConnection().GetConnection())
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Users WHERE id = @param1;";
                    SqlParameter p1 = new SqlParameter(@"param1", SqlDbType.Int)
                    {
                        Value = id
                    };
                    command.Parameters.Add(p1);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = BuildUserObject(reader);
                        }
                    }
                }
            }
            return user;
        }

        public int AddUser(User user)
        {
            try
            {                
                using (SqlConnection connection = new DbConnection().GetConnection())
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText =
                            "insert into Users(name, city, zipcode)" +
                            "values (@param2, @param3, @param4) SELECT SCOPE_IDENTITY();";

                        command.Parameters.AddWithValue("@param2", user.Name);
                        command.Parameters.AddWithValue("@param3", user.City);
                        command.Parameters.AddWithValue("@param4", user.ZipCode);

                        int id = Convert.ToInt32(command.ExecuteScalar());
                        return id;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong while inserting an user into the database! Try again");
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                using (SqlConnection connection = new DbConnection().GetConnection())
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "DELETE from USERS where id = @param1";
                        command.Parameters.AddWithValue("@param1", id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong while deleting an user into the database! Try again");
            }
        }


        private User BuildUserObject(SqlDataReader reader)
        {
            User user = new User
            {
                Id = Convert.ToInt32(reader["id"]),
                City = reader["city"].ToString(),
                ZipCode = reader["zipcode"].ToString(),
                Name = reader["name"].ToString()
            };

            return user;
        }

    }
}
