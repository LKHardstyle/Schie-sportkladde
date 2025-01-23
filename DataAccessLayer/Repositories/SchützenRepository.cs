using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dapper;
using DataAcceessLayer;
using DataAccessLayer.Contracts;
using DataAccessLayer.CustomQueryResults;
using DomainModel.Models;


namespace DataAccessLayer.Repositories
{
    public class SchützenRepository : ISchützenRepository
    {
        public event Action<string> OnError;
        private void ErrorOccured(string ErrorMessage)
        {
            if (OnError != null)
            {
                OnError.Invoke(ErrorMessage);
            }
        }
        public async Task<List<JoinedSchütze>> GetSchützen()
        {
            try
            {
                string query = "Select S.Id, S.Name as SName, S.Geburtsdatum, S.StraßeNr, S.PLZOrt, S.TelefonNr,S.Email, VB.Name as VBName, S.MitgliedsNrVerband, S.EintrittVerband, VE.Name as VEName, S.MitgliedsNrVerein, S.EintrittVerein from Schützen S join Verbände VB on S.VerbandID = VB.Id join Vereine VE on S.VereinID = VE.Id";
                
                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return (await connection.QueryAsync<JoinedSchütze>(query)).ToList();
                }
            }
            catch (Exception)
            {
                string errorMessage = "Fehler beim Laden der Schützen";
                ErrorOccured(errorMessage);
                return new List<JoinedSchütze>();
            }
        }
        public async Task<List<SchützenWaffe>> GetSchützenWaffenCount(int? id = 0)
        {
            try
            {
                string query = "Select COUNT(*) AS [Weaponused], K.SchützeId, W.Name as Waffe From Kladde K join Waffen W on K.WaffeId = W.Id";               
                if (id != 0)
                {
                    query += $" where K.SchützeId = {id}";
                }

                query += " GROUP BY K.SchützeId, W.Name";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return (await connection.QueryAsync<SchützenWaffe>(query)).ToList();
                }
            }
            catch (Exception)
            {
                string errorMessage = "Fehler beim Laden der Namen der Schützen";
                ErrorOccured(errorMessage);
                return new List<SchützenWaffe>();
            }
        }
        public async Task<List<SchützenErgebnis>> GetSchützenErgebnis(int? id = 0)
        {
            try
            {
                string query = "Select S.Id, S.Name, K.Datum, W.Name as Waffe,K.Ergebnis from Schützen S join Kladde K on S.Id = K.SchützeId join Waffen W on K.WaffeId = W.Id ";
                if (id != 0)
                {
                    query += $" where S.Id = {id} AND K.Ergebnis != 0";
                }                

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return (await connection.QueryAsync<SchützenErgebnis>(query)).ToList();
                }
            }
            catch (Exception)
            {
                string errorMessage = "Fehler beim Laden der Namen der Schützen";
                ErrorOccured(errorMessage);
                return new List<SchützenErgebnis>();
            }
        }
        public async Task<List<SchützenName>> GetSchützenName(int? id = 0)
        {
            try
            {
                string query = "Select Id, Name from Schützen";
                if (id != 0)
                    query += $" where Id = {id}";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    return (await connection.QueryAsync<SchützenName>(query)).ToList();
                }
            }
            catch (Exception)
            {
                string errorMessage = "Fehler beim Laden der Namen der Schützen";
                ErrorOccured(errorMessage);
                return new List<SchützenName>();
            }
        }        
        public async Task AddSchütze(Schütze schütze)
        {
            try
            {
                string query = @$"insert into Schützen
                (Name, Geburtsdatum, StraßeNr, PLZOrt, TelefonNr, Email, VerbandID, MitgliedsNrVerband, EintrittVerband, VereinID, MitgliedsNrVerein, EintrittVerein)
                values (@Name, @Geburtsdatum, @StraßeNr, @PLZOrt, @TelefonNr, @Email, @VerbandId, @MitgliedsNrVerband, @EintrittVerband, @VereinId, @MitgliedsNrVerein, @EintrittVerein)";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    await connection.ExecuteAsync(query, schütze);
                }
            }
            catch (Exception)
            {
                string errorMessage = "Fehler beim Speichern von Schützen";
                ErrorOccured(errorMessage);
            }
        }
        public async Task EditSchütze(Schütze schütze)
        {
            try
            {
                string query = @"Update Schützen
                                Set
                                Name = @Name,
                                Geburtsdatum = @Geburtsdatum,
                                StraßeNr = @StraßeNr,
                                PLZOrt = @PLZOrt,
                                TelefonNr = @TelefonNr,
                                Email = @Email,
                                VerbandId = @VerbandId,
                                MitgliedsNrVerband = @MitgliedsNrVerband,
                                EintrittVerband = @EintrittVerband,
                                VereinId = @VereinId,
                                MitgliedsNrVerein = @MitgliedsNrVerein,
                                EintrittVerein = @EintrittVerein
                                where Id = @Id";                                

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    await connection.ExecuteAsync(query, schütze);
                }
            }
            catch (Exception) 
            {
                string errorMessage = "Fehler beim Ändern des Schützen";
                ErrorOccured(errorMessage);
            }
        }
        public async Task DeleteSchütze(JoinedSchütze schütze)
        {
            try
            {
                string query = $"Delete from Schützen where Id = {schütze.Id}";

                using (IDbConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    await connection.ExecuteAsync(query);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Fehler beim Löschen des Schützen";

                if(ex is SqlException sqlEx)
                {
                    if(sqlEx.Number == 547)
                    {
                        errorMessage = "Schütze ist noch in einer Kladde eingetragen";
                    }
                }

                ErrorOccured(errorMessage);
            }
        }
    }
}
