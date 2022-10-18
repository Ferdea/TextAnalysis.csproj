﻿using System;
using System.IO;

namespace TextAnalysis
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var text = File.ReadAllText("HarryPotterText.txt");
            var sentences = SentencesParserTask.ParseSentences(text);
            var frequency = FrequencyAnalysisTask.GetMostFrequentNextWords(sentences);

            while (true)
            {
                Console.Write("Введите первое слово (например, harry): ");
                var beginning = Console.ReadLine();
                if (string.IsNullOrEmpty(beginning)) return;
                var phrase = TextGeneratorTask.ContinuePhrase(frequency, beginning.ToLower(), 100);
                Console.WriteLine(phrase);
            }
        }
    }
}