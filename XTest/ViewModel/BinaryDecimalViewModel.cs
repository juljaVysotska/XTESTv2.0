using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using XTest.Model.Models;
using XTest.Model.Services;

namespace XTest.ViewModel
{
    class BinaryDecimalViewModel : INotifyPropertyChanged
    {
        private BinaryDecimalCodeService codeService = new BinaryDecimalCodeService();

        private BinaryDecimalCode binaryDecimalCodeTest;
        private BinaryDecimalCode binaryDecimalCodePractice;

        private int selectedIndex;
        private int testNumber;
        private int correctAnsver;
        private TestMode testMode;
        private string testTask;
        private string practiceTask;

        private RelayCommand nextTest;

        public int TestNumberBinaryDecimal
        {
            get { return testNumber; }
            set
            {
                testNumber = value;
                OnPropertyChanged("TestNumberBinaryDecimal");
            }
        }

        public int CorrectAnsverBinaryDecimal
        {
            get { return correctAnsver; }
            set
            {
                correctAnsver = value;
                OnPropertyChanged("CorrectAnsverBinaryDecimal");
            }
        }

        public string TestTaskBinaryDecimal
        {
            get { return testTask; }
            set
            {
                testTask = value;
                OnPropertyChanged("TestTaskBinaryDecimal");
            }
        }

        public string PracticeTaskBinaryDecimal
        {
            get { return practiceTask; }
            set
            {
                practiceTask = value;
                OnPropertyChanged("PracticeTaskBinaryDecimal");
            }
        }

        public BinaryDecimalCode BinaryDecimalCodeTest
        {
            get { return binaryDecimalCodeTest; }
            set
            {
                binaryDecimalCodeTest = value;
                OnPropertyChanged("BinaryDecimalCodeTest");
            }
        }

        public BinaryDecimalCode BinaryDecimalCodePractice
        {
            get { return binaryDecimalCodePractice; }
            set
            {
                binaryDecimalCodePractice = value;
                OnPropertyChanged("BinaryDecimalCodePractice");
            }
        }

        public int BinaryDecimalSelectedTabIndex
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                TestNumberBinaryDecimal = 1;
                CorrectAnsverBinaryDecimal = 0;
                testMode = TestMode.Encoding;
                TestTaskBinaryDecimal = "Закодируйте сообщение";
                OnPropertyChanged("BinaryDecimalSelectedTabIndex");
                selectedIndex = value;
            }
        }

        public RelayCommand NextBinaryDecimalTest
		{
            get
            {
                return nextTest ??
                    (nextTest = new RelayCommand(obj =>
                    {
                        Random random = new Random();
                        if (testMode == TestMode.Encoding)
                        {
                            string encode = codeService.enncodeNumber(Convert.ToInt32(BinaryDecimalCodeTest.Message), BinaryDecimalCodeTest.Code);
                            CorrectAnsverBinaryDecimal += BinaryDecimalCodeTest.Result.Equals(encode) ? 1 : 0;
                        }
                        else if (testMode == TestMode.Decoding)
                        {
                            string decode = Convert.ToString(codeService.decodeNumber(BinaryDecimalCodeTest.Message, BinaryDecimalCodeTest.Code));
                            CorrectAnsverBinaryDecimal += BinaryDecimalCodeTest.Result.Equals(decode) ? 1 : 0;
                        }
                        BinaryDecimalCodeTest.Result = "";
						if (testNumber >= 5)
						{
							testMode = TestMode.Decoding;
							TestTaskBinaryDecimal = "Декодируйте сообщение";
							BinaryDecimalCodeTest.Message = codeService.enncodeNumber(random.Next(100, 10000), BinaryDecimalCodeTest.Code);
							BinaryDecimalCodeTest.Code = codeService.generateCode();
						}
						else
						{
							BinaryDecimalCodeTest.Message = random.Next(100, 10000).ToString();
							BinaryDecimalCodeTest.Code = codeService.generateCode();
						}
						TestNumberBinaryDecimal++;
                        if (testNumber >= 11)
                        {
                            BinaryDecimalCodeTest.Message = codeService.enncodeNumber(random.Next(100, 10000), BinaryDecimalCodeTest.Code);
                            TestNumberBinaryDecimal = 1;
                            CorrectAnsverBinaryDecimal = 0;
                            testMode = TestMode.Encoding;
                            TestTaskBinaryDecimal = "Закодируйте сообщение";
                        }
                    }));
            }
        }

        public BinaryDecimalViewModel()
        {
            Random random = new Random();
            TestTaskBinaryDecimal = "Закодируйте сообщение";
            PracticeTaskBinaryDecimal = "Закодируйте сообщение";
            TestNumberBinaryDecimal = 1;
            CorrectAnsverBinaryDecimal = 0;
            testMode = TestMode.Encoding;
            BinaryDecimalCodeTest = new BinaryDecimalCode();
            BinaryDecimalCodeTest.Code = codeService.generateCode();
			int r1 = random.Next(100, 10000);
            BinaryDecimalCodeTest.Message = r1.ToString();
            BinaryDecimalCodePractice = new BinaryDecimalCode();
            BinaryDecimalCodePractice.Code = codeService.generateCode();
			int r2 = random.Next(100, 10000);
			BinaryDecimalCodePractice.Message = r2.ToString();
		}

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
