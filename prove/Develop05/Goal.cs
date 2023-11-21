using System;
using System.Collections.Generic;
using System.IO;

abstract class Goal
{
    protected string _name;
    protected string _description;
    protected int _points;
    protected bool _completed;

    public string Name => _name;
    public string Description => _description;
    public int Points => _points;

    public virtual void RecordEvent() { }
    public virtual void DisplayStatus() { }
    public virtual string Serialize()
    {
        return $"{_name}, {_description}, {_completed}, {_points}";
    }
    public virtual void Deserialize(string data)
    {
        string[] parts = data.Split(',');
        if (parts.Length == 4)
        {
            _name = parts[0].Trim();
            _description = parts[1].Trim();
            _completed = bool.Parse(parts[2].Trim());
            _points = int.Parse(parts[3].Trim());
        }
    }
}
