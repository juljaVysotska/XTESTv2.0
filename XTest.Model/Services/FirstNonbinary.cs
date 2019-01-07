using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTest.Model.Services
{
    public class FirstNonbinary
    {
        int q;
        int n;
        char[] alph;
        char[] code;

        string razm = "размещения";
        string oprSoch = "Определенные сочетания";
        string smenno = "сменно-качественный";
        string allSoch = "все сочетания";
        string per = "перестановках";

        public char[] GenerateAlph()
        {
            Random rand = new Random();
            q = rand.Next(1, 4);
            alph = new char[q];
            for (int i = 0; i < q; i++)
            {
                alph[q] = (char)rand.Next('A', 'Z' + 1);
            }
            return alph;
        }

        //public char[] GetCode()
        //{
        //    Random rand = new Random();
        //    n = rand.Next(1, 3);
        //    code = new char[alph.Length];
        //    switch (rand.Next(1, 5))
        //    {
        //        case 1:
        //            for (int i = 0; i < l; i++)
        //            {
        //                int j;
        //                for (j = 0; j < n; j++)
        //                    if (a[j] == i + 1) break;
        //                if (j == n)
        //                {
        //                    a[n] = i + 1;
        //                    if (n < l - 1) set(a, n + 1, l);
        //                    else
        //                    {
        //                        for (int k = 0; k < l; k++) Console.Write(a[k]);
        //                        Console.WriteLine();
        //                    }
        //                }
        //            }
        //            break;
        //        case 2:
        //            break;
        //        case 3:
        //            break;
        //        case 4:
        //            break;
        //        case 5:
        //            break;
        //    }
        //}
    }
}
