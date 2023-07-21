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
using System.Text;

[assembly: InternalsVisibleTo("BPS UnitTest")]
namespace BPSLib.Core
{
    internal static class Plain
    {
		private static StringBuilder _plainStringBuilder;

        private static void Init()
        {
            _plainStringBuilder = new StringBuilder();
        }

		internal static string Parse(Dictionary<string, object> data)
		{
            Init();

            // loops bps file adding each key-value to output
            foreach (var d in data)
            {
                _plainStringBuilder.Append(d.Key);
                _plainStringBuilder.Append(":");
                ParseValue(d.Value);
                _plainStringBuilder.AppendLine(";");
            }

            return _plainStringBuilder.ToString();
		}

		private static void ParseValue(object value)
        {
            // null values
            if (value == null)
			{
                _plainStringBuilder.Append("null");
			}
			// value is an array
			else if (value.GetType().IsArray)
            {
                _plainStringBuilder.Append("[");
                ParseArray((Array)value);
                _plainStringBuilder.Append("]");
            }
            // it's a normal value
            else
			{
				if (value.GetType().Equals(typeof(string)))
				{
					_plainStringBuilder.Append("\"");
                    _plainStringBuilder.Append(((string)value).Replace("\"", "\\\""));
                    _plainStringBuilder.Append("\"");
				}
				else if (value.GetType().Equals(typeof(char)))
				{
					var c = (char)value;
                    _plainStringBuilder.Append("'");
					_plainStringBuilder.Append(c.Equals('\'') ? "\\" : "");
					_plainStringBuilder.Append(c);
                    _plainStringBuilder.Append("'");
                }
				else if (value.GetType().Equals(typeof(bool)))
				{
                    _plainStringBuilder.Append(value.ToString().ToLower());
                }
                else if (value.GetType().Equals(typeof(float)) || value.GetType().Equals(typeof(double)) || value.GetType().Equals(typeof(decimal)))
                {
                    _plainStringBuilder.Append(value.ToString().ToLower());
                }
                else
				{
					_plainStringBuilder.Append(value);
				}
			}
        }

		private static void ParseArray(Array arr)
		{
			// loops each value in array
			foreach (var v in arr)
			{
                // parses a value recursively
                ParseValue(v);
                _plainStringBuilder.Append(",");
			}
			// prevents to remove "[" when no data
			if (arr.Length > 0)
			{
                _plainStringBuilder.Remove(_plainStringBuilder.Length - 1, 1);
			}
		}

	}
}
