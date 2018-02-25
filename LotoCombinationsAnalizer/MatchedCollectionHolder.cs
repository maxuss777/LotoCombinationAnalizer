using System;
using System.Collections.Generic;

namespace LotoCombinationsAnalizer
{
	public class MatchedCollectionHolder
	{
		public List<int> wonCombination { get; set; }
		public List<string> FourMatchedNumber { get; set; }
		public List<string> FiveMatchedNumber { get; set; }
		public List<string> SixMatchedNumber { get; set; }

		public MatchedCollectionHolder()
		{
			wonCombination = new List<int>();
			FourMatchedNumber = new List<string>();
			FiveMatchedNumber = new List<string>();
			SixMatchedNumber = new List<string>();
		}

		public virtual int GetObjectState()
		{
			return FourMatchedNumber.Count + FiveMatchedNumber.Count + SixMatchedNumber.Count + wonCombination.Count;
		}
	}
}
