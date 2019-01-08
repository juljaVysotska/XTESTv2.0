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
    class EntropyViewModel : INotifyPropertyChanged
	{
		private EntropyCodeService codeService = new EntropyCodeService();

		private string[] ensembleTest;
		private string[][] unconditionalTest;
		private string[] ensemblePractice;

		private string ensembleResult;
		private string unconditionalResultHX;
		private string unconditionalResultHMaxX;


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

		public string UnconditionalResultHX
		{
			get { return unconditionalResultHX; }
			set
			{
				unconditionalResultHX = value;
				OnPropertyChanged("UnconditionalResultHX");
			}
		}

		public string UnconditionalResultHMaxX
		{
			get { return unconditionalResultHMaxX; }
			set
			{
				unconditionalResultHMaxX = value;
				OnPropertyChanged("UnconditionalResultHMaxX");
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
							double encode = codeService.getHX(UnconditionalTest);
							double encodeMax = codeService.getMaxHX(UnconditionalTest);
							double result;
							double resultMax;
							double.TryParse(UnconditionalResultHX, out result);
							double.TryParse(UnconditionalResultHMaxX, out resultMax);
							CorrectAnsver += (encode == result && encodeMax == resultMax) ? 1 : 0;
						}
						else if (testMode == TestMode.Default)
						{

						}
						EnsembleResult = "";
						UnconditionalResultHX = "";
						UnconditionalResultHMaxX = "";
						if (testNumber >= 3 && testNumber < 5)
						{
							testMode = TestMode.Decoding;
							EnsambleVisibility = false;
							UnconditionalVisibility = true;
						}
						else
							EnsembleTest = codeService.generateEnsemble();
						if (testNumber >= 5)
						{
							testMode = TestMode.Default;
							UnconditionalVisibility = false;
							ConditionalVisibility = true;

						}
						else
							UnconditionalTest = codeService.generateUnconditional();
						//else
						//	GreyaCodeTest.Message = codeService.generateLine(11);
						TestNumber++;
						if (testNumber >= 7)
						{
							TestNumber = 1;
							MessageBox.Show("Правильных ответов " + CorrectAnsver.ToString() + " из 6");
							if (!MainWindow.results.ContainsKey(TestType.Entropy))
							{
								MainWindow.results.Add(TestType.Entropy, new Result("Энтропия", 10));
							}
							MainWindow.results[TestType.Entropy].correctTests = correctAnsver;
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
