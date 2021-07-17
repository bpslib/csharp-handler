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
		/// <summary>
		/// Generated list of tokens.
		/// </summary>
		internal List<Token> Tokens { get; }

		// control vars
		private readonly string _input = "";
		private char _curChar;
		private int _curIndex = 0;
		private int _curLine = 1;
		private int _curCollumn = 0;


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
			NextChar();
			while (!EndOfInput())
			{
				// to skip the skip chars
				if (Symbols.IsSkip(_curChar))
				{
					if (_curChar.Equals(Symbols.NEWLINE))
					{
						NextLine();
					}
					NextChar();
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
					NextChar();
				}
				// key, boolean or null
				else if (char.IsLetter(_curChar) || _curChar.Equals('_'))
				{
					var lexeme = _curChar.ToString();
					var initCol = _curCollumn;
					NextChar();

					// loops the key
					while (!EndOfInput() && !_curChar.Equals(Symbols.COLON) && (_curChar.Equals('_') || char.IsLetterOrDigit(_curChar)))
					{
						lexeme += _curChar;
						NextChar();
					}

					// true, false or null
					if (lexeme.Equals("true") || lexeme.Equals("false") || lexeme.Equals("null"))
					{
						Tokens.Add(new Token(TokenCategory.BOOL, lexeme, _curLine, initCol));
					}
					// key
					else
					{
						Tokens.Add(new Token(TokenCategory.KEY, lexeme, _curLine, initCol));
					}
				}
				// open array
				else if (_curChar.Equals(Symbols.LEFT_BRACKETS))
				{
					Tokens.Add(new Token(TokenCategory.OPEN_ARRAY, _curChar.ToString(), _curLine, _curCollumn));
					NextChar();
				}
				// close array
				else if (_curChar.Equals(Symbols.RIGHT_BRACKETS))
				{
					Tokens.Add(new Token(TokenCategory.CLOSE_ARRAY, _curChar.ToString(), _curLine, _curCollumn));
					NextChar();
				}
				// end of data
				else if (_curChar.Equals(Symbols.SEMICOLON))
				{
					Tokens.Add(new Token(TokenCategory.END_OF_DATA, _curChar.ToString(), _curLine, _curCollumn));
					NextChar();
				}
				// array sep
				else if (_curChar.Equals(Symbols.COMMA))
				{
					Tokens.Add(new Token(TokenCategory.ARRAY_SEP, _curChar.ToString(), _curLine, _curCollumn));
					NextChar();
				}
				// data sep
				else if (_curChar.Equals(Symbols.COLON))
				{
					Tokens.Add(new Token(TokenCategory.DATA_SEP, _curChar.ToString(), _curLine, _curCollumn));
					NextChar();
				}
				// string
				else if (_curChar.Equals(Symbols.DQUOTE))
				{
					var lexeme = _curChar.ToString();
					var initCol = _curCollumn;
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
					NextChar();
				}
				// char
				else if (_curChar.Equals(Symbols.QUOTE))
				{
					var lexeme = _curChar.ToString();
					var initCol = _curCollumn;
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
					NextChar();
				}
				// numeric
				else if (char.IsDigit(_curChar) || _curChar.Equals(Symbols.DOT) || _curChar.Equals(Symbols.MINUS))
				{
					var lexeme = _curChar.ToString();
					var initCol = _curCollumn;
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
				}
				else
				{
					throw new Exception("Invalid character '" + _curChar + "' encountered at line " + _curLine + " and collumn " + _curCollumn + ".");
				}
			}

			Tokens.Add(new Token(TokenCategory.EOF, null, -1, -1));
		}

		#endregion Public


		#region Private

		/// <summary>
		/// Verify if the input is in the end.
		/// </summary>
		/// <returns>True if is in the end.</returns>
		private bool EndOfInput()
		{
			return _curIndex > _input.Length;
		}

		/// <summary>
		/// Get the next char to <v>_curChar</v>.
		/// </summary>
		private void NextLine()
		{
			++_curLine;
			_curCollumn = 0;
		}

		/// <summary>
		/// Get the next char to <v>_curChar</v>.
		/// </summary>
		private void NextChar()
		{
			if (_curIndex < _input.Length)
			{
				_curChar = _input[_curIndex];
				++_curCollumn;
			}
			++_curIndex;
		}

		#endregion Private

		#endregion Methods
	}
}
