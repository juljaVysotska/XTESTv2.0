using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using XTest.Model.Models;
using XTest.Model.Services;

namespace XTest.ViewModel
{
    public class EllaesCodeViewModel : INotifyPropertyChanged
    {
        private RelayCommand next;
        private int check { get; set; }
        private int mark { get; set; }
        private EllaesCodeService _service { get; set; }
        public EllaesCode EllaesCode { get; set; }
        public int[][] array;
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


        public EllaesCodeViewModel(EllaesCodeService ellaesCodeService, EllaesCode ellaesCode)
        {
            check = 1;
            mark = 0;
            this.EllaesCode = ellaesCode;
            this._service = ellaesCodeService;
            OldArray = _service.GenerateArray(4, 4);
            Array = _service.ResizeArray(OldArray);
        }


        public RelayCommand Next
        {
            get
            {
                return next ??
                  (next = new RelayCommand(obj =>
                  {
                      if (check < 4)
                      {
                          OldArray = _service.Code(OldArray);
                          if (_service.Equals(OldArray, Array))
                              mark += 1;

                          OldArray = _service.GenerateArray(4, 4);
                          Array = _service.ResizeArray(OldArray);
                          
                      }
                      else if (check == 4)
                      {
                          MessageBox.Show("Исправьте ошибки в сообщении:");
                          OldArray = _service.Code(OldArray);
                          if (_service.Equals(OldArray, Array))
                              mark += 1;

                          Array = _service.GenerateArrayWithException(4, 4);
                          OldArray = _service.FuckingCSharp(Array);
                      }
                      else
                      {
                          OldArray = _service.Decode(OldArray);
                          if (_service.Equals(OldArray, Array))
                              mark += 1;
                          Array = _service.GenerateArrayWithException(4, 4);
                          OldArray = _service.FuckingCSharp(Array);
                      }
                      check += 1;
                      if (check == 9)
                      {
                          MessageBox.Show("Правильных ответов " + mark.ToString() + " из 8");
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
