using System;
using System.Collections.Generic;
using LotoCombinationsAnalizer.Objects;
using ServiceStack;

namespace LotoCombinationsAnalizer
{
	public class Analizer
	{
		private Generator _generator;
		private List<List<int>> _historicalWonNumbers;
		private List<List<int>> _uniqArraysList;

		public Analizer()
		{
			ArraysHolder arrayHolder = new ArraysHolder();
			_generator = new Generator();
			_uniqArraysList = arrayHolder.GetMaximumPassibleArrays();
			_historicalWonNumbers = arrayHolder.GetHistoryResult();
		}

		public List<Winner> FindTheBestWinners(int attempts)
		{
			List<Winner> winners = new List<Winner>();

			for (int i = 0; i < _uniqArraysList.Count; i++)
			{
				var currentArray = _uniqArraysList[i];
				var winner = new Winner();

				for (int j = 0; j < attempts; j++)
				{
					var combination = _generator.GetNewCombination(currentArray);
					var collections = FindWinnerCollections(combination);

					if (collections != null)
					{
						collections.wonCombination = combination;
						winner.collectionsList.Add(collections);
						winner.WinArray = currentArray;
					}

					Console.Clear();
					Console.WriteLine("_uniqArraysList.Count: {0} - left {1}", _uniqArraysList.Count, i);
					Console.WriteLine("attempts: {0} - left: {1}", attempts, j);
					Console.WriteLine("winners.Count: {0}", winners.Count);
				}
				if (winner.collectionsList.Count != 0)
				{
					winners.Add(winner);
				}
			}

			return winners;
		}

		public MatchedCollectionHolder FindWinnerCollections(List<int> generatedNumber)
		{
			MatchedCollectionHolder collections = new MatchedCollectionHolder();
			int firstState = collections.GetObjectState();

			for (int i = 0; i < _historicalWonNumbers.Count; i++)
			{
				var numberOfMatch = FindMatchNumbers(generatedNumber, _historicalWonNumbers[i]);

				if (numberOfMatch == 4)
					collections.FourMatchedNumber.Add(_historicalWonNumbers[i].ToJson());

				if (numberOfMatch == 5)
					collections.FiveMatchedNumber.Add(_historicalWonNumbers[i].ToJson());

				if (numberOfMatch == 6)
					collections.SixMatchedNumber.Add(_historicalWonNumbers[i].ToJson());
			}

			return firstState != collections.GetObjectState() ? collections : null;
		}

		private int FindMatchNumbers(List<int> numberToCompare, List<int> historyNumber)
		{
			int numberOfMatch = 0;
			for (int i = 0; i < historyNumber.Count; i++)
			{
				if (historyNumber.Contains(numberToCompare[i]))
					numberOfMatch++;
			}

			return numberOfMatch;
		}
	}
}