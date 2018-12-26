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
        public int[][] arrayCode;
        public int[][] arrayDecode;
        private int check { get; set; }
        private int mark { get; set; }
        private int[][] OldArray { get; set; }
        private int[][] OldArrayCode { get; set; }
        private int[][] OldArrayDecode { get; set; }


        public int[][] Array
        {
            get { return array; }
            set
            {
                array = value;
                OnPropertyChanged("Array");
            }
        }

        public int[][] ArrayCode
        {
            get { return arrayCode; }
            set
            {
                arrayCode = value;
                OnPropertyChanged("ArrayCode");
            }
        }

        public int[][] ArrayDecode
        {
            get { return arrayDecode; }
            set
            {
                arrayDecode = value;
                OnPropertyChanged("ArrayDecode");
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


        public RidMallerViewModel()
        {
            var ridMaller = new RidMaller();
            this._service = ridMaller;
            Array = ridMaller.RandomMessage(ridMaller.GenerateArray());
            OldArray = ridMaller.FuckenCSharp(Array);
            ArrayCode = ridMaller.RandomMessage(ridMaller.GenerateArray());
            OldArrayCode = ridMaller.FuckenCSharp(ArrayCode);
            ArrayDecode = _service.RandomMessageDecode(_service.GenerateArray());
            OldArrayDecode = _service.FuckenCSharp(ArrayDecode);
            check = 1;
            mark = 0;
        }


        private RelayCommand next;
        private RelayCommand nextCode;
        private RelayCommand nextDecode;


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
                          Array = _service.RandomMessage(_service.GenerateArray());
                          OldArray = _service.FuckenCSharp(Array);
                          check = 1;
                          mark = 0;
                      }
                  }));
            }

        }

        public RelayCommand NextCode
        {
            get
            {
                return nextCode ??
                  (nextCode = new RelayCommand(obj =>
                  {
                      OldArrayCode = _service.Code(OldArrayCode);
                      if (_service.Equals(OldArrayCode, ArrayCode))
                          MessageBox.Show("Правильно!");
                      else
                          MessageBox.Show("Не правильно!");

                      ArrayCode = _service.RandomMessage(_service.GenerateArray());
                      OldArrayCode = _service.FuckenCSharp(ArrayCode);
                  }));
            }

        }

        public RelayCommand NextDecode
        {
            get
            {
                return nextDecode ??
                  (nextDecode = new RelayCommand(obj =>
                  {
                      OldArrayDecode = _service.Decode(OldArrayDecode);
                      if (_service.Equals(OldArrayDecode, ArrayDecode))
                          MessageBox.Show("Правильно!");
                      else
                          MessageBox.Show("Не правильно!");

                      ArrayDecode = _service.RandomMessageDecode(_service.GenerateArray());
                      OldArrayDecode = _service.FuckenCSharp(ArrayDecode);
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
