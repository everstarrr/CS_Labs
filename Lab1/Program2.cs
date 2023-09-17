namespace Lab1;

public class Program2
{
    public static void Rofls()
    {
        Console.Write("Enter x array size: ");
        int x = Convert.ToInt32(Console.ReadLine()); // размер массива x
        Console.Write("Enter y array size: ");
        int y = Convert.ToInt32(Console.ReadLine()); // размер массива y
        int[,] arr = new int[y, x]; // двумерный массив
        Random rnd = new Random();
        int val = 0; // вспомогательная переменная
        int col = 0;

        // инициализация и вывод массива
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                arr[i, j] = rnd.Next(-256, 256);
                Console.Write(arr[i, j] + " ");
            }

            Console.WriteLine();
        }

        // поиск суммы элементов в строках с ъотя бы одним отрицательным элементом
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                if (arr[i, j] < 0)
                {
                    for (int k = 0; k < x; k++)
                        val += arr[i, k];
                    Console.WriteLine("Sum of elements in {0} row: {1}", i, val);
                    break;
                }
            }
        }

        // поиск седловой точки
        for (int i = 0; i < y; i++)
        {
            val = arr[i, 0];
            for (int j = 0; j < x; j++)
            {
                if (arr[i, j] < val)
                {
                    val = arr[i, j];
                    col = j;
                }
            }
            for (int k = 0; k < y; k++)
            {
                if (arr[k, col] > val)
                    break;
                if (k==y-1)
                    Console.WriteLine("Saddle point: {0}, {1}", k, col);
            }
        }
    }
}