using System;
using System.Collections.Generic;

namespace LotoCombinationsAnalizer
{
	public class Generator
	{
		private Random _random;

		public Generator()
		{
			_random = new Random();
		}

		public List<int> GetNewCombination(List<int> currentArray)
		{
			List<int> combination = new List<int>();

			for (;;)
			{
				var k = currentArray[_random.Next(1, 42)];

				if (!combination.Contains(k))
				{
					combination.Add(k);
				}
				if (combination.Count == 6)
				{
					break;
				}
			}
			combination.Add(_random.Next(1, 9));

			return combination;
		}
	}
}