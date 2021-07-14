using BPSLib;
using System;
using System.Collections.Generic;

namespace Tester
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				var bps = BPS.Load("../../../../../docs/examples/all_data_structures.bps");

				Console.WriteLine(bps.Plain() + "\n");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
