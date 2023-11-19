using System;
using System.Collections.Generic;
using System.IO;

class Tracker
{
    private List<Goal> _userGoals = new List<Goal>();
    private int _totalPoints;

    public void CreateGoal()
    {
        Console.WriteLine("The types of goals are:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");

        Console.Write("What types of goals would you like to create? ");
        string choice = Console.ReadLine();

        Goal newGoal;

        switch (choice)
        {
            case "1":
                Console.Write("What is the name of your goal? ");
                string simpleName = Console.ReadLine();
                Console.Write("What is the short description of it? ");
                string simpleDescription = Console.ReadLine();
                Console.Write("What is the amount of points associated with this goal? ");
                int simpleValue;
                while (!int.TryParse(Console.ReadLine(), out simpleValue) || simpleValue < 0)
                {
                    Console.WriteLine("Invalid response. Please enter a valid non-negative integer.");
                }
                newGoal = new SimpleGoal(simpleName, simpleValue);
                break;
            case "2":
                Console.Write("What is the name of your goal? ");
                string eternalName = Console.ReadLine();
                Console.Write("What is a short description of it? ");
                string eternalDescription = Console.ReadLine();
                Console.Write("What is the amount of points associated with this goal? ");
                int eternalPoints;
                while (!int.TryParse(Console.ReadLine(), out eternalPoints) || eternalPoints < 0)
                {
                    Console.WriteLine("Invalid input. Please enter a valid non-negative integer.");
                }
                newGoal = new EternalGoal(eternalName, eternalPoints);
                break;
            case "3":
                Console.Write("What is the name of your goal? ");
                string checklistName = Console.ReadLine();
                Console.Write("What is a short description of it? ");
                string checklistDescription = Console.ReadLine();
                Console.Write("How many times does this goal need to be accomplished for a bonus? ");
                int checklistTimes;
                while (!int.TryParse(Console.ReadLine(), out checklistTimes) || checklistTimes < 0)
                {
                    Console.WriteLine("Invalid input. Please enter a valid non-negative integer.");
                }
                Console.Write("What is the bonus for accomplishing it that many times? ");
                int checklistBonus;
                while (!int.TryParse(Console.ReadLine(), out checklistBonus) || checklistBonus < 0)
                {
                    Console.WriteLine("Invalid input. Please enter a valid non-negative integer.");
                }
                Console.Write("What is the amount of points associated with this goal? ");
                int checklistValue;
                while (!int.TryParse(Console.ReadLine(), out checklistValue) || checklistValue < 0)
                {
                    Console.WriteLine("Invalid input. Please enter a valid non-negative integer.");
                }
                newGoal = new ChecklistGoal(checklistName, checklistTimes, checklistBonus, checklistValue);
                break;
            default:
                Console.WriteLine("Invalid choice. Creating a Simple Goal by default.");
                newGoal = new SimpleGoal("Default Simple Goal", 0);
                break;
        }

        _userGoals.Add(newGoal);
        Console.WriteLine("Goal created successfully!");
    }

    public void RecordEvent()
    {
        Console.WriteLine("Recording an event...");

        // Display the list of goals
        Console.WriteLine("The goals are:");
        for (int i = 0; i < _userGoals.Count; i++)
        {
            Console.Write($"{i}. ");
            _userGoals[i].DisplayStatus();
        }

        Console.Write("Which goal did you accomplish? Enter the index: ");
        if (int.TryParse(Console.ReadLine(), out int goalIndex) && goalIndex >= 0 && goalIndex < _userGoals.Count)
        {
            _userGoals[goalIndex].RecordEvent();
            _totalPoints += _userGoals[goalIndex].Points;
            Console.WriteLine($"Congratulations! You earned {_userGoals[goalIndex].Points} points.");
        }
        else
        {
            Console.WriteLine("Invalid index. Please enter a valid index.");
        }

        // Display the total score after recording an event
        Console.WriteLine($"You have a total of {_totalPoints} points.");
    }

    public void DisplayGoals()
    {
        if (_totalPoints == 0)
        {
            Console.WriteLine("You have 0 points.");
        }
        Console.WriteLine("The goals are:");

        for (int i = 0; i < _userGoals.Count; i++)
        {
            Console.Write($"{i}. ");
            _userGoals[i].DisplayStatus();
        }
    }

    public void SaveProgress()
    {
        Console.Write("Enter the filename to save progress (or press Enter for the default 'progress.txt'): ");
        string fileName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(fileName))
        {
            fileName = "progress.txt";
        }
        else if (!fileName.EndsWith(".txt"))
        {
            fileName += ".txt"; // Append .txt if not provided
        }

        try
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var goal in _userGoals)
                {
                    writer.WriteLine(goal.Serialize());
                }
            }

            Console.WriteLine($"Progress saved successfully to {fileName}!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while saving progress: {ex.Message}");
        }
    }

    public void LoadProgress()
    {
        Console.Write("Enter the filename to load progress (or press Enter for the default 'progress.txt'): ");
        string fileName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(fileName))
        {
            fileName = "progress.txt";
        }
        else if (!fileName.EndsWith(".txt"))
        {
            fileName += ".txt"; // Append .txt if not provided
        }

        try
        {
            _userGoals.Clear(); // clear before loading

            using (StreamReader reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    string goalData = reader.ReadLine();
                    Goal loadedGoal = DeserializeGoal(goalData);
                    if (loadedGoal != null)
                    {
                        _userGoals.Add(loadedGoal);
                    }
                }
            }

            Console.WriteLine($"Progress loaded successfully from {fileName}!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while loading progress: {ex.Message}");
        }

        // Display the total score after loading progress
        Console.WriteLine($"You have a total of {_totalPoints} points.");
    }

    private Goal DeserializeGoal(string data)
    {
        string[] parts = data.Split(':');
        if (parts.Length == 2)
        {
            string goalType = parts[0].Trim();
            switch (goalType)
            {
                case "SimpleGoal":
                    SimpleGoal simpleGoal = new SimpleGoal("", 0);
                    simpleGoal.Deserialize(parts[1]);
                    return simpleGoal;
                case "EternalGoal":
                    EternalGoal eternalGoal = new EternalGoal("", 0);
                    eternalGoal.Deserialize(parts[1]);
                    return eternalGoal;
                case "ChecklistGoal":
                    ChecklistGoal checklistGoal = new ChecklistGoal("", 0, 0, 0);
                    checklistGoal.Deserialize(parts[1]);
                    return checklistGoal;
                default:
                    return null;
            }
        }

        return null;
    }

    private void CheckAchievements()
    {
        if (_totalPoints >= 100)
        {
            Console.WriteLine("Congratulations! You've earned the 'Achiever' badge!");
        }
    }
}
