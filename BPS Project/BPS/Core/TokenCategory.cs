/**
* 
* MIT License
*
* Copyright (c) 2021 Carlos Eduardo de Borba Machado
*
*/
namespace BPSLib.Core
{
    internal enum TokenCategory : ushort
    {
        EOF = 0,
        KEY = 1,
        NULL = 2,
        STRING = 3,
        CHAR = 4,
        INTEGER = 5,
        FLOAT = 6,
        BOOL = 7,
        OPEN_ARRAY = 8,
        CLOSE_ARRAY = 9,
        END_OF_DATA = 10,
        DATA_SEP = 11,
        ARRAY_SEP = 12
    }
}
