using Evolution.Lib;
using Evolution.Utils.Random;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Evolution
{
	class Program
	{
		static void Main(string[] args)
		{
			int mapWidth = 25;
			int mapHeight = 25;
			var map = new Map(mapWidth, mapHeight);
			foreach (var cell in map.Cells)
			{
				cell.Food = 100;
				cell.MaxFood = 100;
				cell.IncreaseSpeed = 35;
			}

			int startOrgaismCount = 100;

			for (int i = 0; i < startOrgaismCount; i++)
			{
				int xPos = RandomGenerator.Random.GetRandom(mapWidth);
				int yPos = RandomGenerator.Random.GetRandom(mapHeight);
				var organism = new Organism(map, xPos, yPos) { IsNew = false };
				map.Cells[xPos, yPos].Organisms.Add(organism);
			}

			var controller = new LifeCycleController(map);

			int iterations = 0;
			while(iterations < 100)
			{
				if(iterations % 10 == 0)
				{
					Save(map);
					//Load(map);
				}

				map.DoNextStep();

				controller.DoInitialPhase();

				controller.DoEatingPhase();

				controller.DoReproductionPhase();

				controller.DoFinishPhase();

				controller.DoDeadPhase();

				iterations++;
			}
		}

		private static Map Load(Map map)
		{
			var str = File.ReadAllText("out.txt");
			return MapSerializer.Deserialize(str);
		}

		private static void Save(Map map)
		{
			string result = MapSerializer.Serialize(map);
			File.WriteAllText("out.txt", result);
		}
	}
}
