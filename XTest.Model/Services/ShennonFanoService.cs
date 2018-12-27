using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTest.Model.Services
{
    public class ShennonFanoService
    {
        Random rnd = new Random();

        public Dictionary<int, double> generateMessages()
        {
            Dictionary<int, double> result = new Dictionary<int, double>();
            double message = 1.0;
            int i = 0;
            while (message > 0)
            {
                double minus = rnd.Next((int)(message * 100))/100;
                message -= minus;
                result.Add(i, minus);
                i++;
            }
            
            return result;
        }

        public bool isCalculatedCorrectly()
        {
            return false;
        }

        public Dictionary<int, double> calculate()
        {
            return null;
        }
    }
}
