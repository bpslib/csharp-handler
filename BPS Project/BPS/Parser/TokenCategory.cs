/*
 * MIT License
 *
 * Copyright (c) 2021 Carlos Eduardo de Borba Machado
 *
 */

namespace BPSLib.Parser
{
	/// <summary>
	/// Enum <e>TokenCategory</e> contains all token categories.
	/// </summary>
	internal enum TokenCategory : ushort
	{
		EOF         = 0,
		KEY         = 1,
		STRING      = 2,
		INTEGER     = 3,
		FLOAT       = 4,
		TRUE        = 5,
		FALSE       = 6,
		OPEN_ARRAY  = 7,
		CLOSE_ARRAY = 8
	}
}
