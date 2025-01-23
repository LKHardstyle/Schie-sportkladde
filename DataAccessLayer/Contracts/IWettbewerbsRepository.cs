using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Contracts
{
    public interface IWettbewerbsRepository
    {
        public event Action<string> OnError;
        public Task<List<Wettbewerb>> GetWettbewerbe();
        public Task EditWettbewerb(Wettbewerb wettbewerb);
        public Task DeleteWettbewerb(Wettbewerb wettbewerb);
        public Task AddWettbewerb(Wettbewerb wettbewerb);
    }
}
