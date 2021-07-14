/*
 * MIT License
 *
 * Copyright (c) 2021 Carlos Eduardo de Borba Machado
 *
 */

using System;
using System.Collections.Generic;

namespace BPSLib.Parser.File
{
	internal class Lexer
	{
		internal List<Token> Tokens { get; }

		private string input;
		private string curChar;
		private int curIndex = 0;

		internal Lexer(string input)
		{
			Tokens = new List<Token>();
			this.input = input;
		}

		internal void Parse()
		{
			while (!EndOfInput())
			{
				NextChar();

				// skip chars
				if (Symbols.IsSkip(curChar))
				{
					continue;
				}

				// skip comments
				if (curChar.Equals(Symbols.HASH))
				{
					while (!EndOfInput() && !curChar.Equals(Symbols.NEWLINE))
					{
						NextChar();
					}
				}
				// is value
				else if (curChar.Equals(Symbols.COLON))
				{
					while (!EndOfInput() && !curChar.Equals(Symbols.SEMICOLON))
					{
						NextChar();
						if (curChar.Equals(Symbols.COMMA))
						{
							NextChar();
							if (curChar.Equals(Symbols.COMMA))
							{
								throw new Exception("[double comma] - Invalid character '" + curChar + "' encountered.");
							}
						}
						while (!EndOfInput() && (curChar.Equals(Symbols.SPACE) || curChar.Equals(Symbols.NEWLINE)))
						{
							NextChar();
						}
						if (curChar.Equals(Symbols.SEMICOLON))
						{
							break;
						}
						var lexeme = curChar;

						// open array
						if (curChar.Equals(Symbols.LEFT_BRACKETS))
						{
							Tokens.Add(new Token(TokenCategory.OPEN_ARRAY, lexeme));
						}
						// close array
						else if (curChar.Equals(Symbols.RIGHT_BRACKETS))
						{
							Tokens.Add(new Token(TokenCategory.CLOSE_ARRAY, lexeme));
						}
						// is a value
						else
						{
							// string
							if (curChar.Equals(Symbols.QUOTE) || curChar.Equals(Symbols.DQUOTE))
							{
								var closeQuote = curChar;
								var beforeChar = curChar;
								NextChar();
								while (!EndOfInput() && (!curChar.Equals(closeQuote) && !beforeChar.Equals("\\")))
								{
									beforeChar = curChar;
									lexeme += curChar;
									NextChar();
								}
								lexeme += curChar;
								Tokens.Add(new Token(TokenCategory.STRING, lexeme));
							}
							// numeric
							else if (char.IsDigit(char.Parse(curChar)) || curChar.Equals(Symbols.DOT) || curChar.Equals(Symbols.MINUS))
							{
								var dotted = curChar.Equals(Symbols.DOT);
								NextChar();
								while (!EndOfInput() && (char.IsDigit(char.Parse(curChar)) || curChar.Equals(Symbols.DOT)))
								{
									if (curChar.Equals(Symbols.DOT))
									{
										if (dotted)
										{
											throw new Exception("[double dot] - Invalid character '" + curChar + "' encountered.");
										}
										else
										{
											dotted = true;
										}
									}
									lexeme += curChar;
									NextChar();
								}
								// float or int
								if (lexeme.Contains(Symbols.DOT))
								{
									Tokens.Add(new Token(TokenCategory.FLOAT, lexeme));
								}
								else
								{
									Tokens.Add(new Token(TokenCategory.INTEGER, lexeme));
								}
								PreviousChar();
							}
							// boolean
							else if (curChar.Equals("t") || curChar.Equals("f"))
							{
								NextChar();
								while (!EndOfInput() && char.IsLetter(char.Parse(curChar)))
								{
									lexeme += curChar;
									NextChar();
								}
								// true or false
								if (lexeme.Equals("true"))
								{
									Tokens.Add(new Token(TokenCategory.TRUE, lexeme));
								}
								else if (lexeme.Equals("false"))
								{
									Tokens.Add(new Token(TokenCategory.FALSE, lexeme));
								}
								else
								{
									throw new Exception("Invalid lexeme '" + lexeme + "' encountered.");
								}
								PreviousChar();
							}
							else
							{
								throw new Exception("Invalid character '" + curChar + "' encountered.");
							}
						}
					}
				}
				// is key
				else if (char.IsLetter(char.Parse(curChar)) || curChar.Equals("_"))
				{
					var lexeme = curChar;
					NextChar();

					while (!EndOfInput() && !curChar.Equals(Symbols.COLON) && (curChar.Equals("_") || char.IsLetterOrDigit(char.Parse(curChar))))
					{
						lexeme += curChar;
						NextChar();
					}
					curIndex--;
					Tokens.Add(new Token(TokenCategory.KEY, lexeme));
				}
				else
				{
					throw new Exception("Invalid character '" + curChar + "' encountered.");
				}
			}

			Tokens.Add(new Token(TokenCategory.EOF, ""));
		}

		private bool EndOfInput()
		{
			return curIndex >= input.Length;
		}

		private void NextChar()
		{
			if (curIndex < input.Length)
			{
				curChar = input[curIndex++].ToString();
			}
		}

		private void PreviousChar()
		{
			if (curIndex - 2 > 0)
			{
				curChar = input[--curIndex - 1].ToString();
			}
		}
	}
}
