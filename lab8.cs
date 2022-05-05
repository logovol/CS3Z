//1. Факториал числа
private static int factorialLoop(int value)
{
    if (value == 0)
        return 1;
    int result = 2;
    for (int i = 2; i < value; i++)
    {
        result *= i + 1;
    }
    return result;
}

private static int factorialRecursion(int value)
{
    if (value == 0)
        return 1;
    return value * factorialRecursion(value - 1);
}

//2.Ошибка приведения

//3. Запрос
SELECT
Name,
SUM(1) as _count

FROM Users
WHERE Name LIKE 'A%'
GROUP BY Name
HAVING SUM(1) > 1;

//4. 5