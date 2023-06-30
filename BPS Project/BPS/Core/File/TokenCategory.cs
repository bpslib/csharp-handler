/**
 * 
 * MIT License
 *
 * Copyright (c) 2021 Carlos Eduardo de Borba Machado
 *
 */

namespace BPSLib.Core.File
{
	internal enum TokenCategory : ushort
	{
		EOF         = 0,
		KEY         = 1,
		NULL        = 2,
		STRING      = 3,
		CHAR        = 4,
		INTEGER     = 5,
		FLOAT       = 6,
		DOUBLE      = 7,
		BOOL        = 8,
		OPEN_ARRAY  = 9,
		CLOSE_ARRAY = 10,
		END_OF_DATA = 11,
		DATA_SEP    = 12,
		ARRAY_SEP   = 13
	}
}
