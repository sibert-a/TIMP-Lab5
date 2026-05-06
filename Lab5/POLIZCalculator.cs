using System;
using System.Collections.Generic;

public class POLIZCalculator
{
    private static bool IsOperator(string token)
    {
        return token == "+" || token == "-" || token == "*" || token == "/" || token == "^";
    }

    private static int ApplyOperation(string op, int a, int b)
    {
        switch (op)
        {
            case "+": return a + b;
            case "-": return a - b;
            case "*": return a * b;
            case "/":
                if (b == 0)
                    throw new DivideByZeroException("Деление на ноль");
                return a / b;
            case "^": return (int)Math.Pow(a, b);
            default: throw new ArgumentException($"Неизвестный оператор: {op}");
        }
    }

    public static int CalculatePOLIZ(string poliz)
    {
        if (string.IsNullOrWhiteSpace(poliz))
            throw new ArgumentException("ПОЛИЗ выражение пустое");

        Stack<int> stack = new Stack<int>();
        string[] tokens = poliz.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (string token in tokens)
        {
            if (IsOperator(token))
            {
                if (stack.Count < 2)
                    throw new InvalidOperationException("Недостаточно операндов");

                int b = stack.Pop();
                int a = stack.Pop();
                stack.Push(ApplyOperation(token, a, b));
            }
            else
            {
                if (!int.TryParse(token, out int number))
                    throw new ArgumentException($"Неверный токен: {token}");
                stack.Push(number);
            }
        }

        if (stack.Count != 1)
            throw new InvalidOperationException("Некорректное ПОЛИЗ выражение");

        return stack.Pop();
    }
}