using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

            Console.Clear();
            Console.WriteLine($"_uniqArraysList.Count: {_uniqArraysList.Count} - left: ");
            Console.WriteLine($"attempts: {attempts} - left:");
            Console.WriteLine("winners.Count:");

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

                    WriteAt(i.ToString(), 37, 0);
                    WriteAt(j.ToString(), 21, 1);
                    WriteAt(winners.Count.ToString(), 15, 2);
				}
				if (winner.collectionsList.Count != 0)
				{
					winners.Add(winner);
				}
			}

			return winners;
		}

        private void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
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