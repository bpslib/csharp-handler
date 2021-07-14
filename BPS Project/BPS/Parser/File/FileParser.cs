/*
 * MIT License
 *
 * Copyright (c) 2021 Carlos Eduardo de Borba Machado
 *
 */

using System;
using System.Collections.Generic;
using System.Globalization;

namespace BPSLib.Parser.File
{
	/// <summary>
	/// Class <c>FileParser</c> manages the file parser.
	/// </summary>
	internal class FileParser
	{
		/// <summary>
		/// Generated BPSFile.
		/// </summary>
		internal BPSFile BPSFile { get; }

		/// <summary>
		/// To parse input.
		/// </summary>
		internal string Input { get; private set; }

		// control vars
		private List<Token> _tokens;
		private Token _curToken;
		private int _curIndex = -1;

		private string _key;
		private readonly Stack<List<object>> _arrStack = new Stack<List<object>>();

		private const int CONTEXT_KEY = 0;
		private const int CONSTEXT_ARRAY = 1;
		private int _context = CONTEXT_KEY;

		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		internal FileParser()
		{
			CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

			BPSFile = new BPSFile();
			Input = "";
		}

		/// <summary>
		/// Constructor setting the input.
		/// </summary>
		/// <param name="input">the input to be parsed.</param>
		internal FileParser(string input)
		{
			CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

			BPSFile = new BPSFile();
			Input = input;
		}

		#endregion Constructors


		#region Methods

		#region Public

		/// <summary>
		/// Do the file parser.
		/// </summary>
		internal void Parse()
		{
			var lexer = new FileLexer(Input);
			lexer.Parse();
			_tokens = lexer.Tokens;
			Start();
		}

		#endregion Public

		#region Private

		private void Start()
		{
			Key();
			ConsumeToken(TokenCategory.EOF);
		}

		private void Key()
		{
			NextToken();
			switch (_curToken.Category)
			{
				case TokenCategory.KEY:
					_key = _curToken.Image;
					Value();
					Key();
					break;
				default:
					break;
			}
		}

		private void Value()
		{
			NextToken();
			switch (_curToken.Category)
			{
				case TokenCategory.OPEN_ARRAY:
					OpenArray();
					break;
				case TokenCategory.CLOSE_ARRAY:
					CloseArray();
					break;
				case TokenCategory.STRING:
					String();
					break;
				case TokenCategory.INTEGER:
					Integer();
					break;
				case TokenCategory.FLOAT:
					Float();
					break;
				case TokenCategory.TRUE:
					True();
					break;
				case TokenCategory.FALSE:
					False();
					break;
				default:
					throw new Exception("Invalid token '" + _curToken.Image + "' encountered.");
			}
		}

		private void String()
		{
			var value = _curToken.Image.Substring(1, _curToken.Image.Length - 2);
			if (_context == CONSTEXT_ARRAY)
			{
				_arrStack.Peek().Add(value);
				Value();
			}
			else
			{
				BPSFile.Add(_key, value);
			}
		}

		private void Integer()
		{
			var value = int.Parse(_curToken.Image);
			if (_context == CONSTEXT_ARRAY)
			{
				_arrStack.Peek().Add(value);
				Value();
			}
			else
			{
				BPSFile.Add(_key, value);
			}
		}

		private void Float()
		{
			var value = float.Parse(_curToken.Image);
			if (_context == CONSTEXT_ARRAY)
			{
				_arrStack.Peek().Add(value);
				Value();
			}
			else
			{
				BPSFile.Add(_key, value);
			}
		}

		private void True()
		{
			if (_context == CONSTEXT_ARRAY)
			{
				_arrStack.Peek().Add(true);
				Value();
			}
			else
			{
				BPSFile.Add(_key, true);
			}
		}

		private void False()
		{
			if (_context == CONSTEXT_ARRAY)
			{
				_arrStack.Peek().Add(false);
				Value();
			}
			else
			{
				BPSFile.Add(_key, false);
			}
		}

		private void OpenArray()
		{
			if (_arrStack.Count == 0)
			{
				_context = CONSTEXT_ARRAY;
				_arrStack.Push(new List<object>());
			}
			else
			{
				var newD = new List<object>();
				_arrStack.Peek().Add(newD);
				_arrStack.Push(newD);
			}
			Value();
		}

		private void CloseArray()
		{
			if (_arrStack.Count > 1)
			{
				_arrStack.Pop();
				Value();
			}
			else
			{
				_context = CONTEXT_KEY;
				BPSFile.Add(_key, _arrStack.Pop());
			}
		}

		// parser constrols

		private void NextToken()
		{
			if (++_curIndex < _tokens.Count)
			{
				_curToken = _tokens[_curIndex];
			}
		}

		private void ConsumeToken(TokenCategory category)
		{
			if (!_curToken.Category.Equals(category))
			{
				throw new Exception("Invalid token '" + _curToken.Image + "' encountered.");
			}
			NextToken();
		}

		#endregion Private

		#endregion Methods

	}
}
