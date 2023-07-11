/**
 * 
 * MIT License
 *
 * Copyright (c) 2021 Carlos Eduardo de Borba Machado
 *
 */

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("BPS UnitTest")]
namespace BPSLib.Core.File
{
    internal static class Parser
    {
        private static Dictionary<string, object> parsedData;

        // control vars
        private static List<Token> _tokens;
        private static Token _curToken;
        private static int _curIndex;

        private static string _key;
        private static object _value;
        private static Stack<List<object>> _arrStack;

        private const int CONTEXT_KEY = 0;
        private const int CONTEXT_ARRAY = 1;
        private static int _context;

        private static void InitParser()
        {
            parsedData = new Dictionary<string, object>();
            _tokens = new List<Token>();
            _curToken = null;
            _curIndex = -1;
            _key = string.Empty;
            _value = null;
            _arrStack = new Stack<List<object>>();
            _context = CONTEXT_KEY;
        }

        internal static Dictionary<string, object> Parse(string data)
        {
            InitParser();
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

            if (_context == CONTEXT_ARRAY)
            {
                ArraySelector();
            }
        }

        private static void ArraySelector()
        {
            switch (_curToken.Category)
            {
                case TokenCategory.ARRAY_SEP:
                    NextToken();
                    Value();
                    break;
                case TokenCategory.CLOSE_ARRAY:
                    CloseArray();
                    NextToken();
                    ArraySelector();
                    break;
                case TokenCategory.END_OF_DATA:
                case TokenCategory.EOF:
                    break;
                default:
                    throw new Exception("Invalid token '" + _curToken.Image + "' encountered at line " + _curToken.Line + " and collumn " + _curToken.Collumn + ". Expected ',', ']' or ';'.");
            }
        }

        private static void Array()
        {
            OpenArray();
            NextToken();
            Value();
        }

        private static void String()
        {
            _value = _curToken.Image.Substring(1, _curToken.Image.Length - 2);
            SetValue();
        }

        private static void Char()
        {
            string stringValue = _curToken.Image.Substring(1, _curToken.Image.Length - 2);
            stringValue = stringValue.StartsWith("\\") ? stringValue.Substring(1) : stringValue;
            _value = char.Parse(stringValue);
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

        private static void Null()
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
                var newDimension = new List<object>();
                _arrStack.Peek().Add(newDimension);
                _arrStack.Push(newDimension);
            }
        }

        private static object[] ParseArrayRecursively(List<object> list)
        {
            var array = new object[list.Count];
            for (var i = 0; i < list.Count; ++i)
            {
                if (list[i] != null && list[i].GetType().Equals(typeof(List<object>)))
                {
                    array[i] = ParseArrayRecursively((List<object>)list[i]);
                }
                else
                {
                    array[i] = list[i];
                }
            }
            return array;
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
                var array = ParseArrayRecursively(_arrStack.Pop());
                parsedData.Add(_key, array);
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
