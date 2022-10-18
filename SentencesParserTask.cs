using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        private static char[] separators = {'.', '!', '?', ';', ':', '(', ')'};
        private static List<string> GetWordsInSentences(string sentence)
        {
            var wordList = new List<string>();
            var word = new StringBuilder();

            foreach (var item in sentence)
            {
                if (char.IsLetter(item) || item == '\'')
                    word.Append(item);
                else if (word.Length != 0)
                {
                    wordList.Add(word.ToString().ToLower());
                    word.Clear();
                }
            }

            if (word.Length != 0)
            {
                wordList.Add(word.ToString().ToLower());
            }
            wordList.Add(".");
            
            return wordList;
        }
        
        public static List<List<string>> ParseSentences(string text)
        {
            var sentences = text.Split(separators);

            var sentencesList = new List<List<string>>();
            foreach (var item in sentences)
            {
                var words = GetWordsInSentences(item);
                if (words.Count != 0)
                    sentencesList.Add(words);
            }
            
            return sentencesList;
        }
    }
}