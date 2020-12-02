using CmentarzKomunalny.Web.Data.Interfaces;
using CmentarzKomunalny.Web.Models.Cmentarz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmentarzKomunalny.Web.Data.Repositories
{
    public class MockDeadPeopleRepo : IDeadPeopleRepo
    {
        public void AddDeadPersonToDb(DeadPerson deadp)
        {
            throw new NotImplementedException();
        }

        public void DeleteDeadPersonFromDb(DeadPerson deadp)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DeadPerson> GetAllDeadPeople()
        {
            var deadPeople = new List<DeadPerson>
            { 
                new DeadPerson
                {
                    IdDeadPerson = 0,
                    Name = "Adam Adamowski",
                    DateOfBirth = "10/10/1999",
                    DateOfDeath = "10/10/2019"
                },

                new DeadPerson
                {
                    IdDeadPerson = 1,
                    Name = "Mateusz Mateuszowski",
                    DateOfBirth = "05/01/1954",
                    DateOfDeath = "31/06/2001"
                }
            };

            return deadPeople;
    }

        public DeadPerson GetDeadPersonById(int id)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateDeadPerson(DeadPerson deadp)
        {
            throw new NotImplementedException();
        }
    }
}
