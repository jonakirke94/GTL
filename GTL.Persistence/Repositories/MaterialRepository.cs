using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using GTL.Application.Interfaces.Repositories;
using GTL.Application.Interfaces.UnitOfWork;
using GTL.Domain.Entities;
using GTL.Domain.Exceptions;

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
                const string query = @"INSERT INTO [Material] ([ISBN], [Title], [Description], [Edition], [Type])
                 VALUES (@isbn, @title, @description, @edition, @type)";

                var para = new DynamicParameters();
                para.Add("@isbn", material.ISBN.Number);
                para.Add("@title", material.Title);
                para.Add("@description", material.Description);
                para.Add("@edition", material.Edition);
                para.Add("@type", material.Type.ToString());
                try
                {
                    cmd.Connection.Execute(query, para, cmd.Transaction);
                }
                catch (SqlException e)
                {
                    if (e.Number == 2627)
                    {
                        throw new ISBNAlreadyExistException(material.ISBN.Number, e);
                    }

                    throw;

                }
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
            using (var cmd = _context.CreateCommand())
            {
                const string query = @"UPDATE [Material]" +
                                     "SET [Title] = @title, " +
                                     "[Description], " +
                                     "[Edition] = @edition, " +
                                     "[Type] = @type, " +
                                     "[Area] = @area, " +
                                     "[Size] = @size " +
                                     "WHERE ISBN = @isbn";

                var para = new DynamicParameters();
                para.Add("@title", material.Title);
                para.Add("@description", material.Description);
                para.Add("@edition", material.Edition);
                para.Add("@type", material.Type.ToString());
                para.Add("@area", material.Area);
                para.Add("@size", material.Size);
                para.Add("@isbn", material.ISBN.Number);

                cmd.Connection.Execute(query, para, cmd.Transaction);
            }
        }

        public Task<IEnumerable<Material>> GetAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var cmd = _context.CreateCommand())
            {
                const string query = @"SELECT * FROM [User]";

                // TODO Ask Jonathan hvordan vi gør

                //var materials = cmd.Connection.QueryMultipleAsync<Material>(query);

                //return materials.Result == null ? materials.Result : new List<Material>();
            }
            throw new NotImplementedException();
        }
    }
}
