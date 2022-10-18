
using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        private static void GetNGrams(List<string> sentence, int n, Dictionary<string, Dictionary<string, int>> data)
        {
            for (var j = 0; j < sentence.Count - n + 1; j++)
            {
                var gram = new StringBuilder();
                gram.Append(sentence[j]);
                for (var k = 1; k < n - 1; k++)
                {
                    gram.Append(' ');
                    gram.Append(sentence[j + k]);
                }

                if (data.ContainsKey(gram.ToString()))
                {
                    if (data[gram.ToString()].ContainsKey(sentence[j + n - 1]))
                        data[gram.ToString()][sentence[j + n - 1]]++;
                    else
                        data[gram.ToString()][sentence[j + n - 1]] = 1;
                }
                else
                    data[gram.ToString()] = new Dictionary<string, int>{{sentence[n + j - 1], 1}};
            }
        }

        private static Dictionary<string, Dictionary<string, int>> GetFrequencyStatictic(List<List<string>> text)
        {
            var data = new Dictionary<string, Dictionary<string, int>>();
            
            foreach (var item in text)
            {
                GetNGrams(item, 2, data);
                GetNGrams(item, 3, data);
            }

            return data;
        }

        private static List<string> GetSortedListAtDict(Dictionary<string, int> data)
        {
            var dataList = new List<(int, string)>();
            foreach (var item in data)
            {
                if (item.Key == ".")
                {
                    dataList.Add((int.MinValue, "."));
                    continue;
                }
                dataList.Add((int.MaxValue - item.Value, item.Key));
            }

            dataList.Sort();

            var result = new List<string>();
            foreach (var item in data)
            {
                result.Add(item.Key);
            }

            return result;
        }
        
        private static Dictionary<string, List<string>> GetFrequentData(Dictionary<string, Dictionary<string, int>> data)
        {
            var frequentData = new Dictionary<string, List<string>>();

            foreach (var item in data.Keys)
            {
                var sortedData = GetSortedListAtDict(data[item]);
                
                frequentData[item] = new List<string>();
                foreach (var word in sortedData)
                {
                    frequentData[item].Add(word);
                    if (frequentData[item].Count == 10)
                        break;
                }
            }

            return frequentData;
        }
        
        public static Dictionary<string, List<string>> GetMostFrequentNextWords(List<List<string>> text)
        {
            return GetFrequentData(GetFrequencyStatictic(text));
        }
    }
}