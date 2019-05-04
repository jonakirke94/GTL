using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Domain.Entities;

namespace GTL.Persistence.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly IGTLContext _context;
        public MaterialRepository(IGTLContext context)
        {
            _context = context;
        }

        public void Add(Material material)
        {
            using (var cmd = _context.CreateCommand())
            {
                cmd.Connection.Execute($@"INSERT INTO [Material] ([ISBN], [Title], [Description], [Edition], [Type])
                 VALUES (@{nameof(material.ISBN)}, @{nameof(material.Title)}, @{nameof(material.Description)}, 
                 @{nameof(material.Edition)}, @{nameof(material.Type)})", material, transaction: cmd.Transaction);
            }
        }

        public Material GetByTitle(string title)
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

        public void Update(Material material)
        {
            throw new NotImplementedException();
        }
    }
}
