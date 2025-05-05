using PracticalTask;
using System;
using System.Reflection;
using System.Text.Json;

class Program
{
    public static string englishAlphabet = "abcdefghijklmnopqrstuvwxyz";
    public static string text;
    public static string finalText;

    public static readonly HttpClient client = new HttpClient();
    public static readonly Random localRandom = new Random();

    static void Main(string[] args)
    {
        TextCheck();
        StringActions();
        RepeatCharacters();
        FindingLargestSubstring();
        SortSelection();
        RandomNumberGenerate();
    }

    //Проверка на корректный ввод
    public static void TextCheck()
    {
        try
        {
            text = Console.ReadLine();
            bool[] textBools = new bool[text.Length];
            bool checkSymbol = false;
            for (int i = 0; i < text.Length; i++)
            {
                foreach (char j in englishAlphabet)
                {
                    if (text[i] == j)
                    {
                        checkSymbol = true;
                    }
                }
                textBools[i] = checkSymbol;
                checkSymbol = false;
            }
            string inappropriateCharacters = null;
            for (int i = 0; i < textBools.Length; i++)
            {
                if (textBools[i] == false)
                {
                    inappropriateCharacters += $" {text[i]} ";
                }
            }
            if (inappropriateCharacters != null)
            {
                throw new Exception($"Были введены не подходящие символы:{inappropriateCharacters}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка: " + ex.Message);
            Environment.Exit(0);
        }
    }

    //Действия со строкой
    public static void StringActions()
    {
        if (text.Length % 2 == 0)
        {
            for (int i = (text.Length / 2) - 1; i >= 0; i--)
            {
                finalText += text[i];
            }
            for (int i = text.Length - 1; i >= text.Length / 2; i--)
            {
                finalText += text[i];
            }
        }
        else
        {
            for (int i = text.Length - 1; i >= 0; i--)
            {
                finalText += text[i];
            }
            finalText += text;
        }
        Console.WriteLine(finalText);
    }

    //Подсчёт каждого символа в обработанной строке
    public static void RepeatCharacters()
    {
        var uniqueSymbol = new HashSet<char>();
        for (int i = 0; i < finalText.Length; i++)
        {
            uniqueSymbol.Add(finalText[i]);
        }
        var uniqueSymbolList = new List<char>();
        foreach (var i in uniqueSymbol)
        {
            uniqueSymbolList.Add(i);
        }

        int symbolCount = 0;
        for (int i = 0; i < uniqueSymbolList.Count; i++)
        {
            for (int j = 0; j < finalText.Length; j++)
            {
                if (uniqueSymbolList[i] == finalText[j])
                {
                    symbolCount++;
                }
            }
            Console.WriteLine($"Символ {uniqueSymbolList[i]} повторялся {symbolCount} раз(а)");
            symbolCount = 0;
        }
    }

    //Нахождение наибольшей подстроки, которая начинается и заканчивается на гласную букву из «aeiouy»
    public static void FindingLargestSubstring()
    {
        string vowelsLetters = "aeiouy";
        bool findIndex = false;
        int startIndex = 0;
        for (int i = 0; i < finalText.Length; i++)
        {
            foreach (char j in vowelsLetters)
            {
                if (finalText[i] == j)
                {
                    startIndex = i;
                    findIndex = true;
                    break;
                }
            }
            if (findIndex == true)
            {
                findIndex = false;
                break;
            }
        }
        int finishIndex = 0;
        for (int i = finalText.Length - 1; i >= 0; i--)
        {
            foreach (char j in vowelsLetters)
            {
                if (finalText[i] == j)
                {
                    finishIndex = i;
                    findIndex = true;
                    break;
                }
            }
            if (findIndex == true)
            {
                findIndex = false;
                break;
            }
        }
        for (int i = startIndex; i <= finishIndex; i++)
        {
            Console.Write($"{finalText[i]}");
        }
    }

    //пользователь выбирает сортировку
    public static void SortSelection()
    {
        Console.WriteLine("\nВыберите тип сортировки:\n1 - Быстрая сортировка (Quicksort)\n2 - Сортировка деревом (Tree sort)");
        switch (Console.ReadLine())
        {
            case "1":
                Sorting.Quicksort();
                Console.WriteLine(finalText);
                break;
            case "2":
                Sorting.TreeSort();
                Console.WriteLine(finalText);
                break;
            default:
                Console.WriteLine("Некорректный ввод");
                SortSelection();
                break;
        }
    }

    //программа получает случайное число, которое меньше чем число символов в обработанной строке и удаляет символ в той позиции, номер которой вернёт случайный генератор.
    public static async void RandomNumberGenerate()
    {
        int randomIndex = await GetRandomIndexAsync();
        finalText.Remove(randomIndex, 1);
        Console.WriteLine(finalText);
    }

    private static async Task<int> GetRandomIndexAsync()
    {
        try
        {
            var response = await client.GetAsync($"http://www.randomnumberapi.com/api/v1.0/random?min=0&max={finalText.Length - 1}&count=1");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var numbers = JsonSerializer.Deserialize<int[]>(json);

            return numbers?.Length > 0 ? numbers[0] : localRandom.Next(0, finalText.Length);
        }
        catch
        {
            return localRandom.Next(0, finalText.Length);
        }
    }
}