using System;
using System.Collections.Generic;
using System.Text;

public class ExpressionToPOLIZ
{
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

    private static bool IsOperator(char c)
    {
        return c == '+' || c == '-' || c == '*' || c == '/' || c == '^';
    }

    private static bool IsDigit(char c)
    {
        return c >= '0' && c <= '9';
    }

    public static string ConvertToPOLIZ(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression))
            throw new ArgumentException("Выражение пустое");

        string expr = expression.Replace(" ", "");
        Stack<char> stack = new Stack<char>();
        StringBuilder output = new StringBuilder();
        int i = 0;

        while (i < expr.Length)
        {
            char c = expr[i];

            if (IsDigit(c))
            {
                while (i < expr.Length && IsDigit(expr[i]))
                {
                    output.Append(expr[i]);
                    i++;
                }
                output.Append(' ');
                continue;
            }
            else if (c == '(')
            {
                stack.Push(c);
            }
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
            else if (IsOperator(c))
            {
                while (stack.Count > 0 && stack.Peek() != '(' && GetPriority(stack.Peek()) >= GetPriority(c))
                {
                    output.Append(stack.Pop());
                    output.Append(' ');
                }
                stack.Push(c);
            }
            else
            {
                throw new ArgumentException($"Недопустимый символ: {c}");
            }
            i++;
        }

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