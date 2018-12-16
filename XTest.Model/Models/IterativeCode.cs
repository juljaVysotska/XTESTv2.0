using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTest.Model.Models
{
    class IterativeCode : INotifyPropertyChanged
    {
        public int q { get; set; }
        public int[][] Array { get; set; }
        public object PropertyChanged { get; private set; }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);
                this.PropertyChanged(this, args);
            }
        }
    }
}
