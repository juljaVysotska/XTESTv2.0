using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTest.Model.Services
{
    public class Varshamov
    {
        Random rand = new Random();
        private int factorial(int n)
        {
            if (n == 0)
                return 1;
            else
                return n + factorial(n - 1);
        }

        public int CountOfColumns(int n, int d)
        {
            var num = 1;
            for (int i = 1; i < d - 1; i++)
            {
                num += Formula(n, i);
            }
            return num;
        }

        private int Formula(int n, int i)
        {
            return factorial(n - 1) / (factorial(i) * factorial(n - 1 - i));
        }

        public int CountOfFixedExceptions(int d)
        {
            return (d - 1) / 2;
        }

        public int CountOfRaws(int n, int r)
        {
            return n - r;
        }

        public int CountOfTestDigits(int r)
        {
            if ((int)Math.Sqrt(r) == Math.Sqrt(r))
                return r;
            else
                return r + 1;
        }

        public int[][] GenerateSimpleMatrix(int n)
        {
            int[][] arr = new int[n][];
            for (int i = 0; i < n; i++)
            {
                arr[i] = new int[n];
                for (int j = 0; j < arr[i].Length; j++)
                {
                    if (i == j)
                        arr[i][j] = 1;
                    else
                        arr[i][j] = 0;
                }
            }
            return arr;
        }

        public bool Equals(int[][] a1, int[][] a2)
        {
            var a = true;
            for (int i = 0; i < a1.Length; i++)
            {
                for (int j = 0; j < a2.Length; j++)
                {
                    if (a)
                        a = a1[i][j] == a2[i][j];
                }
            }
            return a;
        }

        public bool Equals(int[] a1, int[] a2)
        {
            var a = true;
            for (int i = 0; i < a1.Length; i++)
            {
                if (a)
                    a = a1[i] == a2[i];
            }
            return a;
        }

        public bool CheckProdMatrix(int b, int[][] arr)
        {
            int[] array = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    array[i] += arr[i][j];
                }
            }
            bool a = true;
            for (int i = 0; i < array.Length; i++)
            {
                a &= array[i] >= b - 1;
            }
            return a && CheckHelper(ArrayToString(arr));

        }

        private string[] ArrayToString(int[][] arr)
        {
            string[] array = new string[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    array[i] += arr[i][j];
                }
            }
            return array;
        }

        public string SimpleArrToString(int[] arr)
        {
            string s = "";
            for (int i = 0; i < arr.Length; i++)
            {
                s += arr[i];
            }
            return s;
        }

        public int[] GenerateArray(int n)
        {
            int[] arr = new int[n];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rand.Next(0, 2);
            }
            return arr;
        }

        public int[] FuckenCSharp(int[] arr)
        {
            int[] array = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                array[i] = arr[i];
            }
            return array;
        }

        public string[] Syndrom(int[][] arr, int[][] simpleArr)
        {
            string[] array = new string[arr.Length + simpleArr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    array[i] += arr[i][j];
                }
            }
            for (int i = 0; i < simpleArr.Length; i++)
            {
                for (int j = 0; j < simpleArr[i].Length; j++)
                {
                    array[arr.Length + i] += simpleArr[i][j];
                }
            }
            return array;
        }

        public int[] CodeNum(int[][] arrSimple, int[] arr)
        {
            int[] array = new int[arr.Length + 4];
            for (int i = 0; i < arr.Length; i++)
            {
                array[i] = arr[i];
            }

            for (int i = 0; i < arrSimple.Length; i++)
            {
                for (int j = 0; j < arrSimple[i].Length; j++)
                {
                    if (arr[i] == 1)
                        array[arrSimple.Length + j] += arrSimple[i][j];
                }
            }

            for (int i = 0; i < 4; i++)
            {
                array[arrSimple.Length + i] = array[arrSimple.Length + i] % 2;
            }

            return array;
        }

        public int[][] GenerateArr(int b)
        {
            int[][] array = new int[6][];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = new int[4];
                for (int j = 0; j < array[i].Length; j++)
                {
                    array[i][j] = rand.Next(0, 2);
                }
            }
            if (CheckHelper(ArrayToString(array)))
                return array;
            else
                return GenerateArr(b);
        }

        public int[][] HMAtrix(int[][] arr)
        {
            int[][] array = new int[arr[0].Length][];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = new int[arr.Length];
                for (int j = 0; j < array[i].Length; j++)
                {
                    array[i][j] = arr[j][i];
                }
            }
            return array;
        }

        public bool CheckHelper(string[] arr)
        {
            bool b = true;
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i; j < arr.Length; j++)
                {
                    b &= !(arr[i] == arr[j]);
                }
            }
            return b;
        }
    }
}
