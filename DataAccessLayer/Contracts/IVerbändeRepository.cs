using DataAccessLayer.CustomQueryResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Models;

namespace DataAccessLayer.Contracts
{
    public interface IVerbändeRepository
    {
        public event Action<string> OnError;
        public Task<List<Verband>> getVerband();
        public Task DeleteVerband(Verband clickedVerband);
        public Task EditVerband(Verband verband);
        public Task AddVerband(Verband verband);
    }
}
