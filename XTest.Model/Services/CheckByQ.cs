using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XTest.Model.Services
{
    public class CheckByQ
    {
        public int q;
        public int score = 0;
        public int n;
        

        public int[] GenerateCode()
        {
            Random rand = new Random();
            q = rand.Next(1, 8);
            n = rand.Next(8, 10);
            int[] code = new int[n];
            for (int i = 0; i < n; i++)
            {
                code[i] = Convert.ToInt32(rand.Next(9));
            }
            return code;
        }

        public int[] Code(int[] code, int q)
        {
            int[] coded = new int[code.Count() + 1];
            int sum = 0;
            int i = 0;
            for (; i < code.Count(); i++)
            {
                coded[i] = code[i];
                sum += code[i];
            }
            coded[i] = sum % q;
            return coded;
        }

        public bool CheckCoding(int[] startCode, int q, int[] checkCode)
        {
            int[] temp = Code(startCode, q);
            int count = temp.Count();
            if (checkCode != null && checkCode.Count() == count) ///если ответ не пустой и длина ответа равна правильной
            {
                for (int i = 0; i < count; i++)
                {
                    if (checkCode[i] != temp[i]) ///если НЕ совпадают числа массива
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public int[] WrongCode(int [] code)
        {
            int count = code.Length;
            code[count-1] += 1;
            return code;
        }
    }
}
