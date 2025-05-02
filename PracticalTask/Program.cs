class Program
{
    static void Main(string[] args)
    {
        string text = Console.ReadLine();
        string finalText = null;
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
}