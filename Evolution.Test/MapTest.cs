using Evolution.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.Test
{
	[TestClass]
	public class MapTest
	{
		[TestMethod]
		public void GetNearestCellsTest()
		{
			Map map = new Map(10, 10);
			// full
			Assert.AreEqual(9, map.GetNearestCells(2, 2).Count);

			// border
			Assert.AreEqual(6, map.GetNearestCells(2, 0).Count);
			Assert.AreEqual(6, map.GetNearestCells(0, 2).Count);
			Assert.AreEqual(6, map.GetNearestCells(2, 9).Count);
			Assert.AreEqual(6, map.GetNearestCells(9, 2).Count);

			// angle
			Assert.AreEqual(4, map.GetNearestCells(0, 0).Count);
			Assert.AreEqual(4, map.GetNearestCells(9, 0).Count);
			Assert.AreEqual(4, map.GetNearestCells(0, 9).Count);
			Assert.AreEqual(4, map.GetNearestCells(9, 9).Count);

			// out of range
			Assert.AreEqual(0, map.GetNearestCells(-1, 2).Count);
			Assert.AreEqual(0, map.GetNearestCells(2, -1).Count);
			Assert.AreEqual(0, map.GetNearestCells(10, 2).Count);
			Assert.AreEqual(0, map.GetNearestCells(2, 10).Count);
		}

		[TestMethod]
		public void DoNextStepTest()
		{
			Map map = new Map(10, 10);
			foreach (var cell in map.Cells)
			{
				cell.IncreaseSpeed = 1;
				cell.MaxFood = 10;
			}

			map.DoNextStep();

			foreach (var cell in map.Cells)
			{
				Assert.AreEqual(1, cell.Food);
			}
		}
	}
}
