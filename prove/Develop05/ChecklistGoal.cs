
class ChecklistGoal : Goal
{
    private int _accomplishedTimes;
    private int _bonusPoints;
    private int _numberOfTimes;
    private int _value;

    public ChecklistGoal(string name, string description, int numberOfTimes, int bonusPoints, int value)
    {
        _name = name;
        _description = description;
        _numberOfTimes = numberOfTimes;
        _bonusPoints = bonusPoints;
        _value = value;
    }

    public override void RecordEvent()
    {
        _completed = true;
        _accomplishedTimes++;

        if (_accomplishedTimes == _numberOfTimes)
        {
            _points += _bonusPoints;
            Console.WriteLine();
            Console.WriteLine($"Congratulations! You earned {_bonusPoints} points for accomplishing {Name} goal {_accomplishedTimes}/{_numberOfTimes} times!!!");
        }
        else
        {
            _points += _value;
        }

        CheckAchievements();
    }

    public override void DisplayStatus()
    {
        Console.WriteLine($"[{(_completed ? "x" : " ")}] Checklist Goal: {Name} ({Description}) ------ currently completed: {_accomplishedTimes}/{_numberOfTimes}");
    }

    public override string Serialize()
    {
        return $"ChecklistGoal: {_name}, {_description}, {_completed}, {_points}, {_numberOfTimes}, {_bonusPoints}, {_value}, {_accomplishedTimes}";
    }

    public override void Deserialize(string data)
    {
        base.Deserialize(data);
        string[] parts = data.Split(',');
        if (parts.Length == 10)
        {
            _numberOfTimes = int.Parse(parts[4].Trim());
            _bonusPoints = int.Parse(parts[5].Trim());
            _value = int.Parse(parts[6].Trim());
            _accomplishedTimes = int.Parse(parts[7].Trim());
        }
    }

    private void CheckAchievements()
    {
        if (_accomplishedTimes >= 3)
        {
            Console.WriteLine("You've earned the 'Consistent Achiever' badge! wohooooooo....");
        }
    }
}
