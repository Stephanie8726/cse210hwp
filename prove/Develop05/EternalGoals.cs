class EternalGoal : Goal
{
    public EternalGoal(string name, int points)
    {
        _name = name;
        _points = points;
    }

    public override void RecordEvent()
    {
        Console.WriteLine($"Recording event for {Name}");
        _completed = true;
    }

    public override void DisplayStatus()
    {
        Console.WriteLine($"[{(_completed ? "x" : " ")}] Eternal Goal: {Name}, {_points}, {_completed}");
    }

}