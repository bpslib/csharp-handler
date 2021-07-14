using Microsoft.VisualStudio.TestTools.UnitTesting;
using BPSLib;
using BPSLib.Parser.Plain;
using System.Collections.Generic;

namespace BPS_UnitTest.Parser.Plain
{
	[TestClass]
	class PlainParserTest
	{
		[TestMethod]
		public void Parse()
		{
			// Arrange
			string plain =
				"string:'string';\n" +
				"int:10;\n" +
				"float:0.5;\n" +
				"boolTrue:true;\n" +
				"boolFalse:false;\n" +
				"stringArr:['yes','no','maybe'];\n" +
				"intArr:[0,1,2,10,-5];\n" +
				"floatArr:[0.9,1.7,0.2,1.06,-5.618];\n" +
				"boolArr:[true,false,true];\n" +
				"multArr2:[[0,1,2],[0,1,2],[0,1,2]];\n";
			BPSFile file = new BPSFile
			{
				{ "string", "string" },
				{ "int", 10 },
				{ "float", 0.5 },
				{ "boolTrue", true },
				{ "boolFalse", false },
				{ "stringArr", new List<string> { "yes", "no", "maybe" } },
				{ "intArr", new List<int> { 0, 1, 2, 10, -5 } },
				{ "floatArr", new List<float> { 0.9f, 1.7f, 0.2f, 1.06f, -5.618f } },
				{ "boolArr", new List<bool> { true, false, true } },
				{ "multArr2", new List<List<int>> { new List<int> { 0, 1, 2 }, new List<int> { 0, 1, 2 }, new List<int> { 0, 1, 2 } } }
			};
			PlainParser parser = new PlainParser(file);

			// Act
			parser.Parse();

			// Assert
			Assert.AreEqual(plain, parser.Plain);
		}
	}
}
