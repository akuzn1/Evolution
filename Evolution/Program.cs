using Evolution.Lib;
using Evolution.Utils.Random;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Evolution
{
	class Program
	{
		static void Main(string[] args)
		{
			RandomGenerator.Random.IsDebug = false;
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

			var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "states.txt");
			if (File.Exists(path))
				File.Delete(path);

			var stateList = new List<State>();
			var state = new State(iterations, map);
			Save(state);
			stateList.Add(state);
			while (iterations < 100)
			{
				//if(iterations % 10 == 0)
				//{
				//	Save(map);
				//}

				map.DoNextStep();

				controller.DoInitialPhase();

				controller.DoEatingPhase();

				controller.DoReproductionPhase();

				controller.DoFinishPhase();

				state = new State(iterations, map);
				Save(state);
				stateList.Add(state);

				controller.DoDeadPhase();

				iterations++;
			}
			Save(map);
			SaveToCSV(stateList);
		}

		private static void SaveToCSV(List<State> stateList)
		{
			var sb = new StringBuilder();
			sb.Append("Iteration,GrassCount,OrganismCount,IsNewCount,IsDeadCount,AgeDistribution");
			for (int i = 0; i < 19; i++)
			{
				sb.Append(",");
			}
			sb.AppendLine();
			foreach (var state in stateList)
			{
				sb.AppendFormat("{0},{1},{2},{3},{4}", state.Iteration, state.GrassCount, state.OrganismCount, state.IsNewCount, state.IsDeadCount);

				for (int i = 0; i < 20; i++)
				{
					sb.AppendFormat(",{0}",state.AgeDistribution[i]);
				}
				sb.AppendLine();
			}
			File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "states.csv"), sb.ToString());
		}

		private static void Save(State state)
		{
			File.AppendAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "states.txt"), new string[] { JsonConvert.SerializeObject(state) });
		}

		private static Map Load(Map map)
		{
			var str = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "out.txt"));
			return MapSerializer.Deserialize(str);
		}

		private static void Save(Map map)
		{
			string result = MapSerializer.Serialize(map);
			File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "out.txt"), result);
		}
	}
}
