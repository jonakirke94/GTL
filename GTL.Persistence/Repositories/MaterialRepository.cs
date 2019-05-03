using System;
using System.Collections.Generic;
using System.Text;
using GTL.Application.Interfaces.Repositories;
using GTL.Domain.Entities;

namespace GTL.Persistence.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        public void Add(string isbn, string title, string description, int edition)
        {
            throw new NotImplementedException();
        }

        public void Update(string isbn, string title, string description, int edition)
        {
            throw new NotImplementedException();
        }

        public Material GetByIsbn(string isbn)
        {
            throw new NotImplementedException();
        }

        public Material GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
