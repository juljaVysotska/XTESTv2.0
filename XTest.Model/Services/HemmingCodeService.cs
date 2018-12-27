using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTest.Model.Services
{
    public class HemmingCodeService
    {


        public static int[] FixBrokenBit(int BrokenBitPosition, int[] Sequence)
        {
            if (Sequence[BrokenBitPosition - 1] == 0) Sequence[BrokenBitPosition - 1] = 1;
            else Sequence[BrokenBitPosition - 1] = 0;
            return Sequence;
        }


        public static int[] SplitOandOne(int[] Sequence, int position)
        {
            if (Sequence[position - 1] == 0) Sequence[position - 1] = 1;
            else Sequence[position - 1] = 0;
            return Sequence;
        }

        public static int FindBrokenBitPosition(int[] BrokenBeats)
        {
            int Position = 0;

            for (int i = 0; i < BrokenBeats.Length; i++)
            {
                Position += BrokenBeats[i];
            }
            return Position;
        }

        public static int[] IsControlBitValue(int[] a)
        {
            int[] Bites = new int[5];

            if (IsEvenNumber(a[2] + a[4] + a[6] + a[8] + a[10] + a[12] + a[14] + a[16] + a[18] + a[20])) Bites[0] = 0;
            else Bites[0] = 1;

            if (IsEvenNumber(a[2] + a[5] + a[6] + a[9] + a[10] + a[13] + a[14] + a[17] + a[18])) Bites[1] = 0;
            else Bites[1] = 1;

            if (IsEvenNumber(a[4] + a[5] + a[6] + a[11] + a[12] + a[13] + a[14] + a[19] + a[20])) Bites[2] = 0;
            else Bites[2] = 1;

            if (IsEvenNumber(a[8] + a[9] + a[10] + a[11] + a[12] + a[13] + a[14])) Bites[3] = 0;
            else Bites[3] = 1;

            if (IsEvenNumber(a[16] + a[17] + a[18] + a[19] + a[20])) Bites[4] = 0;
            else Bites[4] = 1;
            return Bites;
        }

        public static bool Step(int a)
        {
            return a != 0 && (a & (a - 1)) == 0 ? true : false;
        }

        public static bool IsEvenNumber(int a)
        {
            return ((a & 1) == 0) ? true : false;
        }

        public static int[] GenerateSequence(string bytes)
        {
            int[] Sequence = new int[21];
            int[] ControlBeatValue = new int[5];
            int[] NewControlBeatValue = new int[5];
            int[] BrokenBeats = new int[5];
            int BrokenBitConsole;
            int BrokenBeatPosition;
            string str;


            int ch = 1;
            str = Console.ReadLine();
            Console.WriteLine("Введите последовательность не более 16 бит");
            Console.Write("Последовательность - ");
            //==================================Создать элемент, в котором пользователь должен вписать последовательность бит =========
            str = bytes;
            //==============================================================================================================================
            for (int i = 0; i < str.Length; i++)
                Sequence[i] = (int)Char.GetNumericValue(str[i]);

            for (int i = 0, j = 0; i < str.Length && j < 21; i++, j++)
            {
                if (!Step(ch))
                {
                    if (i == 1 && ch <= 4)
                        i = 0;
                    Sequence[j] = (int)Char.GetNumericValue(str[i]);
                    ch++;
                }
                else
                {
                    Sequence[j] = 0;
                    if (i != 0)
                        i--;
                    ch++;
                }
            }

            ControlBeatValue = IsControlBitValue(Sequence);

            for (int i = 0, j = 0; i < 21 && j < 5; i++)
            {
                if (Step(i + 1))
                {
                    Sequence[i] = ControlBeatValue[j];
                    j++;
                }
            }

            Console.WriteLine("Высчитанные и подставленные контрольные биты");

            for (int i = 0; i < Sequence.Length; i++)
            {
                Console.Write(Sequence[i] + " ");
            }

            Console.WriteLine();

            Console.WriteLine("Введите номер бита, который надо сломать");
          /*  Console.Write("позиция - ");
            //===============================создать элемент, куда надо ввести позицию======================================================
            str = "0";
            Random rnd = new Random();
            str =  rnd.Next(Sequence.Length).ToString();
            //============================================================================================================================
            BrokenBitConsole = Int32.Parse(str);

            Sequence = SplitOandOne(Sequence, BrokenBitConsole);

            NewControlBeatValue = IsControlBitValue(Sequence);

            for (int i = 0, j = 0; i < 21 && j < 5; i++)
            {
                if (Step(i + 1) && ControlBeatValue[j] != Sequence[i])
                {
                    BrokenBeats[j] = i++;
                    j++;
                }
            }

            BrokenBeatPosition = FindBrokenBitPosition(BrokenBeats);

            Sequence = FixBrokenBit(BrokenBeatPosition, Sequence);
*/
            return Sequence;
        }

        public static string Main( string bytes)
        {
            int[] sequence;
            string answer = "";
            sequence = GenerateSequence(bytes);
            for (int i = 0; i < sequence.Length; i++)
            {
                //===============================Создать элемент для вывода результата==============================================================
                if (!Step(i + 1)) { answer += (sequence[i]); }
                //============================================================================================================================
            }
            return answer;
        }
    }
}