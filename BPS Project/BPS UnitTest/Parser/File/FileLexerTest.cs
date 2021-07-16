using Microsoft.VisualStudio.TestTools.UnitTesting;
using BPSLib.Parser.File;
using System.Collections.Generic;

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
				"string:\"string\";\n" +
				"char:'c';\n" +
				"int:10;\n" +
				"float:0.5;\n" +
				"bool:true;\n" +
				"arr:[0,1,2];\n";
			List<Token> tokens = new List<Token>
			{
				new Token(TokenCategory.KEY, "string", 1, 1),
				new Token(TokenCategory.DATA_SEP, ":", 1, 7),
				new Token(TokenCategory.STRING, "\"string\"", 1, 8),
				new Token(TokenCategory.END_OF_DATA, ";", 1, 16),
				new Token(TokenCategory.KEY, "char", 2, 1),
				new Token(TokenCategory.DATA_SEP, ":", 2, 5),
				new Token(TokenCategory.CHAR, "'c'", 2, 6),
				new Token(TokenCategory.END_OF_DATA, ";", 2, 9),
				new Token(TokenCategory.KEY, "int", 3, 1),
				new Token(TokenCategory.DATA_SEP, ":", 3, 4),
				new Token(TokenCategory.INTEGER, "10", 3, 5),
				new Token(TokenCategory.END_OF_DATA, ";", 3, 7),
				new Token(TokenCategory.KEY, "float", 4, 1),
				new Token(TokenCategory.DATA_SEP, ":", 4, 6),
				new Token(TokenCategory.FLOAT, "0.5", 4, 7),
				new Token(TokenCategory.END_OF_DATA, ";", 4, 10),
				new Token(TokenCategory.KEY, "bool", 5, 1),
				new Token(TokenCategory.DATA_SEP, ":", 5, 5),
				new Token(TokenCategory.BOOL, "true", 5, 6),
				new Token(TokenCategory.END_OF_DATA, ";", 5, 10),
				new Token(TokenCategory.KEY, "arr", 6, 1),
				new Token(TokenCategory.DATA_SEP, ":", 6, 4),
				new Token(TokenCategory.OPEN_ARRAY, "[", 6, 5),
				new Token(TokenCategory.INTEGER, "0", 6, 6),
				new Token(TokenCategory.ARRAY_SEP, ",", 6, 7),
				new Token(TokenCategory.INTEGER, "1", 6, 8),
				new Token(TokenCategory.ARRAY_SEP, ",", 6, 9),
				new Token(TokenCategory.INTEGER, "2", 6, 10),
				new Token(TokenCategory.CLOSE_ARRAY, "]", 6, 11),
				new Token(TokenCategory.END_OF_DATA, ";", 6, 12),
				new Token(TokenCategory.EOF, null, -1, -1)
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
