using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;

namespace GTL.Application.Authorization
{
    public class CommandResponse 
    {
        public string ErrorMessage { get; set; }

        public bool HasRequestError => string.IsNullOrEmpty(ErrorMessage);
    }

    public class CommandResponse<TModel> : CommandResponse where TModel : class
    {
        public CommandResponse(TModel model)
        {
            Result = model;
        }

        public TModel Result { get; }
    }
}
