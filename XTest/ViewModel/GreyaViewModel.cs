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
    public class GreyaViewModel : INotifyPropertyChanged
    {
        private GreyaCodeService codeService = new GreyaCodeService();

        private GreyaCode greyaCodeTest;
        private GreyaCode greyaCodePractice;

        private int selectedIndex;
        private int testNumber;
        private int correctAnsver;
        private TestMode testMode;
        private string testTask;
        private string practiceTask;

        private RelayCommand nextTest;

        public int TestNumber
        {
            get { return testNumber; }
            set
            {
                testNumber = value;
                OnPropertyChanged("TestNumber");
            }
        }

        public int CorrectAnsver
        {
            get { return correctAnsver; }
            set
            {
                correctAnsver = value;
                OnPropertyChanged("CorrectAnsver");
            }
        }

        public string TestTask
        {
            get { return testTask; }
            set
            {
                testTask = value;
                OnPropertyChanged("TestTask");
            }
        }

        public string PracticeTask
        {
            get { return practiceTask; }
            set
            {
                practiceTask = value;
                OnPropertyChanged("PracticeTask");
            }
        }

        public GreyaCode GreyaCodeTest
        {
            get { return greyaCodeTest; }
            set
            {
                greyaCodeTest = value;
                OnPropertyChanged("GreyaCodeTest");
            }
        }

        public GreyaCode GreyaCodePractice
        {
            get { return greyaCodePractice; }
            set
            {
                greyaCodePractice = value;
                OnPropertyChanged("GreyaCodePractice");
            }
        }

        public int GreyaSelectedTabIndex
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                TestNumber = 1;
                CorrectAnsver = 0;
                testMode = TestMode.Encoding;
                TestTask = "Закодируйте сообщение";
                OnPropertyChanged("GreyaSelectedTabIndex");
                selectedIndex = value;
            }
        }

        public RelayCommand NextTest
        {
            get
            {
                return nextTest ??
                    (nextTest = new RelayCommand(obj =>
                    {
                        if (testMode == TestMode.Encoding)
                        {
                            string encode = codeService.encode(GreyaCodeTest.Message);
                            CorrectAnsver += GreyaCodeTest.Result.Equals(encode) ? 1 : 0;
                        }
                        else if (testMode == TestMode.Decoding)
                        {
                            string decode = codeService.decode(GreyaCodeTest.Message);
                            CorrectAnsver += GreyaCodeTest.Result.Equals(decode) ? 1 : 0;
                        }
                        GreyaCodeTest.Result = "";
                        if (testNumber >= 5)
                        {
                            testMode = TestMode.Decoding;
                            TestTask = "Декодируйте сообщение";
                            GreyaCodeTest.Message = codeService.generateLine(7);
                        }
                        else
                            GreyaCodeTest.Message = codeService.generateLine(11);
                        TestNumber++;
                        if (testNumber >= 11)
                        {
                            GreyaCodeTest.Message = codeService.generateLine(11);
                            TestNumber = 1;
                            CorrectAnsver = 0;
                            testMode = TestMode.Encoding;
                            TestTask = "Закодируйте сообщение";
                        }
                    }));
            }
        }

                    public GreyaViewModel()
        {
            TestTask = "Закодируйте сообщение";
            PracticeTask = "Закодируйте сообщение";
            TestNumber = 1;
            CorrectAnsver = 0;
            testMode = TestMode.Encoding;
            GreyaCodeTest = new GreyaCode();
            GreyaCodeTest.Message = codeService.generateLine(11);
            GreyaCodePractice = new GreyaCode();
            GreyaCodePractice.Message = codeService.generateLine(11);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
