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
    public class VarshamovCodeViewModelPractice : INotifyPropertyChanged
    {
        Random random = new Random();
        private int n;
        private int d;
        private int b;
        private int countOfColumns;
        private int countOfExceptions;
        private int countOfRaws;
        private int selectedIndex;
        private int countOfDigits;
        private int[][] simpleMatrix;
        private int[][] hMatrix;
        private int[][] oneSimpleMatrix;
        private int[] generateArray;
        private int[] oldArray;
        private string[] syndrom;
        private int[] resultArr;
        private string codeNum;
        private int[][] array;
        private RelayCommand next;
        private int check { get; set; }
        private Varshamov _service { get; set; }

        public int N
        {
            get { return n; }
            set
            {
                n = value;
                OnPropertyChanged("N");
            }
        }

        public string[] Syndrom
        {
            get { return syndrom; }
            set
            {
                syndrom = value;
                OnPropertyChanged("Syndrom");
            }
        }

        public int[][] HMatrix
        {
            get { return hMatrix; }
            set
            {
                hMatrix = value;
                OnPropertyChanged("HMatrix");
            }
        }

        public int[][] OneSimpleMatrix
        {
            get { return oneSimpleMatrix; }
            set
            {
                oneSimpleMatrix = value;
                OnPropertyChanged("OneSimpleMatrix");
            }
        }

        public int[] ResultArr
        {
            get { return resultArr; }
            set
            {
                resultArr = value;
                OnPropertyChanged("ResultArr");
            }
        }

        public string CodeNum
        {
            get { return codeNum; }
            set
            {
                codeNum = value;
                OnPropertyChanged("CodeNum");
            }
        }

        public int B
        {
            get { return b; }
            set
            {
                b = value;
                OnPropertyChanged("B");
            }
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

        public int[][] SimpleArray
        {
            get { return simpleMatrix; }
            set
            {
                simpleMatrix = value;
                OnPropertyChanged("SimpleArray");
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

        public int D
        {
            get { return d; }
            set
            {
                d = value;
                OnPropertyChanged("D");
            }
        }


        public int CountOfColumns
        {
            get { return countOfColumns; }
            set
            {
                countOfColumns = value;
                OnPropertyChanged("CountOfColumns");
            }
        }

        public int CountOfExceptions
        {
            get { return countOfExceptions; }
            set
            {
                countOfExceptions = value;
                OnPropertyChanged("CountOfExceptions");
            }
        }

        public int CountOfRaws
        {
            get { return countOfRaws; }
            set
            {
                countOfRaws = value;
                OnPropertyChanged("CountOfRaws");
            }
        }

        public int CountOfDigits
        {
            get { return countOfDigits; }
            set
            {
                countOfDigits = value;
                OnPropertyChanged("CountOfDigits");
            }
        }

        public VarshamovCodeViewModelPractice()
        {
            _service = new Varshamov();
            Initializer();
            check = 1;
        }

        private void Initializer()
        {

   
            N = random.Next(8, 20);
            D = random.Next(3, 8);
            B = 3;
            SelectedIndex = 0;
            SimpleArray = _service.Array(6, 6);
            Array = _service.Array(6, 4);
            generateArray = _service.GenerateArray(6);
            CodeNum = _service.SimpleArrToString(generateArray);
            ResultArr = new int[10];
            OneSimpleMatrix = _service.Array(4, 4);
            HMatrix = _service.Array(4, 6);
        }

        public RelayCommand Next
        {
            get
            {
                return next ??
                  (next = new RelayCommand(obj =>
                  {
                      switch (check)
                      {
                          case 1:
                              var columns = _service.CountOfColumns(n, d);
                              var r = _service.CountOfTestDigits(columns);
                              if (countOfColumns == columns && countOfExceptions == _service.CountOfFixedExceptions(d)
                              && countOfRaws == _service.CountOfRaws(n, r) && countOfDigits == r)
                                  MessageBox.Show("yes");
                              else
                                  MessageBox.Show("no");
                              SelectedIndex = 1;
                              break;
                          case 2:
                              if (_service.Equals(_service.GenerateSimpleMatrix(6), SimpleArray) && _service.CheckProdMatrix(B, Array))
                                  MessageBox.Show("yes");
                              else
                                  MessageBox.Show("no");
                              SelectedIndex = 2;
                              Array = _service.GenerateArr(3);
                              SimpleArray = _service.GenerateSimpleMatrix(6);
                              break;
                          case 3:
                              if (_service.Equals(_service.CodeNum(Array, generateArray), ResultArr))
                                  MessageBox.Show("yes");
                              else
                                  MessageBox.Show("no");
                              SelectedIndex = 3;
                              Array = _service.GenerateArr(3);
                              SimpleArray = _service.GenerateSimpleMatrix(6);
                              break;
                          case 4:
                              if (_service.Equals(_service.GenerateSimpleMatrix(4), OneSimpleMatrix) &&
                              _service.Equals(_service.HMAtrix(Array), HMatrix))
                                  MessageBox.Show("yes");
                              else
                                  MessageBox.Show("no");
                              SelectedIndex = 4;
                              Array = _service.HMAtrix(_service.GenerateArr(3));
                              SimpleArray = _service.GenerateSimpleMatrix(4);
                              ResultArr = _service.GenerateArray(10);
                              oldArray = _service.FuckenCSharp(ResultArr);
                              Syndrom = _service.Syndrom(SimpleArray, Array);
                              ResultArr[random.Next(0, 10)] = (ResultArr[random.Next(0, 10)] + 1) % 2;
                              break;
                          case 5:
                              if (_service.Equals(oldArray, ResultArr))
                                  MessageBox.Show("yes");
                              else
                                  MessageBox.Show("no");
                              check = 0;
                              Initializer();
                              break;
                      }
                      check += 1;
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
