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
            const string sql = "SELECT * FROM Projects;";

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

        public static ProjectModel GetProject(long id)
        {
            const string sql = "SELECT * FROM Projects WHERE Id = @id;";

            using (var conn = GetConnection())
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        return reader.Read() ? new ProjectModel(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)) : null;
                    }
                }
            }
        }

        public static void SaveProject(ProjectModel project)
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {

                    if (project.Id == 0)
                    {
                        // Do an insert
                        cmd.CommandText = "INSERT INTO [Projects] ([ProjectName], [ProjectDescription]) VALUES (@ProjectName, @ProjectDescription); SELECT last_insert_rowid();";
                        cmd.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                        cmd.Parameters.AddWithValue("@ProjectDescription", project.ProjectDescription);

                        project.Id = (long)cmd.ExecuteScalar();
                    }
                    else
                    {
                        // Do an update
                        cmd.CommandText = "UPDATE [Projects] SET [ProjectName] = @ProjectName, [ProjectDescription] = @ProjectDescription WHERE Id = @Id";
                        cmd.Parameters.AddWithValue("@Id", project.Id);
                        cmd.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                        cmd.Parameters.AddWithValue("@ProjectDescription", project.ProjectDescription);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

	    public static void Delete(long id)
	    {
	        const string sql = "DELETE FROM Projects WHERE Id = @Id;";

	        using (var conn = GetConnection())
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@Id", id);

                    cmd.ExecuteNonQuery();
                }
            }
	    }
	}
}