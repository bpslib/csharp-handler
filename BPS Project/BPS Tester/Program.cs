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

				file.Set("name", "Carlos Machado");
				file.Set("age", 26);
				file.Set("height", 1.93);
				file.Set("playVideoGame", false);
				file.Set("wichGames", new List<object> { });
				file.Set("favoriteVideoGame", null);

				file.Save(saveLoad);

				file = BPS.Load(saveLoad);
				file.Set("playVideoGame", true);
				file.Set("wichGames", new List<object> { "BDO", "OSRS", "PW" });
				file.Set("favoriteVideoGame", "OSRS");

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
