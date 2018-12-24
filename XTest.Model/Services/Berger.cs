using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTest.Model.Services
{
    public static class Berger
    {
        static Random rnd = new Random();

        public static string generateDecode()
        {
            return encode(generateEncode());
        }

        public static string generateEncode()
        {
            string result = "";
            for (int i = 0; i < rnd.Next(6, 15); i++)
            {
                result += rnd.Next(2);
            }
            return result;
        }

        public static bool isEncodedCorrectly(string task, string result)
        {
            return result.Equals(encode(task));
        }

        public static bool isDecodedCorrectly(string task, string result)
        {
            return result.Equals(decode(task));
        }

        private static string encode(string input)
        {
            double r = Math.Ceiling(Math.Log(input.Length, 2));
            int count = ~input.Count(c => c.Equals("1"));
            string addition = count.ToString();
            while (addition.Length < r)
            {
                addition = "1" + addition;
            }
            return input + addition;
        }

        private static string decode(string input)
        {
            return "";
        }
    }
}
