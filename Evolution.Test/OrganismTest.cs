using Evolution.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Evolution.Test
{
	[TestClass]
	public class OrganismTest
	{
		[TestMethod]
		public void EatingPriorityTest()
		{
			Organism organism = new Organism(null, 0, 0)
			{
				Energy = 1
			};
			Assert.AreEqual(0, organism.Age);
			Assert.AreEqual(10, organism.GetEatingPriority());

			organism.FinishStep();
			Assert.AreEqual(1, organism.Age);
			Assert.AreEqual(10, organism.GetEatingPriority());

			organism.FinishStep();
			organism.FinishStep();
			organism.FinishStep();
			organism.FinishStep();
			organism.FinishStep();
			organism.FinishStep();
			organism.FinishStep();
			organism.FinishStep();
			organism.FinishStep();
			Assert.AreEqual(10, organism.Age);
			Assert.AreEqual(10, organism.GetEatingPriority());

			organism.FinishStep();
			Assert.AreEqual(11, organism.Age);
			Assert.AreEqual(9, organism.GetEatingPriority());

			organism.FinishStep();
			Assert.AreEqual(12, organism.Age);
			Assert.AreEqual(8, organism.GetEatingPriority());

			organism.FinishStep();
			organism.FinishStep();
			organism.FinishStep();
			organism.FinishStep();
			organism.FinishStep();
			organism.FinishStep();
			organism.FinishStep();
			Assert.AreEqual(19, organism.Age);
			Assert.AreEqual(1, organism.GetEatingPriority());

			organism.FinishStep();
			Assert.AreEqual(20, organism.Age);
			Assert.AreEqual(0, organism.GetEatingPriority());

			organism.FinishStep();
			Assert.AreEqual(21, organism.Age);
			Assert.AreEqual(0, organism.GetEatingPriority());
		}
	}
}
