class Program
{
    static void Main(string[] args)
    {
        string englishAlphabet = "abcdefghijklmnopqrstuvwxyz";
        string text = Console.ReadLine();
        string finalText = null;
        bool checkSymbol = false;
        bool[] textBools = new bool[text.Length];
        try
        {
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
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка: " + ex.Message);
        }
    }
}