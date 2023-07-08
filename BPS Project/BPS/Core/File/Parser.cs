/**
 * 
 * MIT License
 *
 * Copyright (c) 2021 Carlos Eduardo de Borba Machado
 *
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("BPS UnitTest")]
namespace BPSLib.Core.File
{
	internal static class Parser
	{
		private static readonly Dictionary<string, object> parsedData = new Dictionary<string, object>();

		// control vars
		private static List<Token> _tokens;
        private static Token _curToken;
        private static int _curIndex = -1;

        private static string _key;
        private static object _value;
        private static readonly Stack<List<object>> _arrStack = new Stack<List<object>>();

        private const int CONTEXT_KEY = 0;
        private const int CONTEXT_ARRAY = 1;
        private static int _context = CONTEXT_KEY;

		internal static Dictionary<string, object> Parse(string data)
		{
            _tokens = Lexer.Tokenize(data);
			Start();
			return parsedData;
        }

		private static void Start()
		{
			NextToken();
			Statement();
			ConsumeToken(TokenCategory.EOF);
		}

		private static void Statement()
		{
			switch (_curToken.Category)
			{
				case TokenCategory.KEY:
					Key();
					break;
				default:
					break;
			}
		}

		private static void Key()
		{
			_key = _curToken.Image;
			NextToken();
			ConsumeToken(TokenCategory.DATA_SEP);
			Value();
			ConsumeToken(TokenCategory.END_OF_DATA);
			Statement();
		}

		private static void Value()
		{
			switch (_curToken.Category)
			{
				case TokenCategory.OPEN_ARRAY:
					OpenArray();
					NextToken();
					Array();
					break;
				case TokenCategory.STRING:
					String();
					break;
				case TokenCategory.CHAR:
					Char();
					break;
				case TokenCategory.INTEGER:
					Integer();
					break;
				case TokenCategory.FLOAT:
					Float();
					break;
				case TokenCategory.DOUBLE:
					Double();
					ArraySel();
					break;
				case TokenCategory.BOOL:
					Bool();
					break;
				case TokenCategory.NULL:
					Null();
					break;
				default:
					throw new Exception("Invalid token '" + _curToken.Image + "' encountered at line " + _curToken.Line + " and collumn " + _curToken.Collumn + ". Expected a value or array.");
			}
		}

		private static void Array()
		{
			switch (_curToken.Category)
			{
				case TokenCategory.OPEN_ARRAY:
					OpenArray();
					NextToken();
					Array();
					break;
				case TokenCategory.STRING:
					String();
					ArraySel();
					break;
				case TokenCategory.CHAR:
					Char();
					ArraySel();
					break;
				case TokenCategory.INTEGER:
					Integer();
					ArraySel();
					break;
				case TokenCategory.FLOAT:
					Float();
					ArraySel();
					break;
				case TokenCategory.DOUBLE:
					Double();
					ArraySel();
					break;
				case TokenCategory.BOOL:
					Bool();
					ArraySel();
					break;
				case TokenCategory.NULL:
					Null();
					ArraySel();
					break;
				default:
					throw new Exception("Invalid token '" + _curToken.Image + "' encountered at line " + _curToken.Line + " and collumn " + _curToken.Collumn + ". Expected a value or array.");
			}
		}

		private static void ArraySel()
		{
			switch (_curToken.Category)
			{
				case TokenCategory.ARRAY_SEP:
					NextToken();
					Array();
					break;
				case TokenCategory.CLOSE_ARRAY:
					CloseArray();
					NextToken();
					ArraySel();
					break;
				case TokenCategory.END_OF_DATA:
				case TokenCategory.EOF:
					break;
				default:
					throw new Exception("Invalid token '" + _curToken.Image + "' encountered at line " + _curToken.Line + " and collumn " + _curToken.Collumn + ". Expected ',', ']' or ';'.");
			}
		}

		private static void String()
		{
			_value = _curToken.Image.Substring(1, _curToken.Image.Length - 2);
			SetValue();
		}

		private static void Char()
		{
			_value = char.Parse(_curToken.Image.Substring(1, _curToken.Image.Length - 2).Replace("\\", string.Empty));
			SetValue();
		}

		private static void Integer()
		{
			_value = int.Parse(_curToken.Image);
			SetValue();
		}

		private static void Float()
		{
			var strValue = _curToken.Image.EndsWith("f") || _curToken.Image.EndsWith("F") ? _curToken.Image.Substring(0, _curToken.Image.Length - 1) : _curToken.Image;
			_value = float.Parse(strValue);
			SetValue();
		}

		private static void Double()
		{
			var strValue = _curToken.Image.EndsWith("d") || _curToken.Image.EndsWith("D") ? _curToken.Image.Substring(0, _curToken.Image.Length - 1) : _curToken.Image;
			_value = double.Parse(strValue);
			SetValue();
		}

		private static void Bool()
		{
			_value = bool.Parse(_curToken.Image);
			SetValue();
		}

		private static  void Null()
		{
			_value = null;
			SetValue();
		}

		private static void SetValue()
		{
			if (_context == CONTEXT_ARRAY)
			{
				_arrStack.Peek().Add(_value);
			}
			else
			{
				parsedData.Add(_key, _value);
			}
			NextToken();
		}

		private static void OpenArray()
		{
			if (_arrStack.Count == 0)
			{
				_context = CONTEXT_ARRAY;
				_arrStack.Push(new List<object>());
			}
			else
			{
				var newD = new List<object>();
				_arrStack.Peek().Add(newD);
				_arrStack.Push(newD);
			}
		}

		private static void CloseArray()
		{
			if (_arrStack.Count > 1)
			{
				_arrStack.Pop();
			}
			else
			{
				_context = CONTEXT_KEY;
				parsedData.Add(_key, _arrStack.Pop());
			}
		}

		// parser controls

		private static void NextToken()
		{
			if (++_curIndex < _tokens.Count)
			{
				_curToken = _tokens[_curIndex];
			}
		}

		private static void ConsumeToken(TokenCategory category)
		{
			if (!_curToken.Category.Equals(category))
			{
				throw new Exception("Invalid token '" + _curToken.Image + "' encountered at line " + _curToken.Line + " and collumn " + _curToken.Collumn + ". Expected " + TokenImages.TOKEN_IMAGE[(int)category] + ".");
			}
			NextToken();
		}

	}
}
