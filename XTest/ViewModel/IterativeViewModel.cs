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
	class IterativeViewModel : INotifyPropertyChanged
	{
		private IterativeCodeService codeService = new IterativeCodeService();
		private string[][] array;
		private int selectedIndex;
		private int testNumber;
		private int correctAnsver;
		private TestMode testMode;
		private TestMode practiceMode;
		private string testTask;
		private string practiceTask;
		private bool mainArrReadOnlyTest;
		private bool additionalArrReadOnlyTest;
		private bool mainArrReadOnlyPractice;
		private bool additionalArrReadOnlyPractice;

		private IterativeCode iterativeCodeTest;
		private IterativeCode iterativeCodePractice;

		private RelayCommand nextTest;
		private RelayCommand setEncoding;
		private RelayCommand setDecoding;
		private RelayCommand checkPractice;

		public bool MainArrReadOnlyPractice
		{
			get { return mainArrReadOnlyPractice; }
			set
			{
				mainArrReadOnlyPractice = value;
				OnPropertyChanged("MainArrReadOnlyPractice");
			}
		}

		public bool AdditionalArrReadOnlyPractice
		{
			get { return additionalArrReadOnlyPractice; }
			set
			{
				additionalArrReadOnlyPractice = value;
				OnPropertyChanged("AdditionalArrReadOnlyPractice");
			}
		}


		public bool MainArrReadOnlyTest
		{
			get { return mainArrReadOnlyTest; }
			set
			{
				mainArrReadOnlyTest = value;
				OnPropertyChanged("MainArrReadOnlyTest");
			}
		}

		public bool AdditionalArrReadOnlyTest
		{
			get { return additionalArrReadOnlyTest; }
			set
			{
				additionalArrReadOnlyTest = value;
				OnPropertyChanged("AdditionalArrReadOnlyTest");
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

		public IterativeCode IterativeCodeTest
		{
			get { return iterativeCodeTest; }
			set
			{
				iterativeCodeTest = value;
				OnPropertyChanged("IterativeCodeTest");
			}
		}

		public IterativeCode IterativeCodePractice
		{
			get { return iterativeCodePractice; }
			set
			{
				iterativeCodePractice = value;
				OnPropertyChanged("IterativeCodePractice");
			}
		}

		public int IterativeSelectedTabIndex
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
				IterativeCodeTest.Q = codeService.getRandomQ();
				IterativeCodeTest.IntArray = codeService.GenerateArray();
				IterativeCodeTest.ArrayCode = setArray(IterativeCodeTest.IntArray);
				MainArrReadOnlyTest = true;
				AdditionalArrReadOnlyTest = false;
				selectedIndex = value;
				OnPropertyChanged("IterativeSelectedTabIndex");
			}
		}

		public RelayCommand NextTest
		{
			get
			{
				return nextTest ??
					(nextTest = new RelayCommand(obj =>
					{
						int[][] codearr = new int[6][];
						for (int i = 0; i < 6; i++)
						{
							codearr[i] = new int[6];
							for (int j = 0; j < 6; j++)
							{
								int.TryParse(IterativeCodeTest.ArrayCode[i][j], out codearr[i][j]);
							}
						}
						if (testMode == TestMode.Encoding)
						{
							int[][] encode = codeService.encodeArr(IterativeCodeTest.IntArray, IterativeCodeTest.Q);
							CorrectAnsver += equalsArr(codearr, encode) ? 1 : 0;
						}
						else if (testMode == TestMode.Decoding)
						{
							//string decode = codeService.decode(GreyaCodeTest.Message);
							CorrectAnsver += equalsArr(codearr, codeService.encodeArr(IterativeCodeTest.IntArray, IterativeCodeTest.Q)) ? 1 : 0;
						}
						if (testNumber >= 5)
						{
							testMode = TestMode.Decoding;
							TestTask = "Исправить ошибки";
							IterativeCodeTest.Q = codeService.getRandomQ();
							IterativeCodeTest.IntArray = codeService.GenerateArray();
							IterativeCodeTest.ArrayCode = codeService.makeOneMistake(codeService.encodeArr(IterativeCodeTest.IntArray, IterativeCodeTest.Q));
							MainArrReadOnlyTest = false;
							AdditionalArrReadOnlyTest = true;
						}
						else
						{
							IterativeCodeTest.Q = codeService.getRandomQ();
							IterativeCodeTest.IntArray = codeService.GenerateArray();
							IterativeCodeTest.ArrayCode = setArray(IterativeCodeTest.IntArray);
						}
						TestNumber++;
						if (testNumber >= 11)
						{
							IterativeCodeTest.IntArray = codeService.GenerateArray();
							IterativeCodeTest.ArrayCode = setArray(IterativeCodeTest.IntArray);
							TestNumber = 1;
							MessageBox.Show("Правильных ответов " + CorrectAnsver.ToString() + " из 10");
							CorrectAnsver = 0;
							testMode = TestMode.Encoding;
							TestTask = "Закодируйте сообщение";
							MainArrReadOnlyTest = true;
							AdditionalArrReadOnlyTest = false;
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
						IterativeCodePractice.Q = codeService.getRandomQ();
						IterativeCodePractice.IntArray = codeService.GenerateArray();
						IterativeCodePractice.ArrayCode = setArray(IterativeCodePractice.IntArray);
						MainArrReadOnlyPractice = true;
						AdditionalArrReadOnlyPractice = false;
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
						PracticeTask = "Исправить ошибки";
						IterativeCodePractice.Q = codeService.getRandomQ();
						IterativeCodePractice.IntArray = codeService.GenerateArray();
						IterativeCodePractice.ArrayCode = codeService.makeOneMistake(codeService.encodeArr(IterativeCodePractice.IntArray, IterativeCodePractice.Q));
						MainArrReadOnlyPractice = false;
						AdditionalArrReadOnlyPractice = true;
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
						int[][] codearr = new int[6][];
						for (int i = 0; i < 6; i++)
						{
							codearr[i] = new int[6];
							for (int j = 0; j < 6; j++)
							{
								int.TryParse(IterativeCodePractice.ArrayCode[i][j], out codearr[i][j]);
							}
						}
						string ansver;
						if (practiceMode == TestMode.Encoding)
						{
							int[][] encode = codeService.encodeArr(IterativeCodePractice.IntArray, IterativeCodePractice.Q);
							ansver = equalsArr(codearr, encode) ? "Правильно!" : "Неправильно!";
							MessageBox.Show(ansver);
						}
						else if (practiceMode == TestMode.Decoding)
						{
							ansver = equalsArr(codearr, codeService.encodeArr(IterativeCodePractice.IntArray, IterativeCodePractice.Q)) ? "Правильно!" : "Неправильно!";
							MessageBox.Show(ansver);
						}
					}));
			}
		}

		public IterativeViewModel()
		{
			TestTask = "Закодируйте сообщение";
			PracticeTask = "Закодируйте сообщение";
			TestNumber = 1;
			CorrectAnsver = 0;
			testMode = TestMode.Encoding;
			practiceMode = TestMode.Encoding;
			IterativeCodeTest = new IterativeCode();
			IterativeCodePractice = new IterativeCode();
			IterativeCodeTest.IntArray = codeService.GenerateArray();
			IterativeCodeTest.ArrayCode = setArray(IterativeCodeTest.IntArray);
			IterativeCodePractice.IntArray = codeService.GenerateArray();
			IterativeCodePractice.ArrayCode = setArray(IterativeCodePractice.IntArray);
			IterativeCodeTest.Q = codeService.getRandomQ();
			IterativeCodePractice.Q = codeService.getRandomQ();
			MainArrReadOnlyTest = true;
			AdditionalArrReadOnlyTest = false;
			MainArrReadOnlyPractice = true;
			AdditionalArrReadOnlyPractice = false;

		}

		private string[][] setArray(int[][] temp)
		{
			array = new string[6][];
			for (int i = 0; i < 6; i++)
			{
				array[i] = new string[6];
				for (int j = 0; j < 6; j++)
				{
					if (j == 5)
						array[i][j] = "?";
					else if (i == 5)
						array[i][j] = "?";
					else
						array[i][j] = temp[i][j].ToString();
				}
			}
			return array;
		}

		private bool equalsArr(int[][] enteredArr, int[][] encodedArr)
		{
			bool result = true;
			for (int i = 0; i < 6; i++)
			{
				for (int j = 0; j < 6; j++)
				{
					if (enteredArr[i][j] != encodedArr[i][j])
					{
						return false;
					}
				}
			}
			return result;
		}


		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
