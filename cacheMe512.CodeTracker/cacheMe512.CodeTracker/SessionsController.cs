using cacheMe512.CodeTracker.Models;
using Spectre.Console;
using static System.Reflection.Metadata.BlobBuilder;

namespace cacheMe512.CodeTracker;

internal class SessionsController
{
    public IEnumerable<CodingSession> GetAllSessions()
    {
        using var connection = Database.GetConnection();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, StartTime, EndTime, Duration FROM coding_sessions";

        using var reader = command.ExecuteReader();
        var sessions = new List<CodingSession>();

        while (reader.Read())
        {
            var startTime = reader.GetDateTime(1);
            var endTime = reader.GetDateTime(2);
            var durationSeconds = reader.GetInt32(3);

            var duration = TimeSpan.FromSeconds(durationSeconds);

            sessions.Add(new CodingSession
            {
                Id = reader.GetInt32(0),
                StartTime = startTime,
                EndTime = endTime,
                Duration = duration
            });
        }

        return sessions;

    }

    public void InsertSession(CodingSession session)
    {
        using var connection = Database.GetConnection();
        var command = connection.CreateCommand();

        command.CommandText =
            "INSERT INTO coding_sessions (StartTime, EndTime, Duration) VALUES (@StartTime, @EndTime, @Duration)";
        command.Parameters.AddWithValue("@StartTime", session.StartTime);
        command.Parameters.AddWithValue("@EndTime", session.EndTime);
        command.Parameters.AddWithValue("@Duration", session.CalculateDuration().TotalSeconds);
        command.ExecuteNonQuery();
    }

    public bool DeleteSession(int id)
    {
        using var connection = Database.GetConnection();
        var command = connection.CreateCommand();

        command.CommandText = "DELETE FROM coding_sessions WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);

        int rowsAffected = command.ExecuteNonQuery();

        return rowsAffected > 0;
    }


}
