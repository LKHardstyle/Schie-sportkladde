using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Contracts
{
    public interface IVereineRepository
    {
        public event Action<string> OnError;
        public Task<List<Verein>> getVereine(int Id = 0);
        public Task EditVerein(Verein verein);
        public Task AddVerein(Verein verein);
        public Task DeleteVerein(Verein verein);
    }
}
