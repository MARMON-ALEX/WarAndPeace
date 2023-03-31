using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace WarAndPeace
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к файлу для анализа:");
            string inputFilePath = Console.ReadLine();

            if (File.Exists(inputFilePath))
            {
                string outputFilePath = Path.GetDirectoryName(inputFilePath) + @"\UniqueWords.txt";
                Dictionary<string, int> wordsCount = new Dictionary<string, int>();

                using (StreamReader reader = new StreamReader(inputFilePath))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] words = line.Split(
                            new char[] { ' ', '.', ',', ':', ';', '-', '!', '?', '(', ')', '[', ']', '<', '>', '/', '\\', '\t', '\n' },
                            StringSplitOptions.RemoveEmptyEntries);

                        foreach (string word in words)
                        {
                            string lowercaseWord = word.ToLower();

                            if (wordsCount.ContainsKey(lowercaseWord))
                            {
                                wordsCount[lowercaseWord]++;
                            }
                            else
                            {
                                wordsCount.Add(lowercaseWord, 1);
                            }
                        }
                    }
                }

                var sortedWordsCount = wordsCount.OrderByDescending(x => x.Value);

                using (StreamWriter writer = new StreamWriter(outputFilePath))
                {
                    foreach (var wordCount in sortedWordsCount)
                    {
                        writer.WriteLine("{0}: {1}", wordCount.Key, wordCount.Value);
                    }
                }

                Console.WriteLine("Результаты сохранены в файле: " + outputFilePath);
            }
            else
            {
                Console.WriteLine("Файл не найден.");
            }

            Console.WriteLine("Нажмите любую клавишу для выхода.");
            Console.ReadKey();
        }
    }
}