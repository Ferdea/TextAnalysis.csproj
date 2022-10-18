using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {

        public static string ContinuePhrase(Dictionary<string, List<string>> nextWords, string phraseBeginning,
            int wordsCount)
        {
            var sentence = phraseBeginning.Split(' ').ToList();
            var random = new Random();
            string key;
            
            for (var i = 0; i < wordsCount; i++)
            {
                if (sentence.Count >= 2 &&
                    nextWords.ContainsKey(key = $"{sentence[sentence.Count - 2]} {sentence[sentence.Count - 1]}"))
                    sentence.Add(nextWords[key][random.Next(nextWords[key].Count)]);
                else if (nextWords.ContainsKey(key = sentence[sentence.Count - 1]))
                    sentence.Add(nextWords[key][random.Next(nextWords[key].Count)]);
                else
                    break;
                
                if (sentence[sentence.Count - 1] == ".")
                {
                    sentence.RemoveAt(sentence.Count - 1);
                    break;
                }
            }

            var result = String.Join(" ", sentence);
            result += ".";
            return result;
        }
    }
}