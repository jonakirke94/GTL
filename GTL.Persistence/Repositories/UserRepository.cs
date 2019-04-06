using GTL.Application.Interfaces;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;
using GTL.Persistence.Helpers;
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
                    command.Dispose();
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

                    command.AddParamWithValue("@param1", user.Id);
                    command.AddParamWithValue("@param2", user.Name);
                    command.AddParamWithValue("@param3", user.City);
                    command.AddParamWithValue("@param4", user.ZipCode);

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
                command.AddParamWithValue("@param1", user.Id);

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

                    command.AddParamWithValue("@param1", user.Name);
                    command.AddParamWithValue("@param2", user.City);
                    command.AddParamWithValue("@param3", user.ZipCode);

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

                    command.AddParamWithValue("@param1", id);
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
    }
}
