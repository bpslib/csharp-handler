﻿/**
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
namespace BPSLib.Core
{
    internal static class Lexer
    {
        private static List<Token> _tokens;

        // control vars
        private static string _input;
        private static char _curChar;
        private static int _curIndex;
        private static int _curLine;
        private static int _curCollumn;

        private static void Init()
        {
            _tokens = new List<Token>();
            _curIndex = 0;
            _curLine = 1;
            _curCollumn = 1;
        }

        internal static List<Token> Tokenize(string input)
        {
            Init();
            _input = input;

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

                    // true or false
                    if (lexeme.Equals("true") || lexeme.Equals("false"))
                    {
                        _tokens.Add(new Token(TokenCategory.BOOL, lexeme, _curLine, initCol));
                    }
                    // null
                    else if (lexeme.Equals("null"))
                    {
                        _tokens.Add(new Token(TokenCategory.NULL, lexeme, _curLine, initCol));
                    }
                    // key
                    else
                    {
                        _tokens.Add(new Token(TokenCategory.KEY, lexeme, _curLine, initCol));
                    }
                }
                // open array
                else if (_curChar.Equals(Symbols.LEFT_BRACKETS))
                {
                    _tokens.Add(new Token(TokenCategory.OPEN_ARRAY, _curChar.ToString(), _curLine, _curCollumn));
                    NextChar();
                }
                // close array
                else if (_curChar.Equals(Symbols.RIGHT_BRACKETS))
                {
                    _tokens.Add(new Token(TokenCategory.CLOSE_ARRAY, _curChar.ToString(), _curLine, _curCollumn));
                    NextChar();
                }
                // end of data
                else if (_curChar.Equals(Symbols.SEMICOLON))
                {
                    _tokens.Add(new Token(TokenCategory.END_OF_DATA, _curChar.ToString(), _curLine, _curCollumn));
                    NextChar();
                }
                // array sep
                else if (_curChar.Equals(Symbols.COMMA))
                {
                    _tokens.Add(new Token(TokenCategory.ARRAY_SEP, _curChar.ToString(), _curLine, _curCollumn));
                    NextChar();
                }
                // data sep
                else if (_curChar.Equals(Symbols.COLON))
                {
                    _tokens.Add(new Token(TokenCategory.DATA_SEP, _curChar.ToString(), _curLine, _curCollumn));
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
                        if (!_curChar.Equals('\\') || beforeChar.Equals('\\'))
                        {
                            lexeme += _curChar;
                        }
                        beforeChar = _curChar;
                        NextChar();
                    }
                    if (!_curChar.Equals(Symbols.DQUOTE))
                    {
                        throw new Exception("String was not closed at line " + _curLine + " and collumn " + _curCollumn + ".");
                    }
                    lexeme += _curChar;
                    _tokens.Add(new Token(TokenCategory.STRING, lexeme, _curLine, initCol));
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
                    _tokens.Add(new Token(TokenCategory.CHAR, lexeme, _curLine, initCol));
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
                    if (char.ToLower(_curChar).Equals('f'))
                    {
                        lexeme += _curChar;
                        NextChar();
                    }
                    // float or int
                    if (lexeme.Contains(Symbols.DOT.ToString()) || lexeme.ToLower().Contains("f"))
                    {
                        _tokens.Add(new Token(TokenCategory.FLOAT, lexeme, _curLine, initCol));
                    }
                    else
                    {
                        _tokens.Add(new Token(TokenCategory.INTEGER, lexeme, _curLine, initCol));
                    }
                }
                else
                {
                    throw new Exception("Invalid character '" + _curChar + "' encountered at line " + _curLine + " and collumn " + _curCollumn + ".");
                }
            }

            _tokens.Add(new Token(TokenCategory.EOF, null, -1, -1));

            return _tokens;
        }

        private static bool EndOfInput()
        {
            return _curIndex > _input.Length;
        }

        private static void NextLine()
        {
            ++_curLine;
            _curCollumn = 0;
        }

        private static void NextChar()
        {
            if (_curIndex < _input.Length)
            {
                _curChar = _input[_curIndex];
                ++_curCollumn;
            }
            ++_curIndex;
        }
    }
}
