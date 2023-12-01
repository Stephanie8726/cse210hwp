using System;
using System.Collections.Generic;
using System.IO;

class Activity
{
    protected DateTime date;
    protected int minutes;

    public Activity(DateTime date, int minutes)
    {
        this.date = date;
        this.minutes = minutes;
    }

    // Common method to get summary
    public string GetSummary(string activityType, double distance, double speed, double pace)
    {
        return $"{date.ToString("dd MMM yyyy")} {activityType} ({minutes} min) - Distance: {distance} miles, Speed: {speed} mph, Pace: {pace} min per mile";
    }
}

class Running : Activity
{
    private double distance;

    public Running(DateTime date, int minutes, double distance) : base(date, minutes)
    {
        this.distance = distance;
    }

    public string GetRunningSummary()
    {
        double speed = distance / minutes * 60;
        double pace = minutes / distance;

        return GetSummary("Running", distance, speed, pace);
    }
}

class Cycling : Activity
{
    private double speed;

    public Cycling(DateTime date, int minutes, double speed) : base(date, minutes)
    {
        this.speed = speed;
    }

    public string GetCyclingSummary()
    {
        double distance = speed * minutes / 60;
        double pace = 60 / speed;

        return GetSummary("Cycling", distance, speed, pace);
    }
}

class Swimming : Activity
{
    private int laps;

    public Swimming(DateTime date, int minutes, int laps) : base(date, minutes)
    {
        this.laps = laps;
    }
public string GetSwimmingSummary()
{
    double distance = laps * 50 * 0.001 * 0.62;
    double speed = (minutes != 0) ? distance / (minutes) * 60:0;
    double pace = (minutes != 0) ? (minutes / laps) : 0;

    return GetSummary("Swimming", distance, speed, pace);
}
}

class Program
{
    static void Main()
    {
        // Create activities
        Running runningActivity = new Running(new DateTime(2022, 11, 03), 30, 3.0);
        Cycling cyclingActivity = new Cycling(new DateTime(2022, 11, 03), 30, 6.0);
        Swimming swimmingActivity = new Swimming(new DateTime(2022, 11, 03), 30, 10);

        // Display summary for each activity
        Console.WriteLine(runningActivity.GetRunningSummary());
        Console.WriteLine(cyclingActivity.GetCyclingSummary());
        Console.WriteLine(swimmingActivity.GetSwimmingSummary());
    }
}
