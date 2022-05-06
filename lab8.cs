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

//5.Запрос из столбца в строку по группам
CREATE TABLE hometask (id int, group_id int, descr VARCHAR(10));
INSERT INTO hometask VALUES (1, 1, 'Один'), (2, 2, 'Два'), (3, 1, 'Три'), (4, 2, 'Четыре'), (5, 2, 'Пять');
SELECT * FROM hometask;
GO

SELECT group_id,
       STUFF((SELECT ',' + CAST(descr AS VARCHAR)
              FROM hometask t2
              WHERE t1.group_id = t2.group_id FOR XML PATH('')),1,1,'') descr
FROM hometask t1
GROUP BY group_id;
GO
