using CmentarzKomunalny.Web.Models.Cmentarz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmentarzKomunalny.Web.Data.Interfaces
{
    public interface ILodgingsRepo
    {
        void AddLodgeToDb(Lodging lodge);
        void DeleteLodgeFromDb(Lodging lodge);
        void UpdateLodge(Lodging lodge);
        IEnumerable<Lodging> GetAllLodgings();
        Lodging GetLodgeById(int id);
        bool SaveChanges();

    }
}
