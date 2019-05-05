using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GTL.Application.Exceptions;
using GTL.Application.Helper;
using GTL.Application.Infrastructure.RequestModels;
using GTL.Application.Interfaces.Authentication;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;

namespace GTL.Application
{
    public class PasswordHelper : IPasswordHelper
    {
        public bool ValidatePassword(string password, string passwordSalt, string passwordHash)
        {
            return Hasher.Validate(password, passwordSalt, passwordHash);
        }
    }
}
