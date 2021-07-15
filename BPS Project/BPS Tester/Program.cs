using BPSLib;
using System;
using System.Collections.Generic;
using System.IO;

namespace Tester
{
	class Program
	{
		static void Main(string[] args)
		{
			//Test();
			SaveLoadTest();
		}

		static void SaveLoadTest()
		{
			try
			{
				var myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				var defaultTestPath = Path.Combine(myDocuments, "BPS_Tests");
				var saveLoad = Path.Combine(defaultTestPath, "save_load_test");

				var file = new BPSFile();

				file.Add("name", "Carlos Machado");
				file.Add("age", 26);
				file.Add("height", 1.93);
				file.Add("playVideoGame", false);
				file.Add("wichGames", new List<object> { });
				file.Add("favoriteVideoGame", null);

				file.Save(saveLoad);

				file = BPS.Load(saveLoad);
				file.Add("playVideoGame", true);
				file.Add("wichGames", new List<object> { "BDO", "OSRS", "PW" });
				file.Add("favoriteVideoGame", "OSRS");

				file.Save(saveLoad);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		static void Test()
		{
			var bps = BPS.Load("../../../../../docs/examples/all_data_structures.bps");
			Console.WriteLine(bps.Plain() + "\n");
		}
	}
}
