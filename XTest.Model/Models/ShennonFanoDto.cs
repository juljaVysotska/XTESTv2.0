using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTest.Model.Models
{
    public class ShennonFanoDto
    {
        public int key { get; set; }
        public double value { get; set; }
        public string result { get; set; }

        public ShennonFanoDto(int key, double value, string result)
        {
            this.key = key;
            this.value = value;
            this.result = result;
        }
    }
}
