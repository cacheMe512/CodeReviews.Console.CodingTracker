using cacheMe512.CodeTracker.Models;
using Dapper;

namespace cacheMe512.CodeTracker;

internal class SessionsController
{
    public IEnumerable<CodingSession> GetAllSessions()
    {
        using var connection = Database.GetConnection();
        var sessions = connection.Query<CodingSession>(
            "SELECT Id, StartTime, EndTime, Duration AS DurationInSeconds FROM coding_sessions").ToList();

        return sessions;
    }

    public void InsertSession(CodingSession session)
    {
        using var connection = Database.GetConnection();
        connection.Execute(
            "INSERT INTO coding_sessions (StartTime, EndTime, Duration) VALUES (@StartTime, @EndTime, @Duration)",
            new { StartTime = session.StartTime, EndTime = session.EndTime, Duration = session.CalculateDuration().TotalSeconds });
    }

    public bool DeleteSession(int id)
    {
        using var connection = Database.GetConnection();
        var rowsAffected = connection.Execute(
            "DELETE FROM coding_sessions WHERE Id = @Id", new { Id = id });

        return rowsAffected > 0;
    }
}
