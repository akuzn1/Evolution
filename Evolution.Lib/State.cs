using System.Linq;

namespace Evolution.Lib
{
	public class State
	{
		public State(int iteration, Map map)
		{
			Iteration = iteration;
			foreach (var cell in map.Cells)
			{
				GrassCount += cell.Food;
				MaxGrass += cell.MaxFood;
				OrganismCount += cell.Organisms.Count;
				IncreaseGrassSpeed += cell.IncreaseSpeed;
			}
		}

		public int Iteration { get; set; }
		public int GrassCount { get; set; }
		public int MaxGrass { get; set; }
		public int OrganismCount { get; set; }
		public int IncreaseGrassSpeed { get; set; }
	}
}
