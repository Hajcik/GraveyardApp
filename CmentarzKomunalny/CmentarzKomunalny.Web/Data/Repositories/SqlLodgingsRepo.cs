using CmentarzKomunalny.Web.Data.Contexts;
using CmentarzKomunalny.Web.Data.Interfaces;
using CmentarzKomunalny.Web.Models.Cmentarz;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CmentarzKomunalny.Web.Data.Repositories
{
    public class SqlLodgingsRepo : ILodgingsRepo
    {
        private readonly CmentarzContext _context;
        
        public SqlLodgingsRepo(CmentarzContext context)
        {
            _context = context;
        }

        public void AddLodgeToDb(Lodging lodge)
        {
            if (lodge == null)
                throw new ArgumentException(nameof(lodge));

            _context.Lodgings.Add(lodge);
        }

        public void DeleteLodgeFromDb(Lodging lodge)
        {
            if (lodge == null)
                throw new ArgumentNullException(nameof(lodge));

            _context.Lodgings.Remove(lodge);
        }

        public IEnumerable<Lodging> GetAllLodgings()
        {
            return _context.Lodgings.ToList();
        }

        public Lodging GetLodgeById(int id)
        {
            return _context.Lodgings.FirstOrDefault(p => p.IdLodge == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateLodge(Lodging lodge)
        {
            
        }
    }
}
