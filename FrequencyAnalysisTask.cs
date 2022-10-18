using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        private static Dictionary<string, Dictionary<string, int>> GetData(List<List<string>> text)
        {
            var data = new Dictionary<string, Dictionary<string, int>>();
            
            foreach (var item in text)
            {
                for (var i = 2; i <= 3; i++)
                {
                    for (var j = 0; j < item.Count - i + 1; j++)
                    {
                        var gramm = new StringBuilder();
                        gramm.Append(item[j]);
                        for (var k = 1; k < i - 1; k++)
                        {
                            gramm.Append(' ');
                            gramm.Append(item[j + k]);
                        }

                        if (data.ContainsKey(gramm.ToString()))
                        {
                            if (data[gramm.ToString()].ContainsKey(item[j + i - 1]))
                                data[gramm.ToString()][item[j + i - 1]]++;
                            else
                                data[gramm.ToString()][item[j + i - 1]] = 1;
                        }
                        else
                            data[gramm.ToString()] = new Dictionary<string, int>{{item[i + j - 1], 1}};
                    }
                }
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
            return GetFrequentData(GetData(text));
        }
    }
}