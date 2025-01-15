namespace Stopwatch;

class Program
{
    static void Main(string[] args)
    {
        Menu();
    }

    static void Menu()
    {
        Console.Clear();
        Console.WriteLine("---Menu---");
        Console.WriteLine("Select the time:");
        Console.WriteLine("e.g., 10s or 1m");
        Console.WriteLine("S - Second");
        Console.WriteLine("M - Minute");
        Console.WriteLine("0 - Exit");

        string? option = null;

        // check if input is valid
        while (option == null)
        {
            string? input = Console.ReadLine();
            if (input == "0")
            {
                Console.WriteLine("Stopwatch closed.");
                Environment.Exit(1);
            }
            if (!string.IsNullOrWhiteSpace(input))
            {
                input = input.ToLower();
                if (!int.TryParse(input.Substring(0, input.Length - 1), out _))
                {
                    Console.WriteLine("invalid input");
                    Thread.Sleep(500);
                    Menu();
                }
                if (!input.Last().Equals('s') && !input.Last().Equals('m'))
                {
                    Console.WriteLine("use 's' or 'm'");
                }
                else
                {
                    option = input;
                }
            }
        }
        option = option.ToLower();
        char timeType = option.Last();

        int time = int.Parse(option.Substring(0, option.Length - 1));

        Console.WriteLine(time);
        Console.WriteLine(timeType);
        Start(timeType == 's' ? time : time * 60);
    }

    static void Start(int time)
    {
        int currentTime = 0;

        while (currentTime <= time)
        {
            Console.Clear();
            Console.WriteLine(currentTime++);
            Thread.Sleep(1000);
        }

        Console.Clear();
        Console.WriteLine("Finished");
        Thread.Sleep(2000);
        Menu();
    }
}
