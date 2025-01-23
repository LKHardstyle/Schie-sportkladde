using DataAccessLayer.CustomQueryResults;
using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Contracts
{
    public interface IKladdenRepository
    {
        public event Action<string> OnError;        
        public Task<List<JoinedKladde>> getKladde(int? schützeId = 0, string? date = null);
        public Task EditKladde(Kladde kladde);
        public Task AddKladde(Kladde kladde);
        public Task DeleteKladde(JoinedKladde kladde);
        public Task<List<KladdenYear>> GetKladdenYears();
    }
}
