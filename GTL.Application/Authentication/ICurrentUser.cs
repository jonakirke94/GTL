using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.Application.Authentication
{
    public interface ICurrentUser
    {
        bool IsAuthenticated();

        int GetUserId();
    }
}
