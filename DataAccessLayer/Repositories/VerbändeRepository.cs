using DataAcceessLayer;
using DataAccessLayer.Contracts;
using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DataAccessLayer.Repositories
{
    public class VerbändeRepository : IVerbändeRepository
    {
        public event Action<string> OnError;
        private void ErrorOccured(string ErrorMessage)
        {
            if (OnError != null)
            {
                OnError.Invoke(ErrorMessage);
            }
        }

        public async Task<List<Verband>> getVerband()
        {
            try
            {
                string query = "Select * from Verbände";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return (await connection.QueryAsync<Verband>(query)).ToList();
                }
            }
            catch (Exception ex) 
            {
                string errorMessage = "Fehler beim Laden der Verbände";
                return new List<Verband>();
                ErrorOccured(errorMessage);
            }
        }

        public async Task EditVerband(Verband verband)
        {
            try
            {
                string query = @"Update Verbände
                                SET
                                Name = @Name
                                where Id = @Id";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    await connection.ExecuteAsync(query, verband);
                }
            }
            catch(Exception ex)
            {
                string errorMessage = "Fehler beim Ändern des Verbandes";
                ErrorOccured(errorMessage);                
            }
        }

        public async Task DeleteVerband(Verband verband)
        {
            try
            {
                string query = $"Delete From Verbände where Id = {verband.Id}";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    await connection.ExecuteAsync(query);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Fehler beim Löschen des Verbandes";

                if(ex is SqlException sqlEx)
                {
                    errorMessage = "Verband ist noch einem Schützen zugewiesen";
                }
                ErrorOccured(errorMessage);
            }
        }

        public async Task AddVerband(Verband verband)
        {
            try
            {
                string query = "Insert into Verbände (Name) VALUES (@Name)";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    await connection.ExecuteAsync(query,verband);
                }
            }
            catch(Exception ex)
            {
                string errorMessage = "Fehler beim Speichern des Verbandes";
                ErrorOccured(errorMessage);
            }
        }
    }
}
