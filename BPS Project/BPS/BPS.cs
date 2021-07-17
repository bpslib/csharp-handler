/*
 * MIT License
 *
 * Copyright (c) 2021 Carlos Eduardo de Borba Machado
 *
 */

using BPSLib.Util;
using System;
using System.IO;

namespace BPSLib
{
	/// <summary>
	/// Class <c>BPS</c> controls general BPSFile functions.
	/// </summary>
	public class BPS
	{
		#region Methods

		#region Public

		/// <summary>
		/// Reads a BPSFile from passed path.
		/// </summary>
		/// <param name="path">path with or no extension.</param>
		/// <returns>BPSFile containing readed data.</returns>
		public static BPSFile Load(string path)
		{
			string data;
			try
			{
				var sr = new StreamReader(BPSPath.NormalizePath(path));
				data = sr.ReadToEnd();
				sr.Close();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			var file = Parse(data);
			file.Path = path;
			return file;
		}

		/// <summary>
		/// Write a BPS file on path.
		/// </summary>
		/// <param name="file">the file to be write.</param>
		public static void Save(BPSFile file)
		{
			try
			{
				var normalizedPath = BPSPath.NormalizePath(file.Path);
				Directory.CreateDirectory(normalizedPath.Remove(normalizedPath.LastIndexOf(Path.DirectorySeparatorChar)));
				var sw = new StreamWriter(normalizedPath);
				sw.WriteLine(Plain(file));
				sw.Close();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Parse a plain string data and return a BPSFile.
		/// </summary>
		/// <param name="data">a data in string format.</param>
		/// <returns>BPSFile representation from data.</returns>
		public static BPSFile Parse(string data)
		{
			var parser = new Parser.File.FileParser(data.Replace("\r\n", "\n").Replace("\r", string.Empty));
			parser.Parse();
			return parser.BPSFile;
		}

		/// <summary>
		/// Convert a BPSFile data to plain text.
		/// </summary>
		/// <param name="file">BPSFile to convert.</param>
		/// <returns>A String representation from self data.</returns>
		public static string Plain(BPSFile file)
		{
			var parser = new Parser.Plain.PlainParser(file);
			parser.Parse();
			return parser.Plain;
		}

		#endregion Public

		#region Private

		#endregion Private

		#endregion Methods

	}
}
