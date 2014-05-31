using System;
using System.Collections.Generic;
using System.IO;
using Mono.Data.Sqlite;

namespace BugTracker.Data
{
	public class ProjectRepository : DbConfig
	{
        public static IEnumerable<ProjectModel> GetAllProjects()
        {
            const string sql = "SELECT * FROM Project;";

            using (var conn = GetConnection())
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            yield return new ProjectModel(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                    }
                }
            }
        }
	}
}