# Free C# API Hosting

A .NET 8 Web API in C#, based on the Visual Studio Web API template (Weather Forecast).

Live URL: [WeatherForecast API](https://apitest-v059.onrender.com/WeatherForecast/)

## Prevent Spindown
Connect to [UptimeRobot](https://uptimerobot.com/) to avoid the 50-second delay on the first call.

```csharp
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
        var isHealthy = Interlocked.Read(ref _isHealthy) == 1;
        var response = new
        {
            status = isHealthy ? "Healthy" : "Unhealthy",
            totalDuration = uptime.ToString("c") //"1.02:03:04"
        };

        return _isHealthy ? Ok(response) : StatusCode(503, response);
    }

    private static readonly double TimestampToTicks = TimeSpan.TicksPerSecond / (double)Stopwatch.Frequency;
    private TimeSpan GetElapsedTime(long startTimestamp, long endTimestamp)
    {
        var timestampDelta = endTimestamp - startTimestamp;
        var ticks = (long)(TimestampToTicks * timestampDelta);
        return new TimeSpan(ticks);
    }
}
```

## Deploying a C# Web API on Render.com Using Docker

### Step 1: Create a Render Account

1. Go to [Render.com](https://render.com/) and sign up for a free account.
2. After signing up, log in to your Render dashboard.

### Step 2: Create a New Web Service

1. In the Render dashboard, click on the "New" button and select "Web Service".
2. Connect your GitHub repository containing your C# web API project to Render.

### Step 3: Configure the Service

1. **Name**: Give your service a name.
2. **Environment**: Select Docker.
3. **Build Command**: Leave this blank as the Dockerfile handles the build process.
4. **Start Command**: Leave this blank as the Dockerfile specifies the ENTRYPOINT.
5. **Dockerfile Path**: If your Dockerfile is in the root of the repository, leave this as `Dockerfile`.
6. **Docker Context Directory**: If your Dockerfile is in the root, set this to `/`.

### Step 4: Deploy

1. Click on "Create Web Service".
2. Render will start the build process, pulling the necessary images, building your project, and deploying the service.

Your web service should now be up and running on Render.com!

## Hosting Guide for C# API and React Vite TS Website

### Hosting C# API on Render.com

1. **Create a Render Account**: Sign up at [Render.com](https://render.com/).
2. **Deploy Your API**: 
    - Connect your GitHub repository.
    - Configure the service to use Docker.
    - Deploy your application.
    - Use [Supabase free Postgres DB](https://supabase.com/) with `Npgsql.EntityFrameworkCore.PostgreSQL`.
    - Use [Auth0](https://auth0.com/) for free JWT login.

### Hosting React Vite TS Website on Vercel

1. **Create a Vercel Account**: Sign up at [Vercel.com](https://vercel.com/).
2. **Deploy Your Website**: 
    - Connect your GitHub repository.
    - Configure the project settings.
    - Deploy your website.
3. **Overcome CORS with Serverless Functions**: 
    - Use Vercel's serverless functions to proxy API requests.
    - Handle CORS issues effectively.
