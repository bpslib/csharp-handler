/*
 * MIT License
 *
 * Copyright (c) 2021 Carlos Eduardo de Borba Machado
 *
 */

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("BPS UnitTest")]
namespace BPSLib.Parser.File
{
	/// <summary>
	/// Class <c>FileLexer</c> manage lexical analisys from file parser.
	/// </summary>
	internal class FileLexer
	{
		//TODO: add semicolon token

		/// <summary>
		/// Generated list of tokens.
		/// </summary>
		internal List<Token> Tokens { get; }

		// control vars
		private readonly string _input = "";
		private char _curChar;
		private int _curIndex = 0;
		private int _curLine = 0;
		private int _curCollumn = -1;


		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		internal FileLexer()
		{
			Tokens = new List<Token>();
		}

		/// <summary>
		/// Constructor setting the input.
		/// </summary>
		/// <param name="input">the input to be parsed.</param>
		internal FileLexer(string input)
		{
			Tokens = new List<Token>();
			_input = input;
		}

		#endregion Constructors


		#region Methods

		#region Public

		/// <summary>
		/// Do the lexical analisys of a BPS string document.
		/// </summary>
		internal void Parse()
		{
			// maybe need a refactoring
			while (!EndOfInput())
			{
				NextChar();

				// to skip the skip chars
				if (Symbols.IsSkip(_curChar))
				{
					if (_curChar.Equals(Symbols.NEWLINE))
					{
						NextLine();
					}
					continue;
				}

				// to skip comments
				if (_curChar.Equals(Symbols.HASH))
				{
					while (!EndOfInput() && !_curChar.Equals(Symbols.NEWLINE))
					{
						NextChar();
					}
					NextLine();
				}
				// if is key
				else if (char.IsLetter(_curChar) || _curChar.Equals('_'))
				{
					var initCol = _curCollumn;
					var lexeme = _curChar.ToString();
					NextChar();

					// loops the key
					while (!EndOfInput() && !_curChar.Equals(Symbols.COLON) && (_curChar.Equals('_') || char.IsLetterOrDigit(_curChar)))
					{
						lexeme += _curChar;
						NextChar();
					}
					Tokens.Add(new Token(TokenCategory.KEY, lexeme, _curLine, initCol));

					// check if current char is colon before read data
					if (!_curChar.Equals(Symbols.COLON))
					{
						throw new Exception("Invalid character: '" + _curChar + "' encountered at line " + _curLine + " and collumn " + _curCollumn + ".");
					}

					// value
					while (!EndOfInput() && !_curChar.Equals(Symbols.SEMICOLON))
					{
						NextChar();
						// skip comma and verify if there are a duplicated comma (a empty gap)
						if (_curChar.Equals(Symbols.COMMA))
						{
							NextChar();
							if (_curChar.Equals(Symbols.COMMA))
							{
								throw new Exception("Double comma encountered at line " + _curLine + " and collumn " + _curCollumn + ".");
							}
						}
						// to skip the skip chars
						while (!EndOfInput() && Symbols.IsSkip(_curChar))
						{
							NextChar();
						}
						// semicolon indicates the end of value
						if (_curChar.Equals(Symbols.SEMICOLON))
						{
							break;
						}
						lexeme = _curChar.ToString();

						// open array
						if (_curChar.Equals(Symbols.LEFT_BRACKETS))
						{
							Tokens.Add(new Token(TokenCategory.OPEN_ARRAY, lexeme, _curLine, _curCollumn));
						}
						// close array
						else if (_curChar.Equals(Symbols.RIGHT_BRACKETS))
						{
							Tokens.Add(new Token(TokenCategory.CLOSE_ARRAY, lexeme, _curLine, _curCollumn));
						}
						// is a value
						else
						{
							// string
							if (_curChar.Equals(Symbols.DQUOTE))
							{
								initCol = _curCollumn;
								var beforeChar = _curChar;
								NextChar();
								while (!EndOfInput() && (!_curChar.Equals(Symbols.DQUOTE) || beforeChar.Equals('\\')))
								{
									beforeChar = _curChar;
									lexeme += _curChar;
									NextChar();
								}
								lexeme += _curChar;
								Tokens.Add(new Token(TokenCategory.STRING, lexeme, _curLine, initCol));
							}
							// char
							else if (_curChar.Equals(Symbols.QUOTE))
							{
								initCol = _curCollumn;
								NextChar();
								if (_curChar.Equals('\\'))
								{
									lexeme += _curChar;
									NextChar();
								}
								lexeme += _curChar;
								NextChar();
								if (!_curChar.Equals(Symbols.QUOTE))
								{
									throw new Exception("Char was not closed at line " + _curLine + " and collumn " + _curCollumn + ".");
								}
								lexeme += _curChar;
								Tokens.Add(new Token(TokenCategory.CHAR, lexeme, _curLine, initCol));
							}
							// numeric
							else if (char.IsDigit(_curChar) || _curChar.Equals(Symbols.DOT) || _curChar.Equals(Symbols.MINUS))
							{
								initCol = _curCollumn;
								var dotted = _curChar.Equals(Symbols.DOT);
								NextChar();
								while (!EndOfInput() && (char.IsDigit(_curChar) || _curChar.Equals(Symbols.DOT)))
								{
									if (_curChar.Equals(Symbols.DOT))
									{
										if (dotted)
										{
											throw new Exception("Double dot encountered at line " + _curLine + " and collumn " + _curCollumn + ".");
										}
										else
										{
											dotted = true;
										}
									}
									lexeme += _curChar;
									NextChar();
								}
								// float or int
								if (lexeme.Contains(Symbols.DOT.ToString()))
								{
									Tokens.Add(new Token(TokenCategory.FLOAT, lexeme, _curLine, initCol));
								}
								else
								{
									Tokens.Add(new Token(TokenCategory.INTEGER, lexeme, _curLine, initCol));
								}
								//PreviousChar();
							}
							// boolean or null
							else if (_curChar.Equals('t') || _curChar.Equals('f') || _curChar.Equals('n'))
							{
								initCol = _curCollumn;
								NextChar();
								while (!EndOfInput() && char.IsLetter(_curChar))
								{
									lexeme += _curChar;
									NextChar();
								}
								// true, false or null
								if (lexeme.Equals("true") || lexeme.Equals("false") || lexeme.Equals("null"))
								{
									Tokens.Add(new Token(TokenCategory.BOOL, lexeme, _curLine, initCol));
								}
								else
								{
									throw new Exception("Invalid value: '" + lexeme + "' encountered at line " + _curLine + " and collumn " + _curCollumn + ". Expected: 'true', 'false' or 'null'.");
								}
								//PreviousChar();
							}
							else
							{
								throw new Exception("Invalid character: '" + _curChar + "' encountered at line " + _curLine + " and collumn " + _curCollumn + ".");
							}
						}
					}
				}
				else
				{
					throw new Exception("Invalid character: '" + _curChar + "' encountered at line " + _curLine + " and collumn " + _curCollumn + ".");
				}
			}

			Tokens.Add(new Token(TokenCategory.EOF, "", -1, -1));
		}

		#endregion Public


		#region Private

		/// <summary>
		/// Verify if the input is in the end.
		/// </summary>
		/// <returns>True if is in the end.</returns>
		private bool EndOfInput()
		{
			return _curIndex >= _input.Length;
		}

		/// <summary>
		/// Get the next char to <v>_curChar</v>.
		/// </summary>
		private void NextLine()
		{
			++_curLine;
			_curCollumn = -1;
		}

		/// <summary>
		/// Get the next char to <v>_curChar</v>.
		/// </summary>
		private void NextChar()
		{
			if (_curIndex < _input.Length)
			{
				_curChar = _input[_curIndex++];
				++_curCollumn;
			}
		}

		/// <summary>
		/// Get the previous char to <v>_curChar</v>.
		/// </summary>
		private void PreviousChar()
		{
			if (_curIndex - 2 > 0)
			{
				_curChar = _input[--_curIndex - 1];
				--_curCollumn;
			}
		}

		#endregion Private

		#endregion Methods
	}
}
