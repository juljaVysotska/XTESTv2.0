﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using XTest.Model.Models;
using XTest.Model.Services;
using static XTest.MainWindow;

namespace XTest.ViewModel
{
    public class EllaesCodeViewModel : INotifyPropertyChanged
    {
        private RelayCommand next;
        private RelayCommand nextCode;
        private RelayCommand nextDecode;
        //private Result result;
        private int selectedIndex;
        private int check { get; set; }
        private int mark { get; set; }
        private EllaesCodeService _service { get; set; }
        public int[][] array;
        private int[][] OldArray { get; set; }
        private int[][] OldArrayCode { get; set; }
        private int[][] OldArrayDecode { get; set; }
        public int[][] arrayCode;
        public int[][] arrayDecode;

        public Result result
        {
            get {
                Result res;
                if (!results.ContainsKey(TestType.Ellaes))
                {
                    res = new Result("Код Эллаеса", 8);
                    results.Add(TestType.Ellaes, res);
                } else
                {
                    res = results[TestType.Ellaes];
                }
                return res; }
            private set {}
        }

        public int[][] Array
        {
            get { return array; }
            set
            {
                array = value;
                OnPropertyChanged("Array");
            }
        }

        public int Mark
        {
            get
            {
                return result.mark; }
            set
            {
                //mark = value;
                //OnPropertyChanged("Mark");
            }
        }

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }

        public int[][] ArrayCode
        {
            get { return array; }
            set
            {
                array = value;
                OnPropertyChanged("ArrayCode");
            }
        }

        public int[][] ArrayDecode
        {
            get { return array; }
            set
            {
                array = value;
                OnPropertyChanged("ArrayDecode");
            }
        }



        public EllaesCodeViewModel()
        {
            EllaesCodeService ellaesCodeService = new EllaesCodeService();
            selectedIndex = 0;
            //Check = 1;
            //Mark = 0;
            //просто вызов делаю, чтобы сгенерить его
            //result.ToString();
            this._service = ellaesCodeService;
            OldArray = _service.GenerateArray(4, 4);
            Array = _service.ResizeArray(OldArray);
            OldArrayCode = _service.GenerateArray(4, 4);
            ArrayCode = _service.ResizeArray(OldArrayCode);
            ArrayDecode = _service.GenerateArrayWithException(4, 4);
            OldArrayDecode = _service.FuckingCSharp(ArrayDecode);
        }

        public int Check
        {
            get
            {
                return result.currentTestNumber; }
            private set
            {
                //check = value;
                //OnPropertyChanged("Check");
            }
        }


        public RelayCommand Next
        {
            get
            {
                return next ??
                  (next = new RelayCommand(obj =>
                  {
                      if (Check < 4)
                      {
                          OldArray = _service.Code(OldArray);
                          if (_service.Equals(OldArray, Array))
                              result.CorrectAnswer();
                          else
                              result.WrongAnswer();
                          //Mark += 1;

                          OldArray = _service.GenerateArray(4, 4);
                          Array = _service.ResizeArray(OldArray);

                      }
                      else if (Check == 4)
                      {

                          OldArray = _service.Code(OldArray);
                          if (_service.Equals(OldArray, Array))
                              result.CorrectAnswer();
                          else
                              result.WrongAnswer();
                          //Mark += 1;

                          SelectedIndex = 1;
                          Array = _service.GenerateArrayWithException(4, 4);
                          OldArray = _service.FuckingCSharp(Array);

                      }
                      else if (Check <= 8)
                      {
                          OldArray = _service.Decode(OldArray);
                          if (_service.Equals(OldArray, Array))
                              result.CorrectAnswer();
                          else
                              result.WrongAnswer();
                          //Mark += 1;
                          Array = _service.GenerateArrayWithException(4, 4);
                          OldArray = _service.FuckingCSharp(Array);
                      }
                      //Check += 1;
                      if (Check >= 9) 
                      {
                          if (MessageBox.Show("Правильных ответов " + Mark.ToString() + " из 8. Хотите попробовать ещё ? ", "Тест окончен", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                          {
                              result.Reset();
                              OldArray = _service.GenerateArray(4, 4);
                              Array = _service.ResizeArray(OldArray);
                          }
                          //SelectedIndex = 0;
                          //asdfasefasef
                          //Check = 1;
                          //Mark = 0;
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
                          MessageBox.Show("Неправильно!");

                      OldArrayCode = _service.GenerateArray(4, 4);
                      ArrayCode = _service.ResizeArray(OldArrayCode);
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
                          MessageBox.Show("Неправильно!");

                      ArrayDecode = _service.GenerateArrayWithException(4, 4);
                      OldArrayDecode = _service.FuckingCSharp(ArrayDecode);
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
