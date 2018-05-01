using Evolution.Lib;
using Newtonsoft.Json;

namespace Evolution
{
	public static class MapSerializer
	{
		public static string Serialize(Map map)
		{
			return JsonConvert.SerializeObject(map);
		}

		public static Map Deserialize(string str)
		{
			var map = JsonConvert.DeserializeObject<Map>(str);
			foreach (var cell in map.Cells)
			{
				cell.Map = map;
				foreach (var organism in cell.Organisms)
				{
					organism.Map = map;
				}
			}
			return map;
		}
	}
}
