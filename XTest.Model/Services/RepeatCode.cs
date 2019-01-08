using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTest.Model.Services
{
    public class RepeatCode
    {
        private string code;
        private string coded;

        public string GenerateCode()
        {
            int n;
            Random rand = new Random();
            n = rand.Next(8, 10);
            code = "";
            for (int i = 0; i < n; i++)
            {
                code += rand.Next(9);
            }
            return code;
        }

        public string Code()
        {
            coded = code + code;
            return coded;
        }

        public bool CorrectCode(string a, string b) //b is input combination
        {
            if(b != null && a.Count() == b.Count())
            {
                if (a.Equals(b))
                    return true;
            }
            return false;
        }
    }
}
