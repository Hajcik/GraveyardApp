using CmentarzKomunalny.Web.Models.Cmentarz;
using System.Collections.Generic;

namespace CmentarzKomunalny.Web.Data.Interfaces
{
    public interface IGraveyardRepo
    {
        void UpdateGraveyardDb(Graveyard graveyard);
        public IEnumerable<Graveyard> GetGraveyardInfo();
        bool SaveChanges();
    }
}
