using DataAccessLayer.CustomQueryResults;
using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Contracts
{
    public interface ISchützenRepository
    {
        public event Action<string> OnError;
        public Task<List<SchützenName>> GetSchützenName(int? id = 0);
        public Task<List<SchützenErgebnis>> GetSchützenErgebnis(int? id = 0);
        public Task<List<SchützenWaffe>> GetSchützenWaffenCount(int? id = 0);
        public Task<List<JoinedSchütze>> GetSchützen();
        public Task AddSchütze(Schütze schütze);
        public Task EditSchütze(Schütze schütze);
        public Task DeleteSchütze(JoinedSchütze schütze);
    }
}
