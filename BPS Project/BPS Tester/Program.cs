using BPSLib;
using System;

namespace Tester
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				var bps = BPS.Load("../../../../Examples/read_test.bps");

				Console.WriteLine(bps.Plain() + "\n");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
