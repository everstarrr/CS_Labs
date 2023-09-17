using Lab1;

Console.Write("Enter array size: ");
int n = Convert.ToInt32(Console.ReadLine()); // размер массива
int[] arr = new int[n]; // массив
Random rnd = new Random();
int val = arr[0]; // вспомогательная переменная
int pos1 = 0;
int pos2 = 0;

// инициализация и вывод массива
for (int i = 0; i < n; i++)
{
    arr[i] = rnd.Next(-256, 256);
    Console.Write(arr[i] + " ");
}
Console.WriteLine();

// поиск и вывод минимального значения
foreach (int i in arr)
{
    if (i < val)
        val = i;
}
Console.WriteLine("Minimal value: " + val);

// поиск позиции 1
for (int i = 0; i < n; i++)
{
    if (arr[i] > 0)
    {
        pos1 = i;
        break;
    }
}

// поиск позиции 2
pos1--;
for (int i = n - 1; i > pos1; i--)
{
    if (arr[i] > 0)
    {
        pos2 = i;
        break;
    }
}

// вычисление и вывод суммы
pos1+=2;
val = 0;
for (int i = pos1; i < pos2; i++)
{
    val += arr[i];
}
Console.WriteLine("Array sum: " + val);

// изменение массива
val = 0;
for (int i = 0; i < n; i++)
{
    if (arr[i] == 0)
    {
        arr[i] = arr[val];
        arr[val] = 0;
        val++;
    }
}

// вывод массива
for (int i = 0; i < n; i++)
{
    Console.Write(arr[i] + " ");
}
Console.WriteLine('\n');

Program2.Rofls();