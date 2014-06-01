using System;
using System.Collections.Generic;
using System.IO;
using Mono.Data.Sqlite;

namespace BugTracker.Data
{
	public class BugRepository : DbConfig
	{
        //
        public static IEnumerable<BugModel> GetAllBugs(long projectId = 0)
	    {
            var sql = "SELECT * FROM Bugs";

            if (projectId > 0)
            {
                sql = string.Format("{0} WHERE Project = {1};", sql, projectId);
            }

            using (var conn = GetConnection())
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            yield return new BugModel(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), 
                                                      reader.GetString(4), reader.GetBoolean(5), reader.GetDateTime(6), reader.GetString(7), 
                                                      reader.GetString(8));
                    }
                }
            }
        }
	}
}