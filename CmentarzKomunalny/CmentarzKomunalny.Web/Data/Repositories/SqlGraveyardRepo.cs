using CmentarzKomunalny.Web.Data.Interfaces;
using CmentarzKomunalny.Web.Models.Cmentarz;
using CmentarzKomunalny.Web.Data.Contexts;
using System.Linq;
using System.Collections.Generic;

namespace CmentarzKomunalny.Web.Data.Repositories
{
    public class SqlGraveyardRepo : IGraveyardRepo
    {
        private readonly CmentarzContext _context;
        public SqlGraveyardRepo(CmentarzContext context)
        {
            _context = context;
        }

        public IEnumerable<Graveyard> GetGraveyardInfo()
        {
            return _context.GraveyardLimits.ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateGraveyardDb(Graveyard graveyard)
        {
            
        }
    }
}
