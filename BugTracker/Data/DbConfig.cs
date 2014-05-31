using System;
using System.IO;
using Mono.Data.Sqlite;

namespace BugTracker.Data
{
	public class DbConfig
	{
	    private const string DbFile = "bugTracks.db3";

	    protected static SqliteConnection GetConnection()
		{
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DbFile);
			var exists = File.Exists(dbPath);

			if (!exists)
				SqliteConnection.CreateFile(dbPath);

			var conn = new SqliteConnection("Data Source=" + dbPath);

			if (!exists)
				CreateTables(conn);

			return conn;
		}

		private static void CreateTables(SqliteConnection connection)
		{
			connection.Open();

		    var sql = "CREATE TABLE [Project] ([Id] INTEGER PRIMARY KEY AUTOINCREMENT, [ProjectName] VARCHAR(255) NOT NULL, [ProjectDescription] TEXT)";

            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "CREATE TABLE [Bug] ([Id] INTEGER PRIMARY KEY AUTOINCREMENT CONSTRAINT [Project] REFERENCES [Project]([Id]), [FoundBy] VARCHAR(255) NOT NULL," +
                               "[ExpectedBehavior] TEXT, [ObservedBehavior] TEXT, [Steps2Reproduce] TEXT, [Fixed] BOOLEAN," +
                               "[Found] TIMESTAMP, [Priority] VARCHAR(50), [Assigned2] VARCHAR(255) )";

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }

            // Create a sample note to get the user started
            sql = "INSERT INTO [Project] ([ProjectName], [ProjectDescription]) VALUES (@ProjectName, @ProjectDescription);";

            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@ProjectName", "Sample Project");
                cmd.Parameters.AddWithValue("@ProjectDescription", "Sample project for traking bugs!");

                cmd.ExecuteNonQuery();
            }

			connection.Close();
		}

	}
}