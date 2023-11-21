class Program
{
    static void Main()
    {
        Tracker tracker = new Tracker();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Create new goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");

            Console.Write("Select a choice from the menu: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    tracker.CreateGoal();
                    break;
                case "2":
                    tracker.DisplayGoals();
                    break;
                case "3":
                    tracker.SaveProgress();
                    break;
                case "4":
                    tracker.LoadProgress();
                    break;
                case "5":
                    tracker.RecordEvent();
                    break;
                case "6":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
