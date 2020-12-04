using System.Collections.Generic;
using CmentarzKomunalny.Web.Models.Cmentarz;

namespace CmentarzKomunalny.Web.Data.Interfaces
{
    public interface IObituaryRepo
    {
        void AddObituary(Obituary obituary);
        void DeleteObituary(Obituary obituary);
        IEnumerable<Obituary> GetAllObituaries();
        Obituary GetObituaryById(int id);
        bool SaveChanges();
        void UpdateObituary(Obituary obituary);
    }
}
