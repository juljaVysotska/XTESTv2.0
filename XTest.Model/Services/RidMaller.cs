using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTest.Model.Services
{
    public class RidMaller
    {

        public int[][] GenerateArray()
        {
            var NikomyNePokazyvatu = new int[12][];
            NikomyNePokazyvatu[0] = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 };
            NikomyNePokazyvatu[1] = new int[] { 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 };
            NikomyNePokazyvatu[2] = new int[] { 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0 };
            NikomyNePokazyvatu[3] = new int[] { 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 0 };
            NikomyNePokazyvatu[4] = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 };
            NikomyNePokazyvatu[5] = new int[] { 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0 };
            NikomyNePokazyvatu[6] = new int[] { 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0 };
            NikomyNePokazyvatu[7] = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0 };
            NikomyNePokazyvatu[8] = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 0 };
            NikomyNePokazyvatu[9] = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 1, 1, 0 };
            NikomyNePokazyvatu[10] = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0 };
            NikomyNePokazyvatu[11] = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            return NikomyNePokazyvatu;
        }

        public int[][] RandomMessage(int[][] arr)
        {
            Random rand = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i][arr[i].Length - 1] = rand.Next(0, 2);
            }
            return arr;
        }

        public int[][] RandomMessageDecode(int[][] arr)
        {
            Random rand = new Random();
            for (int i = 0; i < arr[0].Length; i++)
            {
                arr[arr.Length - 1][i] = rand.Next(0, 2);
            }
            return arr;
        }

        public bool Equals(int[][] a1, int[][] a2)
        {
            var a = true;
            for (int i = 0; i < a1.Length; i++)
            {
                for (int j = 0; j < a2[0].Length; j++)
                {

                    if (a)
                        a = a1[i][j] == a2[i][j];
                }
            }
            return a;
        }

        public int[][] FuckenCSharp(int[][] arr)
        {
            int[][] array = new int[arr.Length][];
            for (int i = 0; i < arr.Length; i++)
            {
                array[i] = new int[arr[i].Length];
                for (int j = 0; j < arr[0].Length; j++)
                {
                    array[i][j] = arr[i][j];
                }
            }
            return array;
        }

        public int Formula(int a, int[] arr, bool acc)
        {
            var result = 0;
            if (acc)
            {
                switch (a)
                {
                    case 0:
                        result = arr[0];
                        break;
                    case 1:
                        result = (arr[0] + arr[1]) % 2;
                        break;
                    case 2:
                        result = (arr[0] + arr[2]) % 2;
                        break;
                    case 3:
                        result = (arr[0] + arr[1] + arr[2] + arr[5]) % 2;
                        break;
                    case 4:
                        result = (arr[0] + arr[3]) % 2;
                        break;
                    case 5:
                        result = (arr[0] + arr[1] + arr[3] + arr[6]) % 2;
                        break;
                    case 6:
                        result = (arr[0] + arr[2] + arr[3] + arr[8]) % 2;
                        break;
                    case 7:
                        result = (arr[0] + arr[1] + arr[2] + arr[3] + arr[5] + arr[6] + arr[8]) % 2;
                        break;
                    case 8:
                        result = (arr[0] + arr[4]) % 2;
                        break;
                    case 9:
                        result = (arr[0] + arr[1] + arr[4] + arr[7]) % 2;
                        break;
                    case 10:
                        result = (arr[0] + arr[2] + arr[4] + arr[9]) % 2;
                        break;
                    case 11:
                        result = (arr[0] + arr[1] + arr[2] + arr[4] + arr[5] + arr[7] + arr[9]) % 2;
                        break;
                    case 12:
                        result = (arr[0] + arr[3] + arr[4] + arr[10]) % 2;
                        break;
                    case 13:
                        result = (arr[0] + arr[1] + arr[3] + arr[4] + arr[6] + arr[7] + arr[10]) % 2;
                        break;
                    case 14:
                        result = (arr[0] + arr[2] + arr[3] + arr[4] + arr[8] + arr[9] + arr[10]) % 2;
                        break;
                    case 15:
                        result = arr.Sum() % 2;
                        break;
                }
            }
            else
            {
                switch (a)
                {
                    case 0:
                        result = arr[0];
                        break;
                    case 1:
                        result = (arr[0] + arr[1]) % 2;
                        break;
                    case 2:
                        result = (arr[0] + arr[2]) % 2;
                        break;
                    case 3:
                        result = (arr[0] + arr[4]) % 2;
                        break;
                    case 4:
                        result = (arr[0] + arr[8]) % 2;
                        break;
                    case 5:
                        result = (arr[0] + arr[1] + arr[2] + arr[3]) % 2;
                        break;
                    case 6:
                        result = (arr[0] + arr[1] + arr[4] + arr[6]) % 2;
                        break;
                    case 7:
                        result = (arr[0] + arr[1] + arr[8] + arr[9]) % 2;
                        break;
                    case 8:
                        result = (arr[0] + arr[2] + arr[4] + arr[6]) % 2;
                        break;
                    case 9:
                        result = (arr[0] + arr[2] + arr[8] + arr[10]) % 2;
                        break;
                    case 10:
                        result = (arr[0] + arr[4] + arr[8] + arr[12]) % 2;
                        break;
                }
            }
            return result;
        }
    

        public int[][] Code(int [][] arr)
        {
            int [] array = new int[11];
            for (int i = 0; i < arr.Length - 1; i++)
            {
                array[i] = arr[i].Last();
            }
            for (int i = 0; i < 16; i++)
            {
                arr[11][i] = Formula(i, array, true);
            }
            return arr;
        }

        public int[][] Decode(int[][] arr)
        {
            var array = arr[11];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i][16] = Formula(i, array, false);
            }
            return arr;
        }

        public string Output(int [][] arr)
        {
            var str = "";
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    str += ("  " + arr[i][j]);
                }
                str += "\n";
            }
            return str;
        }
    }
}
