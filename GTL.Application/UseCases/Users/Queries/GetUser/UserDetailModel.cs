using GTL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace GTL.Application.Users.Queries.GetUser
{
    public class UserDetailModel
    {
        public int Id { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public string Name { get; set; }

        public bool EditEnabled { get; set; }

        public bool DeleteEnabled { get; set; }
 
        public static Expression<Func<User, UserDetailModel>> Projection
        {
            get
            {
                //skal nok bruge automapper her og resolve permissions
                return user => new UserDetailModel
                {
                    Id = user.Id,
                    City = user.City,
                    ZipCode = user.ZipCode,
                    Name = user.Name,
                    DeleteEnabled = true,
                    EditEnabled = true
                };
            }
        }

        public static UserDetailModel Create(User user)
        {
            return Projection.Compile().Invoke(user);
        }
    }
}

