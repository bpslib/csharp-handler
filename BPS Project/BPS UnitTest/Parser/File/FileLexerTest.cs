using Microsoft.VisualStudio.TestTools.UnitTesting;
using BPSLib;
using BPSLib.Parser.File;
using System.Collections.Generic;
using BPSLib.Parser;

namespace BPS_UnitTest.Parser.File
{
	[TestClass]
	public class FileLexerTest
	{
		[TestMethod]
		public void ParseTest()
		{
			// Arrange
			string input =
				"string:'string';\n" +
				"int:10;\n" +
				"float:0.5;\n" +
				"bool:true;\n" +
				"arr:[0,1,2];\n";
			List<Token> tokens = new List<Token>
			{
				new Token(TokenCategory.KEY, "string", 0, 0),
				new Token(TokenCategory.STRING, "'string'", 0, 0),
				new Token(TokenCategory.KEY, "int", 0, 0),
				new Token(TokenCategory.INTEGER, "10", 0, 0),
				new Token(TokenCategory.KEY, "float", 0, 0),
				new Token(TokenCategory.FLOAT, "0.5", 0, 0),
				new Token(TokenCategory.KEY, "bool", 0, 0),
				new Token(TokenCategory.BOOL, "true", 0, 0),
				new Token(TokenCategory.KEY, "arr", 0, 0),
				new Token(TokenCategory.OPEN_ARRAY, "[", 0, 0),
				new Token(TokenCategory.INTEGER, "0", 0, 0),
				new Token(TokenCategory.INTEGER, "1", 0, 0),
				new Token(TokenCategory.INTEGER, "2", 0, 0),
				new Token(TokenCategory.CLOSE_ARRAY, "]", 0, 0),
				new Token(TokenCategory.EOF, "", 0, 0)
			};
			FileLexer lexer = new FileLexer(input);

			// Act
			lexer.Parse();

			// Assert
			for (var i = 0; i < lexer.Tokens.Count; i++)
			{
				Assert.AreEqual(tokens[i], lexer.Tokens[i]);
			}
		}
	}
}
