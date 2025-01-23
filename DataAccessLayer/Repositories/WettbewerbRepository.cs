using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Models;
using Dapper;
using DataAccessLayer.Contracts;
using System.Data;
using System.Data.SqlClient;
using DataAcceessLayer;

namespace DataAccessLayer.Repositories
{
    public class WettbewerbRepository : IWettbewerbsRepository
    {
        public event Action<string> OnError;
        private void ErrorOccured(string ErrorMessage)
        {
            if (OnError != null)
            {
                OnError.Invoke(ErrorMessage);
            }
        }
        public async Task<List<Wettbewerb>> GetWettbewerbe()
        {
            try
            {
                string query = "Select Id, Name, SpO from Wettbewerbe";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return (await connection.QueryAsync<Wettbewerb>(query)).ToList();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Fehler beim Laden der Wettbewerbe";
                ErrorOccured(errorMessage);
                return new List<Wettbewerb>();
            }
        }

        public async Task EditWettbewerb(Wettbewerb wettbewerb)
        {
            try
            {
                string query = @"Update Wettbewerbe
                                SET
                                Name = @Name,
                                SpO = @SpO
                                where Id = @Id";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    await connection.ExecuteAsync(query, wettbewerb);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Fehler beim Ändern des Wettbewerbs";
                ErrorOccured(errorMessage);
            }
        }
        public async Task DeleteWettbewerb(Wettbewerb wettbewerb)
        {
            try
            {
                string query = $"Delete From Wettbewerbe where id = {wettbewerb.Id}";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    await connection.ExecuteAsync(query);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Fehler beim Löschen des Wettbewerbs";
                ErrorOccured(errorMessage);
            }
        }
        public async Task AddWettbewerb(Wettbewerb wettbewerb)
        {
            try
            {
                string query = "Insert into Wettbewerbe (Name, SpO) VALUES (@Name, @SpO)";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    await connection.ExecuteAsync(query, wettbewerb);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Fehler beim Speichern des Wettbewerbes" +ex;
                ErrorOccured(errorMessage);
            }
        }
    }
}
