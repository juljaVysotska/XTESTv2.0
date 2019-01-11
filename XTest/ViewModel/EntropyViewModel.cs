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
using static XTest.Model.Models.Result;

namespace XTest.ViewModel
{
    class EntropyViewModel : INotifyPropertyChanged
	{
		private EntropyCodeService codeService = new EntropyCodeService();

		private string[] ensembleTest;
		private string[][] unconditionalTest;
		private string[][] conditionalTest;
		private string[] ensemblePractice;
		private string[][] unconditionalPractice;
		private string[][] conditionalPractice;

		private string ensembleResult;
		private string ensembleResultPractice;
		private string unconditionalResultHX;
		private string unconditionalResultHXPractice;
		private string unconditionalResultHMaxX;
		private string unconditionalResultHMaxXPractice;
		private string conditionalResultHYX;
		private string conditionalResultHYXPractice;


		private int selectedIndex;
		private int testNumber;
		private int correctAnsver;

		private bool ensambleVisibility;
		private bool unconditionalVisibility;
		private bool conditionalVisibility;
		private bool ensambleVisibilityPractice;
		private bool unconditionalVisibilityPractice;
		private bool conditionalVisibilityPractice;

		private TestMode testMode;
		private TestMode practiceMode;

		private RelayCommand nextTest;
		private RelayCommand setEnsemble;
		private RelayCommand setUnconditional;
		private RelayCommand setConditional;
		private RelayCommand checkPractice;

		public Result result
		{
			get
			{
				Result res;
				if (!results.ContainsKey(TestType.Entropy))
				{
					res = new Result("Энтропия", 6);
					results.Add(TestType.Entropy, res);
				}
				else
				{
					res = results[TestType.Entropy];
				}
				return res;
			}
			private set { }
		}

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

		public bool EnsambleVisibilityPractice
		{
			get { return ensambleVisibilityPractice; }
			set
			{
				ensambleVisibilityPractice = value;
				OnPropertyChanged("EnsambleVisibilityPractice");
			}
		}	

		public bool UnconditionalVisibilityPractice
		{
			get { return unconditionalVisibilityPractice; }
			set
			{
				unconditionalVisibilityPractice = value;
				OnPropertyChanged("UnconditionalVisibilityPractice");
			}
		}

		public bool ConditionalVisibilityPractice
		{
			get { return conditionalVisibilityPractice; }
			set
			{
				conditionalVisibilityPractice = value;
				OnPropertyChanged("ConditionalVisibilityPractice");
			}
		}

		public int TestNumber
		{
			get { return result.currentTestNumber; }
			set
			{
				
			}
		}

		public int CorrectAnsver
		{
			get { return result.correctTests; }
			set
			{

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

				EnsembleTest = codeService.generateEnsemble();
				UnconditionalTest = codeService.generateUnconditional();
				ConditionalTest = codeService.generateConditional();
				OnPropertyChanged("GreyaSelectedTabIndex");
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

		public string[] EnsemblePractice
		{
			get { return ensemblePractice; }
			set
			{
				ensemblePractice = value;
				OnPropertyChanged("EnsemblePractice");
			}
		}

		public string[][] UnconditionalPractice
		{
			get { return unconditionalPractice; }
			set
			{
				unconditionalPractice = value;
				OnPropertyChanged("UnconditionalPractice");
			}
		}

		public string[][] ConditionalPractice
		{
			get { return conditionalPractice; }
			set
			{
				conditionalPractice = value;
				OnPropertyChanged("ConditionalPractice");
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

		public string[][] ConditionalTest
		{
			get { return conditionalTest; }
			set
			{
				conditionalTest = value;
				OnPropertyChanged("ConditionalTest");
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

		public string EnsembleResultPractice
		{
			get { return ensembleResultPractice; }
			set
			{
				ensembleResultPractice = value;
				OnPropertyChanged("EnsembleResultPractice");
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

		public string UnconditionalResultHXPractice
		{
			get { return unconditionalResultHXPractice; }
			set
			{
				unconditionalResultHXPractice = value;
				OnPropertyChanged("UnconditionalResultHXPractice");
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

		public string UnconditionalResultHMaxXPractice
		{
			get { return unconditionalResultHMaxXPractice; }
			set
			{
				unconditionalResultHMaxXPractice = value;
				OnPropertyChanged("UnconditionalResultHMaxXPractice");
			}
		}

		public string ConditionalResultHYX
		{
			get { return conditionalResultHYX; }
			set
			{
				conditionalResultHYX = value;
				OnPropertyChanged("ConditionalResultHYX");
			}
		}

		public string ConditionalResultHYXPractice
		{
			get { return conditionalResultHYXPractice; }
			set
			{
				conditionalResultHYXPractice = value;
				OnPropertyChanged("ConditionalResultHYXPractice");
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
							int resultt;
							int.TryParse(EnsembleResult, out resultt);
							if (resultt == encode)
								result.CorrectAnswer();
							else
								result.WrongAnswer();
							//CorrectAnsver += result == encode ? 1 : 0;
						}
						else if (testMode == TestMode.Decoding)
						{
							double encode = codeService.getHX(UnconditionalTest);
							double encodeMax = codeService.getMaxHX(UnconditionalTest);
							double resultt;
							double resultMax;
							double.TryParse(UnconditionalResultHX, out resultt);
							double.TryParse(UnconditionalResultHMaxX, out resultMax);
							if (resultt == encode && encodeMax == resultMax)
								result.CorrectAnswer();
							else
								result.WrongAnswer();
							//CorrectAnsver += (encode == result && encodeMax == resultMax) ? 1 : 0;
						}
						else if (testMode == TestMode.Default)
						{
							double encode = codeService.getHYX(ConditionalTest);
							double resultt;
							double.TryParse(ConditionalResultHYX, out resultt);
							if (encode == resultt)
								result.CorrectAnswer();
							else
								result.WrongAnswer();
							//CorrectAnsver += encode == result ? 1 : 0;
						}
						EnsembleResult = "";
						UnconditionalResultHX = "";
						UnconditionalResultHMaxX = "";
						if (TestNumber >= 4 && TestNumber < 6)
						{
							testMode = TestMode.Decoding;
							EnsambleVisibility = false;
							UnconditionalVisibility = true;
						}
						else
							EnsembleTest = codeService.generateEnsemble();
						if (TestNumber >= 6)
						{
							testMode = TestMode.Default;
							UnconditionalVisibility = false;
							ConditionalVisibility = true;

						}
						else
							UnconditionalTest = codeService.generateUnconditional();
						//else
						//	GreyaCodeTest.Message = codeService.generateLine(11);
						if (TestNumber >= 7)
						{
							//TestNumber = 1;
							if (MessageBox.Show("Правильных ответов " + result.correctTests + " из " + result.testsTotal + ". Хотите попробовать ещё ? ", "Тест окончен", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
							{
								result.Reset();
								EnsambleVisibility = true;
								UnconditionalVisibility = false;
								ConditionalVisibility = false;
								testMode = TestMode.Encoding;
								EnsembleTest = codeService.generateEnsemble();
								UnconditionalTest = codeService.generateUnconditional();
								ConditionalTest = codeService.generateConditional();
							}
							//	MessageBox.Show("Правильных ответов " + CorrectAnsver.ToString() + " из 6");
							//if (!MainWindow.results.ContainsKey(TestType.Entropy))
							//{
							//	MainWindow.results.Add(TestType.Entropy, new Result("Энтропия", 10));
							//}
							//MainWindow.results[TestType.Entropy].correctTests = correctAnsver;
							//CorrectAnsver = 0;
							//EnsambleVisibility = true;
							//UnconditionalVisibility = false;
							//ConditionalVisibility = false;
							//testMode = TestMode.Encoding;
						}
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
							int encode = codeService.getInformationCount(EnsemblePractice);
							int result;
							int.TryParse(EnsembleResultPractice, out result);
							ansver = result == encode ? "Правильно!" : "Неправильно!";
							MessageBox.Show(ansver);
						}
						else if (practiceMode == TestMode.Decoding)
						{
							double encode = codeService.getHX(UnconditionalPractice);
							double encodeMax = codeService.getMaxHX(UnconditionalPractice);
							double result;
							double resultMax;
							double.TryParse(unconditionalResultHXPractice, out result);
							double.TryParse(UnconditionalResultHMaxXPractice, out resultMax);
							ansver = (encode == result && encodeMax == resultMax) ? "Правильно!" : "Неправильно!";
							MessageBox.Show(ansver);
						}
						else if (practiceMode == TestMode.Default)
						{
							double encode = codeService.getHYX(ConditionalPractice);
							double result;
							double.TryParse(ConditionalResultHYXPractice, out result);
							ansver = encode == result ? "Правильно!" : "Неправильно!";
							MessageBox.Show(ansver);
						}
						EnsembleResult = "";
						UnconditionalResultHX = "";
						UnconditionalResultHMaxX = "";
						
					}));
			}
		}

		public RelayCommand SetEnsemble
		{
			get
			{
				return setEnsemble ??
					(setEnsemble = new RelayCommand(obj =>
					{
						EnsambleVisibilityPractice = true;
						UnconditionalVisibilityPractice = false;
						ConditionalVisibilityPractice = false;
						practiceMode = TestMode.Encoding;
						EnsemblePractice = codeService.generateEnsemble();
					}));
			}
		}

		public RelayCommand SetUnconditional
		{
			get
			{
				return setUnconditional ??
					(setUnconditional = new RelayCommand(obj =>
					{
						EnsambleVisibilityPractice = false;
						UnconditionalVisibilityPractice = true;
						ConditionalVisibilityPractice = false;
						practiceMode = TestMode.Decoding;
						UnconditionalPractice = codeService.generateUnconditional();
					}));
			}
		}

		public RelayCommand SetConditional
		{
			get
			{
				return setConditional ??
					(setConditional = new RelayCommand(obj =>
					{
						EnsambleVisibilityPractice = false;
						UnconditionalVisibilityPractice = false;
						ConditionalVisibilityPractice = true;
						practiceMode = TestMode.Default;
						ConditionalPractice = codeService.generateConditional();
					}));
			}
		}

		public EntropyViewModel()
		{
			EnsambleVisibility = true;
			UnconditionalVisibility = false;
			ConditionalVisibility = false;
			EnsambleVisibilityPractice = true;
			UnconditionalVisibilityPractice = false;
			ConditionalVisibilityPractice = false;
			EnsembleTest = codeService.generateEnsemble();
			EnsemblePractice = codeService.generateEnsemble();
			UnconditionalTest = codeService.generateUnconditional();
			UnconditionalPractice = codeService.generateUnconditional();
			ConditionalTest = codeService.generateConditional();
			ConditionalPractice = codeService.generateConditional();
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
