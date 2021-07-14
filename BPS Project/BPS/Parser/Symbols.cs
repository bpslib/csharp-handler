/*
 * MIT License
 *
 * Copyright (c) 2021 Carlos Eduardo de Borba Machado
 *
 */

using System.Linq;

namespace BPSLib.Parser
{
	/// <summary>
	/// Class <c>Symbols</c> contains common language symbols.
	/// </summary>
	internal class Symbols
	{
		internal const string HASH           = "#";
		internal const string LEFT_BRACKETS  = "[";
		internal const string RIGHT_BRACKETS = "]";
		internal const string DOT            = ".";
		internal const string COMMA          = ",";
		internal const string COLON          = ":";
		internal const string SEMICOLON      = ";";
		internal const string MINUS          = "-";
		internal const string QUOTE          = "'";
		internal const string DQUOTE         = "\"";
		internal const string NEWLINE        = "\n";
		internal const string SPACE          = " ";
		internal const string TAB            = "\t";

		internal static readonly string[] Skip =
		{
			SPACE,
			TAB,
			NEWLINE
		};

		internal static bool IsSkip(string c)
		{
			return Skip.Contains(c);
		}
	}
}
