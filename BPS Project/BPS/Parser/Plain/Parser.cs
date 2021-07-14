/*
 * MIT License
 *
 * Copyright (c) 2021 Carlos Eduardo de Borba Machado
 *
 */

using System;
using System.Collections.Generic;
using System.Globalization;

namespace BPSLib.Parser.Plain
{
	internal class Parser
	{
		#region Vars

		public BPSFile BPSFile { get; }
		public string Plain { get; set; }

		#endregion Vars


		#region Constructors

		internal Parser(BPSFile file)
		{
			BPSFile = file;
			Plain = "";
			CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
		}

		#endregion Constructors


		#region Methods

		#region Public

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
