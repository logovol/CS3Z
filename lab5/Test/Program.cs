using System;
using System.Linq;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            int way = 0;
            int arrLength = 0;
            int threadsNumber = 0;


            while (true)
            {
                Console.WriteLine("1. для теста потоков");
                Console.WriteLine("Любая клавиша для выхода");
                if (!Int32.TryParse(Console.ReadLine(), out way))
                    way = 0;

                switch (way)
                {
                    case 1:

                        Console.Write("Введите натуральное число для создания массива: ");
                        if (!Int32.TryParse(Console.ReadLine(), out arrLength) && arrLength > 0)
                        {
                            Console.WriteLine("Введено не натуральное число");
                            return;
                        }

                        Console.Write("Задайте количество потоков: ");
                        if (!Int32.TryParse(Console.ReadLine(), out threadsNumber) && threadsNumber > 0)
                        {
                            Console.WriteLine("Вы не задали количество потоков");
                            return;
                        }
                        
                        int[] arrNatural = Enumerable.Range(1, arrLength).ToArray();                        
                        int step = arrLength / threadsNumber;
                        int[] partsSum = new int[threadsNumber];

                        Parallel.For(0, threadsNumber, (counter) =>
                        {
                            int sum = 0;
                            for (int i = counter * step; (counter != threadsNumber - 1) ? i < (counter + 1) * step : i < arrLength; i++)
                                sum += arrNatural[i];
                            partsSum[counter] = sum;
                        });

                        int finalSum = 0;
                        foreach (var sum in partsSum)
                            finalSum += sum;
                        Console.WriteLine($"Результат: {finalSum}");
                        Console.WriteLine();
                        break;
                        
                    default:
                        return;
                }                
            }
        }
    }
}
