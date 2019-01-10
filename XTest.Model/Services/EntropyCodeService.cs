using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTest.Model.Services
{
	public class EntropyCodeService
	{


		public int getInformationCount(string[] Ensemble)
		{
			Dictionary<string, double> asamples = new Dictionary<string, double>();
			asamples.Add("X1", 0.5);
			asamples.Add("X2", 0.25);
			asamples.Add("X3", 0.125);
			asamples.Add("X4", 0.125);

			int result = 0;
			for (int i = 0; i < Ensemble.Length; i++)
			{
				if (Ensemble[i] != null)
				{
					result += (int)Math.Log(1 / asamples[Ensemble[i]], 2);
				}
			}
			return result;
		}

		public string[] generateEnsemble()
		{
			string[] arr = new string[9];
			Random r = new Random();
			for (int i = 0; i < r.Next(4, 8); i++)
			{
				arr[i] = "X" + r.Next(1, 5);
			}
			return arr;
		}

		public string[][] generateUnconditional()
		{
			Random r = new Random();
			string[][] result = new string[6][];
			int count = r.Next(3, 7);
			double rest = 1;
			for (int i = 0; i < count; i++)
			{
				result[i] = new string[2];
				result[i][0] = "P(X" + i +") = ";
				
				double d = r.Next(1, (int)(100/count)*2) / 100.0;
				while (rest - d <=0 && rest!=0)
				{
					d = r.Next(100) / 100.0;
				}
				if (i == count-1)
					result[i][1] = Math.Round(rest, 2).ToString();
				else
					result[i][1] = Math.Round(d, 2).ToString();
				rest = rest - d;
			}
			return result;
		}

		public double getHX(string[][] Unconditional)
		{
			double result = 0.0;
			for (int i = 0; i < Unconditional.Length; i++)
			{
				if (Unconditional[i] != null)
				{
					result += Math.Round(double.Parse(Unconditional[i][1]) * Math.Log(double.Parse(Unconditional[i][1]), 2),3);
				}
			}
			return Math.Round( result, 3);
		}

		public double getMaxHX(string[][] Unconditional)
		{
			return Math.Round( Math.Log(Unconditional.Length, 2), 3);
		}

		public string[][] generateConditional()
		{
			Random r = new Random();
			string[][] result = new string[4][];
			for (int k = 0; k < 4; k++)
			{
				result[k] = new string[4];
			}
			for (int i = 0; i < 4; i++)
			{
				double rest = 1;
				for (int j = 0; j < 4; j++)
				{
					double d = r.Next(1, 50) / 100.0;
					while (rest - d <= 0 && rest != 0)
					{
						d = r.Next(100) / 100.0;
					}
					if (j == 3)
						result[j][i] = Math.Round(rest, 2).ToString();
					else
						result[j][i] = Math.Round(d, 2).ToString();
					rest = rest - d;
				}
			}
			return result;
		}

		public double getHYX(string[][] Conditional)
		{
			double result = 0.0;
			for (int i = 0; i < Conditional.Length; i++)
			{
				for (int j = 0; j < Conditional.Length; j++)
				{
					result += Math.Round(double.Parse(Conditional[i][j]) * Math.Log(double.Parse(Conditional[i][j]), 2), 3);
					
				}
			}
			return Math.Round(result, 3);
		}

	}
}
