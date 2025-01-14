namespace Calculator;

enum EOperation
{
    Sum = 1,
    Subtract = 2,
    Multiply = 3,
    Divide = 4,
}

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("First value: ");
        float? value1 = null;

        // Check if first value is float
        while (value1 == null)
        {
            var input = Console.ReadLine();
            if (float.TryParse(input, out _))
            {
                value1 = float.Parse(input);
            }
        }

        Console.WriteLine("Second value: ");
        float? value2 = null;

        // Check if second value is float
        while (value2 == null)
        {
            var input = Console.ReadLine();
            if (float.TryParse(input, out _))
            {
                value2 = float.Parse(input);
            }
        }

        Console.WriteLine("Operation (1 - Sum | 2 - Subtract | 3 - Multiply | 4 - Divide): ");
        int? operation = null;

        // Check if operation is available
        while (operation == null)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out _))
            {
                var parsedInput = int.Parse(input);
                if (parsedInput >= 1 && parsedInput <= 4)
                {
                    operation = parsedInput;
                }
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Result: {Operation((float)value1, (float)value2, (int)operation)}");
    }

    static float Operation(float v1, float v2, int operation)
    {
        float result = 0;
        switch (operation)
        {
            case 1: result = v1 + v2; break;
            case 2: result = v1 - v2; break;
            case 3: result = v1 * v2; break;
            case 4: result = v1 / v2; break;
        }
        return result;
    }
}
