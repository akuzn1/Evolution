using Evolution.Lib;
using Evolution.Utils.Random;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Evolution.Test
{
	[TestClass]
	public class LifeCycleControllerTest
	{
		[TestMethod]
		public void InitialPhaseTest()
		{
			var map = new Map(1, 1);
			map.Cells[0, 0].Organisms.Add(new Organism() { IsAlive = false });

			var controller = new LifeCycleController(map);
			Assert.ThrowsException<InvalidOperationException>(() => controller.DoInitialPhase());
		}

		[TestMethod]
		public void EatingPhaseTest()
		{
			RandomGenerator.Random.IsDebug = true;
			var map = new Map(1, 1);
			map.Cells[0, 0].Organisms.Add(new Organism(map, 0, 0));
			map.Cells[0, 0].Food = 20;

			var controller = new LifeCycleController(map);
			controller.DoEatingPhase();

			Assert.AreEqual(11, map.Cells[0, 0].Food);
		}

		[TestMethod]
		public void EatingPhaseTestWithConcurence()
		{
			RandomGenerator.Random.IsDebug = true;
			var map = new Map(1, 1);
			map.Cells[0, 0].Organisms.Add(new Organism(map, 0, 0));
			map.Cells[0, 0].Organisms.Add(new Organism(map, 0, 0));
			map.Cells[0, 0].Organisms.Add(new Organism(map, 0, 0));
			map.Cells[0, 0].Food = 20;

			var controller = new LifeCycleController(map);
			controller.DoEatingPhase();

			Assert.AreEqual(0, map.Cells[0, 0].Food);

			Assert.AreEqual(4, map.Cells[0, 0].Organisms[0].Energy);
			Assert.AreEqual(4, map.Cells[0, 0].Organisms[1].Energy);
			Assert.AreEqual(-3, map.Cells[0, 0].Organisms[2].Energy);
		}

		[TestMethod]
		public void ReproductionPhaseTest()
		{
			RandomGenerator.Random.IsDebug = true;
			var map = new Map(2, 2);
			map.Cells[0, 0].Organisms.Add(new Organism(map, 0, 0) { IsNew = false });
			map.Cells[0, 0].Food = 20;
			var controller = new LifeCycleController(map);
			controller.DoEatingPhase();
			controller.DoReproductionPhase();

			int count = 0;
			foreach (var cell in map.Cells)
			{
				count += cell.Organisms.Count;
			}
			Assert.AreEqual(3, count);
		}

		[TestMethod]
		public void DeadPhaseTest()
		{
			RandomGenerator.Random.IsDebug = true;
			var map = new Map(1, 1);
			map.Cells[0, 0].Organisms.Add(new Organism(map, 0, 0) { IsNew = false });
			map.Cells[0, 0].Organisms.Add(new Organism(map, 0, 0) { IsNew = false });
			map.Cells[0, 0].Organisms.Add(new Organism(map, 0, 0) { IsNew = false });
			map.Cells[0, 0].Food = 20;

			var controller = new LifeCycleController(map);
			controller.DoEatingPhase();
			controller.DoFinishPhase();
			controller.DoDeadPhase();

			Assert.AreEqual(2, map.Cells[0, 0].Organisms.Count);
		}

		[TestMethod]
		public void FinishPhaseTest()
		{
			var map = new Map(1, 1);
			map.Cells[0, 0].Organisms.Add(new Organism(map, 0, 0) { Energy = 10 });
			Assert.IsTrue(map.Cells[0, 0].Organisms[0].IsNew);
			Assert.AreEqual(0, map.Cells[0, 0].Organisms[0].Age);

			var controller = new LifeCycleController(map);
			controller.DoFinishPhase();

			Assert.AreEqual(1, map.Cells[0, 0].Organisms[0].Age);
		}
	}
}
