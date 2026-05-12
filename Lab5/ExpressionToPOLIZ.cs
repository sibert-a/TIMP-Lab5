using System;
using System.Collections.Generic;
using System.Text;

public class ExpressionToPOLIZ
{
    // Определение приоритета операторов
    private static int GetPriority(char op)
    {
        switch (op)
        {
            case '(': return 0;
            case ')': return 1;
            case '+':
            case '-': return 2;
            case '*':
            case '/': return 3;
            case '^': return 4;
            default: return -1;
        }
    }

    // Проверка, является ли символ оператором
    private static bool IsOperator(char c)
    {
        return c == '+' || c == '-' || c == '*' || c == '/' || c == '^';
    }

    // Проверка, является ли символ цифрой
    private static bool IsDigit(char c)
    {
        return c >= '0' && c <= '9';
    }

    // Главная функция: перевод в ПОЛИЗ
    public static string ConvertToPOLIZ(string expression)
    {
        // Проверка на пустое выражение
        if (string.IsNullOrWhiteSpace(expression))
            throw new ArgumentException("Выражение пустое");

        // Удаляем пробелы
        string expr = expression.Replace(" ", "");
        Stack<char> stack = new Stack<char>();      // Стек для операторов
        StringBuilder output = new StringBuilder(); // Выходная строка ПОЛИЗ
        int i = 0;

        // Основной цикл обработки
        while (i < expr.Length)
        {
            char c = expr[i];

            // Если цифра - читаем всё число
            if (IsDigit(c))
            {
                while (i < expr.Length && IsDigit(expr[i]))
                {
                    output.Append(expr[i]);
                    i++;
                }
                output.Append(' '); // Разделитель
                continue;
            }
            // Открывающая скобка - в стек
            else if (c == '(')
            {
                stack.Push(c);
            }
            // Закрывающая скобка - выталкиваем до '('
            else if (c == ')')
            {
                while (stack.Count > 0 && stack.Peek() != '(')
                {
                    output.Append(stack.Pop());
                    output.Append(' ');
                }
                if (stack.Count > 0 && stack.Peek() == '(')
                    stack.Pop();
                else
                    throw new ArgumentException("Несоответствие скобок");
            }
            // Оператор - проверяем приоритет
            else if (IsOperator(c))
            {
                while (stack.Count > 0 && stack.Peek() != '(' && GetPriority(stack.Peek()) >= GetPriority(c))
                {
                    output.Append(stack.Pop());
                    output.Append(' ');
                }
                stack.Push(c);
            }
            // Недопустимый символ
            else
            {
                throw new ArgumentException($"Недопустимый символ: {c}");
            }
            i++;
        }

        // Выталкиваем оставшиеся операторы из стека
        while (stack.Count > 0)
        {
            char op = stack.Pop();
            if (op == '(' || op == ')')
                throw new ArgumentException("Несоответствие скобок");
            output.Append(op);
            output.Append(' ');
        }

        return output.ToString().Trim();
    }
}