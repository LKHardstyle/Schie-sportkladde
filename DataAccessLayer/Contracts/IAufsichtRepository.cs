using DataAccessLayer.CustomQueryResults;
using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Contracts
{
    public interface IAufsichtRepository
    {
        public event Action<string> OnError;
        public Task<List<Aufsicht>> GetAufsicht();
        public Task DeleteAufsicht(Aufsicht aufsicht);
        public Task EditAufsicht(Aufsicht aufsicht);
        public Task AddAufsicht(Aufsicht aufsicht);       
    }
}
