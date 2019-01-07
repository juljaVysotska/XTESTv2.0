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
    public class GreyaViewModel : INotifyPropertyChanged
    {
        private GreyaCodeService codeService = new GreyaCodeService();

        private GreyaCode greyaCodeTest;
        private GreyaCode greyaCodePractice;

        private int selectedIndex;
        private int testNumber;
        private int correctAnsver;
        private TestMode testMode;
        private TestMode practiceMode;
        private string testTask;
        private string practiceTask;

        private RelayCommand nextTest;
        private RelayCommand setEncoding;
        private RelayCommand setDecoding;
        private RelayCommand checkPractice;

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
				GreyaCodeTest.Message = codeService.generateLine(11);
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
							MessageBox.Show("Правильных ответов " + CorrectAnsver.ToString() + " из 10");
							if (!MainWindow.results.ContainsKey("Код Грея"))
							{
								MainWindow.results.Add("Код Грея", new Result("Greya", 10));
							}
							MainWindow.results["Код Грея"].correctTests = correctAnsver;
							GreyaCodeTest.Message = codeService.generateLine(11);
                            TestNumber = 1;
                            CorrectAnsver = 0;
                            testMode = TestMode.Encoding;
                            TestTask = "Закодируйте сообщение";
                        }
                    }));
            }
        }

		public RelayCommand SetEncoding
		{
			get
			{
				return setEncoding ??
					(setEncoding = new RelayCommand(obj =>
					{
						practiceMode = TestMode.Encoding;
						PracticeTask = "Закодируйте сообщение";
						GreyaCodePractice.Message = codeService.generateLine(11);
					}));
			}
		}

		public RelayCommand SetDecoding
		{
			get
			{
				return setDecoding ??
					(setDecoding = new RelayCommand(obj =>
					{
						practiceMode = TestMode.Decoding;
						PracticeTask = "Декодируйте сообщение";
						GreyaCodePractice.Message = codeService.generateLine(7);
					}));
			}
		}

		public RelayCommand CheckPractice
		{
			get
			{
				return checkPractice ??
					(checkPractice = new RelayCommand(obj =>
					{
						string ansver;
						if (practiceMode == TestMode.Encoding)
						{
							string encode = codeService.encode(GreyaCodePractice.Message);
							ansver = GreyaCodePractice.Result.Equals(encode) ? "Правильно!" : "Неправильно!";
							MessageBox.Show(ansver);
						}
						else if (practiceMode == TestMode.Decoding)
						{
							string decode = codeService.decode(GreyaCodePractice.Message);
							ansver = GreyaCodePractice.Result.Equals(decode) ? "Правильно!" : "Неправильно!";
							MessageBox.Show(ansver);
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
