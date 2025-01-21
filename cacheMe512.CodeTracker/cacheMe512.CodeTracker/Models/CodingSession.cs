namespace cacheMe512.CodeTracker.Models;

internal class CodingSession
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public CodingSession(DateTime start, DateTime end)
    {
        StartTime = start;
        EndTime = end;
    }

    public TimeSpan CalculateDuration(DateTime start, DateTime end) => (end - start);

}
