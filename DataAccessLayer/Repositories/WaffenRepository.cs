using DataAcceessLayer;
using DataAccessLayer.CustomQueryResults;
using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataAccessLayer.Contracts;
using System.Runtime.CompilerServices;

namespace DataAccessLayer.Repositories
{
    public class WaffenRepository : IWaffenRepository
    {
        public event Action<string> OnError;
        private void ErrorOccured(string ErrorMessage)
        {
            if (OnError != null)
            {
                OnError.Invoke(ErrorMessage);
            }
        }

        public async Task<List<WaffenNameDisziplin>> GetWaffenNameDisziplin(int? WaffeId = 0)
        {
            try
            {
                string query = "Select Id, Name, Disziplin from Waffen";
                if (WaffeId != 0)
                    query += $" where Id = {WaffeId}";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return (await connection.QueryAsync<WaffenNameDisziplin>(query)).ToList();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Fehler beim Laden der Namen der Waffen";
                ErrorOccured(errorMessage);
                return new List<WaffenNameDisziplin>();
            }
        }
        public async Task<List<Waffe>> GetWaffen()
        {
            try
            {
                string query = "Select * from Waffen";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return (await connection.QueryAsync<Waffe>(query)).ToList();
                }
            }
            catch (Exception ex)
            {
                string errorMesssage = "Fehler beim Laden der Waffe";
                ErrorOccured(errorMesssage);
                return new List<Waffe>();
            }
        }
        public async Task EditWaffe(Waffe waffe)
        {
            try
            {
                string query = @"Update Waffen
                                SET
                                Name = @Name,
                                Kaliber = @Kaliber,
                                Disziplin = @Disziplin
                                where Id = @Id";
                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    await connection.ExecuteAsync(query, waffe);
                }
            }
            catch (Exception ex) 
            {
                string errorMessage = "Fehler beim Ändern der Waffe";
                ErrorOccured(errorMessage);
            }
        }
        public async Task AddWaffe(Waffe waffe)
        {
            try
            {
                string query = @"Insert into Waffen (Name, Kaliber, Disziplin) VALUES (@Name, @Kaliber, @Disziplin)";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    await connection.ExecuteAsync(query, waffe);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Fehler beim Speichern der Waffe";
                ErrorOccured(errorMessage);
            }
        }
        public async Task DeleteWaffe(Waffe waffe)
        {
            try
            {
                string query = $"Delete from Waffen where Id = {waffe.Id}";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    await connection.ExecuteAsync(query);
                }
            }
            catch(Exception ex)  
            {
                string errorMessage = "Fehler beim Löschen der Waffe";

                if (ex is SqlException sqlEx)
                {
                    errorMessage = "Waffe ist noch in einer Kladde eingetragen  ";   
                }
                ErrorOccured(errorMessage);
            }
        }
    }
}