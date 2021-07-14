/*
 * MIT License
 *
 * Copyright (c) 2021 Carlos Eduardo de Borba Machado
 *
 */

using System.Collections.Generic;
using System.Globalization;

namespace BPSLib.Parser.Plain
{
	/// <summary>
	/// Class <c>PlainParser</c> manage the parser from BPSFile to string representation.
	/// </summary>
	internal class PlainParser
	{
		#region Vars

		/// <summary>
		/// Generated plain text.
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
			if (value.GetType().Equals(typeof(List<object>)))
			{
				Plain += "[";
				ParseArray(value);
				Plain += "]";
			}
			else
			{
				if (value.GetType().Equals(typeof(string)))
				{
					Plain += "'" + value + "'";
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
			foreach (var v in (List<object>)value)
			{
				if (v.GetType().Equals(typeof(List<object>)))
				{
					Plain += "[";
					ParseArray(v);
					Plain += "],";
				}
				else
				{
					ParseValue(v);
					Plain += ",";
				}
			}
			Plain = Plain.Substring(0, Plain.Length - 1);
		}

		#endregion Private

		#endregion Methods
	}
}
