using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Contracts
{
    public interface ISchießstandRepository
    {
        public event Action<string> OnError;
        public Task<List<Schießtstand>> GetSchießtstände();
        public Task EditSchießstand(Schießtstand schießtstand);
        public Task DeleteSchießstand(Schießtstand schießtstand);
        public Task AddSchießstand(Schießtstand schießstand);
    }
}
