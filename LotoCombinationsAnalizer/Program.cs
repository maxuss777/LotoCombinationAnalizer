using System;
using System.Collections.Generic;
using LotoCombinationsAnalizer.Objects;
using ServiceStack;

namespace LotoCombinationsAnalizer
{
	public class Program
	{
		private static List<int> _array = new List<int>
		{
			1,4,5,6,7,8,9,10,11,12,13,14,15,16,3,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,2
		};

		static void Main()
		{
            AnaliseAndFindBestWinners();
		}

		public static List<Winner> GetWinnersFromHistory()
		{
			StatisticHolder statisticHolder = new StatisticHolder();

			return statisticHolder.GetResult();
		}

		public static List<Winner> FindWinners(int attempts)
		{
			Analizer analizer = new Analizer();

			return analizer.FindTheBestWinners(attempts);
		}

		public static List<List<int>> GetHisoryResult()
		{
			ArraysHolder arraysHolder = new ArraysHolder();

			return arraysHolder.GetHistoryResult();
		}

		public static void WriteResultToLocalFileAsJson(List<Winner> winners)
		{
			ResultWriter resultWriter = new ResultWriter();

			resultWriter.WriteToLocalFileAsJsonObject(winners);
		}

		public static void WriteFilteredResultToLocalFile(List<Winner> winners)
		{
			ResultWriter resultWriter = new ResultWriter();

			resultWriter.WriteFilteredResultToLocalFile(winners);
		}

		public static void WriteResultToLocalFileAsText(List<Winner> winners)
		{
			ResultWriter resultWriter = new ResultWriter();

			resultWriter.WriteToLocalFileAsStringBlock(winners);
		}

		public static void AnaliseAndFindBestWinners()
		{
			var winners = FindWinners(50);
			WriteResultToLocalFileAsJson(winners);

			var bestWinnerWithFourMatched = new Winner();
			var bestWinnerWithFiveMatched = new Winner();
			var bestWinnerWithSixMatched = new Winner();

			foreach (var winner in winners)
			{
				foreach (var winnersCollection in winner.collectionsList)
				{
					if (winnersCollection.FourMatchedNumber.Count > bestWinnerWithFourMatched.collectionsList.Count)
						bestWinnerWithFourMatched = winner;

					if (winnersCollection.FiveMatchedNumber.Count > bestWinnerWithFiveMatched.collectionsList.Count)
						bestWinnerWithFiveMatched = winner;

					if (winnersCollection.SixMatchedNumber.Count > bestWinnerWithSixMatched.collectionsList.Count)
						bestWinnerWithSixMatched = winner;
				}
			}

			var filteredWinnersList = new List<Winner>();
			filteredWinnersList.Add(bestWinnerWithFourMatched);
			filteredWinnersList.Add(bestWinnerWithFiveMatched);
			filteredWinnersList.Add(bestWinnerWithSixMatched);

			WriteFilteredResultToLocalFile(filteredWinnersList);
		}
	}
}
