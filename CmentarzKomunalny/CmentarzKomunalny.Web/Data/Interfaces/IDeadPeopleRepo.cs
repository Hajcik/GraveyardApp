using CmentarzKomunalny.Web.Models.Cmentarz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmentarzKomunalny.Web.Data.Interfaces
{
    public interface IDeadPeopleRepo
    {
        IEnumerable<DeadPerson> GetAllDeadPeople();
        DeadPerson GetDeadPersonById(int id);
        void AddDeadPersonToDb(DeadPerson deadp);
        void DeleteDeadPersonFromDb(DeadPerson deadp);
        void UpdateDeadPerson(DeadPerson deadp);

        bool SaveChanges();
        
    }
}
