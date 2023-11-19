using System;
using System.Collections.Generic;
using System.IO;

class ChecklistGoal : Goal
{
    private int _accomplishedTimes;
    private int _bonusPoints;
    private int _numberOfTimes;
    private int _value;

    public ChecklistGoal(string name, int numberOfTimes, int bonusPoints, int value)
    {
        _name = name;
        _numberOfTimes = numberOfTimes;
        _bonusPoints = bonusPoints;
        _value = value;
    }

    public override void RecordEvent()
    {
        Console.WriteLine($"Recording event for {Name}");
        _completed = true;
        _accomplishedTimes++;

        if (_accomplishedTimes == _numberOfTimes)
        {
            _points += _bonusPoints;
            Console.WriteLine($"Congratulations! You earned {_bonusPoints} points for accomplishing {Name} {_accomplishedTimes} times.");
        }
        else
        {
            _points += _value;
        }

        CheckAchievements();
    }

    public override void DisplayStatus()
    {
        Console.WriteLine($"[{(_completed ? "x" : " ")}] Checklist Goal: {Name}, {_accomplishedTimes}/{_numberOfTimes}, {_bonusPoints}, {_points}, {_completed}");
    }

    public override string Serialize()
    {
        return $"{base.Serialize()},{_numberOfTimes},{_bonusPoints},{_value},{_accomplishedTimes}";
    }

    public override void Deserialize(string data)
    {
        base.Deserialize(data);
        string[] parts = data.Split(',');
        if (parts.Length == 7)
        {
            _numberOfTimes = int.Parse(parts[3].Trim());
            _bonusPoints = int.Parse(parts[4].Trim());
            _value = int.Parse(parts[5].Trim());
            _accomplishedTimes = int.Parse(parts[6].Trim());
        }
    }

    private void CheckAchievements()
    {
        if (_accomplishedTimes >= 3)
        {
            Console.WriteLine("Congratulations! You've earned the 'Consistent Achiever' badge!");
        }
    }
}