using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTest.Model.Services
{
    static class Berger
    {
        Random rnd = new Random();

        public string generateDecode()
        {
            return encode(generateEncode());
        }

        public string generateEncode()
        {
            string result = "";
            for (int i = 0; i < rnd.Next(6, 15); i++)
            {
                result += rnd.Next(1);
            }
            return result;
        }

        public bool isEncodedCorrectly(string task, string result)
        {
            return true;
        }

        public bool isDecodedCorrectly(string task, string result)
        {
            return true;
        }

        private string encode()
        {
            return "";
        }

        private string decode()
        {
            return "";
        }
    }
}
