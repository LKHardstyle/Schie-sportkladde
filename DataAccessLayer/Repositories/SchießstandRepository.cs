using DataAcceessLayer;
using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataAccessLayer.Contracts;

namespace DataAccessLayer.Repositories
{
    public class SchießstandRepository : ISchießstandRepository
    {
        public event Action<string> OnError;
        private void ErrorOccured(string ErrorMessage)
        {
            if (OnError != null)
            {
                OnError.Invoke(ErrorMessage);
            }
        }

        public async Task<List<Schießtstand>> GetSchießtstände()
        {
            try
            {
                string query = "Select Id, Name as Schießstand from Schießstände";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return (await connection.QueryAsync<Schießtstand>(query)).ToList();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Fehler beim Laden der Schießstände";
                ErrorOccured(errorMessage);
                return new List<Schießtstand>();
            }
        }
        public async Task EditSchießstand(Schießtstand schießtstand)
        {
            try
            {
                string query = @"Update Schießstände
                                Set 
                                Name = @Name
                                where Id = @Id";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    await connection.ExecuteAsync(query, schießtstand);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Fehler beim Ändern des Schießstandes";
                ErrorOccured(errorMessage);
            }
        }
        public async Task DeleteSchießstand(Schießtstand schießtstand)
        {
            try
            {
                string query = $"Delete from Schießstände where Id = {schießtstand.Id}";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    await connection.ExecuteAsync(query);
                }

            }
            catch (Exception ex)
            {
                string errorMessage = "Fehler beim Löschen des Schießstandes";

                if (ex is SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)
                    {
                        errorMessage = "Dem Schießstand ist noch eine Aufsicht zugewiesen";
                    }
                }
            }
        }
        public async Task AddSchießstand(Schießtstand schießstand)
        {
            try
            {
                string query = "Insert into Schießstände (Name) VALUES (@Name)";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    await connection.ExecuteAsync(query, schießstand);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Fehler beim Speichern des Schießstandes";
                ErrorOccured(errorMessage);
            }
        }
    }
}
