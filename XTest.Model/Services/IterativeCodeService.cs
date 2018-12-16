using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTest.Model.Services
{
    class IterativeCodeService
    {

        public int getRandomQ()
        {
            Random rdm = new Random();
            int q = rdm.Next(2, 11);
            return q;
        }

        public int[][] GenerateArray()
        {
            int[][] arr = new int[5][];
            Random rdm = new Random();
            //q = rdm.Next(2, 11);
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = new int[5];
                for (int j = 0; j < arr[0].Length; j++)
                {
                    arr[i][j] = rdm.Next(10);
                }
            }
            return arr;
        }

        public int[][] encodeArr(int[][] arrForEncoding, int q)
        {
            int[][] result = new int[6][];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new int[6];
                for (int j = 0; j < result[0].Length; j++)
                {
                    if (i < 5 && j < 5)
                    {
                        result[i][j] = arrForEncoding[i][j];
                    }
                    else if (i == 5)
                    {
                        result[i][j] = calcLine(sumArrColumn(result, j), q);
                    }
                    else
                    {
                        result[i][j] = calcLine(sumArrLine(result, i), q);
                    }
                }
            }
            return result;
        }

        public int[][] makeOneMistake(int[][] arrForMistake)
        {
            int[][] arrWithMisst = arrForMistake;
            Random rdm = new Random();
            int i = rdm.Next(arrForMistake.Length);
            int j = rdm.Next(arrForMistake[0].Length);
            int misstake = rdm.Next(10);
            while (misstake == arrWithMisst[i][j])
            {
                misstake = rdm.Next(10);
            }
            arrWithMisst[i][j] = misstake;
            return arrWithMisst;
        }



        private int calcLine(int sumOfEl, int q)
        {
            return q - sumOfEl % q;
        }

        private int sumArrLine(int[][] arr, int lineNumb)
        {
            int sum = 0;
            for (int i = 0; i < arr[0].Length; i++)
            {
                sum += arr[lineNumb][i];
            }
            return sum;
        }

        private int sumArrColumn(int[][] arr, int ColumnNumb)
        {
            int sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                sum += arr[i][ColumnNumb];
            }
            return sum;
        }
    }
}
