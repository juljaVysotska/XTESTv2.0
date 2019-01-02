using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTest.Model.Services
{
    public static class ShennonFanoService
    {
        static Random rnd = new Random();

        public static Dictionary<int, double> generateMessages()
        {
            Dictionary<int, double> result = new Dictionary<int, double>();
            double message = 1.0;
            int i = 0;
            while (message > 0)
            {
                double minus = message > 0.1 ? rnd.Next(10,Math.Max((int)(message * 100/2), 10))/100.0 : rnd.Next(1, Math.Max((int)(message * 100), 1)) / 100.0;
                message = Math.Round(message * 100 - minus * 100)/100.0;
                result.Add(i, minus);
                i++;
            }
            sortValues(result);
            calculate(result);
            return result;
        }

        private static void sortValues(Dictionary<int, double> messageFrequencies)
        {
            List<double> values = messageFrequencies.Values.ToList();
            values.Sort();
            values.Reverse();
            for (int i = 0; i < values.Count(); i++)
            {
                messageFrequencies[i] = values[i];
            }
        }

        public static bool isCalculatedCorrectly(Dictionary<int, double> task, Dictionary<int, string> input)
        {
            return input.SequenceEqual(calculate(task));
        }

        public static Dictionary<int, string> calculate(Dictionary<int, double> messageFrequencies)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            calculateRecursive(result, messageFrequencies, 0, messageFrequencies.Count() - 1, 1.0, "");
            return result;
        }

        private static void calculateRecursive(Dictionary<int, string> result, Dictionary<int, double> messageFrequencies, int start, int end, double percentage, string path)
        {
            if (start == end)
            {
                result.Add(start, path);
            }
            else
            {
                double tempPercentage = percentage;
                int i = start;
                for (; i <= end; i++)
                {
                    tempPercentage = Math.Round(tempPercentage - messageFrequencies[i], 2);
                    if (tempPercentage <= percentage / 2)
                    {
                        break;
                    }
                }
                calculateRecursive(result, messageFrequencies, start, i, Math.Round(percentage - tempPercentage, 2), path + "1");
                calculateRecursive(result, messageFrequencies, i+1, end, tempPercentage, path + "0");
            }
        }
    }
}
