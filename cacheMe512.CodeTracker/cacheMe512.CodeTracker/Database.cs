using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace cacheMe512.CodeTracker;

internal static class Database
{
    private static string ConnectionString;
    private static string LogLevel;

    static Database()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        ConnectionString = configuration.GetConnectionString("CodeTrackerDatabase");
        LogLevel = configuration["AppSettings:LogLevel"];
    }

    internal static SqliteConnection GetConnection()
    {
        var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        return connection;
    }

    internal static void InitializeDatabase()
    {
        if (LogLevel == "Debug" && File.Exists("code-tracker.db"))
        {
            File.Delete("code-tracker.db");
            Console.WriteLine("Existing database deleted for testing.");
        }

        using (var connection = GetConnection())
        {
            var command = connection.CreateCommand();
            command.CommandText =
                @"CREATE TABLE IF NOT EXISTS coding_sessions (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        StartTime TEXT NOT NULL,
                        EndTime TEXT NOT NULL,
                        Duration INTEGER NOT NULL
                    );";
            command.ExecuteNonQuery();

            SeedData(connection);
        }
    }
    private static void SeedData(SqliteConnection connection)
    {
        var command = connection.CreateCommand();

        var random = new Random();
        for (int i = 0; i < 100; i++)
        {
            string start = DateTime.Now.AddDays(-random.Next(0, 30)).ToString("yyyy-MM-dd HH:mm:ss");

            int randomHours = random.Next(0, 9);
            int randomMinutes = random.Next(0, 60);

            TimeSpan randomDuration = new TimeSpan(randomHours, randomMinutes, 0);

            DateTime endTime = DateTime.Parse(start).Add(randomDuration);
            int totalSeconds = (int)randomDuration.TotalSeconds;

            command.CommandText =
                "INSERT INTO coding_sessions (StartTime, EndTime, Duration) VALUES (@start, @end, @duration);";

            command.Parameters.AddWithValue("@start", start);
            command.Parameters.AddWithValue("@end", endTime.ToString("yyyy-MM-dd HH:mm:ss"));
            command.Parameters.AddWithValue("@duration", totalSeconds);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }

        Console.WriteLine("Database seeded with sample data.");
    }


}
