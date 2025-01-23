using DataAccessLayer.CustomQueryResults;
using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Contracts
{
    public interface IWaffenRepository
    {
        public event Action<string> OnError;
        public Task<List<WaffenNameDisziplin>> GetWaffenNameDisziplin(int? WaffeId = 0);
        public Task<List<Waffe>> GetWaffen();
        public Task EditWaffe(Waffe waffe);
        public Task AddWaffe(Waffe waffe);
        public Task DeleteWaffe(Waffe waffe);
    }
}
