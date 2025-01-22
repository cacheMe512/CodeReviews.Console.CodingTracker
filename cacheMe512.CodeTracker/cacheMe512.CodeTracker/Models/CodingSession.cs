﻿namespace cacheMe512.CodeTracker.Models;

internal class CodingSession
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan Duration { get; set; }


    public TimeSpan CalculateDuration() => (EndTime - StartTime);

}
