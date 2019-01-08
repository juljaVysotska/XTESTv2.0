using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTest.Model.Services
{
    public class RecurentCode
    {
        Random rand = new Random();
        public int[] GenerateArray(int k)
        {
            var length = k * 4;
            var array = new int[length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(0, 2);
            }
            return array;
        }

        public int[] Code(int[] arr, int k)
        {
            var array = new int[arr.Length];
            for (int i = 0; i < k; i++)
            {
                array[i] = 0;
            }
            for (int i = 0; i < arr.Length - k; i++)
            {
                array[i + k] = (arr[i] + arr[i + k]) % 2;
            }
            return array;
        }

        public int[][] GenerateForDecode(int k)
        {
            int[][] arr = new int[2][];
            arr[0] = GenerateArray(k);
            arr[1] = Code(arr[0], k);
            var i = rand.Next(0, arr[0].Length - k);
            arr[0][i] = (arr[0][i] + 1) % 2;
            return arr;
        }

        public int[] Decode(int[][] arr, int k)
        {
            var test = Code(arr[0], k);
            int a = 0;
            for (int i = 0; i < arr[1].Length; i++)
            {
                if (test[i] != arr[1][i])
                    a = i;
            }
            test[a - k] = (test[a - k] + 1) % 2;
            return test;
        }

        public string Output(int[] arr)
        {
            string s = "";
            for (int i = 0; i < arr.Length; i++)
            {
                s += arr[i];
            }
            return s;
        }
    }
}
