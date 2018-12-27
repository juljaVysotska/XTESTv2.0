using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XTest.Model.Models
{
    public class BinaryDecimalCode : INotifyPropertyChanged
    {
        private string message;
        private string result;
        private string code;

        public BinaryDecimalCode()
        {
            this.message = "";
            this.result = "";
            this.code = "";
        }

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }

        public string Result
        {
            get { return result; }
            set
            {
                result = value;
                OnPropertyChanged("Result");
            }
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
