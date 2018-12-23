using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTest.Model.Services
{
    class BinaryDecimalCodeService
    {
        public string generateCode()
        {
            string result = "-1";
            Random random = new Random();
            int second = random.Next(1, 3);
            int third = random.Next(2, 5);
            int fourth = random.Next(3, 9);
            int d = second == 2 ? 6 : 7;
            while (third + fourth < d)
            {
                third = random.Next(3, 10);
                fourth = random.Next(3, 10);
            }
            result = result.Insert(0, "-" + second.ToString());
            result = result.Insert(0, "-" + third.ToString());
            result = result.Insert(fourth < third ? 1 : 0, fourth.ToString());
            return result;
        }

        public int decodeNumber(string binaryNumb, string code)
        {
            int[] binNumb = binaryNumb.Select(n => int.Parse(n.ToString())).ToArray();
            int[] intCode = code.Split('-').Select(n => Convert.ToInt32(n)).ToArray();
            int count = binaryNumb.Length / 4;
            int number = 0;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < binaryNumb.Length; i = i + 4)
            {
                number += intCode[0] * binNumb[i];
                number += intCode[1] * binNumb[i + 1];
                number += intCode[2] * binNumb[i + 2];
                number += intCode[3] * binNumb[i + 3];
                sb.Append(number.ToString());
                number = 0;
            }
            int result = Convert.ToInt32(sb.ToString());
            return result;
        }

        public string enncodeNumber(int number, string code)
        {
            int[] numbs = Convert.ToString(number).Select(n => int.Parse(n.ToString())).ToArray();
            int[] intCode = code.Split('-').Select(n => Convert.ToInt32(n)).ToArray();
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < numbs.Length + 1; i++)
            {
                int current = numbs[i - 1];
                for (int j = 0; j < intCode.Length; j++)
                {
                    if (current < intCode[j])
                    {
                        sb.Append("0");
                    }
                    else
                    {
                        sb.Append("1");
                        current = current - intCode[j];
                    }

                }
            }
            return sb.ToString();
        }

    }
}
