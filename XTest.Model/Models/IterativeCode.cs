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
        public int[][] array;

        public IterativeCode()
        {
            this.q = 0;
            array = null;
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

        public int[][] Array
        {
            get { return array; }
            set
            {
                array = value;
                OnPropertyChanged("Arrary");
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
