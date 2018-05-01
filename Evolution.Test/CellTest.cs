using Evolution.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Evolution.Test
{
	[TestClass]
	public class CellTest
	{
		[TestMethod]
		public void StepTest()
		{
			var cell = new Cell(null, 0, 0, food: 0, maxFood: 3, increaseSpeed: 1);
			cell.DoNextStep();
			Assert.AreEqual(1, cell.Food);
			cell.DoNextStep();
			Assert.AreEqual(2, cell.Food);
			cell.DoNextStep();
			Assert.AreEqual(3, cell.Food);
			cell.DoNextStep();
			Assert.AreEqual(3, cell.Food);
		}
	}
}
