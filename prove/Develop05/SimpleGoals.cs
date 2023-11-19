using System;
using System.Collections.Generic;
using System.IO;

class SimpleGoal : Goal
{
    private int _value;

    public SimpleGoal(string name, int value)
    {
        _name = name;
        _value = value;
    }

    public override void RecordEvent()
    {
        Console.WriteLine($"Recording event for {Name}");
        _completed = true;
        _points += _value; // Bonus points

        // achievement badges
        CheckAchievements();
    }

    public override void DisplayStatus()
    {
        Console.WriteLine($"[{(_completed ? "x" : " ")}] Simple Goal: {Name}, {_value}, {_points}, {_completed}");
    }

    public override string Serialize()
    {
        return $"{base.Serialize()},{_value}";
    }

    public override void Deserialize(string data)
    {
        base.Deserialize(data);
        string[] parts = data.Split(',');
        if (parts.Length == 4)
        {
            _value = int.Parse(parts[3].Trim());
        }
    }

    private void CheckAchievements()
    {
        if (_points >= 50)
        {
            Console.WriteLine("Congratulations! You've earned the 'Overachiever' badge!");
        }
    }
}
