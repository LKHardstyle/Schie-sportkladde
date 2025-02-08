using DataAcceessLayer;
using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Contracts;
using Dapper;

namespace DataAccessLayer.Repositories
{
    public class VereineRepository : IVereineRepository
    {
        public event Action<string> OnError;
        private void ErrorOccured(string ErrorMessage)
        {
            if (OnError != null)
            {
                OnError.Invoke(ErrorMessage);
            }
        }

        public async Task<List<Verein>> getVereine(int Id = 0)
        {
            try
            {
                string query = "Select * from Vereine";
                if (Id != 0)
                {
                    query += $" where Id = {Id}";
                }

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return (await connection.QueryAsync<Verein>(query)).ToList();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Fehler beim Laden der Vereine";
                ErrorOccured(errorMessage);
                return new List<Verein>();
            }
        }     
        public async Task EditVerein(Verein verein)
        {
            try
            {
                string query = @"Update Vereine
                                Set 
                                Name = @Name
                                where Id = @Id";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    await connection.ExecuteAsync(query, verein);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Fehler beim Ändern des Vereins";
                ErrorOccured(errorMessage);                
            }
        }
        public async Task AddVerein(Verein verein)
        {
            try
            {
                string query = "Insert into Vereine (Name) VALUES (@Name)";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    await connection.ExecuteAsync(query, verein);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Fehler beim Speichern des Vereins";
                ErrorOccured(errorMessage);
            }
        }
        public async Task DeleteVerein(Verein verein)
        {
            try
            {
                string query = $"Delete from Vereine where Id = {verein.Id}";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    await connection.ExecuteAsync(query);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Fehler beim Löschen des Schützen";
                
                if (ex is SqlException sqlEx)
                {
                    errorMessage = "Verein ist noch einem Schützen zugewiesen";
                }
                ErrorOccured(errorMessage);
            }
        }
    }
}
