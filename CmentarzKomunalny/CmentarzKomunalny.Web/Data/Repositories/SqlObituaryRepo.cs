using CmentarzKomunalny.Web.Data.Interfaces;
using CmentarzKomunalny.Web.Models.Cmentarz;
using CmentarzKomunalny.Web.Data.Contexts;
using System.Linq;
using System;
using System.Collections.Generic;

namespace CmentarzKomunalny.Web.Data.Repositories
{
    public class SqlObituaryRepo : IObituaryRepo
    {
        private readonly CmentarzContext _context;
        public SqlObituaryRepo(CmentarzContext context)
        {
            _context = context;
        }
        public void AddObituary(Obituary obituary)
        {
            if (obituary == null)
                throw new ArgumentException(nameof(obituary));

            _context.Obituaries.Add(obituary);
        }

        public void DeleteObituary(Obituary obituary)
        {
            if (obituary == null)
                throw new ArgumentException(nameof(obituary));

            _context.Obituaries.Remove(obituary);
        }

        public IEnumerable<Obituary> GetAllObituaries()
        {
            return _context.Obituaries.ToList();
        }

        public Obituary GetObituaryById(int id)
        {
            return _context.Obituaries.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateObituary(Obituary obituary)
        {
            
        }
    }
}
