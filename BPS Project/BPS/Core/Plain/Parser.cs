/**
 * 
 * MIT License
 *
 * Copyright (c) 2021 Carlos Eduardo de Borba Machado
 *
 */

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("BPS UnitTest")]
namespace BPSLib.Core.Plain
{
    internal static class Parser
    {
		private static readonly StringBuilder plainStringBuilder = new StringBuilder();

		internal static string Parse(Dictionary<string, object> data)
		{
            // loops bps file adding each key-value to output
            foreach (var d in data)
            {
                plainStringBuilder.Append(d.Key);
                plainStringBuilder.Append(":");
                ParseValue(d.Value);
                plainStringBuilder.AppendLine(";");
            }

            return plainStringBuilder.ToString();
		}

		private static void ParseValue(object value)
        {
            // null values
            if (value == null)
			{
                plainStringBuilder.Append("null");
			}
			// value is an array
			else if (value.GetType().Equals(typeof(List<object>)))
			{
                plainStringBuilder.Append("[");
                ParseArray((List<object>)value);
                plainStringBuilder.Append("]");
			}
			// it's a normal value
			else
			{
				if (value.GetType().Equals(typeof(string)))
				{
					plainStringBuilder.Append("\"");
					// todo: parse double quotes
					plainStringBuilder.Append((string)value);
                    plainStringBuilder.Append("\"");
				}
				else if (value.GetType().Equals(typeof(char)))
				{
					var c = (char)value;
                    plainStringBuilder.Append("'");
					plainStringBuilder.Append(c.Equals('\'') ? "\\" : "");
					plainStringBuilder.Append(c);
                    plainStringBuilder.Append("'");
                }
				else if (value.GetType().Equals(typeof(bool)))
				{
                    plainStringBuilder.Append(value.ToString().ToLower());
				}
				else
				{
					plainStringBuilder.Append(value);
				}
			}
        }

		private static void ParseArray(List<object> arr)
		{
			// loops each value in array
			foreach (var v in arr)
			{
                // parses a value recursively
                ParseValue(v);
                plainStringBuilder.Append(",");
			}
			// prevents to remove "[" when no data
			if (arr.Count > 0)
			{
                plainStringBuilder.Remove(plainStringBuilder.Length - 1, 1);
			}
		}

	}
}
