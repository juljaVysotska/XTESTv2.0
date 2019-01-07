using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace XTest.Model.Models
{
    public class IterativeCode : INotifyPropertyChanged
    {
        public int q;
        public string[][] array;
        public int[][] intArray;
        public int[][] result;

        public IterativeCode()
        {
            this.q = 0;
            array = null;
            result = null;
        }


        public int Q
        {
            get { return q; }
            set
            {
                q = value;
                OnPropertyChanged("Q");
            }
        }

        public string[][] ArrayCode
		{
            get { return array; }
            set
            {
                array = value;
                OnPropertyChanged("ArrayCode");
            }
        }

		public int[][] IntArray
		{
			get { return intArray; }
			set
			{
				intArray = value;
				OnPropertyChanged("IntArray");
			}
		}

		public int[][] Result
		{
			get { return result; }
			set
			{
				result = value;
				OnPropertyChanged("Result");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
