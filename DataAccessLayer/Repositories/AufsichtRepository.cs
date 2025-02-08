using DataAcceessLayer;
using DataAccessLayer.CustomQueryResults;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Models;
using Dapper;
using DataAccessLayer.Contracts;

namespace DataAccessLayer.Repositories
{
    public class AufsichtRepository : IAufsichtRepository
    {
        public event Action<string> OnError;
        private void ErrorOccured(string ErrorMessage)
        {
            if (OnError != null)
            {
                OnError.Invoke(ErrorMessage);
            }
        }

        public async Task<List<Aufsicht>> GetAufsicht()
        {
            try
            {
                string query = "Select Id, Name from Aufsichten";               

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return (await connection.QueryAsync<Aufsicht>(query)).ToList();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Fehler beim Laden der Waffen";
                ErrorOccured(errorMessage);
                return new List<Aufsicht>();
            }
        }        

        public async Task DeleteAufsicht(Aufsicht aufsicht)
        {
            try
            {
                string query = $"Delete From Aufsichten where Id = {aufsicht.Id}";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    await connection.ExecuteAsync(query);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Fehler beim Löschen der Aufsicht";
                if (ex is SqlException sqlEx)
                {
                    errorMessage = "Aufsicht ist noch in einer Kladde eingetragen  ";
                }
                ErrorOccured(errorMessage);
            }
        }
        public async Task EditAufsicht(Aufsicht aufsicht)
        {
            try
            {
                string query = @"Update Aufsichten
                                SET
                                Name = @Name                                
                                where Id = @Id";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    await connection.ExecuteAsync(query, aufsicht);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Fehler beim Ändern der Aufsicht" + ex;
                ErrorOccured(errorMessage);
            }
        }
        public async Task AddAufsicht(Aufsicht aufsicht)
        {
            try
            {
                string query = "Insert into Aufsichten (Name) VALUES (@Name)";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    await connection.ExecuteAsync(query, aufsicht);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Fehler beim Speichern der Aufsicht";
                ErrorOccured(errorMessage);
            }
        }
    }
}
