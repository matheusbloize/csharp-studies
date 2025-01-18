using System.Text;
using System.Text.RegularExpressions;

namespace TextEditor;

enum EActions
{
    Read,
    Write,
    Delete
}

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
        Console.WriteLine("Choose your option:");
        Console.WriteLine("1 - Read file");
        Console.WriteLine("2 - Write file");
        Console.WriteLine("3 - Delete file");
        Console.WriteLine("0 - Exit");

        short? option = null;

        while (option == null)
        {
            var input = Console.ReadLine();

            if (input == "0")
            {
                Console.WriteLine("Goodbye.");
                Environment.Exit(0);
            }

            if (string.IsNullOrWhiteSpace(input) || !short.TryParse(input, out _))
                Console.WriteLine("Enter a valid option");
            else
            {
                short shortInput = Convert.ToInt16(input);
                if (shortInput >= 0 && shortInput <= 3)
                    option = shortInput;
                else
                    Console.WriteLine("Invalid number option");
            }
        }

        Console.WriteLine("everything ok");
        Console.WriteLine(option);

        UseFile(Convert.ToInt16(option));
    }

    static void UseFile(short option)
    {
        switch (option)
        {
            case 1: ReadFile(); break;
            case 2: WriteFile(); break;
            case 3: DeleteFile(); break;
            default: Menu(); break;
        }
    }

    static void ReadFile()
    {
        string text = "";
        var path = GetPath(EActions.Read);
        if (path == "NULL")
        {
            Thread.Sleep(1000);
            Menu();
        }
        using (var file = new StreamReader(path))
        {
            text = file.ReadToEnd();
        }
        Console.WriteLine(text);
        Console.ReadLine();
        Menu();
    }

    static void WriteFile()
    {
        Console.Clear();
        Console.WriteLine("Write your text below (press ESC to exit)");
        Console.WriteLine("----------------");
        var textBuilder = new StringBuilder();

        while (true)
        {
            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
                break;

            if (key.Key == ConsoleKey.Enter)
            {
                textBuilder.AppendLine();
                Console.WriteLine(); // Reflect the Enter key visually
            }
            else if (key.Key == ConsoleKey.Backspace)
            {
                if (textBuilder.Length - 1 >= 0)
                {
                    textBuilder.Remove(textBuilder.Length - 1, 1);
                    Console.Clear();
                    for (int i = 0; i < textBuilder.Length; i++)
                    {
                        Console.Write(textBuilder.ToString()[i]); // Show actual string
                    }
                }
            }
            else
            {
                textBuilder.Append(key.KeyChar);
                Console.Write(key.KeyChar); // Reflect the typed character visually
            }
        }

        var path = GetPath(EActions.Write);
        if (path == "NULL")
        {
            Thread.Sleep(1000);
            Menu();
        }
        using (var file = new StreamWriter(path))
        {
            file.Write(textBuilder.ToString());
        }

        Console.WriteLine("Your text file was written");
        Thread.Sleep(1000);
        Menu();
    }

    static void DeleteFile()
    {
        var path = GetPath(EActions.Delete);
        if (path == "NULL")
        {
            Thread.Sleep(1000);
            Menu();
        }
        File.Delete(path);
        Console.WriteLine("Your text file was deleted");
        Thread.Sleep(1000);
        Menu();
    }

    static string GetPath(EActions action)
    {
        Console.Clear();
        Console.WriteLine("Specify the path to the file:");
        string? path = null;

        while (path == null)
        {
            var input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input) && CheckPath(input))
                path = input;
            else
                Console.WriteLine("Invalid path");
        }
        if (action != EActions.Write && !File.Exists(path))
        {
            Console.WriteLine("File doesn't exist");
            path = "NULL";
        }
        return path;
    }

    static bool CheckPath(string? input)
    {
        var regex = new Regex(@"^D:\\(?:[^D:\\]+\\)*TextEditor\\texts.*(.txt)$");
        return regex.IsMatch(input!);
    }
}
