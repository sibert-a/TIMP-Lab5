class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Калькулятор (ПОЛИЗ)");
        Console.WriteLine("Операторы: + - * / ^ ( )");
        Console.WriteLine("Только целые числа\n");

        while (true)
        {
            Console.Write("Введите выражение (exit для выхода): ");
            string input = Console.ReadLine();
            if (input?.ToLower() == "exit") break;

            try
            {
                string expr = input.Replace(" ", "");
                string poliz = ExpressionToPOLIZ.ConvertToPOLIZ(expr);
                int result = POLIZCalculator.CalculatePOLIZ(poliz);

                Console.WriteLine($"ПОЛИЗ: {poliz}");
                Console.WriteLine($"Результат: {result}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}\n");
            }
        }
    }
}