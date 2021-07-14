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
	internal class Parser
	{
		internal BPSFile BPSFile;

		private readonly string input;
		private List<Token> tokens;
		private Token curToken;
		private int curIndex = -1;

		private string key;
		private Stack<List<object>> arrStack;
		private const int STATE = 0;
		private const int ARRAY = 1;
		private int context = STATE;

		#region Constructors

		internal Parser(string input)
		{
			BPSFile = new BPSFile();
			this.input = input;
			arrStack = new Stack<List<object>>();

			CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
		}

		#endregion Constructors


		#region Methods

		#region Public

		internal void Parse()
		{
			var lexer = new Lexer(input);
			lexer.Parse();
			tokens = lexer.Tokens;
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
			switch (curToken.Category)
			{
				case TokenCategory.KEY:
					key = curToken.Image;
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
			switch (curToken.Category)
			{
				case TokenCategory.OPEN_ARRAY:
					OpenArray();
					break;
				case TokenCategory.CLOSE_ARRAY:
					CloseArray();
					break;
				case TokenCategory.STRING:
					{
						var value = curToken.Image.Substring(1, curToken.Image.Length - 2);
						if (context == ARRAY)
						{
							arrStack.Peek().Add(value);
							Value();
						}
						else
						{
							BPSFile.Add(key, value);
						}
					}
					break;
				case TokenCategory.INTEGER:
					{
						var value = int.Parse(curToken.Image);
						if (context == ARRAY)
						{
							arrStack.Peek().Add(value);
							Value();
						}
						else
						{
							BPSFile.Add(key, value);
						}
					}
					break;
				case TokenCategory.FLOAT:
					{
						var value = float.Parse(curToken.Image);
						if (context == ARRAY)
						{
							arrStack.Peek().Add(value);
							Value();
						}
						else
						{
							BPSFile.Add(key, value);
						}
					}
					break;
				case TokenCategory.TRUE:
					{
						if (context == ARRAY)
						{
							arrStack.Peek().Add(true);
							Value();
						}
						else
						{
							BPSFile.Add(key, true);
						}
					}
					break;
				case TokenCategory.FALSE:
					{
						if (context == ARRAY)
						{
							arrStack.Peek().Add(false);
							Value();
						}
						else
						{
							BPSFile.Add(key, false);
						}
					}
					break;
				default:
					throw new Exception("Invalid token '" + curToken.Image + "' encountered.");
			}
		}

		private void OpenArray()
		{
			if (arrStack.Count == 0)
			{
				context = ARRAY;
				arrStack.Push(new List<object>());
			}
			else
			{
				var newD = new List<object>();
				arrStack.Peek().Add(newD);
				arrStack.Push(newD);
			}
			Value();
		}

		private void CloseArray()
		{
			if (arrStack.Count > 1)
			{
				arrStack.Pop();
				Value();
			}
			else
			{
				context = STATE;
				BPSFile.Add(key, arrStack.Pop());
			}
		}

		private void NextToken()
		{
			if (++curIndex < tokens.Count)
			{
				curToken = tokens[curIndex];
			}
		}

		private void ConsumeToken(TokenCategory category)
		{
			if (!curToken.Category.Equals(category))
			{
				throw new Exception("Invalid token '" + curToken.Image + "' encountered.");
			}
			NextToken();
		}

		#endregion Private

		#endregion Methods

	}
}
