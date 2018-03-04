using System.Collections.Generic;
using System.IO;
using LotoCombinationsAnalizer.Objects;
using ServiceStack;

namespace LotoCombinationsAnalizer
{
	public class ResultWriter
	{
		public void WriteToLocalFileAsStringBlock(List<Winner> winners)
		{
			var i = 1;
			using (StreamWriter file = new StreamWriter(@"C:\Users\MaximAndEugenia\Desktop\WinnerTextObjects.txt"))
			{
				foreach (var winner in winners)
				{
					file.WriteLine("----------------------------");
					file.WriteLine("Winner {0}:\n", i);
					file.WriteLine("WinArray: {0}\n", winner.WinArray.ToJson());
					foreach (var collection in winner.collectionsList)
					{
						file.WriteLine("won combination: {0}\n", collection.wonCombination.ToJson());
						file.WriteLine("4 match: {0}\n", collection.FourMatchedNumber.ToJson());
						file.WriteLine("5 match: {0}\n", collection.FiveMatchedNumber.ToJson());
						file.WriteLine("6 match: {0}\n", collection.SixMatchedNumber.ToJson());
					}
					i++;
				}
			}
		}
		public void WriteToLocalFileAsJsonObject(List<Winner> winners)
		{
			using (StreamWriter file = new StreamWriter(@"C:\Users\Maksym\Desktop\WinnerJsonObjects.txt"))
			{
				foreach (var winner in winners)
				{
					file.WriteLine(winner.WinArray.ToJson());
					file.WriteLine(winner.collectionsList.ToJson());
				}

				//file.Write(winners.ToJson());
			}
		}
		public void WriteFilteredResultToLocalFile(List<Winner> winners)
		{
			var i = 1;
			using (StreamWriter file = new StreamWriter(@"C:\Users\Maksym\Desktop\WinnerFilteredResult.txt"))
			{
				foreach (var winner in winners)
				{
					file.WriteLine("----------------------------");
					file.WriteLine("Winner {0}:\n", i);
					file.WriteLine("WinArray: {0}\n", winner.WinArray.ToJson());
					foreach (var collection in winner.collectionsList)
					{
						file.WriteLine("won combination: {0}\n", collection.wonCombination.ToJson());
						file.WriteLine("4 match: {0}\n", collection.FourMatchedNumber.ToJson());
						file.WriteLine("5 match: {0}\n", collection.FiveMatchedNumber.ToJson());
						file.WriteLine("6 match: {0}\n", collection.SixMatchedNumber.ToJson());
					}
					i++;
				}
			}
		}
	}
}
