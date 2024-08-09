using System;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

[ApiController]
[Route("[controller]")]
public class KeepAliveController : ControllerBase
{
    private static long _startTimestamp = Stopwatch.GetTimestamp();
    private static int _isHealthy = 1;

    public static void StartUptimeTimer()
    {
        //in Main() of exe: KeepAliveController.StartUptimeTimer()
        _startTimestamp = Stopwatch.GetTimestamp();
    }

    public static void SetIsHealthy(bool isHealthy)
    {
        Interlocked.Exchange(ref _isHealthy, isHealthy ? 1 : 0);
    }

    //like default,  WebApplication.CreateBuilder(args).Services.AddHealthChecks();
    [HttpGet]
    public IActionResult Get()
    {
        var uptime = GetElapsedTime(_startTimestamp, Stopwatch.GetTimestamp());
        bool isHealthy = true;//Interlocked.Read(ref _isHealthy) == 1;
        var response = new
        {
            status = isHealthy ? "Healthy" : "Unhealthy",
            totalDuration = uptime.ToString("c") //"1.02:03:04"
        };

        return isHealthy ? Ok(response) : StatusCode(503, response);
    }

    private static readonly double TimestampToTicks = TimeSpan.TicksPerSecond / (double)Stopwatch.Frequency;
    private TimeSpan GetElapsedTime(long startTimestamp, long endTimestamp)
    {
        var timestampDelta = endTimestamp - startTimestamp;
        var ticks = (long)(TimestampToTicks * timestampDelta);
        return new TimeSpan(ticks);
    }
}
