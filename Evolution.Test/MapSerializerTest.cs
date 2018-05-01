using Evolution.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Evolution.Test
{
	[TestClass]
	public class MapSerializerTest
	{
		[TestMethod]
		public void SerializationTest()
		{
			Map map = new Map(1, 2);
			foreach (var cell in map.Cells)
			{
				cell.IncreaseSpeed = 1;
				cell.MaxFood = 20;
			}

			map.Cells[0, 0].Organisms.Add(new Organism(map, 0, 0));
			map.Cells[0, 0].Organisms.Add(new Organism(map, 0, 0));
			map.Cells[0, 1].Organisms.Add(new Organism(map, 0, 0));
			map.Cells[0, 0].Food = 20;
			map.Cells[0, 1].Food = 10;

			var serializedObject = MapSerializer.Serialize(map);
			Map res = MapSerializer.Deserialize(serializedObject);

			Assert.AreEqual(1, res.Width);
			Assert.AreEqual(2, res.Height);
			Assert.AreEqual(map.Cells.Length, res.Cells.Length);
			Assert.AreEqual(res, res.Cells[0, 0].Map);
			Assert.AreEqual(res, res.Cells[0, 0].Organisms[0].Map);
			Assert.AreEqual(20, res.Cells[0, 0].Food);
		}
	}
}
