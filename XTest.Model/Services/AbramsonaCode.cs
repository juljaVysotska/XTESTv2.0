using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTest.Model.Services
{
    public class AbramsonaCode
    {
        public string neprPol = "10011";
        public string obrPol = "110101";
        string code;
        public string coded;
        string answer;

        public string Code()
        {
            Random rand = new Random();
            code = "";
            for (int i = 0; i < 10; i++)
            {
                code += rand.Next(0, 2);
            }

            coded = code + BinToDec();
            return code;
        }

        string BinToDec()
        {
            int dec = Convert.ToInt32(code, 2);
            int pol = Convert.ToInt32(obrPol, 2);
            int ans = dec % pol;
            string ret = Convert.ToString(ans, 2);
            if (ret.Length < 5)
            {
                int temp = 5 - ret.Length;
                string timed = "";
                for(; temp>0; temp--)
                {
                    timed += "0";
                }
                timed += ret;
                ret = timed;
            }
            return ret;
        }

        public bool CorrectCode(string a, string b)
        {
            if (b != null && a.Count() == b.Count())
            {
                if (a.Equals(b))
                    return true;
            }
            return false;
        }

    }
}
