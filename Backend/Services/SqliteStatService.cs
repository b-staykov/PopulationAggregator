using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class SqliteStatService : IStatService
    {
        private readonly IDbManager _dbManager;

        public SqliteStatService(IDbManager dbManagerManager)
        {
            _dbManager = dbManagerManager;
        }

        public List<Tuple<string, int>> GetCountryPopulations()
        {
            List<Tuple<string, int>> result = new List<Tuple<string, int>>();

            using var conn = (SqliteConnection)_dbManager?.GetConnection();

            if (conn == null)
            {
                throw new InvalidOperationException("Failed to get connection");
            }

            conn.Open();

            try
            {
                var sql = @"SELECT country.CountryName [Country],
		                            SUM(city.Population) [Population]	
                              FROM City
	                            INNER JOIN State on State.StateId = City.StateId 
	                            INNER JOIN Country on Country.CountryId = State.CountryId
                            WHERE City.Population > 0
                            GROUP BY Country.CountryId";

                using var cmd = new SqliteCommand(sql, conn);
                using var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    result.Add(Tuple.Create(rdr.GetString(0), (int)rdr.GetInt32(1)));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}{Environment.NewLine}{e.StackTrace}");
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public Task<List<Tuple<string, int>>> GetCountryPopulationsAsync()
        {
            return Task.FromResult<List<Tuple<string, int>>>(GetCountryPopulations());
        }
    }
}
