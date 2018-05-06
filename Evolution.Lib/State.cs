using System;
using System.Linq;

namespace Evolution.Lib
{
	public class State
	{
		public State(int iteration, Map map)
		{
			Iteration = iteration;
			AgeDistribution = new int[25];
			OrganismsDistribution = new int[map.Cells.Length];
			int i = 0;
			foreach (var cell in map.Cells)
			{
				GrassCount += cell.Food;
				MaxGrass += cell.MaxFood;
				OrganismCount += cell.Organisms.Count;
				IncreaseGrassSpeed += cell.IncreaseSpeed;
				IsNewCount += cell.Organisms.Count(p => p.IsNew);
				IsDeadCount += cell.Organisms.Count(p => !p.IsAlive);
				AverageDeadAge += cell.Organisms.Where(p => !p.IsAlive).Sum(p => p.Age);
				AverageAge += cell.Organisms.Sum(p => p.Age);
				EatingPriority += cell.Organisms.Sum(p => p.EatingPriority);
				foreach (var organism in cell.Organisms)
				{
					AgeDistribution[Math.Min(organism.Age, 24)] ++;
				}
				OrganismsDistribution[i] += cell.Organisms.Count;
				i++;
			}
			AverageAge /= OrganismCount;
			EatingPriority /= OrganismCount;
			AverageDeadAge /= IsDeadCount;
		}

		public int Iteration { get; set; }
		public int GrassCount { get; set; }
		public int MaxGrass { get; set; }
		public int OrganismCount { get; set; }
		public double EatingPriority { get; set; }
		public int IncreaseGrassSpeed { get; set; }
		public int IsNewCount { get; set; }
		public int IsDeadCount { get; set; }
		public double AverageDeadAge { get; set; }
		public double AverageAge { get; set; }

		public int[] AgeDistribution { get; set; }
		public int[] OrganismsDistribution { get; set; }
	}
}
