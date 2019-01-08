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
    class EntropyViewModel : INotifyPropertyChanged
	{
		private EntropyCodeService codeService = new EntropyCodeService();

		private string[] ensembleTest;
		private string[][] unconditionalTest;
		private string ensembleResult;
		private string[] ensemblePractice;

		private int selectedIndex;
		private int testNumber;
		private int correctAnsver;

		private bool ensambleVisibility;
		private bool unconditionalVisibility;
		private bool conditionalVisibility;

		private TestMode testMode;
		private TestMode practiceMode;

		private RelayCommand nextTest;
		private RelayCommand setEncoding;
		private RelayCommand setDecoding;
		private RelayCommand checkPractice;

		public bool EnsambleVisibility
		{
			get { return ensambleVisibility; }
			set
			{
				ensambleVisibility = value;
				OnPropertyChanged("EnsambleVisibility");
			}
		}

		public bool UnconditionalVisibility
		{
			get { return unconditionalVisibility; }
			set
			{
				unconditionalVisibility = value;
				OnPropertyChanged("UnconditionalVisibility");
			}
		}

		public bool ConditionalVisibility
		{
			get { return conditionalVisibility; }
			set
			{
				conditionalVisibility = value;
				OnPropertyChanged("ConditionalVisibility");
			}
		}

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

		public int EntropySelectedTabIndex
		{
			get
			{
				return selectedIndex;
			}
			set
			{
				//TestNumber = 1;
				//CorrectAnsver = 0;
				//testMode = TestMode.Encoding;
				//TestTask = "Закодируйте сообщение";
				OnPropertyChanged("GreyaSelectedTabIndex");
				//GreyaCodeTest.Message = codeService.generateLine(11);
				selectedIndex = value;
			}
		}

		public string[] EnsembleTest
		{
			get { return ensembleTest; }
			set
			{
				ensembleTest = value;
				OnPropertyChanged("EnsembleTest");
			}
		}

		public string[][] UnconditionalTest
		{
			get { return unconditionalTest; }
			set
			{
				unconditionalTest = value;
				OnPropertyChanged("UnconditionalTest");
			}
		}

		public string EnsembleResult
		{
			get { return ensembleResult; }
			set
			{
				ensembleResult = value;
				OnPropertyChanged("EnsembleResult");
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
							int encode = codeService.getInformationCount(EnsembleTest);
							int result;
							int.TryParse(EnsembleResult, out result);
							CorrectAnsver += result == encode ? 1 : 0;
						}
						else if (testMode == TestMode.Decoding)
						{
							//	string decode = codeService.decode(GreyaCodeTest.Message);
							//	CorrectAnsver += GreyaCodeTest.Result.Equals(decode) ? 1 : 0;
						}
						EnsembleResult = "";
						if (testNumber >= 3)
						{
							testMode = TestMode.Decoding;
							EnsambleVisibility = false;
							UnconditionalVisibility = true;
						}
						if (testNumber >= 5)
						{
							testMode = TestMode.Decoding;
							UnconditionalVisibility = false;
							ConditionalVisibility = true;

						}
						//else
						//	GreyaCodeTest.Message = codeService.generateLine(11);
						TestNumber++;
						if (testNumber >= 7)
						{
							MessageBox.Show("Правильных ответов " + CorrectAnsver.ToString() + " из 6");
							if (!MainWindow.results.ContainsKey("Entropy"))
							{
								MainWindow.results.Add("Entropy", new Result("Энтропия", 10));
							}
							MainWindow.results["Entropy"].correctTests = correctAnsver;
							TestNumber = 1;
							CorrectAnsver = 0;
							EnsambleVisibility = true;
							UnconditionalVisibility = false;
							ConditionalVisibility = false;
							testMode = TestMode.Encoding;
						}
					}));
			}
		}

		public EntropyViewModel()
		{
			EnsambleVisibility = true;
			UnconditionalVisibility = false;
			ConditionalVisibility = false;
			EnsembleTest = codeService.generateEnsemble();
			UnconditionalTest = codeService.generateUnconditional();
			TestNumber = 1;
			CorrectAnsver = 0;
			testMode = TestMode.Encoding;
			practiceMode = TestMode.Encoding;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
