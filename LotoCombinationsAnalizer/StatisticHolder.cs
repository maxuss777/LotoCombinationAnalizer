using System.Collections.Generic;
using System.IO;
using LotoCombinationsAnalizer.Objects;
using ServiceStack.Text;

namespace LotoCombinationsAnalizer
{
	public class StatisticHolder
	{
		private List<Winner> winnersLst;

		public StatisticHolder()
		{
			winnersLst = new List<Winner>();
		}

		public List<Winner> GetResult()
		{
			string line;

			using (StreamReader file = new StreamReader(@"C:\Users\MaximAndEugenia\Desktop\WinnerJsonObjects.txt"))
			{
				while ((line = file.ReadLine()) != null)
				{
					var winner = new Winner
					{
						WinArray = JsonSerializer.DeserializeFromString<List<int>>(line),
						collectionsList = JsonSerializer.DeserializeFromString<List<MatchedCollectionHolder>>(file.ReadLine()),
					};
					winnersLst.Add(winner);
				}
			}

			return winnersLst;
		}
	}
}