using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using XTest.Model.Services;

namespace XTest.ViewModel
{
    public class RidMallerViewModel : INotifyPropertyChanged
    {
        public RidMaller _service { get; set; }
        public int[][] array;
        private int check { get; set; }
        private int mark { get; set; }
        private int[][] OldArray { get; set; }

        public int[][] Array
        {
            get { return array; }
            set
            {
                array = value;
                OnPropertyChanged("Array");
            }
        }

        private string output;
        public string Output
        {
            get { return output; }
            set
            {
                output = value;
                OnPropertyChanged("Output");
            }
        }

        public RidMallerViewModel(RidMaller ridMaller)
        {
            this._service = ridMaller;
            Array = ridMaller.RandomMessage(ridMaller.GenerateArray());
            OldArray = ridMaller.FuckenCSharp(Array);
            check = 1;
            mark = 0;

        }

        private RelayCommand next;

        public RelayCommand Next
        {
            get
            {
                return next ??
                  (next = new RelayCommand(obj =>
                  {
                      if (check < 3)
                      {
                          OldArray = _service.Code(OldArray);
                          if (_service.Equals(OldArray, Array))
                              mark += 1;

                          Array = _service.RandomMessage(_service.GenerateArray());
                          OldArray = _service.FuckenCSharp(Array);

                      }
                      else if (check == 3)
                      {
                          MessageBox.Show("decode");
                          OldArray = _service.Code(OldArray);
                          if (_service.Equals(OldArray, Array))
                              mark += 1;

                          Array = _service.RandomMessageDecode(_service.GenerateArray());
                          OldArray = _service.FuckenCSharp(Array);
                      }
                      else
                      {
                          OldArray = _service.Decode(OldArray);
                          if (_service.Equals(OldArray, Array))
                              mark += 1;
                          Array = _service.RandomMessageDecode(_service.GenerateArray());
                          OldArray = _service.FuckenCSharp(Array);
                      }
                      check += 1;
                      if (check == 7)
                      {
                          MessageBox.Show(mark.ToString());
                      }
                  }));
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
