using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ServiceStack;
using ServiceStack.Text;

namespace LotoCombinationsAnalizer
{
	public class ArraysHolder
	{
		private List<int> _list;

		public ArraysHolder()
		{
			InitNumberList();
		}

		public List<List<int>> GetMaximumPassibleArrays()
		{
			string firstListState = _list.ToJson();
			List<string> uniqLists = new List<string>();

			for (; ; )
			{
				uniqLists.Add(_list.ToJson());
				var penultItem = _list[_list.Count - 2];

				for (int i = 1; ; i++)
				{
					if (i == _list.Count-1)
						i = 1;

					var k = _list[i];
					_list[i] = _list[i + 1];
					_list[i + 1] = k;

					uniqLists.Add(_list.ToJson());

					if (_list[_list.Count-1] == penultItem)
					{
						break;
					}
				}

				var j = _list[0];
				_list[0] = _list[1];
				_list[1] = j;

				string currentListState = _list.ToJson();

				if (firstListState == currentListState)
					break;
			}

			return DesirialiseList(uniqLists);
		}

		public List<List<int>> GetHistoryResult()
		{
			List<List<int>> results = new List<List<int>>();
            string line;
            var file = new StreamReader(@"C:\Users\Maksym\Desktop\WonNumbers.txt");
            
            while ((line = file.ReadLine()) != null)
            {
                var list = line.Split(',').Select(Int32.Parse).ToList();
	            var megaBall = list[6];
	            list.Remove(megaBall);
                results.Add(list);
            }

            return results;
		}

		private List<List<int>> DesirialiseList(List<string> lists)
		{
			List<List<int>> desirialisedList = new List<List<int>>();

			for (int i = 0; i < lists.Count; i++)
			{
				desirialisedList.Add(JsonSerializer.DeserializeFromString<List<int>>(lists[i]));
			}

			return desirialisedList;
		}

		private void InitNumberList()
		{
			_list = new List<int>();
			for (int i = 1; i < 43; i++)
			{
				_list.Add(i);
			}
		}
	}
}
