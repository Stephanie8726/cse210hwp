class SimpleGoal : Goal
{
    private int _value;

    public SimpleGoal(string name, string description, int value)
    {
        _name = name;
        _description = description;
        _value = value;
    }

    public override void RecordEvent()
    {
        _completed = true;
        _points += _value; // Bonus points

        // achievement badges
        CheckAchievements();
    }

    public override void DisplayStatus()
    {
        Console.WriteLine($"[{(_completed ? "x" : " ")}] Simple Goal: {Name} ({Description})");
    }

    public override string Serialize()
    {
        return $"SimpleGoal: {_name}, {_description}, {_completed}, {_points}, {_value}";
    }

    public override void Deserialize(string data)
    {
        base.Deserialize(data);
        string[] parts = data.Split(',');
        if (parts.Length == 6)
        {
            _value = int.Parse(parts[5].Trim());
        }
    }

    private void CheckAchievements()
    {
        if (_points >= 50)
        {
            Console.WriteLine("You've earned the 'Overachiever' badge!");
        }
    }
}