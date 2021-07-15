/*
 * MIT License
 *
 * Copyright (c) 2021 Carlos Eduardo de Borba Machado
 *
 */

using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("BPS UnitTest")]
namespace BPSLib.Parser.Plain
{
	/// <summary>
	/// Class <c>PlainParser</c> manage the parser from BPSFile to string representation.
	/// </summary>
	internal class PlainParser
	{
		#region Vars

		/// <summary>
		/// Generated output plain text.
		/// </summary>
		internal string Plain { get; private set; }

		/// <summary>
		/// To parse file.
		/// </summary>
		internal BPSFile BPSFile { get; set; }

		#endregion Vars


		#region Constructors

		/// <summary>
		/// Default contructor.
		/// </summary>
		internal PlainParser()
		{
			BPSFile = new BPSFile();
			Plain = "";

			CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
		}

		/// <summary>
		/// Constructor init BPSFile.
		/// </summary>
		/// <param name="file">To be parsed BPSFile file.</param>
		internal PlainParser(BPSFile file)
		{
			BPSFile = file;
			Plain = "";

			CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
		}

		#endregion Constructors


		#region Methods

		#region Public

		/// <summary>
		/// Do the parser.
		/// </summary>
		internal void Parse()
		{
			// loops bps file adding each key-value to output
			foreach (var d in BPSFile)
			{
				Plain += d.Key + ":";
				ParseValue(d.Value);
				Plain += ";\n";
			}
		}

		#endregion Public


		#region Private

		/// <summary>
		/// Parse a value.
		/// </summary>
		/// <param name="value">the value to be parsed.</param>
		private void ParseValue(object value)
		{
			// null values
			if (value == null)
			{
				Plain += "null";
			}
			// value is an array
			else if (value.GetType().Equals(typeof(List<object>)))
			{
				Plain += "[";
				ParseArray(value);
				Plain += "]";
			}
			// it's a normal value
			else
			{
				if (value.GetType().Equals(typeof(string)))
				{
					var str = (string)value;
					if (str.Length > 1)
					{
						Plain += "\"" + value + "\"";
					}
					else
					{
						Plain += "'" + value + "'";
					}
				}
				else if (value.GetType().Equals(typeof(bool)))
				{
					Plain += value.ToString().ToLower();
				}
				else
				{
					Plain += value;
				}
			}
		}

		/// <summary>
		/// Parse an array value.
		/// </summary>
		/// <param name="value">the array value to be parsed.</param>
		private void ParseArray(object value)
		{
			var arr = (List<object>)value;
			// loops each value in array
			foreach (var v in arr)
			{
				// parses a value recursively
				ParseValue(v);
				Plain += ",";
			}
			// prevents to remove "[" when no data
			if (arr.Count > 0)
				Plain = Plain.Substring(0, Plain.Length - 1);
		}

		#endregion Private

		#endregion Methods
	}
}
