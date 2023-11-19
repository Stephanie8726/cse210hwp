using System;
using System.Collections.Generic;
using System.IO;

abstract class Goal
{
    protected string _name;
    protected int _points;
    protected bool _completed;

    public string Name => _name;
    public int Points => _points;

    public virtual void RecordEvent() { }
    public virtual void DisplayStatus() { }
    public virtual string Serialize()
    {
        return $"{GetType().Name}:{_name},{_points},{_completed}";
    }
    public virtual void Deserialize(string data)
    {
        string[] parts = data.Split(',');
        if (parts.Length == 3)
        {
            _name = parts[0].Trim();
            _points = int.Parse(parts[1].Trim());
            _completed = bool.Parse(parts[2].Trim());
        }
    }
}
