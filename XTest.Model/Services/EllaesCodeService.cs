using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTest.Model.Services
{
    public class EllaesCodeService
    {
        public int[][] Code(int[][] arr)
        {
            Array.Resize(ref arr, arr.Length + 1);
            arr[arr.Length - 1] = new int[arr[0].Length];
            arr[arr.Length - 1].Initialize();
            for (int i = 0; i < arr.Length; i++)
            {
                Array.Resize(ref arr[i], arr[i].Length + 1);
                arr[i][arr[i].Length - 1] = Sum(arr[i]) % 2;
            }
            arr[arr.Length - 1] = SumForColumn(arr);
            arr[arr.Length - 1][arr.Length - 1] = 0;
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

        public int[][] GenerateArray(int x, int y)
        {
            Random rand = new Random();
            int[][] array = new int[x][];
            for (int i = 0; i < x; i++)
            {
                array[i] = new int[y];
                for (int j = 0; j < y; j++)
                {
                    array[i][j] = rand.Next(0, 2);
                }
            }

            return array;
        }

        public int[][] GenerateTestArray(int x, int y)
        {
            Random rand = new Random();
            int[][] array = new int[x + 1][];
            for (int i = 0; i < x + 1; i++)
            {
                array[i] = new int[y + 1];
                for (int j = 0; j < y + 1; j++)
                {
                    if (i == x || j == y)
                    {
                        array[i][j] = 0;
                    }
                    else
                    {
                        array[i][j] = rand.Next(0, 2);
                    }
                }
            }

            return array;
        }

        public int[][] FuckenCSharp(int [][] arr)
        {
            int[][] array = new int[arr.Length][];
            for (int i = 0; i < arr.Length; i++)
            {
                array[i] = new int[arr.Length];
                for (int j = 0; j < arr.Length; j++)
                {
                    array[i][j] = arr[i][j];
                }
            }
            return array;
        }

        public int[][] GenerateArrayWithException(int x, int y)
        {
            Random rand = new Random();
            int[][] array = Code(GenerateArray(x, y));
            int i = rand.Next(0, array.Length - 1);
            int j = rand.Next(0, array[0].Length - 1);
            array[i][j] = (array[i][j] + 1) % 2;
            return array;
        }

        private int Sum(int[] array)
        {
            int sum = 0;
            foreach (var item in array)
            {
                sum += item;
            }
            return sum;
        }

        private int[] SumForColumn(int[][] array)
        {
            int[] sum = new int[array[0].Length];
            sum.Initialize();
            for (int i = 0; i < array[0].Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    sum[i] = (sum[i] + array[j][i]) % 2;
                }
            }
            return sum;
        }

        private int[] SumForRaw(int[][] array)
        {
            int[] sum = new int[array.Length];
            sum.Initialize();
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    sum[i] = (sum[i] + array[i][j]) % 2;
                }
            }
            return sum;
        }

        public int[][] ResizeArray(int[][] arr)
        {
            Array.Resize(ref arr, arr.Length + 1);
            arr[arr.Length - 1] = new int[arr[0].Length];
            arr[arr.Length - 1].Initialize();
            for (int i = 0; i < arr.Length; i++)
            {
                Array.Resize(ref arr[i], arr[i].Length + 1);
                arr[i][arr[i].Length - 1] = 0;
            }

            return arr;
        }

        public int[][] Decode(int[][] array)
        {
            int a = 0, b = 0;
            int[][] arr = array;
            Array.Resize(ref arr, arr.Length - 1);
            for (int i = 0; i < arr.Length; i++)
            {
                Array.Resize(ref arr[i], arr[i].Length - 1);
            }
            arr = Code(arr);
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[0].Length; j++)
                {
                    if (i != array.Length - 1 | j != array[0].Length - 1)
                    {
                        if (i == array.Length - 1 && array[i][j] != arr[i][j])
                        {
                            b = j;

                        }
                        else if (j == array[0].Length - 1 && array[i][j] != arr[i][j])
                        {
                            a = i;
                        }
                    }
                }
            }
            array[a][b] = (array[a][b] + 1) % 2;
            return array;
        }
    }
}
