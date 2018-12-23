using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XTest.Model.Models
{
    class BinaryDecimalCode : INotifyPropertyChanged
    {
        private string code;
        private int number;
        private string encodeNumb;

        public BinaryDecimalCode()
        {
            this.code = "";
            this.number = 0;
            this.encodeNumb = "";
        }

        public string Code
        {
            get { return code; }
            set
            {
                code = value;
                OnPropertyChanged("Code");
            }
        }

        public string EncodeNumb
        {
            get { return encodeNumb; }
            set
            {
                encodeNumb = value;
                OnPropertyChanged("EncodeNumb");
            }
        }

        public int Number
        {
            get { return number; }
            set
            {
                number = value;
                OnPropertyChanged("Number");
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
