using System;
using System.Collections.Generic;
using System.IO;
using Android.App;
using Android.Preferences;
using Mono.Data.Sqlite;

namespace BugTracker.Data
{
	public class BugRepository : DbConfig
	{
        //
        public static IEnumerable<BugModel> GetAllBugs(long projectId)
	    {
            var sql = string.Format("SELECT * FROM Bugs WHERE Project = {0};", projectId);

            using (var conn = GetConnection())
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            yield return NewBugFromReader(reader);
                    }
                }
            }
        }

        private static BugModel NewBugFromReader(SqliteDataReader reader)
        {
            return new BugModel(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), 
                                reader.GetDateTime(6), reader.GetString(7), reader.GetString(8), reader.GetInt32(9));
        }

	    public static void Save(BugModel bug)
	    {
            using (var conn = GetConnection())
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {

                    if (bug.Id == 0)
                    {
                        // Do an insert
                        cmd.CommandText = "INSERT INTO [Bugs] ([ExpectedBehavior], [ObservedBehavior], [Steps2Reproduce], [Found], [FoundBy], [Priority], [Assigned2], [Project])" + 
                                          "VALUES (@expectedB, @observedB, @steps, @found, @foundBy, @priority, @assigned2, @project); SELECT last_insert_rowid();";
                        cmd.Parameters.AddWithValue("@expectedB", bug.ExpectedBehavior);
                        cmd.Parameters.AddWithValue("@observedB", bug.ObservedBehavior);
                        cmd.Parameters.AddWithValue("@steps", bug.Steps2Reproduce);
                        cmd.Parameters.AddWithValue("@found", DateTime.Now);
                        cmd.Parameters.AddWithValue("@foundBy", PreferenceManager.GetDefaultSharedPreferences(Application.Context).GetString("Manager", "Cairo"));
                        cmd.Parameters.AddWithValue("@priority", bug.Priority);
                        cmd.Parameters.AddWithValue("@assigned2", bug.Assigned2);
                        cmd.Parameters.AddWithValue("@project", bug.Project);

                        bug.Id = (long)cmd.ExecuteScalar();
                    }
                    else
                    {
                        // Do an update
                        //cmd.CommandText = "UPDATE [bugs] SET [bugName] = @bugName, [bugDescription] = @bugDescription WHERE Id = @Id";
                        //cmd.Parameters.AddWithValue("@Id", bug.Id);
                        //cmd.Parameters.AddWithValue("@bugName", bug.bugName);
                        //cmd.Parameters.AddWithValue("@bugDescription", bug.bugDescription);

                        //cmd.ExecuteNonQuery();
                    }
                }
            }
	        
	    }
	}
}