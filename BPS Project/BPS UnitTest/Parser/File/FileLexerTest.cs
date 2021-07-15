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
				new Token(TokenCategory.KEY, "string"),
				new Token(TokenCategory.STRING, "'string'"),
				new Token(TokenCategory.KEY, "int"),
				new Token(TokenCategory.INTEGER, "10"),
				new Token(TokenCategory.KEY, "float"),
				new Token(TokenCategory.FLOAT, "0.5"),
				new Token(TokenCategory.KEY, "bool"),
				new Token(TokenCategory.TRUE, "true"),
				new Token(TokenCategory.KEY, "arr"),
				new Token(TokenCategory.OPEN_ARRAY, "["),
				new Token(TokenCategory.INTEGER, "0"),
				new Token(TokenCategory.INTEGER, "1"),
				new Token(TokenCategory.INTEGER, "2"),
				new Token(TokenCategory.CLOSE_ARRAY, "]"),
				new Token(TokenCategory.EOF, "")
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
