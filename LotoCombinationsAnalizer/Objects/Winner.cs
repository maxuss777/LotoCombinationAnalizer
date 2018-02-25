using System.Collections.Generic;

namespace LotoCombinationsAnalizer.Objects
{
	public class Winner
	{
		public List<int> WinArray;
		public List<MatchedCollectionHolder> collectionsList;

		public Winner()
		{
			collectionsList=new List<MatchedCollectionHolder>();
		}
	}
}

