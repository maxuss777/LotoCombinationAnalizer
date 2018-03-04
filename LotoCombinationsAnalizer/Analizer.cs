using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<Winner>> FindTheBestWinnersAsync(int attempts)
        {
            List<Winner> winners = new List<Winner>();

            var attemptsInfo = $"required/finished attempts: {attempts * _uniqArraysList.Count} / ";
            var tasksInfo = $"Tasks started/finished: ";

            Console.WriteLine($"_uniqArraysList.Count: {_uniqArraysList.Count}");
            Console.WriteLine(attemptsInfo);
            Console.WriteLine(tasksInfo);
            Console.WriteLine("winners.Count:");

            List<Task> tasks = new List<Task>();
            var locker = new object();

            var attemtsCounter = 0;
            var finishedTasksCounter = 0;

            for (int i = 0; i < _uniqArraysList.Count; i++)
            {
                var currentArray = _uniqArraysList[i];
                var winner = new Winner();

                tasks.Add(Task.Run(() =>
                 {
                     var currentArr = currentArray;
                     for (int j = 0; j < attempts; j++)
                     {
                         var combination = _generator.GetNewCombination(currentArr);
                         var collections = FindWinnerCollections(combination);

                         if (collections != null)
                         {
                             collections.wonCombination = combination;
                             winner.collectionsList.Add(collections);
                             winner.WinArray = currentArr;
                         }

                         if (winner.collectionsList.Count != 0)
                         {
                             winners.Add(winner);
                         }
                         //attemtsCounter += j;
                     }

                     lock (locker)
                     {
                         WriteAt(winners.Count.ToString(), 15, 3);
                         WriteAt($" / {(finishedTasksCounter++)}", $"{tasks.Count}".Length + tasksInfo.Length, 2);
                         WriteAt(attemtsCounter.ToString(), attemptsInfo.Length, 1);
                     }
                 }));
            }
            WriteAt(tasks.Count.ToString(), 24, 2);

            await Task.WhenAll(tasks);

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