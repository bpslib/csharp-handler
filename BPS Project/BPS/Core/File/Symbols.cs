/**
 * 
 * MIT License
 *
 * Copyright (c) 2021 Carlos Eduardo de Borba Machado
 *
 */

using System.Linq;

namespace BPSLib.Core.File
{
	internal class Symbols
	{
		internal const char HASH           = '#';
		internal const char LEFT_BRACKETS  = '[';
		internal const char RIGHT_BRACKETS = ']';
		internal const char DOT            = '.';
		internal const char COMMA          = ',';
		internal const char COLON          = ':';
		internal const char SEMICOLON      = ';';
		internal const char MINUS          = '-';
		internal const char DQUOTE         = '"';
		internal const char SPACE          = ' ';
		internal const char QUOTE          = '\'';
		internal const char NEWLINE        = '\n';
        internal const char RETURN         = '\r';
        internal const char TAB            = '\t';

		internal static readonly char[] Skip =
		{
			SPACE,
			TAB,
			NEWLINE,
			RETURN
		};

		internal static bool IsSkip(char c)
		{
			return Skip.Contains(c);
		}
	}
}
