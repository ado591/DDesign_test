using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UniqueWordsCounter{
    class Program{
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к тектовому файлу: ");
            string inputFilePath = Console.ReadLine();
            Console.WriteLine("Введите путь к файлу, в который хотите записать результат: ");
            string outputFilePath = Console.ReadLine();

            Dictionary<string, int> wordCounts = new Dictionary<string, int>();

            using (StreamReader reader = new StreamReader(inputFilePath))
            {
                while (!reader.EndOfStream){
                    string currentLine = reader.ReadLine();
                    string[] words = currentLine.Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string word in words){
                        string cleanedWord = CleanWord(word);
                        if (!string.IsNullOrEmpty(cleanedWord)){
                            if (wordCounts.ContainsKey(cleanedWord)){
                                wordCounts[cleanedWord]++;
                            }
                            else{
                                wordCounts.Add(cleanedWord, 1);
                            }
                        }
                    }
                }
            }

            List<KeyValuePair<string, int>> sortedWordCounts = wordCounts.ToList();
            sortedWordCounts.Sort((a, b) => b.Value.CompareTo(a.Value));

            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                foreach (KeyValuePair<string, int> pair in sortedWordCounts){
                    writer.WriteLine(pair.Key + "\t" + pair.Value);
                }
            }
        }

        static string CleanWord(string word)
        {
            string cleanedWord = new string(word.Where(c => char.IsLetter(c)).ToArray());
            return cleanedWord.ToLower();
        }
    }
}