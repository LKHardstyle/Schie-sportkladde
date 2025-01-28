using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Models;
using DataAccessLayer.Contracts;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using DataAcceessLayer;
using DataAccessLayer.CustomQueryResults;
using System.Runtime.CompilerServices;

namespace DataAccessLayer.Repositories
{
    public class KladdenRepository : IKladdenRepository
    {

        public event Action<string> OnError;
        
        private void ErrorOccured(string ErrorMessage)
        {
            if(OnError != null)
            {
                OnError.Invoke(ErrorMessage);
            }
        }
        public async Task<List<JoinedKladde>> getKladde(int? schützeId = 0, string? date = null)
        {
            try
            {
                string query = "Select K.Id, S.Name as Schütze, K.Datum, Wet.Name as Wettbewerb, W.Name as Waffe, W.Kaliber, K.Schusszahl, K.Ergebnis, W.Disziplin, St.Name as Schießstand, A.Name as Aufsicht from Kladde K join Schützen S on K.SchützeId = S.Id join Wettbewerbe Wet on K.WettbewerbId = Wet.Id join Waffen W on K.WaffeId = W.Id join Schießstände St on K.SchießstandId = St.Id join Aufsichten A on K.AufsichtId = A.Id";

                bool hasCondition = false;

                if (schützeId != 0)
                {
                    query += $" where K.SchützeId = '{schützeId}'";
                    hasCondition = true;
                }

                if (!string.IsNullOrEmpty(date))
                {
                    string yearFilter = $"K.Datum LIKE '%{date}'";

                    if (hasCondition)
                    {
                        query += $" and {yearFilter}";
                    }
                    else
                    {
                        query += $" where {yearFilter}";
                    }
                }

                query += " ORDER BY YEAR(CONVERT(DATE, K.Datum, 104)) ASC";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return (await connection.QueryAsync<JoinedKladde>(query)).ToList();
                }
            }
            catch (Exception)
            {                
                string errorMessage = "Es gab einen Fehler beim Laden der Kladde";
                ErrorOccured(errorMessage);
                return new List<JoinedKladde>();
            }
        }
        public async Task<List<KladdenYear>> GetKladdenYears(int? schützeId = 0)
        {
            try
            {
                string query = "SELECT DISTINCT YEAR(CONVERT(DATE, SUBSTRING(Datum, 7, 4) + '-' + SUBSTRING(Datum, 4, 2) + '-' + SUBSTRING(Datum, 1, 2), 120)) AS Date FROM Kladde";
                if (schützeId != 0)
                {
                    query += $" where SchützeId = {schützeId} AND Ergebnis != 0";
                }

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return (await connection.QueryAsync<KladdenYear>(query)).ToList();
                }
            }
            catch (Exception) 
            {

                string errorMessage = "Es gab einen Fehler beim Laden der Jahre der Kladdeneinträge";
                ErrorOccured(errorMessage);
                return new List<KladdenYear>();
            }
        }
        public async Task EditKladde(Kladde kladde)
        {
            try
            {
                string query = @"update Kladde
                                set
                                SchützeId = @SchützeId,
                                Datum = @Datum,
                                WettbewerbId = @WettbewerbId,
                                WaffeId = @WaffeId,
                                Schusszahl = @Schusszahl,
                                SchießstandId = @SchießstandId,
                                Ergebnis = @Ergebnis,
                                AufsichtId = @AufsichtId
                                where Id = @Id";                
                
                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    await connection.ExecuteAsync(query, kladde);
                }
            }
            catch (Exception)
            {
                string errorMessage = "Fehler beim Ändern der Kladde";
                ErrorOccured(errorMessage);
            }
        }
        public async Task AddKladde(Kladde kladde)
        {
            try
            {
                string query = @"Insert into Kladde
                (SchützeId, Datum, WettbewerbId, WaffeId, Schusszahl, SchießstandId, Ergebnis, AufsichtId)
                values(@SchützeId, @Datum, @WettbewerbId, @WaffeId, @Schusszahl, @SchießstandId, @Ergebnis, @AufsichtId)";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    await connection.ExecuteAsync(query, kladde);
                }
            }
            catch (Exception a)
            {
                string errorMessage = "Fehler beim Hinzufügen der Kladde" + a;
                ErrorOccured(errorMessage);
            }
        }
        public async Task DeleteKladde(JoinedKladde kladde)
        {
            try
            {
                string query = $"Delete from Kladde where Id = {kladde.Id}";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    await connection.ExecuteAsync(query);
                }
            }
            catch (Exception)
            {
                string errorMessage = "Fehler beim Löschen der Kladde";
                ErrorOccured(errorMessage);
            }
        }
    }
}
