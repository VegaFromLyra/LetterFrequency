using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LetterFrequency
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string[] inputLines = File.ReadAllLines(@"C:\Dev\LetterFrequency\LetterFrequency\letter_frequencies.in");

                int N = Int32.Parse(inputLines[0]);

                string outputFileName = @"C:\Dev\LetterFrequency\LetterFrequency\letter_frequencies.out";

                // Clear the output file before doing anything
                File.WriteAllText(outputFileName, string.Empty);

                using (StreamWriter writer = File.AppendText(outputFileName))
                {
                    for (int i = 0; i < N; i++)
                    {
                        WriteToFile(String.Format("Case #{0}", i + 1), writer);
                        WriteToFile(String.Join("\n", GenerateLetterFrequency(inputLines[i + 1])), writer);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error occured: {0}", e.Message);
            }
        }

        private static void WriteToFile(string input, TextWriter writer)
        {
            writer.WriteLine(input);
        }

        static string[] GenerateLetterFrequency(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                throw new Exception("Please provide valid input");
            }

            // Remove spaces
            input = input.Replace(" ", "");

            Dictionary<char, int> letterFrequencyDictionary = new Dictionary<char, int>();

            foreach (char character in input)
            {
                if (letterFrequencyDictionary.ContainsKey(character))
                {
                    letterFrequencyDictionary[character]++;
                }
                else
                {
                    letterFrequencyDictionary[character] = 1;
                }
            }

            List<KeyValuePair<char, int>> frequencyList = letterFrequencyDictionary.ToList();

            var orderedListAccordingToValue = frequencyList.OrderByDescending(item => item.Value);

            List<KeyValuePair<char, int>> finalList = orderedListAccordingToValue.ToList();

            finalList.Sort(CompareKeyValue);

            string[] result = new string[finalList.Count];

            for (int i = 0; i < finalList.Count; i++)
            {
                result[i] = finalList[i].Key.ToString() + " " + finalList[i].Value.ToString();
            }

            return result;
        }

        private static int CompareKeyValue(KeyValuePair<char, int> item1, KeyValuePair<char, int> item2)
        {
            if (item1.Value.Equals(item2.Value))
            {
                return item1.Key.CompareTo(item2.Key);
            }

            // if Item1 has greater count then it should appear first
            return item1.Value.CompareTo(item2.Value) > 0 ? -1 : 1;
        }

    }
}
