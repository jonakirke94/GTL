using GTL.Application.Interfaces;
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
        private readonly IAdoContext _context;

        public UserRepository(IAdoContext context)
        {
            _context = context;
        }
  
        public IEnumerable<User> GetUsers()
        {
            try
            {
                List<User> users = new List<User>();

                using (var command = _context.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Users";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = BuildUserObject(reader);
                            users.Add(user);
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
                using (var command = _context.CreateCommand())
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
                    _context.SaveChanges();

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

            using (var command = _context.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Users WHERE id = @param1;";
                    SqlParameter p1 = new SqlParameter(@"param1", SqlDbType.Int)
                    {
                        Value = id
                    };
                    command.Parameters.Add(p1);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = BuildUserObject(reader);
                        }
                    }
                
            }
            return user;
        }

        public int AddUser(User user)
        {
            try
            {

                using (var command = _context.CreateCommand())
                {
                    command.CommandText =
                            "insert into Users(name, city, zipcode)" +
                            "values (@param1, @param2, @param3) SELECT SCOPE_IDENTITY();";

                    SqlParameter p1 = new SqlParameter(@"param1", SqlDbType.VarChar);
                    SqlParameter p2 = new SqlParameter(@"param2", SqlDbType.VarChar);
                    SqlParameter p3 = new SqlParameter(@"param3", SqlDbType.VarChar);

                    p1.Value = user.Name;
                    p2.Value = user.City;
                    p3.Value = user.ZipCode;

                    command.Parameters.Add(p1);
                    command.Parameters.Add(p2);
                    command.Parameters.Add(p3);

                    int id = Convert.ToInt32(command.ExecuteScalar());
                    _context.SaveChanges();
                    return id;
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

                using (var command = _context.CreateCommand())
                {
                    command.CommandText = "DELETE from USERS where id = @param1";

                        AddParam(command, "@param1", id);
                        command.ExecuteNonQuery();
                        _context.SaveChanges();

                }

            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong while deleting an user into the database! Try again");
            }
        }


        private User BuildUserObject(IDataReader reader)
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

        //make extension method
        private void AddParam<T>(IDbCommand cmd, string name, T value)
        {
            var parameter = cmd.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;

            cmd.Parameters.Add(parameter);
        }


    }
}
