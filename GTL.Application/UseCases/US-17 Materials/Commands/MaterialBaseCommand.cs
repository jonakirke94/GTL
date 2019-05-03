using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace GTL.Application.UseCases.US_17_Materials.Commands
{
    public class MaterialBaseCommand : IRequest
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Edition { get; set; }
    }
}
