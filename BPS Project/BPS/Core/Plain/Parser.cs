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
namespace BPSLib.Core.Plain
{
    internal static class Parser
    {
		private static StringBuilder plainStringBuilder;

        private static void Init()
        {
            plainStringBuilder = new StringBuilder();
        }

		internal static string Parse(Dictionary<string, object> data)
		{
            Init();

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
			else if (value.GetType().IsArray)
            {
                plainStringBuilder.Append("[");
                ParseArray((Array)value);
                plainStringBuilder.Append("]");
            }
            // it's a normal value
            else
			{
				if (value.GetType().Equals(typeof(string)))
				{
					plainStringBuilder.Append("\"");
                    plainStringBuilder.Append(((string)value).Replace("\"", "\\\""));
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
                else if (value.GetType().Equals(typeof(float)))
                {
                    plainStringBuilder.Append(value.ToString().ToLower());
                    plainStringBuilder.Append('f');
                }
                else if (value.GetType().Equals(typeof(double)))
                {
                    plainStringBuilder.Append(value.ToString().ToLower());
                    plainStringBuilder.Append('d');
                }
                else
				{
					plainStringBuilder.Append(value);
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
                plainStringBuilder.Append(",");
			}
			// prevents to remove "[" when no data
			if (arr.Length > 0)
			{
                plainStringBuilder.Remove(plainStringBuilder.Length - 1, 1);
			}
		}

	}
}
