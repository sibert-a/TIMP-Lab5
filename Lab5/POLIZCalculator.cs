using System;
using System.Collections.Generic;

public class POLIZCalculator
{
    // Проверка, является ли строка оператором
    private static bool IsOperator(string token)
    {
        return token == "+" || token == "-" || token == "*" || token == "/" || token == "^";
    }

    // Выполнение арифметической операции
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

    // Главная функция: вычисление выражения в ПОЛИЗ
    public static int CalculatePOLIZ(string poliz)
    {
        // Проверка на пустое выражение
        if (string.IsNullOrWhiteSpace(poliz))
            throw new ArgumentException("ПОЛИЗ выражение пустое");

        Stack<int> stack = new Stack<int>(); // Стек для операндов
        // Разбиваем строку на элементы (числа и операторы)
        string[] tokens = poliz.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        // Обрабатываем каждый элемент
        foreach (string token in tokens)
        {
            // Если оператор
            if (IsOperator(token))
            {
                // Проверяем, хватает ли операндов
                if (stack.Count < 2)
                    throw new InvalidOperationException("Недостаточно операндов");

                int b = stack.Pop(); // Правый операнд
                int a = stack.Pop(); // Левый операнд
                stack.Push(ApplyOperation(token, a, b)); // Результат в стек
            }
            else // Иначе число
            {
                if (!int.TryParse(token, out int number))
                    throw new ArgumentException($"Неверный токен: {token}");
                stack.Push(number); // Число в стек
            }
        }

        // В стеке должен остаться один результат
        if (stack.Count != 1)
            throw new InvalidOperationException("Некорректное ПОЛИЗ выражение");

        return stack.Pop();
    }
}