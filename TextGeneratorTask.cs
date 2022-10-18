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
            var result = phraseBeginning.Split(' ').ToList();
            var random = new Random();
            
            for (var i = 0; i < wordsCount; i++)
            {
                if (result.Count >= 2
                    && nextWords.ContainsKey($"{result[result.Count - 2]} {result[result.Count - 1]}"))
                    result.Add(nextWords[$"{result[result.Count - 2]} {result[result.Count - 1]}"]
                        .ElementAt(random.Next(
                            nextWords[$"{result[result.Count - 2]} {result[result.Count - 1]}"].Count)));
                else if (nextWords.ContainsKey(result[result.Count - 1]))
                    result.Add(nextWords[result[result.Count - 1]].ElementAt(random.Next(nextWords[result[result.Count - 1]].Count)));
                else
                    break;
                if (result.ElementAt(result.Count - 1) == ".")
                {
                    result.RemoveAt(result.Count - 1);
                    break;
                }
            }

            
            var res = String.Join(" ", result);
            res += ".";
            return res;
        }
    }
}