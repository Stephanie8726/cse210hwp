class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public override void RecordEvent()
    {
        _completed = true;
    }

    public override void DisplayStatus()
    {
        Console.WriteLine($"[{(_completed ? "x" : " ")}] Eternal Goal: {Name} ({Description})");
    }

    public override string Serialize()
    {
        return $"EternalGoal: {_name}, {_description}, {_completed}, {_points}";
    }

    public override void Deserialize(string data)
    {
        base.Deserialize(data);
        // No need to parse boolean _completed as it's not used for EternalGoal
    }
}
