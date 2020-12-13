using CmentarzKomunalny.Web.Data.Contexts;
using CmentarzKomunalny.Web.Data.Interfaces;
using CmentarzKomunalny.Web.Models.Cmentarz;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmentarzKomunalny.Web.Data.Repositories
{
    public class SqlDeadPeopleRepo : IDeadPeopleRepo
    {
        private readonly CmentarzContext _context;

        public SqlDeadPeopleRepo(CmentarzContext context)
        {
            _context = context;
        }

        public void AddDeadPersonToDb(DeadPerson deadp)
        {
            if (deadp == null)
                throw new ArgumentException(nameof(deadp));

            _context.DeadPeople.Add(deadp);
        }

        public void DeleteDeadPersonFromDb(DeadPerson deadp)
        {
            if (deadp == null)
                throw new ArgumentNullException(nameof(deadp));

            _context.DeadPeople.Remove(deadp);
        }

        public IEnumerable<DeadPerson> GetAllDeadPeople()
        {
            return _context.DeadPeople.ToList();
        }

        public DeadPerson GetDeadPersonById(int id)
        {
            return _context.DeadPeople.FirstOrDefault(p => p.IdDeadPerson == id);
        }

        public IEnumerable<DeadPerson> GetDeadPeopleByLodgeId(int id)
        {
            return _context.DeadPeople.Where(x => x.LodgingId == id).ToList();

        }


        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateDeadPerson(DeadPerson deadp)
        {

        }

    }
}
